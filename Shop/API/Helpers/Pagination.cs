using System.Collections.Generic;

namespace Helpers.Pagination
{
    public class Pagination<T> where T : class
    {
        public Pagination(int page_index, int page_size, int count, IReadOnlyList<T> data)
        {
            PageIndex = page_index;
            PageSize = page_size;
            Count = count;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}