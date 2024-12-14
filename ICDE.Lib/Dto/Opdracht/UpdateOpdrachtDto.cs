namespace ICDE.Lib.Dto.Opdracht;
public class UpdateOpdrachtDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public bool IsToets { get; set; }
    public Guid GroupId { get; set; }
}
