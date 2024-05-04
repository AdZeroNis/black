using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shared.Models.File;
using System.IO;

namespace IbulakStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        //  2024/4/27/about-mission.jpg

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] FileDto model)
        {
            try
            {
                var file = model.File;
                var year = DateTime.Now.Year;
                var yearFolder = Path.Combine(_environment.WebRootPath, year.ToString());
                if (!Path.Exists(yearFolder))
                {
                    Directory.CreateDirectory(yearFolder);
                }
                var month = DateTime.Now.Month;
                var monthFolder = Path.Combine(yearFolder, month.ToString());
                if (!Path.Exists(monthFolder))
                {
                    Directory.CreateDirectory(monthFolder);
                }
                var day = DateTime.Now.Day;
                var dayFolder = Path.Combine(monthFolder, day.ToString());
                if (!Path.Exists(dayFolder))
                {
                    Directory.CreateDirectory(dayFolder);
                }

                if (file?.Length > 0)
                {
                    var imageName = file.FileName;
                    var extension = Path.GetExtension(imageName);
                    

                    var fullPath = Path.Combine(dayFolder, imageName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok($"{year}/{month}/{day}/{imageName}");
                }
                else
                {
                    
                    return BadRequest("فایل وجود ندارد");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
