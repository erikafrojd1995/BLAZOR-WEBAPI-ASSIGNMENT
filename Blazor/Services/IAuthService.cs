using Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Services
{
    public interface IAuthService
    {
        public Task<LoginResponseModel> LoginAsync(LoginModel model);
        public Task<bool> RegisterModel(RegisterModel model);
        
    }
}
