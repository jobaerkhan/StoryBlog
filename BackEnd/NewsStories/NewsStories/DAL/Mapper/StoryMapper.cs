﻿using NewsStories.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NewsStories.DAL.Mapper
{
    public partial class StoryMapper : EntityTypeConfiguration<Story>
    {
        public StoryMapper()
        {
            this.ToTable("Story");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Title).IsRequired();
            this.Property(c => c.Title).HasMaxLength(200);

            this.Property(c => c.Body).IsRequired();
            this.Property(c => c.Body).HasMaxLength(2000);

            this.Property(c => c.PublishedDate).IsRequired();

            this.HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        }
    }
}