namespace ICDE.Data.Entities;
internal interface IVersionable
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public bool RelationshipChanged { get; set; }
}
