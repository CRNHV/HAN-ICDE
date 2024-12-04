using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Data.Entities;

namespace ICDE.Data.Repositories.Interfaces;
public interface IPlanningRepository : IRepositoryBase<Planning>
{
    public Task<Planning> CreateCloneOf(int id);
}
