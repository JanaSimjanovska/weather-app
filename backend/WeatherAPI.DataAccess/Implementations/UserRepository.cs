using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.DataAccess.Interfaces;
using WeatherAPI.Domain.Models;

namespace WeatherAPI.DataAccess.Implementations
{
    public class UserRepository : IUserRepository<User>
    {
        private WeatherAPIDbContext _context;
        public UserRepository(WeatherAPIDbContext weatherAPIDbContext) { 
            _context = weatherAPIDbContext;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users
                .FirstOrDefault(x => x.Id == id);
        }
       
        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users
                .FirstOrDefault(x => x.Username == username);
        }

        public User LoginUser(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }
}
