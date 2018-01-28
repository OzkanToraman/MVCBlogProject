namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post :EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string PostBody { get; set; }

        public DateTime PostDate { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(200)]
        public string Summary { get; set; }

        public bool IsDeleted { get; set; }

        public int LikeCount { get; set; }

        [StringLength(25)]
        public string Tags { get; set; }

        public int? StatusId { get; set; }

        public string PostPic { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Status Status { get; set; }
    }
}
