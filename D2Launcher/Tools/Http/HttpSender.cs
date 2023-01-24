using Disciples2ApiModels.D2ApiModels;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;

namespace D2Launcher.Tools.Http;

public class HttpSender : IHttpSender
{
    private readonly HttpClient client;
    public HttpSender()
    {
        this.client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public ModInfo[] GetModsInfo()
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

    public async Task<string> DownloadModFileAsync(string modName, string modDir)
    {
        QueryBuilder qBuilder = new()
        {
            { "modName", modName }
        };
        UriBuilder builder = new(Program.Url)
        {
            Path = "/d2client/mod/download",
            Query = qBuilder.ToString()
        };
        HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
        HttpResponseMessage res = await client.SendAsync(msg);
        if (res.IsSuccessStatusCode)
        {
            string saveFilePath = Path.Combine(modDir, $"temp-{modName}.zip");
            using Stream stream = await res.Content.ReadAsStreamAsync();
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            using FileStream fs = File.Create(saveFilePath);
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(fs);
            return saveFilePath;
        }
        throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
    }
    public ModInfo[] GetSoftInfo()
    {
        try
        {
            UriBuilder builder = new(Program.Url)
            {
                Path = "/d2client/soft/soft-list"
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null;
        }
    }

    public string GetInfo()
    {
        try
        {
            UriBuilder builder = new(Program.Url)
            {
                Path = "/Info/info"
            };
            HttpRequestMessage msg = new(HttpMethod.Get, builder.Uri);
            HttpResponseMessage res = client.Send(msg);
            if (res.IsSuccessStatusCode)
            {
                using Stream stream = res.Content.ReadAsStream();
                using StreamReader reader = new(stream);
                return reader.ReadToEnd();
            }
            throw new Exception($"Bad status code {res.StatusCode} {res.ReasonPhrase ?? "No reason"}!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null;
        }
    }
}
