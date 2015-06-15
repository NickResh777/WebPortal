using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities.Profile;

namespace WebPortal.DataAccessLayer.Mapping {
    public class ProfileAttributeValueMap : EntityTypeConfiguration<ProfileAttributeValue>{
        public ProfileAttributeValueMap(){
            HasKey(pAttrValue => pAttrValue.Id);
            Property(pAttrValue => pAttrValue.IntValue).IsOptional();

            Property(pAttrValue => pAttrValue.TypeId)
                .IsRequired();

            // Property 'IntValue'
            Property(pAttrValue => pAttrValue.IntValue)
                .IsOptional();
                
            // Property 'CharValue'
            Property(pAttrValue => pAttrValue.CharValue).
                IsOptional().
                HasColumnType("char").
                HasMaxLength(1);

            // Property 'StringValue'
            Property(pAttrValue => pAttrValue.StringValue).
                IsOptional().
                HasMaxLength(50);

            // Property 'String100Value'
            Property(pAttrValue => pAttrValue.String100Value)
                .IsOptional()
                .HasMaxLength(100);

            // Property 'BinaryValue'
            Property(pAttrValue => pAttrValue.BinaryValue)
                .IsOptional();

            HasRequired(pAttrValue => pAttrValue.ProfileAttribute);
        }
    }
}
