using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dewhitee.Sorting
{
    /// <summary>
    /// 
    /// </summary>
    public class SortingOrders : SortingOrdersBase
    {
        private readonly SortingOrder[] _sortingOrders;

        public SortingOrders(string[] fieldNames, string order)
        {
            _sortingOrders = (from fn in fieldNames select new SortingOrder(fn, order)).ToArray();
            InitCurrent(order);
        }

        public override string Next()
        {
            foreach (var so in _sortingOrders)
            {
                if (so.Valid(Current))
                    return so.Next();
            }
            return Current;
        }

        public override string Next(string orderOrFieldName)
        {
            foreach (var so in _sortingOrders)
            {
                if (so.Valid(orderOrFieldName))
                    return so.Next();
            }
            return orderOrFieldName;
        }

        protected override void InitCurrent(string order)
        {
            foreach (var so in _sortingOrders)
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
