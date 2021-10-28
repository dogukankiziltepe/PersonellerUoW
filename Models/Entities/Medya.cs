using System;
using System.ComponentModel.DataAnnotations;

namespace PersonellerUoW.Models.Entities
{
    public class Medya
    {
        [Key]
        public int MediaID { get; set; }
        public string MediaName { get; set; }
        public string MediaURL { get; set; }
    }
}
