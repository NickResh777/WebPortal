using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities.Authentication;

namespace WebPortal.DataAccessLayer.Mapping
{
    class AppUserMap : EntityTypeConfiguration<AppUser>{
        public AppUserMap(){
            ToTable("AppUsers");

            HasKey(appUser => appUser.Id);

            Property(appUser => appUser.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(user => user.AuthProvider )
                .IsRequired()
                .HasMaxLength(20);

            Property(appUser => appUser.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(100);

            Property(appUser => appUser.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(200);

            Property(appUser => appUser.PasswordSalt)
                .IsRequired()
                .HasColumnName("Salt");

            // SHA-256 generated hash should have length of 64 symbols in hex
            Property(appUser => appUser.PasswordHash)
                .IsRequired()
                .HasColumnName("Hash")
                .HasColumnType("char")
                .HasMaxLength(64);

            HasOptional(appUser => appUser.Member)
                .WithRequired(m => m.AppUser);
        }
    }
}
