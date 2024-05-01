using System.Linq.Expressions;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class BeersQueries(IMongoClient mongoClient) : IQueries<Beers>
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("BrewUp-Sales");
    
    public async Task<Beers> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<Beers>(nameof(Beers));
        var filter = Builders<Beers>.Filter.Eq("_id", id);
        return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
            ? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
            : null)!;
    }

    public async Task<PagedResult<Beers>> GetByFilterAsync(Expression<Func<Beers, bool>>? query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        if (--page < 0)
            page = 0;

        var collection = _database.GetCollection<Beers>(nameof(Beers));
        var queryable = query != null
            ? collection.AsQueryable().Where(query)
            : collection.AsQueryable();

        var count = await queryable.CountAsync(cancellationToken: cancellationToken);
        var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PagedResult<Beers>(results, page, pageSize, count);
    }
}