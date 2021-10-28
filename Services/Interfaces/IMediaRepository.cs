using System;
using Microsoft.AspNetCore.Http;
using PersonellerUoW.Models;
using PersonellerUoW.Models.Entities;

namespace PersonellerUoW.Services.Interfaces
{
    public interface IMediaRepository:IRepository<Medya>
    {
        public string SaveMediaToDisk(PersonelViewModel personelView);
    }
}
