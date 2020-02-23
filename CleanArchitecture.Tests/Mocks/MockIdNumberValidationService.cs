using CleanArchitecture.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Tests.Mocks
{
    public class MockIdNumberValidationService : IIdNumberValidationService
    {
        public async Task<bool> IsIdNumberValid(string idNumber)
        {
            throw new NotImplementedException();
        }
    }
}
