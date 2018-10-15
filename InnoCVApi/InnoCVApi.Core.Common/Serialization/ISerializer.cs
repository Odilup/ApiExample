namespace InnoCVApi.Core.Common.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);

        string Serialize<TEntity>(TEntity entity) where TEntity : class;

        object Deserialize(string serializedContent);

        TEntity Deserialize<TEntity>(string serializedContent) where TEntity : class;
    }
}