using System;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Services
{
    public class CografyaRepository:Repository<Cografya>, ICografyaRepository
    {
        public CografyaRepository(Context context) : base(context)
        {
        }
    }
}
