using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeMoney.Core.Models
{
    public class PagedRequest
    {
        public PagedRequest()
        {
        }

        public PagedRequest(int page, int pageSize, IList<FilterRequest> filters, IList<OrderRequest> orders = null)
        {
            Page = page;
            PageSize = pageSize;
            Filters = filters;
            Orders = orders;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IList<OrderRequest> Orders { get; set; }

        public IList<FilterRequest> Filters { get; set; }

        public PagedRequest AddFilter(string filterName, string value, string op = "=")
        {
            //no validation for value: could be null?!
            if (!Filters.Any(x => string.Equals(x.FilterName, filterName, StringComparison.InvariantCultureIgnoreCase)))
                Filters.Add(new FilterRequest(filterName, value, op));
            return this;
        }

        public string GetFilterValue(string filterName)
        {
            var filters = Filters.Where(x => string.Equals(x.FilterName, filterName, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
            if (!filters.Any()) return null;
            return string.Join(",", filters.Select(x => x.Value));
        }

        public PagedRequest ExcludeFilter(string filterName)
        {
            var filter =
                Filters.FirstOrDefault(x => string.Equals(x.FilterName, filterName, StringComparison.InvariantCultureIgnoreCase));
            while (filter != null)
            {
                Filters.Remove(filter);
                filter = Filters.FirstOrDefault(x =>
                    string.Equals(x.FilterName, filterName, StringComparison.InvariantCultureIgnoreCase));
            }

            return this;
        }

        public string OrdersToString()
        {
            if (!Orders.Any()) return "";
            return string.Join(",", Orders.Select(x => x.Field + (x.Ascending ? " ASC" : " DESC")));
        }
    }
}