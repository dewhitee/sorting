using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dewhitee.Sorting
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Sorting<TEntity>
    {
        protected readonly ICollection<TEntity> _entities;
        private readonly SortingOrder _sortingOrder;

        public Sorting(ICollection<TEntity> entities, SortingOrder sortingOrder)
        {
            _entities = entities;
            _sortingOrder = sortingOrder;
        }

        public ICollection<TEntity> Sort<TField>(Func<TEntity, TField> func)
        {
            if (_sortingOrder is null || _sortingOrder.Current is null)
                return _entities;

            return GetSorted(_sortingOrder, func);
        }

        public static ICollection<TEntity> Sort<TField>(ICollection<TEntity> entities, SortingOrder sortingOrder, Func<TEntity, TField> func)
        {
            if (sortingOrder is null || sortingOrder.Current is null)
                return entities;

            return GetSorted(entities, sortingOrder, func);
        }

        public static ICollection<TEntity> Sort(ICollection<TEntity> entities, SortingOrdersMapped<TEntity> sortingOrders)
        {
            if (sortingOrders is null || sortingOrders.Current is null)
                return entities;

            return GetSorted(entities, sortingOrders.CurrentSortingOrder, sortingOrders.GetCurrentFunc());
        }

        protected ICollection<TEntity> GetSorted<TField>(SortingOrder sortingOrder, Func<TEntity, TField> func)
        {
            return sortingOrder.GetMode() switch
            {
                SortingOrderMode.Ascending => _entities.OrderBy(func).ToList(),
                SortingOrderMode.Descending => _entities.OrderByDescending(func).ToList(),
                SortingOrderMode.Invalid or _ => _entities,
            };
        }

        protected static ICollection<TEntity> GetSorted<TField>(ICollection<TEntity> entities, SortingOrder sortingOrder, Func<TEntity, TField> func)
        {
            return sortingOrder.GetMode() switch
            {
                SortingOrderMode.Ascending => entities.OrderBy(func).ToList(),
                SortingOrderMode.Descending => entities.OrderByDescending(func).ToList(),
                SortingOrderMode.Invalid or _ => entities,
            };
        }
    }

    public class SortingMultiple<TEntity, TField> : Sorting<TEntity>
    {
        private readonly IDictionary<SortingOrder, Func<TEntity, TField>> _mappedSortingOrders;

        public SortingMultiple(ICollection<TEntity> entities, SortingOrder sortingOrder, Func<TEntity, TField> func)
            : base(entities, null)
        {
            _mappedSortingOrders = new Dictionary<SortingOrder, Func<TEntity, TField>>() { { sortingOrder, func } };
        }

        public SortingMultiple(ICollection<TEntity> entities, IDictionary<SortingOrder, Func<TEntity, TField>> mappedSortingOrders)
            : base(entities, null)
        {
            _mappedSortingOrders = mappedSortingOrders;
        }

        public ICollection<TEntity> Sort()
        {
            if (_mappedSortingOrders is null or { Count: 0 })
                return _entities;

            foreach (var mso in _mappedSortingOrders)
            {
                if (mso.Key.Current is null)
                    continue;

                return GetSorted(mso.Key, mso.Value);
            }

            return _entities;
        }
    }
}
