namespace MVC.Blog.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact :EntityBase
    {
        public int Id { get; set; }
        public string ContactContent { get; set; }
    }
}
