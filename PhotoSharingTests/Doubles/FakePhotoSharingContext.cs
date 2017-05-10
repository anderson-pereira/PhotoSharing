using PhotoSharing.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharingTests.Doubles
{
    public class FakePhotoSharingContext : IPhotoSharingContext
    {
        SetMap map = new SetMap();

        public IQueryable<Comment> Comments
        {
            get
            {
                return map.Get<Comment>().AsQueryable();
            }
            set
            {
                map.Use<Comment>(value);
            }
        }

        public IQueryable<Photo> Photos
        {
            get
            {
                return map.Get<Photo>().AsQueryable();
            }
            set
            {
                map.Use(value); //Difere o tipo pelo parâmetro.
            }
        }

        public bool ChangesSaved { get; set; }

        public T Add<T>(T entity) where T : class
        {
            map.Get<T>().Add(entity);

            return entity;
        }

        public T Delete<T>(T entity) where T : class
        {
            map.Get<T>().Remove(entity);

            return entity;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Comment FindCommentById(int ID)
        {   
            //Como não possui o find do context deve ser feito do zero.s
            Comment comment = (from c in this.Comments where c.CommentID == ID select c).First();

            return comment;
        }

        public Photo FindPhotoById(int ID)
        {
            Photo photo = (from p in this.Photos where p.PhotoID == ID select p).First();

            return photo;
        }

        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        //Classe auxiliar 
        class SetMap :KeyedCollection<Type, Object>
        {
            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);

                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }

                Add(set);

                return set;
            }

            //Enumarador
            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            //Saber o tipo que irá retornar.
            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}
