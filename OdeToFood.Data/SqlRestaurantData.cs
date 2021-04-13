using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
                db.Restaurants.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            //var query = db.Restaurants.Where(r => r.Name.StartsWith(name)
            //    || string.IsNullOrEmpty(name)).OrderBy(r => r.Name).ToList();

            var query = from r in db.Restaurants
                       where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                       orderby r.Name
                       select r;

            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            // Attach - track changes of an existing object
            var entity = db.Restaurants.Attach(updatedRestaurant);
            // Tells EF this object has been modified
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}
