namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        [Key]
        public int BookID { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurame { get; set; }

        public string AuthorOtchestevo { get; set; }

        public string BookDescription { get; set; }

        public string BookGenre { get; set; }

        public int? BookAmount { get; set; }

        public double? BookPrice_Single { get; set; }

        public byte[] BookImage { get; set; }
    }
}
