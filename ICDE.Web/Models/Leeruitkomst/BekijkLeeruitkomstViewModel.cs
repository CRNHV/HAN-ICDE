using System;
using System.Collections.Generic;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Web.Models.Leeruitkomst;

public class BekijkLeeruitkomstViewModel
{
    public LeeruitkomstDto Recent { get; set; }
    public List<Guid> EarlierVersions { get; set; }
}
