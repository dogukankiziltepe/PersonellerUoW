using System;
using System.ComponentModel.DataAnnotations;

namespace PersonellerUoW.Models.Entities
{
    public class Cografya
    {
        [Key]
        public int ID { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public int UstID { get; set; }
    }
}
