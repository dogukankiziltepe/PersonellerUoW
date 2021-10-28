using System;
using Microsoft.AspNetCore.Http;
using PersonellerUoW.Models.Entities;

namespace PersonellerUoW.Models
{
    public class PersonelViewModel
    {
        public int PersonelID { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Cografya City { get; set; }
        public Cografya Country { get; set; }
        public string MediaUrl { get; set; }
        public IFormFile Media { get; set; }
    }
}
