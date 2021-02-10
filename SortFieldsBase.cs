using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSIAviatests.Models.Shared
{
    /// <summary>
    /// Abstract base class for defining the creation of <see cref="SortingOrders"/> for specific fields of a view model.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class SortFieldsBase<TEntity>
    {
        /// <summary>
        /// Sorts the <paramref name="entities"/> collection in <paramref name="sortingOrder"/>.
        /// </summary>
        /// <param name="entities">Collection of <typeparamref name="TEntity"/> entities.</param>
        /// <param name="sortingOrder">Order of sorting (ascending or descending by specific fields).</param>
        /// <returns></returns>
        public ICollection<TEntity> SortUsing(ICollection<TEntity> entities, string sortingOrder) 
            => Sorting<TEntity>.Sort(entities, GetSortingOrders(sortingOrder));

        /// <summary>
        /// Returns the sorting orders of specific fields mapped with the functions.
        /// </summary>
        /// <param name="sortingOrder">Order of sorting (ascending or descending by specific fields).</param>
        /// <returns>Sorting orders of specific fields mapped with the functions.</returns>
        public abstract SortingOrdersMapped<TEntity> GetSortingOrders(string sortingOrder);
    }
}
