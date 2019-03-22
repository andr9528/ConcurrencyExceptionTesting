using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Internal;
using Domain.Concrete;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Core;

namespace Repository.EntityFramework
{
    /// <summary>  
    ///  This class should be accessed via the Generic or Serializable classes that inherit from it.  
    /// </summary>   
    public abstract class BaseEntityRepositoryHandler : IBaseRepository
    {
        internal EntityRepository repo = null;
        public BaseEntityRepositoryHandler(EntityRepository repo)
        {
            this.repo = repo;
        }

        public void ResetRepo()
        {
            throw new NotImplementedException();

            //repo.Dispose();
            //repo = null;

            //repo = new EntityRepository();
        }

        public void Save()
        {
            repo.SaveChanges();
        }

        #region Help Methods
        internal EntityState CheckEntryState(EntityState state, EntityEntry entry)
        {
            if (entry != null)
                state = entry.State;
            return state;
        }

        internal bool VerifyEntryState(EntityState actualState, EntityState desiredState)
        {
            return actualState == desiredState ? true : false;
        }

        internal string GetAmountAdded(ICollection<bool> results)
        {
            return string.Format("Added {0} out of {1}.", results.Where(b => b).Count(), results.Count);
        }
        #endregion

        #region Find Query Builders

        // There should be one query for each case in either 'Find' or 'FindMultiple',
        // meaning if there is a case to find YourDomainClass in both methods,
        // then there should be one query builder, meant to build queries for YourDomainClass
        // as both find methods make use of the same query, only the amount of elements returned are diffent.
        /*
        private IQueryable<YourDomainClass> BuildFindYourDomainClassQuery(IYourDomainClass y, IQueryable<YourDomainClass> query)
        {
            Check whether or not a property has been set, if it has been set, add a where to the query including the property.

            // e.g
            if (y.PropertyA != default(PropertyAType))
                query = query.Where(x => x.PropertyA == y.PropertyA);

            // If it is a string then use the method 'IsNullOrEmpty' and the method 'Contains'

            if (!y.PropertyB.IsNullOrEmpty())
                query = query.Where(x => x.PropertyB.Contains(y.PropertyB));
            return query;
        }
        */

        private IQueryable<DummyImplicit> BuildFindDummyImplicitQuery(IDummyImplicit i, IQueryable<DummyImplicit> query)
        {
            if (i.ArbitraryInt != default(int))
                query = query.Where(x => x.ArbitraryInt == i.ArbitraryInt);
            if (!i.ArbitraryString.IsNullOrEmpty())
                query = query.Where(x => x.ArbitraryString.Contains(i.ArbitraryString));

            return query;
        }

        private IQueryable<DummyExplicit> BuildFindDummyExplicitQuery(IDummyExplicit e, IQueryable<DummyExplicit> query)
        {
            if (e.ArbitraryInt != default(int))
                query = query.Where(x => x.ArbitraryInt == e.ArbitraryInt);
            if (!e.ArbitraryString.IsNullOrEmpty())
                query = query.Where(x => x.ArbitraryString.Contains(e.ArbitraryString));

            return query;
        }

        private IQueryable<DummyTimestamp> BuildFindDummyTimestampQuery(IDummyTimestamp t, IQueryable<DummyTimestamp> query)
        {
            if (t.ArbitraryInt != default(int))
                query = query.Where(x => x.ArbitraryInt == t.ArbitraryInt);
            if (!t.ArbitraryString.IsNullOrEmpty())
                query = query.Where(x => x.ArbitraryString.Contains(t.ArbitraryString));

            return query;
        }

        #endregion

        #region Find Multiple Methods

        private ICollection<T> FindMultipleResults<T>(IQueryable<T> query) where T : class, IEntity
        {
            var result = query.ToList().Distinct();
            if (result.Count() > 0)
                return new List<T>(result);
            else
                throw new Exception(string.Format("Found no result for {0}", typeof(T).Name));
        }
        // Create methods for all the different classes, where you should be able to get multiple specific elements.

        // e.g
        /*
        internal ICollection<YourDomainClass> FindMultipleYourDomainClass(IYourDomainClass y)
        {
            var query = repo.YourDomainClassInPlural.AsQueryable();
            query = BuildFindYourDomainClassQuery(y, query);

            return FindMultipleResults(query);
        }
        */

        internal ICollection<DummyImplicit> FindMultipleDummyImplicits(IDummyImplicit i)
        {
            var query = repo.DummyImplicits.AsQueryable();
            query = BuildFindDummyImplicitQuery(i, query);

            return FindMultipleResults(query);
        }

        internal ICollection<DummyExplicit> FindMultipleDummyExplicits(IDummyExplicit e)
        {
            var query = repo.DummyExplicits.AsQueryable();
            query = BuildFindDummyExplicitQuery(e, query);

            return FindMultipleResults(query);
        }

        internal ICollection<DummyTimestamp> FindMultipleDummyTimestamps(IDummyTimestamp t)
        {
            var query = repo.DummyTimestamps.AsQueryable();
            query = BuildFindDummyTimestampQuery(t, query);

            return FindMultipleResults(query);
        }

        #endregion

        #region Find Single Methods

        private T FindAResult<T>(IQueryable<T> query) where T : class, IEntity
        {
            var result = query.ToList().Distinct();
            if (result.Count() == 1)
                return result.First();
            else if (result.Count() > 1)
                throw new Exception(string.Format("More than 1 result found when searching for a {0}", typeof(T).Name));
            else
                throw new Exception(string.Format("No results found when searching for a {0}", typeof(T).Name));
        }
        // Create methods for all the different classes, where you should be able to get one specific element.

        // e.g
        /*
        internal IYourDomainClass FindYourDomainClass(IYourDomainClass y)
        {
            var query = repo.YourDomainClassAsPlural.AsQueryable();
            query = BuildFindYourDomainClassQuery(y, query);

            return FindAResult(query);
        }
        */

        internal IDummyImplicit FindDummyImplicit(IDummyImplicit i)
        {
            var query = repo.DummyImplicits.AsQueryable();
            query = BuildFindDummyImplicitQuery(i, query);

            return FindAResult(query);
        }

        internal IDummyExplicit FindDummyExplicit(IDummyExplicit e)
        {
            var query = repo.DummyExplicits.AsQueryable();
            query = BuildFindDummyExplicitQuery(e, query);

            return FindAResult(query);
        }

        internal IDummyTimestamp FindDummyTimestamp(IDummyTimestamp t)
        {
            var query = repo.DummyTimestamps.AsQueryable();
            query = BuildFindDummyTimestampQuery(t, query);

            return FindAResult(query);
        }

        #endregion

        #region Add Methods

        // There should be one method for each case in the switch on the Generic version, or each overload in the Serializable version

        // e.g
        /*
        internal bool AddYourDomainClass(IYourDomainClass y)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Add(y);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Added);
        }        
         */

        internal bool AddDummyImplicit(IDummyImplicit i)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Add(i);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Added);
        }

        internal bool AddDummyExplicit(IDummyExplicit e)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Add(e);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Added);
        }

        internal bool AddDummyTimestamp(IDummyTimestamp t)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Add(t);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Added);
        }

        #endregion

        #region Update Methods

        // There should be one method for each case in the switch on the Generic version, or each overload in the Serializable version

        // e.g
        /*
        internal bool UpdateYourDomainClass(IYourDomainClass y)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Update(y);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Modified);
        }        
         */

        internal bool UpdateDummyImplicit(IDummyImplicit i)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Update(i);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Modified);
        }

        internal bool UpdateDummyExplicit(IDummyExplicit e)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Update(e);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Modified);
        }

        internal bool UpdateDummyTimestamp(IDummyTimestamp t)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Update(t);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Modified);
        }
        #endregion

        #region Delete Methods

        // There should be one method for each case in the switch on the Generic version, or each overload in the Serializable version

        // e.g
        /*
        internal bool DeleteYourDomainClass(IYourDomainClass y)
        {
            EntityEntry entry = null;
            EntityState state = EntityState.Unchanged;

            entry = repo.Remove(y);

            state = CheckEntryState(state, entry);
            return VerifyEntryState(state, EntityState.Deleted);
        }        
         */

        #endregion
    }
}
