using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _db = new RestaurantDbContext();

        //POST
        public async Task<IHttpActionResult> PostRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid && restaurant != null)
            {
                _db.Restaurants.Add(restaurant);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //GET ALL
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> allRestaurants = await _db.Restaurants.ToListAsync();
            return Ok(allRestaurants);
        }
        //GetBy ID
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Restaurant restaurant = await _db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
        //PUT (Update)
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody] Restaurant model)
        {
            
            if(ModelState.IsValid && !(model == null))
            {
                //This is our entity
                Restaurant restaurant = await _db.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    restaurant.Name = model.Name;
                    restaurant.Rating = model.Rating;
                    restaurant.Style = model.Style;
                    restaurant.DollarSigns = model.DollarSigns;

                    await _db.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);

        }
        //DeleteByID

        public async Task<IHttpActionResult> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _db.Restaurants.Remove(restaurant);

            if(await _db.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
