namespace ClassLibraryContracts.Models
{
    public abstract class BaseEntityDto<TKey> where TKey : notnull
    {
        public TKey Id { get; set; }
    }
}
