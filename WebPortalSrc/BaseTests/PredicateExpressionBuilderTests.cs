using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WebPortal.BusinessLogic;
using WebPortal.Entities.Members;

namespace BaseTests
{
    [TestFixture]
    public class PredicateExpressionBuilderTests
    {

        [Test]
        public void test1(){
             PredicateExpressionBuilder<Member> builder = new PredicateExpressionBuilder<Member>();
             
            builder.And(m => m.IsTrial);
            builder.And(m => m.Gender == "M");
            builder.And(m => m.Email == "nickresh");


        }
    }
}
