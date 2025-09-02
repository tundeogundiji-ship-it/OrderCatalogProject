using ProductCatalog.Dormain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Contracts.Authentication
{
    public interface ITokenProvider
    {
        string GenerateJwtToken(User user);
    }
}
