using System;

namespace PaymentService.Domain.AuthUtils
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}