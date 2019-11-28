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

I didn't try too hard to implement deletion of sample databases because I have found deleting databases at runtime to be pretty fussy. I figure if you are running tests in an environment like AppVeyor, then you get a clean environment with every build. So, I felt that deleting databases as a cleanup activity was just not necessary.

I looked around and saw a couple other libraries doing exactly what I set out to do here, which was interesting to see. Maybe I should've looked around before writing mine, but I enjoy stuff like this -- so here we are!

## Reference
All of these are static methods of the `LocalDb` object:

[string GetConnectionString(string databaseName)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L10)

[SqlConnection GetConnection(string databaseName, IEnumerable\<InitializeStatement\> initializeStatements)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L15) See also [InitializeStatement](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/Models/InitializeStatement.cs)

[SqlConnection GetConnection(string databaseName, Action\<SqlConnection\> initialize = null)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L37)

[bool TryDropDatabase(string databaseName, out string message)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L61)

[bool ObjectExists(SqlConnection connection, string objectName)](https://github.com/adamosoftware/SqlServer.LocalDb/blob/master/SqlServer.LocalDb/LocalDb.cs#L115)
