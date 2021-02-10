using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TSIAviatests.Models.Shared
{
    public enum SortingOrderMode
    {
        Ascending,
        Descending,
        Invalid
    }

    /// <summary>
    /// Incapsulates the sorting handle.
    /// </summary>
    [NotMapped]
    public class SortingOrder
    {
        private string _currentOrder;
        private readonly string _fieldName;
        private string Ascending => $"asc-{_fieldName}";
        private string Descending => $"desc-{_fieldName}";

        protected SortingOrder(string fieldName)
        {
            _fieldName = fieldName?.ToLower();
        }

        /// <summary>
        /// Checks for validity of <paramref name="currentOrder"/>. If it is valid - sets it as the current order.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="currentOrder"></param>
        public SortingOrder(string fieldName, string currentOrder)
            : this(fieldName)
        {
            if (Valid(currentOrder))
                _currentOrder = currentOrder;
        }

        public SortingOrder(string fieldName, string[] currentOrders)
            : this(fieldName)
        {
            foreach (var order in currentOrders)
            {
                if (Valid(order))
                {
                    _currentOrder = order;
                    break;
                }
            }
        }

        public string Current => _currentOrder;

        /// <summary>
        /// Always finds the next sorting order if <see cref="_fieldName"></see> is not null or empty.
        /// </summary>
        /// <returns></returns>
        public string Next()
        {
            return _currentOrder = _currentOrder?.ToLower() switch
            {
                var value when value == Ascending => Descending,
                var value when value == Descending => Ascending,
                _ => Ascending,
            };
        }

        public SortingOrderMode GetMode()
        {
            if (_currentOrder is null)
                return SortingOrderMode.Invalid;

            if (_currentOrder.StartsWith("asc-"))
            {
                return SortingOrderMode.Ascending;
            }
            else if (_currentOrder.StartsWith("desc-"))
            {
                return SortingOrderMode.Descending;
            }
            else
            {
                return SortingOrderMode.Invalid;
            }
        }

        /// <summary>
        /// Checks if current order is a valid order mode of this sorting order.
        /// </summary>
        /// <returns>true if order is valid.</returns>
        public bool Valid() => Valid(_currentOrder);

        /// <summary>
        /// Checks if <paramref name="currentOrder"/> is a valid order mode of this sorting order.
        /// </summary>
        /// <param name="currentOrder"></param>
        /// <returns>true if order is valid.</returns>
        public bool Valid(string currentOrder)
        {
            string lower = currentOrder?.ToLower();
            return !string.IsNullOrEmpty(lower) && (Ascending == currentOrder || Descending == currentOrder || 
                Ascending == Ascend(lower) || Descending == Descend(lower));
        }

        private static string Ascend(string fieldName) => $"asc-{fieldName}";
        private static string Descend(string fieldName) => $"desc-{fieldName}";
    }
}
