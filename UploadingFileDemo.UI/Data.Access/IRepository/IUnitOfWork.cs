using System;

namespace UploadingFileDemo.UI.Data.Access.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IUploadImageRepository UploadImage { get;}
        void Save();
    }
}
