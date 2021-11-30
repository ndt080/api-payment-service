using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaymentService.Database.Context;
using PaymentService.Domain.IRepositories;
using PaymentService.Domain.Models;

namespace PaymentService.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;

        public UserRepository(UsersContext context) => _context = context;

        public IEnumerable<User> GetAll() => _context.Users;

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user ?? throw new KeyNotFoundException("User not found");
        }

        public User GetByToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            return user;
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        
        public void Add(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}