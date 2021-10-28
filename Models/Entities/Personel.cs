using System;
using System.ComponentModel.DataAnnotations;

namespace PersonellerUoW.Models.Entities
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }
        [StringLength(40)]
        [Required]
        public string Name { get; set; }
        [StringLength(20)]
        [Required]
        public string Surname { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Cografya City { get; set; }
        [Required]
        public virtual Cografya Country { get; set; }
        public virtual Medya Media { get; set; }
    }
}
