using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.BusinessLogic
{
    public class PredicateExpressionBuilder<TEntity> where TEntity: BaseEntity{
        private Expression<Func<TEntity, bool>> _predicate;

        public PredicateExpressionBuilder<TEntity> And(Expression<Func<TEntity, bool>> newPredicate){
            if (_predicate == null){
                  _predicate = newPredicate;
                  return this;
            }

            // todo: combine predicates here ! URGENT !
            BinaryExpression body = Expression.AndAlso(_predicate.Body, newPredicate.Body);
            _predicate = Expression.Lambda<Func<TEntity, bool>>(body, _predicate.Parameters.First());  
            return this;
        }

        public PredicateExpressionBuilder<TEntity> Or(Expression<Func<TEntity, bool>> newPredicate){
            if (_predicate == null){
                _predicate = newPredicate;
                return this;
            }

            BinaryExpression expressionOr = Expression.Or(_predicate, newPredicate);
            _predicate = Expression.Lambda<Func<TEntity, bool>>(expressionOr, _predicate.Parameters.First());
            return this;
        } 

        public Expression<Func<TEntity, bool>> GetExpression(){

            return _predicate;
        } 
    }
}
