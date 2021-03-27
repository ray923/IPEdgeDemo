using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RestaurantCollection.WebApi.DataAccess;
//using RestaurantCollection.WebApi.DTO.Common;
using RestaurantCollection.WebApi.DTO.Forms;
//using RestaurantCollection.WebApi.DTO.ViewModels;
using RestaurantCollection.WebApi.Models;
using RestaurantCollection.Service;

namespace RestaurantCollection.WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RestaurantController: ControllerBase
  {
    private readonly IRestaurantService _restaurantService;
    public RestaurantController(IRestaurantService restaurantService)
    {
      _restaurantService = restaurantService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurant()
    {
      //Get data from the service;
      var restaurantList = await _restaurantService.GetAllRestaurant();
      return Ok(restaurantList);
    }

    [HttpPost()]
    public async Task<IActionResult> UpdateRestaurant([FromBody] Restaurant restaurant)
    {
      //Update the restaurant service
      var result = await _restaurantService.UpdateRestaurant(restaurant);
      return Created("", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurantRate([FromBody] Restaurant rate, int id)
    {
      //Update the restaruant vote by id
      var result = await _restaurantService.UpdateRestaurant(rate);
      return Ok(result);
    }

    [HttpGet("query")]
    public async Task<IActionResult> GetRestaurantByCity([FromQuery] RestaurantQueryModel query)
    {
      var result = await _restaurantService.GetRestaurantByCity(query);
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurantById(int id)
    {
      var restaruant = new Restaurant()
      {
        Id = id
      };
      //delete the restaurant by id
      var result = await _restaurantService.DeleteRestaurantById(restaruant);
      return Ok(result);
    }

    [HttpGet("sort")]
    public async Task<IActionResult> GetRestaurantSortByRating()
    {
      //get restaurant sorted by rating
      var result = await _restaurantService.GetRestaurantSortByRating();
      return Ok(result);
    }
  }
}
