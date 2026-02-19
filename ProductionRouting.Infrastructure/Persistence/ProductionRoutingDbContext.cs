using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ProductionRouting.Domain.Entities;

namespace ProductionRouting.Infrastructure.Persistence;

public class ProductionRoutingDbContext : DbContext
{
    public ProductionRoutingDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Ruleset> Rulesets { get; set; }
    public DbSet<Rule> Rules { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<EvaluationLog> EvaluationLogs { get; set; }
}

