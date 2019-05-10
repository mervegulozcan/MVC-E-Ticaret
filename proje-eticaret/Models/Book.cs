namespace proje_eticaret.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;

    public partial class Book
    {
        [Key]
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string BookName { get; set; }

        [Required]
        [StringLength(30)]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }

        public DateTime RelaseDate { get; set; }

        [Required]
        [StringLength(30)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] picture { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public string Base64String      //BURAYI BÝZ EKLEDÝK RESÝM ÝÇÝN
        {
            get
            {
                var base64Str = string.Empty;
                if (picture != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        int offset = 78;
                        ms.Write(picture, offset, picture.Length - offset);
                        var bmp = new System.Drawing.Bitmap(ms);
                        using (var jpegms = new MemoryStream())
                        {
                            bmp.Save(jpegms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            base64Str = Convert.ToBase64String(jpegms.ToArray());
                        }
                    }
                }
                return base64Str;
            }
        }
    }
}
