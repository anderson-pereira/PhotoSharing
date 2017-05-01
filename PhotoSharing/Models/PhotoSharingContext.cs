using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharing.Models
{
    public class PhotoSharingContext : DbContext
    {

        public PhotoSharingContext() : base()
        {
            Database.CommandTimeout = 180;
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}