using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharing.Models
{
    interface IPhotoSharingContext
    {
        //Propriedade é um tipo de método, já que os comportamentos são refletidos por métodos.
        IQueryable<Photo> Photos { get; }
        IQueryable<Comment> Comments { get; }
        int SaveChanges();
        T Add<T>(T entity) where T : class;
        Photo FindPhotoById(int ID);
        Comment FindCommentById(int ID);
        T Delete<T>(T entity) where T : class;
    }
}
