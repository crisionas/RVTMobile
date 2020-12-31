using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RVTMobileAPP.Models.AuthUser;
using RVTMobileAPP.Models.Registration;
using RVTMobileAPP.Models.Vote;

namespace RVTMobileAPP.Interfaces
{
    public interface IUser
    {
        Task<RegistrationResponse> Registration(RegistrationMessage model);
        Task<AuthResponse> Login(AuthMessage model);
        Task<VoteResponse> Vote(VoteMessage model);
    }
}
