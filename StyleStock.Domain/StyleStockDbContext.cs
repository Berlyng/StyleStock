using Microsoft.EntityFrameworkCore;
using StyleStock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Domain

{
    public class StyleStockDbContext : DbContext
    {
		public DbSet<Suppliers> Suppliers { get; set; }
		public StyleStockDbContext(DbContextOptions<StyleStockDbContext> options) : base(options)
        {

        }
    }
}
