namespace ACME.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ACME.Data;

    public class DataService
    {
        #region Fields

        protected readonly object Shared = new object();

        #endregion

        #region Public Methods

        public T DeleteAllAndInsert<T>(T model)
            where T : class
        {
            try
            {
                lock (this.Shared)
                {
                    using (var da = new DataAccess())
                    {
                        var oldRecords = da.GetList<T>();
                        foreach (var oldRecord in oldRecords) da.Delete(oldRecord);

                        da.Insert(model);

                        return model;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return model;
            }
        }

        public void DeleteAll<T>()
            where T : class
        {
            try
            {
                lock (this.Shared)
                {
                    using (var da = new DataAccess())
                    {
                        var oldRecords = da.GetList<T>();
                        foreach (var oldRecord in oldRecords) da.Delete(oldRecord);
                    }
                }
            }
            catch
            {
            }
        }

        public void DeleteAllAndInsertAll<T>(List<T> model)
            where T : class
        {
            try
            {
                lock (this.Shared)
                {
                    using (var da = new DataAccess())
                    {
                        var oldRecords = da.GetList<T>();
                        foreach (var oldRecord in oldRecords) da.Delete(oldRecord);

                        foreach (var item in model) da.Insert(item);
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public T InsertOrUpdate<T>(T model)
            where T : class
        {
            try
            {
                lock (this.Shared)
                {
                    using (var da = new DataAccess())
                    {
                        var oldRecord = da.Find<T>(model.GetHashCode());
                        if (oldRecord != null)
                        {
                            da.Update(model);
                            return model;
                        }

                        da.Insert(model);
                        return model;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return model;
            }
        }

        public T Find<T>(T model)
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    da.Insert(model);
                    return model;
                }
            }
        }

        public List<T> Get<T>()
            where T : class
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    return da.GetList<T>();
                }
            }
        }

        public T First<T>()
            where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>().FirstOrDefault();
            }
        }

        public void Insert<T>(T model)
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    da.Insert(model);
                }
            }
        }

        public void Update<T>(T model)
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    da.Update(model);
                }
            }
        }

        public void Delete<T>(T model)
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    da.Delete(model);
                }
            }
        }

        public void Save<T>(List<T> list)
            where T : class
        {
            lock (this.Shared)
            {
                using (var da = new DataAccess())
                {
                    foreach (var record in list) this.InsertOrUpdate(record);
                }
            }
        }

        #endregion
    }
}