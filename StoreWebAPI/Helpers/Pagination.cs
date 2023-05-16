namespace StoreWebAPI.Helpers
{
    public class Pagination<TEntity> where TEntity : class
    {
        public Pagination(int pageIndex, int pageSize, int totalCount, IList<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IList<TEntity> Data { get; set; }



    }
}
