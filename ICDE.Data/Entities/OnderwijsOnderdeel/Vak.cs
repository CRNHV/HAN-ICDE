﻿using System.ComponentModel.DataAnnotations;

namespace ICDE.Data.Entities.OnderwijsOnderdeel;
public class Vak : IOnderwijsOnderdeel
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public List<Leeruitkomst> Leeruitkomsten { get; set; }
    public List<Cursus> Cursussen { get; set; }
}