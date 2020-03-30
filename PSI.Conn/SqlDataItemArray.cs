using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PSI.Conn
{
    [DataContract]
    public class SqlDataItemArray
    {
        List<SqlDataItem> sqlDataItemList = new List<SqlDataItem>();
        private string databaseName;
        public SqlDataItemArray()
        {
        }
        [DataMember]
        public List<SqlDataItem> SqlDataItemList
        {
            get { return sqlDataItemList; }
            set { sqlDataItemList = value; }
        }
        [DataMember]
        public string DatabaseName
        {
            set { databaseName = value; }
            get { return databaseName; }
        }
        public void AppendSqlItem(SqlDataItem item)
        {
            sqlDataItemList.Add(item);
        }


    }
}
