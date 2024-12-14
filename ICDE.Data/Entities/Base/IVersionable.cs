namespace ICDE.Data.Entities.Base;
public interface IVersionable
{
    public int Id { get; set; }
    public int VersieNummer { get; set; }
    public Guid GroupId { get; set; }
    public bool RelationshipChanged { get; set; }
}
