﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class AccountContext:IdentityDbContext<User>
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
