using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations {
    internal class UpdatePropertySqlQueryGenerator {
        public object Key{
            get; 
            set; 
        }

        public string UpdatedProperty{
            get; 
            set; 
        }

        public string TableName{
            get; 
            set; 
        }


        public object PropertyValue{
            get; 
            set; 
        }

        public string GenerateSql(){
            if (string.IsNullOrEmpty(UpdatedProperty)) {
                throw new NullReferenceException("UpdatedProperty");
            }

            if (string.IsNullOrEmpty(TableName)) {
                throw new NullReferenceException("TableName");
            }

            var sb = new StringBuilder();

            // append ---->> SELECT
            sb.Append("UPDATE ");

            // append ---->> SELECT [ReceiverId] 
            sb.AppendFormat("[{0}] ", TableName);

            // append ---->> SELECT [ReceiverId]
            //  ---------->> FROM [Messages]
            //sb.AppendFormat("FROM [{0}] ", TableName);
            sb.AppendFormat("SET [{0}] = {1}", PropertyValue);
             

            if (Key != null) {
                // ----->>> WHERE
                sb.AppendFormat("WHERE ");

                if (Key is int) {
                    // if key is INT
                    sb.Append("[Id] = @p0");
                } else if (Key is EntityKey) {
                    // if key is EntityKey
                    var entityKeys = ((EntityKey)Key).EntityKeyValues;
                    if (entityKeys.Length > 0) {
                        int length = (entityKeys.Length - 1);
                        foreach (var keyMember in entityKeys) {

                            sb.AppendFormat("([{0}] = {1})", keyMember.Key, keyMember.Value);
                            if (--length > 0) {
                                sb.Append(" AND ");
                            }
                        }
                    }
                }
            }


            return sb.ToString();
        }
    }
}
