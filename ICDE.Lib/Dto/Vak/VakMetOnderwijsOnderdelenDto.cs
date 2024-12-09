using ICDE.Lib.Dto.Cursus;
using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Dto.Vak;
public class VakMetOnderwijsOnderdelenDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public Guid GroupId { get; set; }
    public int VersieNummer { get; set; }

    public List<LeeruitkomstDto> Leeruitkomsten { get; set; } = new();
    public List<CursusDto> Cursussen { get; set; } = new();

    public List<VakDto> EerdereVersies { get; set; } = new();
}
