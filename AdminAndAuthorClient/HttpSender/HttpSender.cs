using AdminAndAuthorClient.UserDataStorage;
using Disciples2ApiModels.ApiModels;
using Disciples2ApiModels.D2ApiModels;
using Disciples2ClientDataBaseModels.DBModels;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AdminAndAuthorClient.Http
{
    public class HttpSender : IHttpSender
    {
        private readonly HttpClient client;
        public void SetToken(string token)
        {
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
        public HttpSender(HttpClient httpClient = null)
        {
            this.client = httpClient ?? new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            if (UserStorage.IsParamsSet())
            {
                SetToken(UserStorage.Token);
            }
        }
        public async Task<string> PostLoginRequestAsync(string userName, string password)
        {
            try
            {
                LoginModel login = new()
                {
                    UserName = userName,
                    Password = password
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/login"
                };

                HttpResponseMessage res = await client.PostAsJsonAsync(builder.Uri, login, Program.SerializerOptions);
                if (res.IsSuccessStatusCode)
                {
                    string token = await res.Content.ReadAsStringAsync();
                    return token;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string PostLoginRequest(string userName, string password)
        {
            try
            {
                LoginModel login = new()
                {
                    UserName = userName,
                    Password = password
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/login"
                };
                HttpRequestMessage msg = new(HttpMethod.Post, builder.Uri)
                {
                    Content = FormatHttpContent(login)
                };
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    using Stream stream = res.Content.ReadAsStream();
                    using StreamReader reader = new(stream);
                    string token = reader.ReadToEnd();
                    return token;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static StringContent FormatHttpContent<T>(T obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj, Program.SerializerOptions), Encoding.UTF8, "application/json");
        }

        public async Task<ModInfo[]> GetModsInfoAsync()
        {
            try
            {
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mods-list"
                };
                return await client.GetFromJsonAsync<ModInfo[]>(builder.Uri, Program.SerializerOptions);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public ModInfo[] GetModsInfo()
        {
            try
            {
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mods-list"
                };
                HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    using Stream stream = res.Content.ReadAsStream();
                    using StreamReader reader = new(stream);
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<ModInfo[]>(json, Program.SerializerOptions);
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string[] GetModFiles(string modName)
        {
            try
            {
                QueryBuilder qBuilder = new()
                {
                    { "modName", modName }
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mod/files",
                    Query = qBuilder.ToString()
                };
                HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    using Stream stream = res.Content.ReadAsStream();
                    using StreamReader reader = new(stream);
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<string[]>(json, Program.SerializerOptions);
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task DeleteModFileAsync(string modName, string fileName)
        {
            try
            {
                QueryBuilder qBuilder = new()
                {
                    { "modName", modName },
                    {"fileName", fileName}
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mod/file",
                    Query = qBuilder.ToString()
                };

                HttpResponseMessage res = await client.DeleteAsync(builder.Uri);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Delete mod file successfuly!");
                    return;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteModFile(string modName, string fileName)
        {
            try
            {
                QueryBuilder qBuilder = new()
                {
                    { "modName", modName },
                    {"fileName", fileName}
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mod/file",
                    Query = qBuilder.ToString()
                };

                HttpRequestMessage msg = new(HttpMethod.Delete, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Mod file will be deleted!");
                    return;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task UploadModFileAync(string modName, string version, string file, DateTimeOffset? updateDateTime = null)
        {
            try
            {
                QueryBuilder qBuilder = new()
                {
                    { "modName", modName },
                    { "version", version }
                };
                if (updateDateTime != null)
                {
                    qBuilder.Add("updateDateTime", updateDateTime.ToString());
                }
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mod/upload",
                    Query = qBuilder.ToString()
                };
                MultipartFormDataContent content = new()
                {
                    { new ByteArrayContent(await File.ReadAllBytesAsync(file)), "mod", Path.GetFileName(file) }
                };
                HttpResponseMessage res = await client.PostAsync(builder.Uri, content);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Upload mod {modName} successfuly!");
                    return;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task DownloadModFileAsync(string modName, string modDir, string extractPath)
        {
            try
            {
                if (File.Exists(modDir))
                {
                    File.Delete(modDir);
                }
                QueryBuilder qBuilder = new()
                {
                    { "modName", modName }
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/d2client/mod/download",
                    Query = qBuilder.ToString()
                };
                HttpResponseMessage res = await client.GetAsync(builder.Uri);
                if (res.IsSuccessStatusCode)
                {
                    string saveFilePath = Path.Combine(modDir, $"temp-{modName}.zip");
                    byte[] resBytes = await res.Content.ReadAsByteArrayAsync();
                    if (File.Exists(saveFilePath))
                    {
                        File.Delete(saveFilePath);
                    }
                    await File.WriteAllBytesAsync(saveFilePath, resBytes);
                    UnZiper.ProcessModFile(modName, saveFilePath, extractPath);
                    MessageBox.Show($"Successfuly download mod {modName}!");
                    return;
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public AuthorizedInfo CheckAuthorized()
        {
            try
            {
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/isauthorized"
                };
                HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    using Stream stream = res.Content.ReadAsStream();
                    using StreamReader reader = new(stream);
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<AuthorizedInfo>(json, Program.SerializerOptions);
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new()
                {
                    Name = "Error",
                    Roles = null
                };
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                QueryBuilder qBuilder = new()
            {
                { "id", id.ToString() }
            };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/by-id",
                    Query = qBuilder.ToString()
                };
                HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                if (res.IsSuccessStatusCode)
                {
                    using Stream stream = res.Content.ReadAsStream();
                    return JsonSerializer.Deserialize<User>(stream, Program.SerializerOptions);
                }
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool DeleteUserById(int id)
        {
            try
            {
                QueryBuilder qBuilder = new()
                {
                    { "id", id.ToString() }
                };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/by-id",
                    Query = qBuilder.ToString()
                };
                HttpRequestMessage msg = new(HttpMethod.Delete, builder.Uri);
                HttpResponseMessage res = client.Send(msg);
                return res.IsSuccessStatusCode;
                throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool CreateUser(string userName, string password, string email, params string[] roles)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException($"\"{nameof(userName)}\" не может быть неопределенным или пустым.", nameof(userName));
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException($"\"{nameof(password)}\" не может быть неопределенным или пустым.", nameof(password));
                }

                if (roles is null)
                {
                    throw new ArgumentNullException(nameof(roles));
                }
                var create = new { userName, password, email = email ?? null, roles };
                UriBuilder builder = new(Program.Url)
                {
                    Path = "/user/create"
                };
                HttpRequestMessage msg = new(HttpMethod.Post, builder.Uri)
                {
                    Content = FormatHttpContent(create)
                };
                HttpResponseMessage res = client.Send(msg);
                return res.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
