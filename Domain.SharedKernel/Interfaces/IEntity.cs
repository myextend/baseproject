namespace Domain.SharedKernel.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
