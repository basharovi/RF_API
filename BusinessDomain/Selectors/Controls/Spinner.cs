using RapidFireLib.Lib.Core;
namespace BusinessDomain.Selectors.Controls
{
    public class Spinner : IQuerySelector
    {
        public string Select(string modelName)
        {
            string sql = "";
            switch (modelName)
            {
                case "Student": sql = "SELECT StudentId, Name FROM Student"; break;
                default:
                    break;
            }
            return sql;
        }
        
       
    }
}
