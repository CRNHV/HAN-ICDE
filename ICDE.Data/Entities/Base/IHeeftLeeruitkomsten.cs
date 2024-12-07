using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDE.Data.Entities.Base;
public interface IHeeftLeeruitkomsten
{
    public List<Leeruitkomst> Leeruitkomsten { get; set; }
}
