namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MediaUpload")]
    public partial class MediaUpload :EntityBase
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public bool SliderImage { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
