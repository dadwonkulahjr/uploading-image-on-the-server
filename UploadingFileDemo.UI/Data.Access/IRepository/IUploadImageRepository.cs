using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using UploadingFileDemo.UI.Models;

namespace UploadingFileDemo.UI.Data.Access.IRepository
{
    public interface IUploadImageRepository : IDefaultRepository<UploadImage>
    {
        void Update(UploadImage uploadImageToUpdate);
    }
}
