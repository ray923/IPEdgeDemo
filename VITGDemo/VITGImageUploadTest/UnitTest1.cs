using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using VITGImageUpload.Services;
using VITGImageUpload.Data;
using VITGImageUpload.Data.Models;

namespace VITGImageUploadTest
{
    public class UnitTest1
    {
        private IUploadService _service;
        private ImageUploadContext _config;
        public UnitTest1()
        {
            _config = new ImageUploadContext(new DbContextOptions<ImageUploadContext>());
            _service = new UploadToDBService(_config);
        }
        [Fact]
        public void TestCase1GetAllImages()
        {
            //arrange
            //act
            var result = _service.GetAllImages();
            //assert
            Assert.IsType<Task<List<ImageInfo>>>(result);
        }
    }
}
