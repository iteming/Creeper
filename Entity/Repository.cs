using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Entity
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly DbHelper _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public Repository(DbHelper context)
        {
            _context = context;
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual IQueryable<T> Get()
        {
            return Entities.AsQueryable();
        }

        public virtual IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return Entities.Where(filter).AsQueryable();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Insert list
        /// </summary>
        /// <param name="list">Entity</param>
        public virtual void Insert(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    //_context.Entry(entity).State = EntityState.Added;

                    Entities.Add(entity);
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Update list
        /// </summary>
        /// <param name="list">Entity</param>
        public virtual void Update(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    //Entities.Attach(entity);
                    //_context.Entry(entity).State = EntityState.Modified;
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }
        
        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="list">Entity</param>
        public virtual void Delete(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    _context.Entry(entity).State = EntityState.Deleted;
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }


        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }


        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
    }
}
