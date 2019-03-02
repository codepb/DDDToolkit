namespace DDDToolkit.ApplicationLayer.Repositories
{
    public class QueryOptions
    {
        public PagingOptions PagingOptions { get; set; }
        public string[] OnlyIncludePaths { get; set; }
    }
}
