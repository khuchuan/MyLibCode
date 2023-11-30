using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.SalesModels;

#nullable disable

namespace DAL.Contexts
{
    public partial class SalesContext : TemplateDbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

    }
}
