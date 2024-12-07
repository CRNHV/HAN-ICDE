namespace ICDE.Lib.Dto.Cursus;
public class CursusMetEerdereVersiesDto
{

    CursusMetPlanningDto Cursus { get; set; }
    public List<CursusDto> EerdereVersies { get; set; } = new();
}
