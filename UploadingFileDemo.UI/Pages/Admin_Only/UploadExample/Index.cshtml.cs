using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadingFileDemo.UI.Data.Access.IRepository;
using UploadingFileDemo.UI.Models;

namespace UploadingFileDemo.UI.Pages.Admin_Only.UploadExample
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvirnment;
        private readonly IUnitOfWork _unitOfWork;
        //private const long ImageMaxSizeUpload = 1024;
        private const long ImageMaxSizeUpload = 100000;
        [BindProperty]
        public UploadImage UploadImage { get; set; }
        public IndexModel(IWebHostEnvironment hostingEnvironment, IUnitOfWork unitOfWork)
        {
            _hostingEnvirnment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet(int id)
        {
            Random ran = new();
            id = ran.Next(1, 5);
            UploadImage = _unitOfWork.UploadImage.Get(id);
            if (UploadImage != null)
            {
                return Page();
            }
            UploadImage = new();
            return Page();
        }
        [BindProperty]
        [Required(ErrorMessage = "Please select a file.")]
        [Display(Name = "Image")]
        public IFormFile Photo { get; set; }
        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvirnment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (ModelState.IsValid)
            {
                string newGuid = Guid.NewGuid().ToString();
                string upload = Path.Combine(webRootPath, @"images\iamtuse_upload");

                string extention = Path.GetExtension(files[0].FileName);
                long fileLength = files[0].Length;


                string fileName = files[0].FileName;
                string combineNew = fileName + "__" + newGuid + extention;

                if (extention == ".png" || extention == ".jpg" || extention == ".jpeg")
                {
                    //Process file
                    if (fileLength <= ImageMaxSizeUpload)
                    {
                        //Process file.
                        if (Photo != null)
                        {
                            UploadImage = new();
                            if (UploadImage.Image == null)
                            {
                                UploadImage.Image = ProcessUploadedFile();
                                _unitOfWork.UploadImage.Add(UploadImage);
                                _unitOfWork.Save();
                                TempData["success"] = "Upload successful!";
                                return RedirectToPage("/Admin_Only/UploadExample/Index");
                            }
                            else
                            {
                                string filePath = Path.Combine(_hostingEnvirnment.WebRootPath, "images", "iamtuse_upload", UploadImage.Image);
                                System.IO.File.Delete(filePath);
                            }

                            UploadImage.Image = ProcessUploadedFile();

                            _unitOfWork.UploadImage.Update(UploadImage);
                            TempData["update"] = "Image updated successful!";
                            return RedirectToPage("/Admin_Only/UploadExample/Index");

                        }

                    }
                    else
                    {
                        //Return error message
                        ModelState.AddModelError(string.Empty, "The file is to large.");
                        if (!ModelState.IsValid)
                        {
                            TempData["fileSizeLarge"] = "The file is too large. The size of the image should be not less than 1MB.";
                            UploadImage = new();
                            return Page();
                        }
                    }

                }
                else
                {
                    //Return error message
                    ModelState.AddModelError(string.Empty, "Invalid file extention.");
                    if (!ModelState.IsValid)
                    {
                        TempData["invalidFileExtention"] = "Only file with the following extentions are allowed (.jpg, .jpeg, or png).";
                        UploadImage = new();
                        return Page();
                    }
                }

            }

            return Page();
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {

                string uploadedFolder = Path.Combine(_hostingEnvirnment.WebRootPath, "images", "iamtuse_upload");
                uniqueFileName = Guid.NewGuid().ToString() + " _ " + Photo.FileName;
                string filePath = Path.Combine(uploadedFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }


}







