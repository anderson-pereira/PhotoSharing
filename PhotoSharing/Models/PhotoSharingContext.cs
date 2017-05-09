using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharing.Models
{
    public class PhotoSharingContext : DbContext, IPhotoSharingContext
    {

        public PhotoSharingContext() : base()
        {
            Database.CommandTimeout = 180;
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Comment> Comments { get; set; }

        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get
            {
                return Photos;
            }
        }

        IQueryable<Comment> IPhotoSharingContext.Comments
        {
            get
            {
                return Comments;
            }
        }

        public T Add<T>(T entity) where T : class
        {
            //Set invoca o DbSet.
            return Set<T>().Add(entity);
        }

        public Photo FindPhotoById(int ID)
        {
            return Set<Photo>().Find(ID);
        }

        public Comment FindCommentById(int ID)
        {
            return Set<Comment>().Find(ID);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Set<T>().Remove(entity);
        }
    }
}