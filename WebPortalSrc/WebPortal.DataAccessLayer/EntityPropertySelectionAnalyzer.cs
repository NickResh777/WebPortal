using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WebPortal.DataAccessLayer {
    public class EntityPropertySelectionAnalyzer : IEntityPropertySelectionAnalyzer {
        public string GetPropertyName<T>(Expression<Func<T, object>> propertySelector) where T : class{
            MemberExpression expression = null;

            if (propertySelector.Body.NodeType == ExpressionType.MemberAccess){
                expression = (MemberExpression) propertySelector.Body;
            }  else if (propertySelector.Body.NodeType == ExpressionType.Convert){
                expression = (MemberExpression) (((UnaryExpression) propertySelector.Body).Operand);
            }

            return GetPropertyNameInternal(expression);
        }

        private string GetPropertyNameInternal(MemberExpression memberExp){
            int dotIndex = memberExp.Member.Name.IndexOf('.');
            string propertyName = memberExp.Member.Name;
            if (dotIndex != (-1)){
                // delete any symbols before the '.' (dot) symbol
                propertyName = propertyName.Substring(dotIndex + 1);
            }
            return propertyName;
        }

        public void ValidateSelector<T>(Expression<Func<T, object>> propertySelector) where T : class {
            if (propertySelector == null){
                // NULL 
                throw new NullReferenceException("Parameter <propertySelector> is NULL (not defined)");
            }

            var isValid = propertySelector.Body.NodeType == ExpressionType.MemberAccess ||
                          propertySelector.Body.NodeType == ExpressionType.Convert;

            if (!isValid){
                const string exMsg = @"Expression must have the following types: <MemberAccess> or <Convert>";
                var ex = new ArgumentException(exMsg);
                throw ex;
            }

            // ensure selector has the <MemberAccess> expression
            EnsureExpressionBodyIsMemberExpression<T>(propertySelector);

            // ensure selector has selected property of type other than class
            EnsureSelectedPropertyIsNotClass<T>(propertySelector);
        }

        private void EnsureExpressionBodyIsMemberExpression<T>(Expression<Func<T, object>> propertySelector){
            if (propertySelector.Body.NodeType == ExpressionType.MemberAccess){
                 // the current selector has the type of <MemberExpression> as its node type is 'MemberAccess'
            } else if (propertySelector.Body.NodeType == ExpressionType.Convert){
                var unaryExpression = (UnaryExpression)propertySelector.Body;
                // operand should be of type <MemberExpression>
                if (!(unaryExpression.Operand is MemberExpression)){
                    const string exMsg = @"Expression body should have the operand of type <MemberExpression>";
                    var ex = new ArgumentException(exMsg);
                    throw ex;
                }
            }
        }


        private void EnsureSelectedPropertyIsNotClass<T>(Expression<Func<T, object>> propertySelector){
            MemberExpression memberExpression = null;
            if (propertySelector.Body.NodeType == ExpressionType.MemberAccess){
                memberExpression = (MemberExpression) propertySelector.Body;
            } else if (propertySelector.Body.NodeType == ExpressionType.Convert){
                memberExpression = (MemberExpression) (((UnaryExpression) propertySelector.Body).Operand);
            }

            if (memberExpression.Member.MemberType != MemberTypes.Property){
                  var ex = new ArgumentException("Selected property must have the MemberType of 'Property'");
                  throw ex;
            }

            var selectedPropertyClassType = memberExpression.Member.DeclaringType;
            var selectedEntityPropertyInfo = selectedPropertyClassType.GetProperty(memberExpression.Member.Name);
            
            if (selectedEntityPropertyInfo.PropertyType != typeof (string)){
                if (selectedEntityPropertyInfo.PropertyType.IsClass || 
                     selectedEntityPropertyInfo.PropertyType.IsInterface){
                    const string exMsg = @"Selected property should not be of type class.
                                           Only primitive properties should be selected: <string>, <int>, <double>, <char>";
                    var ex = new ArgumentException(exMsg);
                    throw ex;
                }
            }

        }
    }
}
