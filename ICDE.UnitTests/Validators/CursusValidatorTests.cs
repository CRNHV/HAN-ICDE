using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.UnitTests.Validators;
public class CursusValidatorTests
{
    [Fact]
    public void Validate_ReturnsError_WhenPlanningIsNull()
    {
        // Arrange
        var cursus = new Cursus
        {
            Naam = "Test Cursus",
            Planning = null,
            Leeruitkomsten = new List<Leeruitkomst>()
        };
        var validator = new CursusValidator(cursus);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Cursus: Test Cursus heeft geen planning.", result.Messages);
    }

    [Fact]
    public void Validate_ReturnsError_WhenLeeruitkomstenAreMissing()
    {
        // Arrange
        var leeruitkomst1 = new Leeruitkomst { Naam = "Leeruitkomst 1" };
        var leeruitkomst2 = new Leeruitkomst { Naam = "Leeruitkomst 2" };

        var cursus = new Cursus
        {
            Naam = "Test Cursus",
            Planning = new Planning
            {
                PlanningItems = new List<PlanningItem>
                    {
                        new PlanningItem
                        {
                            Les = new Les
                            {
                                Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1 }
                            }
                        }
                    }
            },
            Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst2 } // Missing leeruitkomst1
        };
        var validator = new CursusValidator(cursus);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Cursus: Test Cursus mist leeruitkomsten: Leeruitkomst 2", result.Messages);
    }
      
    [Fact]
    public void Validate_ReturnsSuccess_WhenAllLeeruitkomstenMatch()
    {
        // Arrange
        var leeruitkomst1 = new Leeruitkomst { Naam = "Leeruitkomst 1" };
        var leeruitkomst2 = new Leeruitkomst { Naam = "Leeruitkomst 2" };

        var cursus = new Cursus
        {
            Naam = "Test Cursus",
            Planning = new Planning
            {
                PlanningItems = new List<PlanningItem>
                    {
                        new PlanningItem
                        {
                            Les = new Les
                            {
                                Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1, leeruitkomst2 }
                            }
                        }
                    }
            },
            Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1, leeruitkomst2 }
        };
        var validator = new CursusValidator(cursus);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.True(result.Success);
        Assert.Contains("Cursus: Test Cursus is succesvol gevalideerd.", result.Messages);
    }
}