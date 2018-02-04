namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments :EntityBase
    {
        public int Id { get; set; }

        [Required]
        public string CommentBody { get; set; }

        public DateTime CommentDate { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public bool IsDeleted { get; set; }

        public int LikeCount { get; set; }

        public virtual Post Post { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
