namespace ClassLibraryContracts.Entities
{
    public abstract class BaseEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; init; }
    }
}
