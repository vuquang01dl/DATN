using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Net;
using System.Numerics;
using TT1.AppDbContexts;
using TT1.Entities;
using TT1.Payloads.DataResponses;

namespace TT1.Payloads.Converters
{
    public class UserConverter
    {
        public readonly AppDbContext _context;
        public UserConverter()
        {
            _context = new AppDbContext();
        }
        public DataResponseUser EntityToDTO(User user)
        {
            return new DataResponseUser
            {
                avata = _context.Accounts.FirstOrDefault(a => a.Id == user.accountId).avata,
                user_name = user.user_name,
                phone = user.phone,
                email = user.email,
                address = user.address,
                status = _context.Accounts.Include(x => x.users).SingleOrDefault(x => x.users.Any(y => y.accountId == user.accountId)).status
        };
        }
    }
}
