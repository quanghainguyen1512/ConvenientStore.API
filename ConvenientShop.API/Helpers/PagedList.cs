using System;
using System.Collections.Generic;
using System.Linq;

namespace ConvenientShop.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        // public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        // {
        //     TotalCount = count;
        //     PageSize = pageSize;
        //     CurrentPage = pageNumber;
        //     TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        //     AddRange(items);
        // }

        public void AddMoreDetail(int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        }
    }
}