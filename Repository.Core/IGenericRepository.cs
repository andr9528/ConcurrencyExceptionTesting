using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core
{
    public interface IGenericRepository
    {
        /*
         * Classes that implement this class should also inherit from a class implementing IBaseRepository
         *
         * Remember to always call Save after Adding, Updateing and Deleteing.
         * Also Remember to call ResetRepo after Updateing and Deleteing.
         */

        T Find<T>(T predicate) where T : class, IEntity;
        ICollection<T> FindMultiple<T>(T predicate) where T : class, IEntity;
        bool Update<T>(T element) where T : class, IEntity;
        bool Delete<T>(T element) where T : class, IEntity;
        bool Add<T>(T element) where T : class, IEntity;
        string AddMultiple<T>(ICollection<T> elements) where T : class, IEntity;
    }
}
