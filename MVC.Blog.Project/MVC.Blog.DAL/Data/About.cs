namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About : EntityBase
    {
        [Key]
        public string AboutContent { get; set; }
    }
}
