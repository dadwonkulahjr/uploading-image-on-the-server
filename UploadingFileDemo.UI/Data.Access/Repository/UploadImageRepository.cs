using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using UploadingFileDemo.UI.Data.Access.IRepository;
using UploadingFileDemo.UI.Models;

namespace UploadingFileDemo.UI.Data.Access.Repository
{
    public class UploadImageRepository : DefaultRepository<UploadImage>, IUploadImageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UploadImageRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Update(UploadImage uploadImageToUpdate)
        {
            var findImageFromDb = _applicationDbContext.UploadImage.Find(uploadImageToUpdate.Id);
            if (findImageFromDb != null)
            {
                if (findImageFromDb.Image != null)
                {
                    findImageFromDb.Image = uploadImageToUpdate.Image;
                    _applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
