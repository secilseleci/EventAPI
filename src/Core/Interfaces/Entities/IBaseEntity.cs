 
namespace Core.Interfaces.Entities
{
    public interface IBaseEntity<T>
    {
        public T  Id { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<Guid>//global
    {

    }
}