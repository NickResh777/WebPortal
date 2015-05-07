using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Mapping {
    class AuthInfoMap : BaseMap<AuthInfo>{
        public AuthInfoMap(){
            HasKey(auth => auth.MemberId);

            Property(auth => auth.LoginProvider).IsOptional();

            Property(auth => auth.PasswordHash)
                .IsRequired()
                .HasMaxLength(32);

            Property(auth => auth.PasswordSalt)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
