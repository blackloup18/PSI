using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PSI.Conn
{
    [DataContract]
    public class SqlDataItem
    {
        List<SqlDataParameter> parameterList = new List<SqlDataParameter>();
        private string commandText = "";
        private string commandType = "";
        private string databaseName = "";
        public SqlDataItem()
        {
        }
        [DataMember]
        public string CommandText
        {
            set { commandText = value; }
            get { return commandText; }
        }
        [DataMember]
        public string CommandType
        {
            set { commandType = value; }
            get { return commandType; }
        }
        [DataMember]
        public string DatabaseName
        {
            set { databaseName = value; }
            get { return databaseName; }
        }
        [DataMember]
        public List<SqlDataParameter> ParameterList
        {
            set { parameterList = value; }
            get { return parameterList; }
        }
        public void AppendParameter(SqlDataParameter param)
        {
            parameterList.Add(param);
        }

        public void AppendParameter(string name, object value, Type type)
        {
            parameterList.Add(new SqlDataParameter(name, value, type));
        }

        public void AppendParameter(string name, string value)
        {
            parameterList.Add(new SqlDataParameter(name, value, typeof(string)));
        }
    }
}
