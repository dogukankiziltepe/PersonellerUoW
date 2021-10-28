using System;
namespace PersonellerUoW.Services.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IPersonelRepository Personel { get; }
        IMediaRepository Media { get; }
        ICografyaRepository Cografya { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        int Complete();

    }
}
