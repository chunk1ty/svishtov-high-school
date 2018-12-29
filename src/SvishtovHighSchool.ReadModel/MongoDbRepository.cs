using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SvishtovHighSchool.ReadModel
{
    public interface IReadEntity
    {
        string Id { get; }
    }

    public interface IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(string id);
    }

    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }

    public class MongoDbRepository<T> : IRepository<T>
        where T : IReadEntity
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            this._mongoDatabase = mongoDatabase;
        }

        private string CollectionName => typeof(T).Name;

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            var cursor = await _mongoDatabase.GetCollection<T>(CollectionName)
                .FindAsync(predicate);
            return cursor.ToEnumerable();
        }

        public Task<T> GetByIdAsync(string id)
        {
            return _mongoDatabase.GetCollection<T>(CollectionName)
                .Find(x => x.Id == id)
                .SingleAsync();
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await _mongoDatabase.GetCollection<T>(CollectionName)
                    .InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error inserting entity {entity.Id}", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var result = await _mongoDatabase.GetCollection<T>(CollectionName)
                    .ReplaceOneAsync(x => x.Id == entity.Id, entity);

                if (result.MatchedCount != 1)
                {
                    throw new RepositoryException($"Missing entoty {entity.Id}");
                }
            }
            catch (MongoWriteException ex)
            {
                throw new RepositoryException($"Error updating entity {entity.Id}", ex);
            }
        }
    }

    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception inner) : base(message, inner) { }
        protected RepositoryException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
