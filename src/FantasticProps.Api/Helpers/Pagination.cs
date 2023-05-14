using System.Collections.Generic;

namespace FantasticProps.Helpers;

public class Pagination<T> where T : class 
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<T> Data { get; set; }
}