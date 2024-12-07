﻿using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Opdracht : OnderwijsOnderdeel, IVersionable
{
    public OpdrachtType Type { get; set; }

    public ICollection<BeoordelingCriterea> BeoordelingCritereas { get; set; } = new List<BeoordelingCriterea>();
    public ICollection<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; } = new List<IngeleverdeOpdracht>();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }
}
