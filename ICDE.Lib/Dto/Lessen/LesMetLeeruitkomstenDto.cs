using ICDE.Lib.Dto.Leeruitkomst;

namespace ICDE.Lib.Dto.Lessen;
public class LesMetLeeruitkomstenDto
{
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public List<LeeruitkomstDto> Leeruitkomsten { get; set; } = new();
}
