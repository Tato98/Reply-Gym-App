using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplyGym
{
    public class UserDatabase
    {
        private readonly SQLiteConnection _dbUser;

        public UserDatabase()
        {
            var dbUserPath = Path.Combine(FileSystem.AppDataDirectory, "app_user_data.db");
            _dbUser = new SQLiteConnection(dbUserPath);
            _dbUser.CreateTable<UserData>();
        }

        public void SaveUserData(UserData userData)
        {
            _dbUser.DeleteAll<UserData>();
            _dbUser.Insert(userData);
        }

        public UserData GetUserData()
        {
            return _dbUser.Table<UserData>().FirstOrDefault();
        }
    }
}
