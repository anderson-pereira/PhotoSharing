using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharing.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseIfModelChanges<PhotoSharingContext>
    {
        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            List<Photo> photos = new List<Photo>()
            {
                    new Photo
                    {
                        Title = "This a god eletric car project!",
                        Description = "This car is one of the best projects of eletric cars.",
                        UserName = "Pedro",
                        PhotoFile = getFileBytes(@"\Images\mercedes.jpg"),
                        ImageMimeType = "image/jpeg",
                        CreatedDate = DateTime.Today.AddDays(-3)
                    },
                     new Photo
                    {
                        Title = "This a god gold car!",
                        Description = "This car very expensive.",
                        UserName = "Pedro",
                        PhotoFile = getFileBytes(@"\Images\gold_audi.jpg"),
                        ImageMimeType = "image/jpeg",
                        CreatedDate = DateTime.Today.AddDays(-2)
                    },
                      new Photo
                    {
                        Title = "This a wonderful car!",
                        Description = "This is very god.",
                        UserName = "Pedro",
                        PhotoFile = getFileBytes(@"\Images\lamborguini.jpg"),
                        ImageMimeType = "image/jpeg",
                        CreatedDate = DateTime.Today.AddDays(-1)
                    },
            };

            photos.ForEach(p => context.Photos.Add(p));
            context.SaveChanges();

            List<Comment> comments = new List<Comment>()
            {
                new Comment
                {
                    PhotoID = 1,
                    UserName = "Carlos",
                    Subject = "This is a future car!",
                    Body = "This a great car of the future, man it's cool!"
                },
                 new Comment
                {
                    PhotoID = 2,
                    UserName = "Fred",
                    Subject = "This a crazy car.",
                    Body = "Man I nedded this car, I like this!"
                },
                  new Comment
                {
                    PhotoID = 3,
                    UserName = "Carlos",
                    Subject = "Lamborguini!",
                    Body = "I love this car!"
                },
            };

            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();
        }

        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;

            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }

            return fileBytes;
        }
    }
}