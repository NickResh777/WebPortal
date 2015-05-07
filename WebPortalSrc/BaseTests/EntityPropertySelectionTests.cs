using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using WebPortal.DataAccessLayer;
using WebPortal.Entities;
using WebPortal.Entities.Members;
using Ninject;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace BaseTests {
    [TestFixture]
    public class EntityPropertySelectionTests{
        private IEntityPropertySelectionAnalyzer _propertySelectionAnalyzer;

        [TestFixtureSetUp]
        public void Init(){
            using (var kernel = new StandardKernel(new DalNInjectModule())){
                _propertySelectionAnalyzer = kernel.Get<IEntityPropertySelectionAnalyzer>();
            }
        }

        [Test]
        public void analyzer_Validate_method_should_throw_exception_when_null_expression_passed(){
            Expression<Func<Message, object>> exp = null;
            Assert.Throws<NullReferenceException>(() => _propertySelectionAnalyzer.ValidateSelector(exp));
        }

        [Test]
        public void analyzer_Validate_method_should_throw_exception_when_expression_body_property_is_method_call(){
            Expression<Func<Message, object>> exp = (m) => m.GetHashCode();
            Assert.Throws<ArgumentException>(() => _propertySelectionAnalyzer.ValidateSelector(exp));
        }

        [Test]
        public void analyzer_Validate_method_should_not_throw_exception_when_expression_body_property_is_MemberAccess(){
            Expression<Func<Message, object>> exp = (m) => m.Header;
            Assert.DoesNotThrow(() =>_propertySelectionAnalyzer.ValidateSelector(exp));
        }

        [Test]
        public void analyzer_Validate_method_should_throw_exception_when_expression_body_property_is_not_class(){
           // Expression<Func<Member, object>> exp = (m) => m.Profile;
           // Assert.Throws<ArgumentException>(() => _propertySelectionAnalyzer.ValidateSelector(exp));
        }

        [Test]
        public void analyzer_GetPropertyName_returns_the_entity_property_name(){
            Expression<Func<Member, object>> exp;

            // must be 'LastName'
            exp = (m) => m.LastName;
            Assert.True(_propertySelectionAnalyzer.GetPropertyName(exp) == "LastName");

            exp = (m) => m.Id;
            Assert.True(_propertySelectionAnalyzer.GetPropertyName(exp) == "Id");
        }              
    }
}
