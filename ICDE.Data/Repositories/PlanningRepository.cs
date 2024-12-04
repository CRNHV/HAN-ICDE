﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class PlanningRepository : RepositoryBase<Planning>, IPlanningRepository
{
    private readonly AppDbContext _context;

    public PlanningRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Planning> CreateCloneOf(int id)
    {
        var planning = await _context.Plannings
            .Include(x => x.PlanningItems)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var newPlanning = (Planning)planning.Clone();
        await _context.Plannings.AddAsync(newPlanning);
        await _context.SaveChangesAsync();
        return newPlanning;
    }

    public override async Task<Planning?> Get(int id)
    {
        return await _context.Plannings
            .Include(x => x.PlanningItems)
            .ThenInclude(x => x.Les)
            .Include(x => x.PlanningItems)
            .ThenInclude(x => x.Opdracht)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}