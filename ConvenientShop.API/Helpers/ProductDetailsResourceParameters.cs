namespace ConvenientShop.API.Helpers
{
    public class ProductDetailsResourceParameters
    {
        const int maxPageSize = 20;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}