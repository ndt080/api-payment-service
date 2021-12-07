using System;
using System.Diagnostics;
using NUnit.Framework;
using PaymentService.Domain.Services;

namespace PaymentService.Test
{
    public class GenerateTest
    {
        [Test]
        public void TestGenerate()
        {
            var s = new SubscribeRegisterService();
            var f = s.GenerateApiKey();
            Console.WriteLine(f.Result);
        }
    }
}