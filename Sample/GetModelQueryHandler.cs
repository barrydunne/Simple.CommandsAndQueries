using Simple.CommandsAndQueries;

namespace Sample
{
    public class GetModelQueryHandler : IQueryAsyncHandler<GetModelQuery, MyModel>
    {
        public async Task<MyModel> HandleAsync(GetModelQuery query) => await Task.FromResult(new MyModel());
    }
}
