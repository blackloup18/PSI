using System.Data;

namespace PSI.Conn
{
    public class Common
    {
        public static CommandType GetCommandType(string commandType)
        {
            switch (commandType)
            {
                case "StoredProcedure": return CommandType.StoredProcedure;
                case "Text": return CommandType.Text;
                case "TableDirect": return CommandType.TableDirect;
                default: return CommandType.Text;
            }
        }
    }
}
