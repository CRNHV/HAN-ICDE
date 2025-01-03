﻿using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Base;

namespace ICDE.Data.Entities;

public class Vak : OnderwijsOnderdeel, IVersionable, ICloneable
{
    public List<Leeruitkomst> Leeruitkomsten { get; set; } = new();
    public List<Cursus> Cursussen { get; set; } = new();

    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }

    [NotMapped]
    public bool RelationshipChanged { get; set; }

    public object Clone()
    {
        return new Vak()
        {
            Cursussen = Cursussen,
            Naam = Naam,
            Beschrijving = Beschrijving,
            Leeruitkomsten = Leeruitkomsten,
        };
    }
}
