namespace ApiGateway.Filters
{
    public class AuthFilter : IFilter
    {
        public string FilterType { get { return "pre"; } }
        public int FilterOrder { get { return 1; } }
        public void Execute()
        {

        }
    }
}