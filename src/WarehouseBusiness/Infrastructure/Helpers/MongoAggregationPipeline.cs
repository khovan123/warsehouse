using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Helpers
{
  public class MongoAggregationPipeline<TCollection>
  {
    private readonly IMongoCollection<TCollection> _mongoCollection;
    private IAggregateFluent<TCollection> _aggregate;

    public MongoAggregationPipeline(IMongoCollection<TCollection> mongoCollection)
    {
      _mongoCollection = mongoCollection;
      _aggregate = _mongoCollection.Aggregate();
    }


    public IAggregateFluent<TCollection> GetCurrentAggregate() => _aggregate;

    public static string TmpCollectionName(string collectionName) => $"tmp_{collectionName}";
    public static string RefField(string collectionName, string field) => $"${TmpCollectionName(collectionName)}.{field}";

    public MongoAggregationPipeline<TCollection> Reset(AggregateOptions? options = null)
    {
      _aggregate = _mongoCollection.Aggregate(options);
      return this;
    }

    public MongoAggregationPipeline<TCollection> Match(Expression<Func<TCollection, bool>> filter)
    {
      _aggregate = _aggregate.Match(filter);
      return this;
    }

    public MongoAggregationPipeline<TCollection> Lookup(string fromCollection, string localField, string foreignField = "_id")
    {
      _aggregate = _aggregate.AppendStage<TCollection>(
          new BsonDocument("$lookup", new BsonDocument
          {
                    { "from", fromCollection },
                    { "localField", localField },
                    { "foreignField", foreignField },
                    { "as", TmpCollectionName(fromCollection) }
          })
      );

      return this;
    }

    public MongoAggregationPipeline<TCollection> Unwind(string fromCollection, bool preserveNullAndEmptyArrays = false)
    {
      _aggregate = _aggregate.AppendStage<TCollection>(
          new BsonDocument("$unwind", new BsonDocument
          {
                    { "path", $"${TmpCollectionName(fromCollection)}" },
                    { "preserveNullAndEmptyArrays", preserveNullAndEmptyArrays }
          })
      );

      return this;
    }

    public MongoAggregationPipeline<TCollection> LookupAndUnwind(string fromCollection, string localField, string foreignField = "_id")
        => Lookup(fromCollection, localField, foreignField)
           .Unwind(fromCollection);

    public MongoAggregationPipeline<TCollection> Project(BsonDocument projectSpec)
    {
      _aggregate = _aggregate.AppendStage<TCollection>(new BsonDocument("$project", projectSpec));
      return this;
    }

    public MongoAggregationPipeline<TCollection> SetFields(BsonDocument projectSpec)
    {
      _aggregate = _aggregate.AppendStage<TCollection>(new BsonDocument("$set", projectSpec));
      return this;
    }

    public MongoAggregationPipeline<TCollection> AddFields(BsonDocument projectSpec)
    {
      _aggregate = _aggregate.AppendStage<TCollection>(new BsonDocument("$addFields", projectSpec));
      return this;
    }

    public IAggregateFluent<TResult> As<TResult>() => _aggregate.As<TResult>();

  }
}
