using System;
using System.Collections.Generic;
using System.Linq;
using PolyclinicService.Database.Context;
using PolyclinicService.Domain.Models.Api;
using PolyclinicService.Domain.Repository;

namespace PolyclinicService.Database.Repository
{
    public class ApiRepository : IApiRepository
    {
        private readonly VisitContext _context;

        public ApiRepository(VisitContext context)
        {
            _context = context;
        }

        public List<ApiKey> GetKeys()
        {
            return _context.ApiKeys.ToList();
        }
        public void AddKey(ApiKey key)
        {
            _context.ApiKeys.Add(key);
            _context.SaveChanges();
        }

        public void RemoveKey(string key)
        {
            var value = _context.ApiKeys.First(apiKey => apiKey.Key == key);
            _context.ApiKeys.Remove(value);
            _context.SaveChanges();
        }

        public void RemoveKeyById(int id)
        {
            var key = _context.ApiKeys.Find(id);
            _context.ApiKeys.Remove(key);
            _context.SaveChanges();
        }

        public bool CheckAccess(string key)
        {
            var value = _context.ApiKeys.First(apiKey => apiKey.Key == key);
            if (value is null) return false;
            if (CheckValid(value)) return true;

            _context.ApiKeys.Remove(value);
            _context.SaveChanges();
            return false;
        }

        public bool CheckValid(ApiKey key)
        {
            return key.SubscriptionEnd > DateTime.Now;
        }
    }
}