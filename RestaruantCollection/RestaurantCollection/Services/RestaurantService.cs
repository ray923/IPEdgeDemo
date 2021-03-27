using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantCollection.WebApi.DataAccess;
using RestaurantCollection.WebApi.Models;

namespace RestaurantCollection.Service
{
  public class RestaurantService: IRestaurantService
  {
      private IRepository _repository;
      public RestaurantService(IRepository repository)
      {
        _repository = repository;
      }
    public async Task<List<Restaurant>> GetAllRestaurant()
    {
      return await _repository.GetRestaurants();
    }
    public async Task<Restaurant> UpdateRestaurant(Restaurant restaurant)
    {
      return await _repository.UpdateRestaurant(restaurant);
    }
    public async Task<Restaurant> UpdateRestaurantRate(Restaurant restaurant)
    {
      return await _repository.UpdateRestaurant(restaurant);
    }
    public async Task<List<Restaurant>> GetRestaurantByCity(RestaurantQueryModel query)
    {
      return await _repository.GetRestaurants(query);
    }
    public async Task<Restaurant> DeleteRestaurantById(Restaurant restaurant)
    {
      return await _repository.DeleteRestaurant(restaurant);
    }
    public async Task<List<Restaurant>> GetRestaurantSortByRating()
    {
      return await _repository.GetRestaurantsSorted();
    }
    
  }
}