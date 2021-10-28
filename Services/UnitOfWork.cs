using System;
using Microsoft.EntityFrameworkCore.Storage;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Services
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly Context context;
        private IDbContextTransaction transaction ;
        public UnitOfWork(Context _context)
        {
            context = _context;
            context.ChangeTracker.LazyLoadingEnabled = false;
            Personel = new PersonelRepository(context);
            Media = new MediaRepository(context);
            Cografya = new CografyaRepository(context);
        }
        public IPersonelRepository Personel { get; private set; }
        public IMediaRepository Media { get; private set; }
        public ICografyaRepository Cografya { get; private set; }

        public void Commit()
        {
            transaction.Commit();
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void CreateTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
