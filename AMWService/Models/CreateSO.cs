using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class CreateSO
    {
        [Key]
        public int ID { get; set; }
        //[Column(TypeName = "nvarchar(100)")]
        public string uuid { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string SO { get; set; }
        public int CustommerID { get; set; }
        //public string Custommer { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public int TypeID { get; set; }
        public int RootcauseID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Problem { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Image { get; set; }
        public int UserID { get; set; }
        public DateTime CreateTime { get; set; }
        //public DateTime ModifyTime { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please choose Photo image")]
        //[Display(Name = "Profile Picture")]S
        public IFormFile ImageFile { get; set;}
        [NotMapped]
        public string ImageSrc { get; set; }
        //public int CreateBy { get; set; }

        //[Required]
        //public DateTime CreateTime { get; set; }
    }
}
