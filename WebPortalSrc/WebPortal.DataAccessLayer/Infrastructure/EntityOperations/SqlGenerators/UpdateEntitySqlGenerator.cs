using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class UpdateEntitySqlGenerator : EntitySqlGenerator{
        private Dictionary<string, object> _setProperties;    

        public UpdateEntitySqlGenerator(SqlGeneratorConfig config) : base(config){
                        
        }

        public void Set(IDictionary<string, object> setValues){
            if (setValues == null){
                throw new NullReferenceException("setValues");
            }

           _setProperties = new Dictionary<string, object>(setValues);
        }

        public Dictionary<string, object> UpdateSet{
            get{
                return _setProperties ?? (_setProperties = new Dictionary<string, object>());
            }
        } 

       


        public UpdateEntitySqlGenerator Set(string property, object value){
            if (_setProperties == null){
                  // 
                  _setProperties = new Dictionary<string, object>();
            }

            if (!_setProperties.ContainsKey(property)){
                // add a pair if it had not been added before
                _setProperties.Add(property, value);
            }

            return this;
        }  

        protected override void GenerateSqlClauseInternal(StringBuilder sb ){
            sb.Append("UPDATE ");
            sb.AppendFormat("[{0}]", TableName);

            sb.AppendFormat(" {0} ", "SET");
            

            int setsCount = UpdateSet.Keys.Count;
            int currentSet = 0;
            if ((_setProperties != null) && (UpdateSet.Count > 0)){
                foreach(var key in UpdateSet.Keys){
                    sb.AppendFormat("[{0}] = {1}", 
                           key,
                           SqlInjectedValueFormatter.ObjectToString(UpdateSet[key])
                    );

                    if (currentSet++ < setsCount - 1){
                        sb.Append(", ");
                    }
                }
            }

            sb.Append(" ");

        }
    }
}
