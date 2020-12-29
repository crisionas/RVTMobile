using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RVTMobileAPP.Models.Registration;

namespace RVTMobileAPP.Interfaces
{
    public interface IUser
    {
        Task<RegistrationResponse> Registration(RegistrationMessage model);
    }
}
