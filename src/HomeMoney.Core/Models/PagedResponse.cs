using System;
using System.Collections.Generic;

namespace HomeMoney.Core.Models
{
    public class PagedResponse<T> : ResultModel<IList<T>>
    {
        public PagedResponse() : base()
        {
            this.Orders = new Dictionary<string, bool>();
        }

        /// <summary>
        ///     Total number of elements
        /// </summary>
        public long Total { get; set; }

        public long Pages
        {
            get
            {
                if (PageSize < 1 || Total < 1) return 0;
                decimal pages = ((decimal) Total / (decimal) PageSize);
                int add = 0;
                if (pages != Math.Truncate(pages))
                {
                    add = 1;
                }

                return (int) Math.Round(pages, 0, MidpointRounding.ToEven) + add;
            }
        }

        /// <summary>
        ///     The number of element for each set
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        ///     The current page (start from 0)
        /// </summary>
        public long Current { get; set; }


        public IDictionary<string, bool> Orders { get; set; }


        public bool HasMoreElements => Current < Pages;
    }

    public class FilterRequest
    {
        public FilterRequest()
        {
        }

        public FilterRequest(string filterName, string value, string op = "=")
        {
            FilterName = filterName;
            Value = value;
            Op = op;
        }

        /// <summary>
        /// Filter Identifier
        /// </summary>
        public string FilterName { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        public string Op { get; set; }
    }

    public class OrderRequest
    {
        public string Field { get; set; }
        public bool Ascending { get; set; }

        public OrderRequest()
        {
        }

        public OrderRequest(string field, bool ascending = true)
        {
            Field = field;
            Ascending = @ascending;
        }
    }
}