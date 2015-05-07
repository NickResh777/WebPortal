using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.FluentSyntax {
    public abstract class FluentSyntaxBase{

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType(){
            return base.GetType();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj){
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode(){
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString(){
            return base.ToString();
        }
    }
}
