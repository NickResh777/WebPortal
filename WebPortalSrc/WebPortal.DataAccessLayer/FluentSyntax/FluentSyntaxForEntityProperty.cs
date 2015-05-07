using WebPortal.DataAccessLayer.Infrastructure;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;

namespace WebPortal.DataAccessLayer.FluentSyntax {
    public class FluentSyntaxForEntityProperty<T> : FluentSyntaxBase where T: class{
        private readonly FluentSyntaxForWhereConditions<T> _whereParent; 
        
        public FluentSyntaxForEntityProperty(string propertyName, 
                                            FluentSyntaxForWhereConditions<T> whereParent, 
                                            int paramIndex){
            PropertyName = propertyName;
            ParameterIndex = paramIndex;
            _whereParent = whereParent;
        }

        public string PropertyName{
            get; 
            private set;
        }

        public int ParameterIndex{
            get; 
            private set;
        }

        public FluentSyntaxForWhereConditions<T> Is(object value){
            var condition = CreateCondition(Comparison.Equals, value);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        private WhereCondition CreateCondition(Comparison comparison, object value){
            return new WhereCondition{
                Column = PropertyName,
                ParameterIndex = ParameterIndex,
                Operator = comparison,
                Value = value
            };
        }


        public FluentSyntaxForWhereConditions<T> IsNot(object propertyValue){
            var condition = CreateCondition(Comparison.NotEquals, propertyValue);
            // Where().Property(m => m.Name).NotEquals("Limo");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> LessThan(object propertyValue){
            var condition = CreateCondition(Comparison.LessThan, propertyValue);
             // Where().Property(m => m.Age).LessThan(23);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> GreaterThan(object propertyValue){
            var condition = CreateCondition(Comparison.GreaterThan, propertyValue);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }


        public FluentSyntaxForWhereConditions<T> IsNull(){
            var condition = CreateCondition(Comparison.Is, "NULL");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> IsNotNull(){
            var condition = CreateCondition(Comparison.Is, "NOT NULL");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        } 
    }
}
