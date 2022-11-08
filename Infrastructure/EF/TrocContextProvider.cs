namespace Infrastructure.EF;

public class TrocContextProvider
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public TrocContextProvider(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public TrocContext NewContext()
    {
        return new TrocContext(_connectionStringProvider);
    }
}