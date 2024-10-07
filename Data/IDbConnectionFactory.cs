using System.Data;

namespace BlazorWebAppWithDapper.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
