using System;
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

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(x => x.Subscriptions)
                .ToList();
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user ?? throw new KeyNotFoundException("User not found");
        }

        public User GetByToken(string token)
        {
            var user = _context.Users
                .Include(x => x.Subscriptions)
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            return user ?? throw new KeyNotFoundException("User not found");
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users
                .Include(x => x.Subscriptions)
                .SingleOrDefault(u => u.Email == email);
            return user ?? throw new KeyNotFoundException("User not found");
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User Add(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            var userEntity = _context.Add(user);
            _context.SaveChanges();

            return userEntity.Entity;
        }

        public void AddSubscriptions(int userId, SubscriptionInfo info)
        {
            var user = GetById(userId);
            _context.Entry(user).Collection(x => x.Subscriptions).Load();
            _context.Entry(user).Collection(x => x.Subscriptions).IsModified = true;

            user.Subscriptions ??= new List<SubscriptionInfo>();
            user.Subscriptions.Add(info);

            _context.Update(user);
            _context.SaveChanges();
        }

        public void DeleteSubscription(int userId, string apiKey)
        {
            var user = GetById(userId);
            _context.Entry(user).Collection(x => x.Subscriptions).Load();
            _context.Entry(user).Collection(x => x.Subscriptions).IsModified = true;

            var subToDelete = user.Subscriptions.Find(x => x.ApiKey == apiKey);
            user.Subscriptions.Remove(subToDelete);
            user.Subscriptions.RemoveAll(info => info.End < DateTime.Now);

            _context.SaveChanges();
        }
    }
}