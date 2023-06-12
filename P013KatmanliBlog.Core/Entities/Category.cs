using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        [Display(Name ="Ad")]
        public string Name { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }

        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public List<Post>? Posts { get; set; }


    }
}
