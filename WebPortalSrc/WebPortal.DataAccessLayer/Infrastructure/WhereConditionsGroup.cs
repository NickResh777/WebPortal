using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.Infrastructure {
    public class WhereConditionsGroup : IWhereConditionRoot{

        private readonly List<IWhereConditionRoot> _conditions = new List<IWhereConditionRoot>();
        private readonly List<LogicalOperation> _logicalOperations = new List<LogicalOperation>();

        public WhereConditionsGroup(){
           
        }

        public List<IWhereConditionRoot> Conditions{
            get{
                return _conditions;
            }
        }

        public List<LogicalOperation> LogicalOperations{
            get{
                return _logicalOperations;
            }
        } 
    }
}
