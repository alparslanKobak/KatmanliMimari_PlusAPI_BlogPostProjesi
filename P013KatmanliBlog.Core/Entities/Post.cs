using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Core.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name ="Gönderi"),DataType(DataType.MultilineText)]
        public string? PostMessage { get; set; }

        [Display(Name ="Gönderi Belgesi"),DataType(DataType.Upload)]
        public string? PostImage { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        
        [Display(Name = "Konu")]
        public int CategoryId { get; set; }

        [Display(Name = "Konu")]
        public Category? Category { get; set; }

        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }

        [Display(Name = "Kullanıcı")]

        public User User { get; set; }


    }
}
