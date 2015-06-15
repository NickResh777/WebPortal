using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class SelectEntitySqlGenerator: EntitySqlGenerator {

        private List<string> _entityProperties; 

        public SelectEntitySqlGenerator() {
            // empty constructor
        }

        /// <summary>
        /// List of properties to select in the 'SELECT' clause
        /// </summary>
        public List<string> SelectedColumns{
            get{
                return _entityProperties ?? (_entityProperties = new List<string>());
            }
        }


        /// <summary>
        /// Select all columns from the table? ('*' asterik symbol)
        /// </summary>
        public bool SelectAllColumns{
            get; 
            set; 
        }

        public void Initialize(string tableName){
            
        }

        protected override void GenerateSqlClauseInternal(StringBuilder sb){
            // append the 'SELECT' clause
            sb.Append("SELECT ");

            if (SelectAllColumns){
                // we need all the members of EntityType
                sb.Append(" * ");
            } else{
           
                SelectedColumns.ForEach(prop =>{
                    sb.AppendFormat("[{0}]", prop);

                    if (SelectedColumns.IndexOf(prop) < (SelectedColumns.Count - 1)){
                         // add some space between different column names and a comma
                        sb.Append(", ");
                    }
                });   
            }

            // add some space before the 'FROM' clause
            sb.Append(" ");

            // GENERATE THE 'FROM' CLAUSE
            GenerateFromClause(sb);
        }
    }
}
