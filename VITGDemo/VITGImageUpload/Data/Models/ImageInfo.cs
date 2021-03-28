using System;

namespace VITGImageUpload.Data.Models
{
    public class ImageInfo
    {
      public int Id {get;set;}

      public string MetaData {get; set;}
      public DateTime UploadTime {get; set;}

      public Byte[] RawData {get; set;}
      public string Url{get;set;}
    }
}