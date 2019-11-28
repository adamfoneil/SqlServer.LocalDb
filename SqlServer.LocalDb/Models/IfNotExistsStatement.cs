namespace SqlServer.LocalDb.Models
{
    /// <summary>
    /// Performs an existence check in database for an object, and executes a statement if the object doesn't exist
    /// </summary>
    public class IfNotExistsStatement
    {
        public IfNotExistsStatement(string objectName, string commandText)
        {
            ObjectName = objectName;
            CommandText = commandText;
        }

        /// <summary>
        /// check existence of this object name
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// command to execute if object doesn't exist
        /// </summary>
        public string CommandText { get; set; }
    }
}
