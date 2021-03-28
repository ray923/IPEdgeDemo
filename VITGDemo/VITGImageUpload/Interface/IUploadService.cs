using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VITGImageUpload.Data.Models;

namespace VITGImageUpload.Services
{
  public interface IUploadService
  {
    Task<bool> Upload(ICollection<IFormFile> files);
    Task<List<ImageInfo>> GetAllImages();
  }
}