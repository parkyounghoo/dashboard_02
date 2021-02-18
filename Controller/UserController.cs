using KPC_Monitoring.Db;
using System.Data;

namespace KPC_Monitoring.Controller
{
    public class UserController
    {
        public DataSet getUserList()
        {
            UserDb db = new UserDb();
            return db.getUserList();
        }
    }
}