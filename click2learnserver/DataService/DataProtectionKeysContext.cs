﻿using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class DataProtectionKeysContext: DbContext, IDataProtectionKeyContext
    {
        // Database To store all secure encrypted keys 
        public DataProtectionKeysContext(DbContextOptions<DataProtectionKeysContext> options): base(options)
        {
            
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
