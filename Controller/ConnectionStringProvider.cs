
using Infrastructure.EF;

namespace WebApiTroc;

public class ConnectionStringProvider : IConnectionStringProvider
{
    private readonly IConfiguration _configuration;

    public ConnectionStringProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }



    public string getConnectionString(string key)
    {
        return _configuration.GetConnectionString(key);
    }
}