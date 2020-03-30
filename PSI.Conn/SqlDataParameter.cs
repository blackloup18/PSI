using System;
using System.Runtime.Serialization;


namespace PSI.Conn
{
    [DataContract]
    public class SqlDataParameter
    {
        string parameterName = "";
        object parameterValue = null;
        string parameterType = "";
        public SqlDataParameter()
        {

        }
        [DataMember]
        public string ParameterName
        {
            set { parameterName = value; }
            get { return parameterName; }
        }
        [DataMember]
        public object ParameterValue
        {
            set { parameterValue = value; }
            get { return parameterValue; }
        }
        [DataMember]
        public string ParameterType
        {
            set { parameterType = value; }
            get { return parameterType; }
        }

        public SqlDataParameter(string name, object value, Type type)
        {
            parameterValue = value;
            parameterName = name;
            parameterType = type.Name;
            if (type == typeof(DateTime))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    parameterValue = DBNull.Value;
                    parameterType = DBNull.Value.ToString();
                    return;
                }

            }
            if (value == null)
            {
                parameterValue = DBNull.Value;
                parameterType = DBNull.Value.ToString();
                return;
            }

        }

    }
}
