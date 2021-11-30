using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public interface ITokenRegisterService
    {
        Task<bool> IsValid(User user);
    }
}