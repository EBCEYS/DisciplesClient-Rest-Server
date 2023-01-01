using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ClientDataBaseModels.DBModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace DataBase.DataBaseAdapters.UsersDataBaseAdapter.Tests
{
    [TestClass()]
    public class UsersDBAdapterTests
    {
        private IUsersDBAdapter Adapter { get; set; } = new UsersDBAdapter(LogManager.CreateNullLogger());
        [TestMethod()]
        public async void LoginUserAsyncTest()
        {
            User adm = await Adapter.LoginUserAsync("adm", "hvZeKKdU4acbLflANhWmxDbDLEKnWhDQKBOWG4bx5Cg=");
            Assert.IsNotNull(adm);
            User usrDoesNotExists = await Adapter.LoginUserAsync("someval", "someval");
            Assert.IsNull(usrDoesNotExists);
        }
    }
}