public interface IErrorLogStore
{
    Task CreateAsync(
        ErrorLog errorLog);
}