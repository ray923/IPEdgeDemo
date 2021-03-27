using System;
using System.Collections.Generic;
using InfoTrackSearch.Data.Models;
using InfoTrackSearch.Service.Services;
using Xunit;

namespace SearchServiceTests
{
    public class GoogleSearchServiceUnitTest
    {
        ISearchService _service;

        public GoogleSearchServiceUnitTest()
        {
            _service = new GoogleSearchService();
        }

        [Fact]
        public void SearhServiceGetResultTest()
        {
            //arrange
            var searchCondition = new SearchCondition()
            {
                SearchKeyword = "online title search",
                SearchUrl=""
            };
            //act
            var resultResponse = _service.GetSearchResults(searchCondition.SearchKeyword);
            //assert
            Assert.NotEmpty(resultResponse);
            Assert.IsType<List<SearchResult>>(resultResponse);
        }

        [Fact]
        public void SearhServiceAllHitResultTest()
        {
            //arrange
            var searchCondition = new SearchCondition()
            {
                SearchKeyword = "online title search",
                SearchUrl="a"
            };
            //act
            var resultResponse = _service.GetSearchResults(searchCondition.SearchKeyword);
            //assert
            Assert.NotEmpty(resultResponse);
            Assert.IsType<List<SearchResult>>(resultResponse);
            
            resultResponse.ForEach((item) => {
                Assert.IsType<SearchResult>(item);
            });

            //act
            var hitResponse = _service.GetHitResults(searchCondition.SearchUrl,resultResponse);
            //assert
            Assert.IsType<List<HitResult>>(hitResponse);
            
            hitResponse.ForEach((item) => {
                Assert.IsType<HitResult>(item);
                Assert.True(item.ResultOrder >0);
                Assert.NotNull(item.ResultPlace);
                Assert.IsType<string>(item.ResultPlace);
            });
        }

        [Fact]
        public void SearhServiceAllMissResultTest()
        {
            //arrange
            var searchCondition = new SearchCondition()
            {
                SearchKeyword = "online title search",
                SearchUrl="zzzzKKJJHSHSHSHS"
            };
            //act
            var resultResponse = _service.GetSearchResults(searchCondition.SearchKeyword);
            //assert
            Assert.NotEmpty(resultResponse);
            Assert.IsType<List<SearchResult>>(resultResponse);
            
            resultResponse.ForEach((item) => {
                Assert.IsType<SearchResult>(item);
            });

            //act
            var hitResponse = _service.GetHitResults(searchCondition.SearchUrl,resultResponse);
            //assert
            Assert.IsType<List<HitResult>>(hitResponse);
            
            hitResponse.ForEach((item) => {
                Assert.IsType<HitResult>(item);
                Assert.True(item.ResultOrder >0);
                Assert.Null(item.ResultPlace);
            });
        }

        [Fact]
        public void SearhServiceEmptyInputResultTest()
        {
            //arrange
            var searchCondition = new SearchCondition()
            {
                SearchKeyword = "online title search",
                SearchUrl=""
            };
            //act
            var resultResponse = _service.GetSearchResults(searchCondition.SearchKeyword);
            //assert
            Assert.NotEmpty(resultResponse);
            Assert.IsType<List<SearchResult>>(resultResponse);
            
            resultResponse.ForEach((item) => {
                Assert.IsType<SearchResult>(item);
            });

            //act
            var hitResponse = _service.GetHitResults(searchCondition.SearchUrl,resultResponse);
            //assert
            Assert.IsType<List<HitResult>>(hitResponse);
            Assert.Empty(hitResponse);
        }

        [Fact]
        public void SearhServicePartHitResultTest()
        {
            //arrange
            var searchCondition = new SearchCondition()
            {
                SearchKeyword = "online title search",
                SearchUrl="infotrack"
            };
            //act
            var resultResponse = _service.GetSearchResults(searchCondition.SearchKeyword);
            //assert
            Assert.NotEmpty(resultResponse);
            Assert.IsType<List<SearchResult>>(resultResponse);
            
            resultResponse.ForEach((item) => {
                Assert.IsType<SearchResult>(item);
            });

            //act
            var hitResponse = _service.GetHitResults(searchCondition.SearchUrl,resultResponse);
            //assert
            Assert.IsType<List<HitResult>>(hitResponse);
            
            var i = 0;
            hitResponse.ForEach((item) => {
                Assert.IsType<HitResult>(item);
                Assert.True(item.ResultOrder >0);
                if(item.ResultPlace != null){
                    i += 1;
                }
            });
            Assert.True(i>0);
        }
    }
}
