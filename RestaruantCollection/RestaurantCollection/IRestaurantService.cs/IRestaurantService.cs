using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantCollection.WebApi.Models;

namespace RestaurantCollection.Service
{
  public interface IRestaurantService
  {
    Task<List<Restaurant>> GetAllRestaurant();
    Task<Restaurant> UpdateRestaurant(Restaurant restaurant);
    Task<Restaurant> UpdateRestaurantRate(Restaurant restaurant);
    Task<List<Restaurant>> GetRestaurantByCity(RestaurantQueryModel query);
    Task<Restaurant> DeleteRestaurantById(Restaurant restaurant);
    Task<List<Restaurant>> GetRestaurantSortByRating();
    
  }
}