using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PersonellerUoW.Models;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Services
{
    public class MediaRepository:Repository<Medya>, IMediaRepository
    {
        public MediaRepository(Context context) : base(context)
        {
        }

        public string SaveMediaToDisk(PersonelViewModel personelView)
        {
            try
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(personelView.Media.FileName);
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media", ImageName);
                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    personelView.Media.CopyTo(stream);
                }
                return ImageName;
            }
            catch (Exception ex)
            {
                return null;
            } 
        }
    }
}
