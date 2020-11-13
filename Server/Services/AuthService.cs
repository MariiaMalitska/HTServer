using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Server.Services
{
    public class AuthService
    {
        private readonly HoldTightContext _context;

        public AuthService(HoldTightContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByGoogleId(HttpContext httpContext)
        {
            var gId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.Include(x=> x.GoogleId).FirstOrDefaultAsync(u=> u.GoogleId.GoogleId==gId);

            if (user == null)
            {
                var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
                user = await CreateNewUser(gId, email);
            }

            return user;
        }

        public async Task<User> CreateNewUser(string gId, string email)
        {
            var user = new User() { GoogleId = new GoogleID() { GoogleId = gId, Email = email } };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
