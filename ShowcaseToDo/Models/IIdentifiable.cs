namespace ShowCaseToDo.Models
{
    public interface IIdentifiable<T>
    {
        T Id { get; set; }
    }
}