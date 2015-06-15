using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class EntitySqlGenerator : SqlGenerator{
        private List<IWhereConditionRoot>  _conditionsList = new List<IWhereConditionRoot>();
        private List<LogicalOperation> _logicalOpsList = new List<LogicalOperation>(); 


        public string TableName{
            get; 
            set; 
        }

        public List<IWhereConditionRoot> WhereConditions{
            get{
                return _conditionsList;
            }
            set{
                _conditionsList = value;
            }
        }

        public List<LogicalOperation> LogicalOperations{
            get{
                
                return _logicalOpsList ?? (_logicalOpsList = new List<LogicalOperation>());
            }
            set{
                _logicalOpsList = value;
            }
        } 


        /// <summary>
        ///  Generate the major part of an SQL query 
        /// </summary>
        protected abstract void GenerateSqlClauseInternal(StringBuilder sb);

        public override string GenerateSql(){
            var sb = new StringBuilder();

            GenerateSqlClauseInternal(sb);

     

            if (WhereConditions != null){
                // GENERATE THE 'WHERE' CLAUSE
                GenerateWhereClause(sb);
            }

            return sb.ToString();
        }


        protected void GenerateFromClause(StringBuilder sb){
               // sql: FROM
               sb.Append("FROM ");

               // sql: FROM [{TableName}]
               sb.AppendFormat("[{0}]", TableName);

               if ( WhereConditions != null ){
                   // add some space after the [{TableName}] 
                   // cause next is 'WHERE' clause coming
                   sb.Append(" ");
               }
        }

        private void GenerateWhereClause(StringBuilder sb){
            // sql: WHERE 
            sb.Append("WHERE ");

            WriteConditions(sb);
        }

        public object[] BuildParametersList( )
        {  
            // call a recursive function with initial value of parameter index (0)
            return BuildParametersRecursive(0, WhereConditions);
        }


        private void WriteConditions(StringBuilder sb){
            for (var conditionIndex = 0; conditionIndex < WhereConditions.Count; conditionIndex++){
                IWhereConditionRoot condition = WhereConditions[conditionIndex]; 

                if (conditionIndex > 0){
                    var logicalOperation = LogicalOperations[conditionIndex - 1];
                    WriteLogicalOperation(sb, logicalOperation);
                }

                if (condition is WhereCondition){
                    WriteWhereCondition(sb, (WhereCondition) condition);
                } else if (condition is WhereConditionsGroup){
                    WriteConditionsGroup(sb, (WhereConditionsGroup) condition);
                }
            }
        }

        private object[] BuildParametersRecursive(int conditionIndex, IEnumerable<IWhereConditionRoot> conditionsList)
        {
            var parametersList = new List<object>();

            foreach (var condition in conditionsList)
            {
                if (condition is WhereCondition){
                    string parameterName = string.Format("@p{0}", conditionIndex++);
                    var sqlParameter = new SqlParameter(parameterName, ((WhereCondition)condition).Value);
                    parametersList.Add(sqlParameter);
                }
                else if (condition is WhereConditionsGroup){
                    // recursive call
                    var recResult = BuildParametersRecursive(conditionIndex,
                        ((WhereConditionsGroup)condition).Conditions);
                    parametersList.AddRange(recResult);
                }
            }

            return parametersList.ToArray();
        }
        

        private void WriteConditionsGroup(StringBuilder sb, WhereConditionsGroup group){
            sb.Append("(");

            int conditionIndex = 0;

            foreach(var condition in group.Conditions){
                if (condition is WhereCondition){
                    if (conditionIndex > 0){
                        var logicalOperation = group.LogicalOperations[conditionIndex - 1];
                        WriteLogicalOperation(sb, logicalOperation);
                    }

                    WriteWhereCondition(sb, (WhereCondition)condition);
                    conditionIndex++;
                } else if (condition is WhereConditionsGroup){
                    // recursive call 
                    WriteConditionsGroup(sb, (WhereConditionsGroup)condition);
                }
            }

            sb.Append(")");
        }

        private void WriteWhereCondition(StringBuilder sb, WhereCondition whereCondition){
            sb.AppendFormat("[{0}]", whereCondition.Column);

            // write the OPERATOR part
            WriteComparisonOperator(sb, whereCondition.Operator);

            WriteConditionIndex(sb, whereCondition.ParameterIndex);
        }

        private void WriteConditionIndex(StringBuilder sb, int paramIndex){
            sb.Append(" ");

            sb.AppendFormat("@p{0}", paramIndex);
        }

        private void WriteLogicalOperation(StringBuilder sb, LogicalOperation logicalOperation){
            switch (logicalOperation){
                case LogicalOperation.AND:
                    sb.Append(" AND ");
                    break;
                case LogicalOperation.OR:
                    sb.Append(" OR ");
                    break;
            }
        }

        private void WriteComparisonOperator(StringBuilder sb, Comparison comparison){
            sb.Append(" ");

            switch (comparison){
                 case Comparison.Equals:
                    sb.Append("=");
                    break;
                case Comparison.NotEquals:
                    sb.Append("!=");
                    break;
                case Comparison.GreaterThan:
                    sb.Append(">");
                    break;
                case Comparison.LessThan:
                    sb.Append("<");
                    break;
                case Comparison.LessOrEquals:
                    sb.Append("<=");
                    break;
                case Comparison.GreaterOrEquals:
                    sb.Append(">=");
                    break;
                case Comparison.Is:
                    sb.Append("is");
                    break;
            }

            sb.Append(" ");
        }
    }
}
