namespace SqlServer.LocalDb.Models
{
    /// <summary>
    /// Describes an object existence check, drop, and creation of a SQL Server object
    /// </summary>
    public class InitializeStatement
    {
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
        /// statement to run if object exists
        /// </summary>
        public string DropStatement { get; set; }

        /// <summary>
        /// command to execute if object doesn't exist
        /// </summary>
        public string CreateStatement { get; set; }
    }
}
