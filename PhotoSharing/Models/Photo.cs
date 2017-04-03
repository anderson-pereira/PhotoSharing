using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharing.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("Picture")]
        [MaxLength]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YY}", ApplyFormatInEditMode = true)]
        public string Description { get; set; }

        public int UserName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}