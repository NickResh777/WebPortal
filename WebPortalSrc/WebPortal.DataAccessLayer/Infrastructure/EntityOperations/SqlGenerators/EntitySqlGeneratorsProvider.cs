using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class EntitySqlGeneratorsProvider : IEntitySqlGeneratorsProvider{
        

        public EntitySqlGeneratorsProvider(){
           
        }

        public SelectEntitySqlGenerator CreateSelectGenerator(){
            var sqlGenerator = new SelectEntitySqlGenerator(CreateConfig());
            return sqlGenerator;
        }

        private SqlGeneratorConfig CreateConfig(){
            return new SqlGeneratorConfig();
        }

        public DeleteEntitySqlGenerator CreateDeleteGenerator(){
            return new DeleteEntitySqlGenerator(CreateConfig());
        }

        public object[] BuildParametersList(IEnumerable<IWhereConditionRoot> whereConditions){

            return BuildParametersRecursive(0, whereConditions);
        }

        private object[] BuildParametersRecursive(int conditionIndex, IEnumerable<IWhereConditionRoot> conditionsList){
            List<object> parametersList = new List<object>();

            foreach(var condition in conditionsList){
                if (condition is WhereCondition){
                    string parameterName = string.Format("@p{0}", conditionIndex++);
                    var sqlParameter = new SqlParameter(parameterName, ((WhereCondition) condition).Value);
                    parametersList.Add(sqlParameter);
                } else if (condition is WhereConditionsGroup){
                    // recursive call
                    var recResult = BuildParametersRecursive(conditionIndex, ((WhereConditionsGroup)condition).Conditions);
                    parametersList.AddRange(recResult);
                }  
            }

            return parametersList.ToArray();
        }


        public UpdateEntitySqlGenerator CreateUpdateGenerator() {
            throw new NotImplementedException();
        }
    }
}
