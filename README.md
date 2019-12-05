This is a small library of static methods to help you write integration tests for SQL Server using LocalDb by creating databases dynamically. You can also initialize databases with seed objects and data via SQL statements or any arbitrary initialization.

To use, install nuget package **SqlServer.LocalDb.Testing**

Then, in your integration tests that require a LocalDb connection, you can write code like this:

```
using (var cn = LocalDb.GetConnection("sample"))
{
  // whatever testing you need to do
}
```
This will open or create a database named `sample` at **(localdb)\mssqllocaldb**

I didn't try too hard to implement deletion of sample databases because I have found deleting databases at runtime to be pretty fussy. I figure if you are running tests in an environment like AppVeyor, then you get a clean environment with every build. So, I felt that deleting databases as a cleanup activity was just not necessary.

I looked around and saw a couple other libraries doing exactly what I set out to do here, which was interesting to see. Maybe I should've looked around before writing mine, but I enjoy stuff like this -- so here we are!

## Reference
All of these are static methods of the `LocalDb` object:

[string GetConnectionString(string databaseName)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L11)

[SqlConnection GetConnection](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L16)`(string databaseName, IEnumerable<InitializeStatement> initializeStatements)` See also [InitializeStatement](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/Models/InitializeStatement.cs)

[SqlConnection GetConnection](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L51)`(string databaseName, Action<SqlConnection> initialize = null)`

[bool TryDropDatabase](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L91)`(string databaseName, out string message)`

[bool ObjectExists](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L139)`(SqlConnection connection, string objectName)`

[void ExecuteInitializeStatements](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L24)`(SqlConnection cn, IEnumerable<InitializeStatement> statements)`

[void ExecuteIfExists](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L155)`(SqlConnection connection, string objectName, string execute)`

## Hi there
If by a crazy turn of events, you find this useful, please consider [buying me a coffee](https://paypal.me/adamosoftware).
