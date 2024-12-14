using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ICDE.Data.Entities.Identity;

namespace ICDE.Data.Entities;
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentNummer { get; set; }

    public User User { get; set; }
    public int? UserId { get; set; }

    public List<IngeleverdeOpdracht> IngeleverdeOpdrachten { get; set; } = new();

}
