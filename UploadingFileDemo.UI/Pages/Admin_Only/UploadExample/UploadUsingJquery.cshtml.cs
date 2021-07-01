using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadingFileDemo.UI.Data.Access.IRepository;
using UploadingFileDemo.UI.Models;

namespace UploadingFileDemo.UI.Pages.Admin_Only.UploadExample
{
    public class UploadUsingJqueryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvirnment;
        private Random _random = new();

        public UploadImage UploadImage { get; set; } = new();
        public UploadUsingJqueryModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvirnment = hostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                id = _random.Next(1, 5);

                if (id.HasValue)
                {
                    UploadImage = _unitOfWork.UploadImage.Get(id.Value);
                    if (UploadImage != null)
                    {
                        return Page();
                    }
                    UploadImage = new();
                }
            }


            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvirnment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (ModelState.IsValid)
            {
                if (UploadImage.Id == 0)
                {
                    string newGuid = Guid.NewGuid().ToString();
                    string upload = Path.Combine(webRootPath, @"images\iamtuse_upload");
                    string extention = Path.GetExtension(files[0].FileName);
                    string fileName = files[0].FileName;
                    string combine = fileName + "__" + newGuid + extention;

                    using (var fileStream = new FileStream(Path.Combine(upload, combine), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    UploadImage.Image = ProcessUploadedFile();
                    _unitOfWork.UploadImage.Add(UploadImage);
                    _unitOfWork.Save();
                    return RedirectToPage("./Admin_Only/UploadExample/Index");
                }
                else
                {

                }
                return Page();
            }
           


            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            var file = HttpContext.Request.Form.Files;
            if (file != null)
            {
                string uploadedFolder = Path.Combine(_hostingEnvirnment.WebRootPath, "images", "iamtuse_upload");
                uniqueFileName = Guid.NewGuid().ToString() + " _ " + file[0].FileName;
                string filePath = Path.Combine(uploadedFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}
