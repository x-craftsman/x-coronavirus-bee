using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.FileManager;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Web;
using Craftsman.Waiter.Domain;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.Services;
using Craftsman.Waiter.WebUI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.Controllers
{
    [Route("api/custom-code")]
    public class CustomCodeController : Controller, IController
    {
        private readonly IFileManager _fileManager;

        public CustomCodeController(
            IFileManager fileManager
        )
        {
            _fileManager = fileManager;
        }

        #region
        // api/custom-code/upload
        [HttpPost("upload")]
        public IActionResult UploadCustomCode(IFormFile formFile)
        {
            var stream = new MemoryStream();
            formFile.CopyTo(stream);
            _fileManager.UploadFile(stream, "code-box", formFile.FileName);
            return Ok(new
            {
                size = formFile.Length,
                path = $"code-box/{formFile.FileName}",
                orgName = formFile.FileName
            });
        }
        #endregion
    }
}
