using System.Data.SqlClient;
using System.Data;
using WaitForIt.Persistence.DapperMigrations;

namespace WaitForIt.Persistence.Repository;

public class DBRepository
{
    public IDbConnection db = DBContext.db;
}
