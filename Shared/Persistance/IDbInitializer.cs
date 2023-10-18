namespace Shared.Persistance
{
    public interface IDbInitializer
    {
        Task Migrate();
    }
}
