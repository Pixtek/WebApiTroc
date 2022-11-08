namespace Infrastructure.EF;

public interface IConnectionStringProvider
{
    string getConnectionString(string key);
}