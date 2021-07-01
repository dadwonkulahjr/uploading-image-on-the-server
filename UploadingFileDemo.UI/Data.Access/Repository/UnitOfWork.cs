using UploadingFileDemo.UI.Data.Access.IRepository;

namespace UploadingFileDemo.UI.Data.Access.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            UploadImage = new UploadImageRepository(applicationDbContext);
        }
        public IUploadImageRepository UploadImage { get; private set; }
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
