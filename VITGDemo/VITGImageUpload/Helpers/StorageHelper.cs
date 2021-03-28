using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using MetadataExtractor;
using System.Text;

namespace VITGImageUpload.Helpers
{
    public static class StorageHelper
    {

        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static string UpdateMetaData(Stream stream)
        {
        var Info = ImageMetadataReader.ReadMetadata(stream);
                    var metaData = new StringBuilder();
                    foreach (var meta in Info)
                    foreach (var tag in meta.Tags)
                        metaData.Append(tag.Name + " - " + tag.Name +" = " + tag.Description + "/n");
        return metaData.ToString();
        }
    }
}
