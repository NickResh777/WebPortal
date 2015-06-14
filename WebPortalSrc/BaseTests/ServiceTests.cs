using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using WebPortal.BusinessLogic;
using WebPortal.BusinessLogic.Services;
using WebPortal.DataAccessLayer;
using Ninject;
using NUnit.Framework;
using WebPortal.DataAccessLayer.IoCInjection;

namespace BaseTests {
    [TestFixture]                            
    public class ServiceTests{

        private IDbContextProvider _dbContextProvider;
        private IHotListService _hotListService;


        
        [TestFixtureSetUp]
        public void Init(){
            using (var kernel = new StandardKernel(new DalNInjectModule(), new ServicesInjectModule())){
                _dbContextProvider = kernel.Get<IDbContextProvider>();
                _hotListService = kernel.Get<IHotListService>();
            }

            CreateDbIfNeeded();
        }

        private void CreateDbIfNeeded(){
            using (var dbContext = _dbContextProvider.CreateContext()){
                if (!dbContext.Database.Exists()){
                    dbContext.Database.Create();
                }
            }
        }


        [Test]   
        public void HotListService_method_AddMemberToHotList_should_throw_error_when_same_IDs_passed(){
            const int memberId = 334;
            const int targetMemberId = 334;
            Assert.Throws<InvalidOperationException>(
                () => _hotListService.AddMemberToHotList(memberId, targetMemberId, true, null));
        }
    }
}
