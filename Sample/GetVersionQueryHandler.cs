using Simple.CommandsAndQueries;

namespace Sample
{
    public class GetVersionQueryHandler : IQueryHandler<GetVersionQuery, string>
    {
        public string Handle(GetVersionQuery query) => "1.0";
    }
}
