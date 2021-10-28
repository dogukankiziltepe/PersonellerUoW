using System;
using System.Collections.Generic;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Services
{
    public class PersonelRepository : Repository<Personel>, IPersonelRepository
    {
        public PersonelRepository(Context context):base(context)
        {
        }

        
    }
}
