This is a small library of static methods to help you write integration tests for SQL Server using LocalDb. It helps by creating sample databases dynamically. You can also initialize databases with seed objects and data via SQL statements or any arbitrary initialization.

To use, install nuget package **SqlServer.LocalDb.Testing**

Then, in your integration tests that require a LocalDb connection, you can write code like this:

```
using (var cn = LocalDb.GetConnection("sample"))
{
  // whatever testing you need to do
}
```
This will open or create a database named `sample` at **(localdb)\mssqllocaldb**

## Reference

[LocalDbConnectionString(string databaseName)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L10)

[GetConnection(string databaseName, IEnumerable\<IfNotExistsStatement\> ifNotExistStatements)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L15)

[SqlConnection GetConnection(string databaseName, Action\<SqlConnection\> initialize = null](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L37)

[TryDropDatabase(string databaseName, out string message)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L61)
