using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using MetadataExtractor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VITGImageUpload.Data;
using VITGImageUpload.Data.Models;
using VITGImageUpload.Helpers;

namespace VITGImageUpload.Services
{
  public class UploadToBlobService: IUploadService
  {
    private readonly ImageUploadContext _dbcontext;
    private readonly AzureStorageConfig _storageConfig;
    public UploadToBlobService(ImageUploadContext dbcontext, IOptions<AzureStorageConfig> config)
    {
      _dbcontext = dbcontext;
      _storageConfig = config.Value;
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
                      var metaData = StorageHelper.UpdateMetaData(stream);
                      //TO-do the metaData and pic url's relationship and save it into DB
                      isUploaded = await UploadFileToStorage(stream, formFile.FileName, _storageConfig);
                  }
              }
              return isUploaded;
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
      List<string> thumbnailUrls = await GetThumbNailUrls(_storageConfig);
      var result = new List<ImageInfo>();
      thumbnailUrls.ForEach((item) => {
        var element = new ImageInfo(){
          Url = item
        };
        result.Add(element);
      });
      return result;
    }

    private static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName,
                                                            AzureStorageConfig _storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  _storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  _storageConfig.ImageContainer +
                                  "/" + fileName);
            Uri sas = new Uri("https://talentblobstore.blob.core.windows.net/talent2014?sp=racwdl&st=2021-03-21T14:40:12Z&se=2021-03-28T22:40:12Z&spr=https&sv=2020-02-10&sr=c&sig=TyiXfN3Bf9eUDQryhc0nvdiyhOrB3xa5PSRQwk2mGfg%3D");

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            //StorageSharedKeyCredential storageCredentials =
                //new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create the blob client.
            BlockBlobClient blobClient = new BlockBlobClient(sas);

            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return await Task.FromResult(true);
        }

        private static async Task<List<string>> GetThumbNailUrls(AzureStorageConfig _storageConfig)
        {
            List<string> thumbnailUrls = new List<string>();

            // Create a URI to the storage account
            Uri accountUri = new Uri("https://" + _storageConfig.AccountName + ".blob.core.windows.net/");

            // Create BlobServiceClient from the account URI
            BlobServiceClient blobServiceClient = new BlobServiceClient(accountUri);

            // Get reference to the container
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(_storageConfig.ImageContainer);

            if (container.Exists())
            {
                foreach (BlobItem blobItem in container.GetBlobs())
                {
                    thumbnailUrls.Add(container.Uri + "/" + blobItem.Name);
                }
            }

            return await Task.FromResult(thumbnailUrls);
        }
  }
}