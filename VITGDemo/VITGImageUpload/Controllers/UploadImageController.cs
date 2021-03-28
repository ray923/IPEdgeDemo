using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MetadataExtractor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VITGImageUpload.Data;
using VITGImageUpload.Data.Models;
using VITGImageUpload.Helpers;
using VITGImageUpload.Services;

namespace VITGImageUpload.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class UploadImageController: ControllerBase
  {
    private readonly IUploadService _service;
    private readonly AzureStorageConfig _storageConfig;
    public UploadImageController(IUploadService service, IOptions<AzureStorageConfig> config)
    {
      _service = service;
      _storageConfig = config.Value;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(ICollection<IFormFile> files)
    {
        bool isUploaded = false;

        try
        {
            if (files.Count == 0)
                return BadRequest("No files received from the upload");

            if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

            if (_storageConfig.ImageContainer == string.Empty)
                return BadRequest("Please provide a name for your image container in the azure blob storage");

            isUploaded = await _service.Upload(files);

            if (isUploaded)
            {
                if (_storageConfig.ImageContainer != string.Empty)
                    return new AcceptedAtActionResult("GetThumbNails", "Images", null, null);
                else
                    return new AcceptedResult();
            }
            else
                return BadRequest("Look like the image couldnt upload to the storage");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET /api/images/thumbnails
    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        try
        {
            if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                return BadRequest("Sorry, can't retrieve your Azure storage details from appsettings.js, make sure that you add Azure storage details there.");

            if (_storageConfig.ImageContainer == string.Empty)
                return BadRequest("Please provide a name for your image container in Azure blob storage.");

            var result = await _service.GetAllImages();

            return new ObjectResult(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
  }
}
