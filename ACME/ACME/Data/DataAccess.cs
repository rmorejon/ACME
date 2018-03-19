namespace ACME.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ACME.Interfaces;
    using ACME.Models;

    using SQLite.Net;

    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        #region Fields

        /// <summary>
        ///     The connection.
        /// </summary>
        private readonly SQLiteConnection connection;

        #endregion

        #region Constructors

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataAccess" /> class.
        /// </summary>
        public DataAccess()
        {
            try
            {
                var config = DependencyService.Get<IConfig>();
                this.connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoryDB, "acme.db3"));
                this.connection.CreateTable<Ticket>();
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        ///     The insert.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        /// <summary>
        ///     The delete.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        /// <summary>
        ///     The first.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T First<T>()
            where T : class
        {
            return this.connection.Table<T>().FirstOrDefault();
        }

        /// <summary>
        ///     The get list.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="List" />.
        /// </returns>
        public List<T> GetList<T>()
            where T : class
        {
            return this.connection.Table<T>().ToList();
        }

        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="pk">
        ///     The pk.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T Find<T>(int pk)
            where T : class
        {
            return this.connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.connection.Dispose();
        }

        #endregion
    }
}