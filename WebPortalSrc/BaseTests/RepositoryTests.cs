using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.DataAccessLayer.IoCInjection;
using WebPortal.Entities;
using WebPortal.Entities.Members;
using Ninject;
using NUnit.Framework;

namespace BaseTests {
    
    [TestFixture]
    public class RepositoryTests{
        private const int MAX_RECORDS_COUNT = 1000;

        #region === REPOSITORIES ===
            private IRepository<Message>           _messagesRepo;
            private IRepository<Member>            _membersRepo;
            private IDbContextProvider             _dbContextProvider;
        #endregion
        private RandomDataGenerator            _randomDataGenerator;

        private int _lastCreatedMemberId;

        

        [TestFixtureSetUp]
        public void Init(){
           using ( var kernel = new StandardKernel(new DalNInjectModule())){
               _messagesRepo = kernel.Get<IRepository<Message>>();
               _membersRepo = kernel.Get<IRepository<Member>>();
               _dbContextProvider = kernel.Get<IDbContextProvider>();
           }
            _randomDataGenerator = new RandomDataGenerator();

            using (var dbContext = new WebPortalDbContext()){
                dbContext.Database.Delete();
                if (!dbContext.Database.Exists()){
                    // create the DB if it does not exist
                    try{
                        dbContext.Database.Create();
                        CreateRandomMembers();
                        CreateRandomMessages();
                    } catch (Exception ex){
                        if (dbContext.Database.Exists()){
                            
                           //    dbContext.Database.Delete();
                        }

                        throw;
                    }
                }
            }
        }

        
        public void CreateRandomMessages(){
            for (var i = 1; i <= MAX_RECORDS_COUNT; ++i){
                var newMessage = new Message{
                    CreatedOn = DateTime.Now,
                    Body = _randomDataGenerator.RandomString(1000),
                    Header = _randomDataGenerator.RandomString(100),
                    IsRead = _randomDataGenerator.RandomBool(),
                    ReceiverId = _randomDataGenerator.RandomIntBetween(1, _lastCreatedMemberId + 1),
                    SenderId = _randomDataGenerator.RandomIntBetween(1, _lastCreatedMemberId + 1)
                };

                _messagesRepo.Insert(newMessage);
            }
        }


        [Test]
        public void CreateRandomMembers(){
            Member member;

            member = new Member{
                CreatedOn = DateTime.Now,
                Email = "nickresh777@gmail.com",
                FirstName = "Nick",
                Gender = "M",
                LastName = "Reshetinsky",
                NickName = "NickResh777"
            };
            _membersRepo.Insert(member);

            member = new Member{
               CreatedOn = DateTime.Now,
               Email = "jcatler@gmail.com",
               FirstName = "Jake",
               LastName = "Catler",
               NickName = "JCatler",
               Gender = "M"
            };
            _membersRepo.Insert(member);

            member = new Member{
                CreatedOn = DateTime.Now,
                Email = "jeremyclarkson@gmail.com",
                FirstName = "Jeremy",
                LastName = "Clarkson",
                Gender = "M",
                NickName = "JClarkson"
            };
            _membersRepo.Insert(member);

            _lastCreatedMemberId = member.Id;
        }


        [Test]
        public void repository_select_messages_with_filter(){
            var readMessages = _messagesRepo.GetWhere(m => m.Id, 10001);
           // Assert.IsTrue(readMessages.Count == 0);
        }


        [Test]
        public void repository_get_members_list(){
            var members = _membersRepo.GetAll();
            Assert.True(members != null);
            Assert.True(members.Count > 0);
        }

        [Test]
        public void repository_where_fluent_syntax_single_property(){
            var messages = _messagesRepo.Where().Property(m => m.Id).LessThan(7000).Select();
            Assert.True(messages != null);
            Assert.True(messages.Count > 0);
            Assert.True(messages.All(m => m.Id < 7000));
            Assert.True(messages.FirstOrDefault( m => m.Id == 7000) == null);
        }


        [Test]
        public void repository_where_fluent_syntax_multiple_properties(){
            //var messages = _messagesRepo.Where().Property(m => m.Id).LessThan(7000).
            //    And().Property(m => m.SenderId).GreaterThan(20000).
            //    Or().Property(m => m.IsRead).Equals(true).Select();
            //Assert.True(messages != null);
            //Assert.True(messages.Count > 0);
            //Assert.True(messages.All(m => m.Id < 7000));
            //Assert.True(messages.All(m => m.SenderId > 20000));

        }


        [Test]
        public void repository_where_fluent_syntax_multiple_properties_priority(){
            //var messages = _messagesRepo.Where().Property(m => m.IsRead).Equals(true).
            //   And().IncPriority().Property(m => m.Id).GreaterThan(6000).
            //   Or().Property(m => m.SenderId).GreaterThan(20000).DecPriority().Select();
            //Assert.True(messages != null);
            //Assert.True(messages.Count > 0);
            //Assert.True(messages.All(m => m.Id < 7000));
            //Assert.True(messages.All(m => m.SenderId > 20000));
            //Assert.True(messages != null);
            //Assert.True(messages.Count > 0);
            //Assert.True(messages.All(m => m.IsRead == true));
            //Assert.True(messages.Any(m => m.Id > 6000));
            //Assert.True(messages.Any(m => m.SenderId < 20000));
        }

        [Test]
        public void repository_GetListWhere_method_invoked_with_SqlOperator(){
            IList<Message> messages = null;
            // check '<' operator (less than) 
            messages = _messagesRepo.GetWhere(m => m.Id, Comparison.LessThan, 3000);
            Assert.True( messages.All(m => m.Id < 3000));
            Assert.True( messages.FirstOrDefault(m => m.Id == 3000) == null);

            // check '>' operator (greater than)
            messages = _messagesRepo.GetWhere(m => m.Id, Comparison.GreaterThan, 6000);
            Assert.True(messages.All(m => m.Id > 6000));
            Assert.True(messages.FirstOrDefault(m => m.Id == 6000) == null);

            // check '<=' operator (less or equals)
            messages = _messagesRepo.GetWhere(m => m.Id, Comparison.LessOrEquals, 9000);
            Assert.IsTrue(messages.All(m => m.Id <= 9000));
            Assert.True(messages.FirstOrDefault(m => m.Id == 9000) != null);

            // check '>=' operator (greater or equals)
            messages = _messagesRepo.GetWhere(m => m.Id, Comparison.GreaterOrEquals, 5601);
            Assert.True(messages.All(m => m.Id >= 5601));
            Assert.True(messages.FirstOrDefault(m => m.Id == 5601) != null);
        }

        [Test]
        public void repository_GetListWhere_method_invoked_with_entity_bool_property(){
            IList<Message> messages = null;
            messages = _messagesRepo.GetWhere(m => m.IsRead, false);
            Assert.True(messages.Count > 0);
            Assert.True(messages.All(m => !m.IsRead));
        }


        [Test]
        public void repository_GetCountWhere_method_invoked(){
            int entitiesCount;
            entitiesCount = _messagesRepo.GetCountWhere(m => m.Header.Contains("33"));
          //  Assert.True(entitiesCount > 0);
        }

        [Test]
        public void repository_GetWhere_NoExpression_selector_invoked(){
            IList<Message> messages = null;
            messages = _messagesRepo.GetWhere("IsRead", false);
            Assert.IsTrue(messages.Count > 0);
            Assert.IsTrue(messages.All(m => m.IsRead == false));


            messages = _messagesRepo.GetWhere("Id", Comparison.GreaterThan, 3000);
            Assert.True(messages.Count > 0);
            Assert.True(messages.All(m => m.Id > 3000));
            Assert.IsNull(messages.FirstOrDefault(m => m.Id == 3000));
        }

        [Test]
        public void repository_GetCount_method_returns_non_zero_value(){
            var messagesCount = _messagesRepo.GetCount();
            Debug.WriteLine("count: " + messagesCount);
            Assert.True(messagesCount > 0);
        }

        [Test]
        [Ignore]
        public void repository_GetById_method_with_single_key(){
          //  var message = _messagesRepo.GetById(_newMessage.Id);
           // Assert.IsTrue(message != null);
        }

        [Test]
        public void repository_where_composite_condition(){
            var selector = _messagesRepo.Where().Property(m => m.Id).LessThan(3000).
                And().OpenBlock().
                Property(m => m.Id).LessThan(3000).
                Or().Property(m => m.IsRead).Is(true).
                CloseBlock();
            var result = selector.Select();
            Assert.True(result != null && result.Count > 0);
        }

        [Test]
        public void repository_get_message_with_member_relation(){

            var message = _messagesRepo.GetById(3);
            Assert.NotNull(message);
            var resultList = _messagesRepo.GetAll();

        }
    }
}
