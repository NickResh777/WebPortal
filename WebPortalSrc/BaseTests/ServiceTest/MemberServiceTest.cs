using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.Core;
using NUnit.Framework;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer.IoCInjection;

namespace BaseTests.ServiceTest
{                     
    [TestFixture]
    public class MemberServiceTest{
        private IMemberService _memberService;

        [SetUp]
        public void Init(){

            // get a new instance MemberService
            _memberService = GlobalInjectionContainer.Instance.Get<IMemberService>();
        }

    }
}
