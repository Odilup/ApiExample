using System.ComponentModel.DataAnnotations.Schema;

namespace InnoCVApi.Domain.Entities
{
    /// <summary>
    /// Base class for every domain's entity model
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseModel<TKey> 
    {
        public abstract TKey Id { get; set; }
    }
}