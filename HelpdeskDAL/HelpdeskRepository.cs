using HelpdeskDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpdeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T : HelpdeskEntity
    {
        private helpdeskContext _db = null;
        public HelpdeskRepository(helpdeskContext context = null)
        {
            _db = context != null ? context : new helpdeskContext();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }

        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;

            try
            {
                HelpdeskEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                _db.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                _db.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

                if (_db.SaveChanges() == 1)
                {
                    operationStatus = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                operationStatus = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return operationStatus;
        }

        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            _db.Set<T>().Remove(currentEntity);
            return _db.SaveChanges();
        }
    }
}
