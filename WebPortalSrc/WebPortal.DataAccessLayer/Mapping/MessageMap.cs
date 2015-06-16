using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities;
using DatingHeaven.Core;

namespace WebPortal.DataAccessLayer.Mapping {
   class MessageMap : EntityTypeConfiguration<Message>, IDebug{
        public MessageMap(){
            ToTable("Messages");
            HasKey(m => m.Id);
            Property(m => m.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.IsRead).IsRequired().HasColumnName("IsRead");
            Property(m => m.ReceiverId).IsRequired().HasColumnName("ReceiverId");
            Property(m => m.SenderId).IsRequired().HasColumnName("SenderId");
            Property(m => m.Header).IsOptional().HasColumnName("Header").HasMaxLength(200);
            Property(m => m.Body).IsRequired().HasColumnName("Body").HasMaxLength(4000);

            HasRequired(message => message.Author)
                .WithMany()
                .HasForeignKey(message => message.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(message => message.Receiver)
                .WithMany()
                .HasForeignKey(message => message.ReceiverId)
                .WillCascadeOnDelete(false);
        }
    }
}
