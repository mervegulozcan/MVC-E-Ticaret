namespace proje_eticaret.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;

    public partial class Film
    {
        [Key]
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string MovieName { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        public DateTime RelaseDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Director { get; set; }

        [Required]
        public string Cast { get; set; }

        public decimal IMDB { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public string Base64String      //BURAYI BÝZ EKLEDÝK RESÝM ÝÇÝN
        {
            get
            {
                var base64Str = string.Empty;
                if (Picture != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        int offset = 78;
                        ms.Write(Picture, offset, Picture.Length - offset);
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
