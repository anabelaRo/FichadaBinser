namespace FichadaBinser.Helpers
{
    using Interfaces;
    using Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            this.connection = new SQLiteConnection(
                config.Platform,
                Path.Combine(config.DirectoryDB, "Fichada.db3"));

            connection.CreateTable<Day>();
        }

        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>() where T : class
        {
            return connection.Table<T>().ToList();
        }

        public T Find<T>(int pk) where T : class
        {
            return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
