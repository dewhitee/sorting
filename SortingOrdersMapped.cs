using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSIAviatests.Models.Shared
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TField"></typeparam>
    public class SortingOrdersMapped<TEntity> : SortingOrdersBase
    {
        private readonly Dictionary<SortingOrder, Func<TEntity, object>> _sortingOrders;

        public SortingOrdersMapped(Dictionary<string, Func<TEntity, object>> mappedFieldNames, string order)
        {
            _sortingOrders = mappedFieldNames.ToDictionary(k => new SortingOrder(k.Key, order), v => v.Value);
            InitCurrent(order);
        }

        public Dictionary<SortingOrder, Func<TEntity, object>> SortingOrders => _sortingOrders;
        public Func<TEntity, object> GetCurrentFunc()
        {
            return (from so in _sortingOrders where so.Key.Current == Current select so.Value).FirstOrDefault();
        }

        public override string Next()
        {
            foreach (var so in _sortingOrders.Keys)
            {
                if (so.Valid(Current))
                    return so.Next();
            }
            return Current;
        }

        public override string Next(string orderOrFieldName)
        {
            foreach (var so in _sortingOrders.Keys)
            {
                if (so.Valid(orderOrFieldName))
                    return so.Next();
            }
            return orderOrFieldName;
        }

        protected override void InitCurrent(string order)
        {
            foreach (var so in _sortingOrders.Keys)
            {
                if (so.Valid(order))
                {
                    Current = so.Current;
                    CurrentSortingOrder = so;
                    break;
                }
            }
        }
    }
}
