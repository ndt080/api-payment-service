using System.Collections.Generic;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByToken(string token);
        User GetByEmail(string name);

        void Update(User user);
        User Add(User user);
        void AddSubscriptions(int userId, SubscriptionInfo info);

        void DeleteSubscription(int userId, string apiKey);
    }
}