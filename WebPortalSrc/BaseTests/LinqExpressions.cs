using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Entities.Members;
using NUnit.Framework;

namespace BaseTests {


    [TestFixture]
    public class LinqExpressionsTests{


        [Test]
        public void expression_NodeType_should_be_MemberAccess_when_entity_string_property_selected(){
            Expression<Func<Member, object>> exp = m => m.Email;
            Assert.True(exp.Body.NodeType == ExpressionType.MemberAccess); 
        }

        [Test]
        public void expression_NodeType_should_be_Convert_when_entity_int_property_selected(){
            Expression<Func<Member, object>> exp = m => m.Id;
            Assert.True(exp.Body.NodeType == ExpressionType.Convert);
        }

        [Test]
        public void expression_NodeType_should_be_Convert_when_entity_char_property_selected(){
            Expression<Func<Member, object>> exp = member => member.Gender;
            Assert.True(exp.Body.NodeType == ExpressionType.Convert);
        }

        [Test]
        public void expression_type_should_be_MemberExpression_when_entity_string_property_selected(){
            Expression<Func<Member, object>> exp = member => member.Email;
            Assert.True(exp.Body is MemberExpression);
        }

        [Test]
        public void expression_type_should_be_UnaryExpression_when_entity_int_property_selected(){
            Expression<Func<Member, object>> exp = member => member.Id;
            Assert.True(exp.Body is UnaryExpression);
        }
    }
}
