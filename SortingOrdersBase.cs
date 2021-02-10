using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSIAviatests.Models.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SortingOrdersBase
    {
        public string Current { get; protected set; }
        public SortingOrder CurrentSortingOrder { get; protected set; }

        /// <summary>
        /// Returns next sorting order of current order.
        /// </summary>
        /// <returns></returns>
        public abstract string Next();

        /// <summary>
        /// Returns next sorting order of specified <paramref name="order"/>.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public abstract string Next(string order);

        /// <summary>
        /// Initializes the current sorting order.
        /// </summary>
        /// <param name="order"></param>
        protected abstract void InitCurrent(string order);
    }
}
