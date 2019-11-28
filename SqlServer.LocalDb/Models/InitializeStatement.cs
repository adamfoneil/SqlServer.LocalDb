namespace SqlServer.LocalDb.Models
{
    /// <summary>
    /// Describes an object existence check, drop, and creation of a SQL Server object
    /// </summary>
    public class InitializeStatement
    {
        private const string objectNameToken = "%obj%";

        public InitializeStatement(string objectName, string dropStatement, string createStatement)
        {
            ObjectName = objectName;
            DropStatement = dropStatement;
            CreateStatement = createStatement;
        }

        /// <summary>
        /// check existence of this object name
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// statement to run if object exists (use %obj% within the statement to insert object name dynamically)
        /// </summary>
        public string DropStatement { get; set; }

        /// <summary>
        /// command create the object (use %obj% within the statement to insert object name dynamically)
        /// </summary>
        public string CreateStatement { get; set; }

        public string ResolveDropStatement() => DropStatement.Replace(objectNameToken, ObjectName);

        public string ResoveCreateStatement() => CreateStatement.Replace(objectNameToken, ObjectName);
    }
}
