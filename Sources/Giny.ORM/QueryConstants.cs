using Giny.Core.IO;
using Giny.ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ORM
{
    class QueryConstants
    {
        public const string Create = "CREATE TABLE if not exists {0} ({1})";

        public const string Drop = "DROP TABLE IF EXISTS {0}";

        public const string Delete = "DELETE FROM {0}";

        public const string Select = "SELECT * FROM `{0}`";

        public const string SelectWhere = "SELECT * FROM `{0}` WHERE {1}";

        public const string CountWhere = "SELECT COUNT(*) FROM `{0}` WHERE {1}";

        public const string Count = "SELECT COUNT(*) FROM `{0}`";

        public const string Insert = "INSERT INTO `{0}` VALUES {1}";

        public const string Update = "UPDATE `{0}` SET {1} WHERE {2} = {3}";

        public const string Remove = "DELETE FROM `{0}` WHERE `{1}` = {2}";

        public const string PrimaryKey = "PRIMARY KEY ({0})";

        private static Dictionary<string, string> TypeMapping = new Dictionary<string, string>()
        {
            { "String", "VARCHAR(255)" },
            { "Int16", "SMALLINT" },
            { "Int32", "INT" },
            { "Int64", "BIGINT" },
            { "Byte", "TINYINT" },
        };

        private const string BinaryType = "BLOB";

        private const string DefaultType = "MEDIUMTEXT";


        public static string ConvertType(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<TypeOverrideAttribute>();

            if (attribute != null)
            {
                return attribute.NewType;
            }
            else if (property.GetCustomAttribute<ProtoSerializeAttribute>() != null)
            {
                return BinaryType;
            }
            else if (TypeMapping.ContainsKey(property.PropertyType.Name))
            {
                return TypeMapping[property.PropertyType.Name];
            }

            return DefaultType;
        }
    }
}
