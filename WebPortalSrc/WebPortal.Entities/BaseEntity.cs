using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    public abstract class BaseEntity{
        private object[] _keys;


        /// <summary>
        /// Get the key for this entity.
        /// Entity key could be of type 'int' for BaseEntity
        /// </summary>
        [NotMapped]
        public object[] Keys{
            get{
                return _keys;
            }
            protected set{
                _keys = value;
            }
        }

        protected void OnEntityKeySet(object newEntityKey){
            if (newEntityKey == null){
                throw new NullReferenceException("newEntityKey");
            }
            if (Keys != null){
                var newKeys = new object[Keys.Length + 1];
                Array.Copy(Keys, newKeys, Keys.Length);
                newKeys[Keys.Length] = newEntityKey;
                Keys = newKeys;
            } else{
                Keys = new object[]{ newEntityKey};
            }
        }



        public override bool Equals(object anotherObject){
            if (anotherObject == null){
                // FALSE since an object cannot equal to NULL
                return false;
            }

            if (ReferenceEquals(this, anotherObject)){
                // the same reference, it's TRUE
                return true;
            }

            if (!(anotherObject is BaseEntity) || (GetType() != anotherObject.GetType())){
                // types do not match, then FALSE
                // typeof(Message) != typeof(LogRecord)
                return false;
            }

            // cast object to BaseEntity
            var baseEntity = (BaseEntity)anotherObject;

            // check keys
            return Keys.Equals(baseEntity.Keys);
        }
    }
}
