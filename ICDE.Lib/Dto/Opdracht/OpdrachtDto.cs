namespace ICDE.Lib.Dto.Opdracht;
public class OpdrachtDto
{
    private bool isToets;

    public OpdrachtDto(bool isToets)
    {
        this.isToets = isToets;
    }

    public Guid GroupId { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public int VersieNummer { get; set; }
    public DateTime Deadline { get; set; }
    public string Type
    {
        get => isToets ? "Dit is een toets" : "Dit is een casus";
    }
}
