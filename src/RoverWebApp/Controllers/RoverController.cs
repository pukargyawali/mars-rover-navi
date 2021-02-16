using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoverControlService;
using RoverFileProcessor;

namespace RoverWebApp.Controllers
{
    public class RoverController : Controller
    {
        private readonly IFileProcessingService _fileProcessingService;        

        public RoverController(IFileProcessingService fileProcessingService) =>
            (_fileProcessingService) = (fileProcessingService);

        // GET: /<controller>/
        public IActionResult RoverHome()
        {
            return View();
        }

        /// <summary>
        /// File sent from front end is received via this endpoint.
        /// File size limit is set to 2 MB
        /// </summary>
        /// <param name="battlePlans"></param>
        /// <returns></returns>
        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 2097152)]
        public async Task<JsonResult> UploadControlFile(IList<IFormFile> roverControlFile)
        {
            using (var stream = roverControlFile[0].OpenReadStream())
            {
                await _fileProcessingService.ReadFileAsync(stream);
                
            }
            var processedRoverData = _fileProcessingService.ProcessedRoverData;

            var roverControlService = new RoverService(processedRoverData);
            var rovers = roverControlService.rovers;

            return Json(rovers);
        }

    }
}
