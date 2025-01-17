using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICDE.Data.Entities;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.UnitTests.Validators;
public class VakValidatorTests
{
    [Fact]
    public void Validate_ShouldPass_WhenAllLeeruitkomstenMatch()
    {
        // Arrange
        var leeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" }, new Leeruitkomst { Naam = "LO2" } };
        var cursussen = new List<Cursus>
            {
                new Cursus { Leeruitkomsten = leeruitkomsten },
            };

        var vak = new Vak { Naam = "TestVak", Leeruitkomsten = leeruitkomsten, Cursussen = cursussen };
        var validator = new VakValidator(vak);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.True(result.Success);
        Assert.Contains("has passed the validation", result.Messages.Last());
    }

    [Fact]
    public void Validate_ShouldFail_WhenExtraLeeruitkomstenExist()
    {
        // Arrange
        var vakLeeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" } };
        var cursussen = new List<Cursus>
            {
                new Cursus { Leeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" }, new Leeruitkomst { Naam = "LO2" } } },
            };

        var vak = new Vak { Naam = "TestVak", Leeruitkomsten = vakLeeruitkomsten, Cursussen = cursussen };
        var validator = new VakValidator(vak);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("extra leeruitkomsten", result.Messages.Last());
    }

    [Fact]
    public void Validate_ShouldFail_WhenMissingLeeruitkomstenExist()
    {
        // Arrange
        var vakLeeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" }, new Leeruitkomst { Naam = "LO2" } };
        var cursussen = new List<Cursus>
            {
                new Cursus { Leeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" } } },
            };

        var vak = new Vak { Naam = "TestVak", Leeruitkomsten = vakLeeruitkomsten, Cursussen = cursussen };
        var validator = new VakValidator(vak);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Validate_ShouldHandleEmptyCursussen()
    {
        // Arrange
        var leeruitkomsten = new List<Leeruitkomst> { new Leeruitkomst { Naam = "LO1" } };

        var vak = new Vak { Naam = "TestVak", Leeruitkomsten = leeruitkomsten, Cursussen = new List<Cursus>() };
        var validator = new VakValidator(vak);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("niet alle leeruitkomsten dekken", result.Messages.Last());
    }

}
