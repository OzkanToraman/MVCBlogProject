namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message :EntityBase
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string MessageBody { get; set; }

        public DateTime MessageDate { get; set; }

        public int? UserId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRead { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string MessageFrom { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
