using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class DeleteEntitySqlGenerator : EntitySqlGenerator {
        public DeleteEntitySqlGenerator(SqlGeneratorConfig config) : base(config){
            
        }

        protected override void GenerateSqlClauseInternal(StringBuilder sb){
            sb.Append("DELETE ");

            // GENERATE the 'FROM' clause to denote the table to delete from
            GenerateFromClause(sb);
        }
    }
}
