[![Nuget](https://img.shields.io/nuget/v/SqlServer.LocalDb.Testing)](https://www.nuget.org/packages/SqlServer.LocalDb.Testing/)

This is a small library of static methods to help you write integration tests for SQL Server. It creates databases dynamically on your localdb instance, and frees you from maintaining a connection string and related config overhead. You can also initialize databases with seed objects and data via SQL statements or any arbitrary initialization.

To use, install nuget package **SqlServer.LocalDb.Testing**

Then, in your integration tests that require a LocalDb connection, you can write code like this:

```csharp
using (var cn = LocalDb.GetConnection("sample"))
{
  // whatever testing you need to do
}
```
This will open or create a database named `sample` at **(localdb)\mssqllocaldb**

I didn't try too hard to implement deletion of sample databases because I have found deleting databases at runtime to be pretty fussy. I figure if you are running tests in an environment like AppVeyor, then you get a clean environment with every build. So, I felt that deleting databases as a cleanup activity was just not necessary.

I looked around and saw a couple other libraries doing exactly what I set out to do here, which was interesting to see. Maybe I should've looked around before writing mine, but I enjoy stuff like this -- so here we are!

## Examples
- [Create database with sample model](https://github.com/adamosoftware/SqlIntegration/blob/master/Testing/SqlMigratorTest.cs#L44), sample model is [here](https://github.com/adamosoftware/SqlIntegration/blob/master/Testing/SqlMigratorTest.cs#L177), and random data creation is [here](https://github.com/adamosoftware/SqlIntegration/blob/master/Testing/SqlMigratorTest.cs#L127)
- [Drop sample db on test class startup](https://github.com/adamosoftware/SqlIntegration/blob/master/Testing/SqlMigratorTest.cs#L28)
- [Another test class initialize example](https://github.com/adamosoftware/Dapper.CX/blob/master/Tests/SqlServer/SqlServerIntegration.cs#L17)
- [Yet another test class initialize, with random data](https://github.com/adamosoftware/Dapper.QX/blob/master/Testing/ExecutionSqlServer.cs#L25), using my [Test Data Generation](https://github.com/adamosoftware/TestDataGen) library.
- Side note: Random data persistence is handled by my [BulkInsert](https://github.com/adamosoftware/SqlIntegration/blob/master/SqlIntegration.Library/BulkInsert.cs) helper from my [SqlIntegration](https://github.com/adamosoftware/SqlIntegration) project.

## Reference [LocalDb.cs](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L13)
- string [GetConnectionString](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L15)
 (string databaseName)
- SqlConnection [GetConnection](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L20)
 (string databaseName, IEnumerable<InitializeStatement> initializeStatements)
- void [ExecuteInitializeStatements](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L28)
 (SqlConnection cn, IEnumerable<InitializeStatement> statements)
- SqlConnection [GetConnection](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L55)
 (string databaseName, [ Action<SqlConnection> initialize ])
- bool [TryDropDatabase](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L96)
 (string databaseName, string message)
- bool [TryDropDatabaseIfExists](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L125)
 (string databaseName, string message)
- bool [ObjectExists](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L169)
 (SqlConnection connection, string objectName)
- void [ExecuteIfExists](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L185)
 (SqlConnection connection, string objectName, string execute)
- Task [CreateFromResourceAsync](https://github.com/adamfoneil/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L193)
 (Assembly assembly, string resourceName, string databaseName)

## Hi there
If by a crazy turn of events, you find this useful, please consider [buying me a coffee](https://paypal.me/adamosoftware).
