using System;

namespace Giny.ORM.Attributes
{
    public class TableAttribute : Attribute
    {
        public string TableName;
        public bool Load;

        public TableAttribute(string tableName, bool load = true)
        {
            this.TableName = tableName;
            this.Load = load;
        }
    }
}
