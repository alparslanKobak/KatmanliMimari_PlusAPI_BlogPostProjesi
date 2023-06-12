using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace P013KatmanliBlog.Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string? Surname { get; set; }

        [Display(Name = "Email"), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Profil Resmi")]
        public string? ProfilePicture { get; set; }

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Adminlik Yetkisi")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Oluşturulma Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public Guid? UserGuid { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
