using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MetadataExtractor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VITGImageUpload.Data;
using VITGImageUpload.Data.Models;
using VITGImageUpload.Helpers;

namespace VITGImageUpload.Services
{
  public class UploadToDBService: IUploadService
  {
    private readonly ImageUploadContext _dbcontext;
    public UploadToDBService(ImageUploadContext dbcontext)
    {
      _dbcontext = dbcontext;
    }
    public async Task<bool> Upload(ICollection<IFormFile> files)
    {
      bool isUploaded = false;
      foreach (var formFile in files)
      {
          if (StorageHelper.IsImage(formFile))
          {
              if (formFile.Length > 0)
              {
                  using (Stream stream = formFile.OpenReadStream())
                  {
                      
                      byte[] bytes = new byte[stream.Length];
                      stream.Read(bytes, 0, bytes.Length);
                      stream.Seek(0, SeekOrigin.Begin);
                      var metaData = StorageHelper.UpdateMetaData(stream);
                      var item = new ImageInfo(){
                        MetaData = metaData,
                        RawData = bytes,
                        UploadTime = DateTime.Now
                      };
                      _dbcontext.ImageInfos.Add(item);
                      _dbcontext.SaveChanges();
                      return !isUploaded;
                  }
              }
          }
          else
          {
              return isUploaded;
          }
      }
      return isUploaded;
    }

    public async Task<List<ImageInfo>> GetAllImages()
    {
      var result = await _dbcontext.ImageInfos.ToListAsync<ImageInfo>();
      return result;
    }
  }
}