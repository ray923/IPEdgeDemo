using System;
using Microsoft.EntityFrameworkCore;
using VITGImageUpload.Data.Models;

namespace VITGImageUpload.Data
{
  public class ImageUploadContext: DbContext
  {
    public ImageUploadContext(DbContextOptions<ImageUploadContext> options): base(options)
    {}

    public DbSet<ImageInfo> ImageInfos {get; set;}
  }
}