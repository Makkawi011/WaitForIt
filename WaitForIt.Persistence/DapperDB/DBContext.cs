using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

using Dapper;

using WaitForIt.Domain;

namespace WaitForIt.Persistence.DapperMigrations;

internal class DBContext
{
    public static IDbConnection db { get; set; } 
    public DBContext(bool IsEventAndEntityTableCreated = true)
    {
        db = new SqlConnection(Constants.ConnectionString);

        if (!IsEventAndEntityTableCreated) CreateTables().GetAwaiter().GetResult();
        

    }

    private async static Task CreateTables()
    {

        using IDbConnection db = new SqlConnection(Constants.ConnectionString);

        string sql1 = @"CREATE TABLE Entities
                        (
                            Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                            Name NVARCHAR(MAX) NOT NULL,
                            Description NVARCHAR(MAX) NULL,
                            Quantity INT NOT NULL
                        );";

        await db.ExecuteAsync(sql1);

        string sql2 = @"CREATE TABLE Events (
                    EventId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    EventName NVARCHAR(MAX) NOT NULL,
                    EventTime DATETIME NOT NULL,
                    EntityId UNIQUEIDENTIFIER NULL,
                    FOREIGN KEY (EntitiesId) REFERENCES Entities(Id)
                  )";

        await db.ExecuteAsync(sql2);
    }



}