namespace Core.Specifications
{
    public class ProductListRequest
    {
        private readonly int MaxPageSize = 50;
        public int PageIndex { get; set; }
        private int _pageSize = 6;
        public int PageSize 
        { 
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
        public string Sort { get; set; } = "PriceAsc";
        private string _search;
        public string Search 
        { 
            get { return _search; }
            set { _search = value.ToLower(); }
        }

    }
}
