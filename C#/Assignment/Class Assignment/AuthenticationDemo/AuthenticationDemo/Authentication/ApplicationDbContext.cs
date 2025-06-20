﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    
using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemo.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
    }
}
