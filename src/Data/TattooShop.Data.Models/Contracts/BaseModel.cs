namespace TattooShop.Data.Models.Contracts
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
