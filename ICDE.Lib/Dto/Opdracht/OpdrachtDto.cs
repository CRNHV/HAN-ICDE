namespace ICDE.Lib.Dto.Opdracht;
public class OpdrachtDto
{
    private bool isToets;

    public OpdrachtDto(bool isToets)
    {
        this.isToets = isToets;
    }

    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public string Type
    {
        get => isToets ? "Dit is een toets" : "Dit is een casus";
    }
}
