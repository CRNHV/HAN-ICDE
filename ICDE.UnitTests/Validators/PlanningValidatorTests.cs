using ICDE.Data.Entities;
using ICDE.Lib.Validation.Leeruitkomsten;

namespace ICDE.UnitTests.Validators;

public class PlanningValidatorTests
{
    [Fact]
    public void Validate_AllLeeruitkomstenCovered_ReturnsSuccess()
    {
        // Arrange
        var leeruitkomst1 = new Leeruitkomst { Naam = "Leeruitkomst1" };
        var leeruitkomst2 = new Leeruitkomst { Naam = "Leeruitkomst2" };

        var planning = new Planning
        {
            Name = "Valid Planning",
            PlanningItems = new List<PlanningItem>
            {
                new PlanningItem
                {
                    Index = 1,
                    Les = new Les { Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1, leeruitkomst2 } },
                },
                new PlanningItem
                {
                    Index = 2,
                    Opdracht = new Opdracht
                    {
                        BeoordelingCritereas = new List<BeoordelingCriterea>
                        {
                            new BeoordelingCriterea { Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1, leeruitkomst2 } }
                        }
                    }
                }
            }
        };

        var validator = new PlanningValidator(planning);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Validate_LearuitkomstNotCovered_ReturnsFailure()
    {
        // Arrange
        var leeruitkomst1 = new Leeruitkomst { Naam = "Leeruitkomst1" };
        var leeruitkomst2 = new Leeruitkomst { Naam = "Leeruitkomst2" };

        var planning = new Planning
        {
            Name = "Invalid Planning",
            PlanningItems = new List<PlanningItem>
            {
                new PlanningItem
                {
                    Index = 1,
                    Les = new Les { Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst1 } },
                },
                new PlanningItem
                {
                    Index = 2,
                    Opdracht = new Opdracht
                    {
                        BeoordelingCritereas = new List<BeoordelingCriterea>
                        {
                            new BeoordelingCriterea { Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst2 } }
                        }
                    }
                }
            }
        };

        var validator = new PlanningValidator(planning);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Planning: Invalid Planning toetst Leeruitkomst2 voordat de leeruitkomst behandeld wordt in een les.", result.Messages);
    }

    [Fact]
    public void Validate_EmptyPlanning_ReturnsSuccess()
    {
        // Arrange
        var planning = new Planning { Name = "Empty Planning", PlanningItems = new List<PlanningItem>() };
        var validator = new PlanningValidator(planning);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.True(result.Success);
        Assert.Contains("Planning: Empty Planning is succesvol gevalideerd.", result.Messages);
    }

    [Fact]
    public void Validate_NoLessonsWithLeeruitkomsten_ReturnsFailure()
    {
        // Arrange
        var leeruitkomst = new Leeruitkomst { Naam = "Leeruitkomst1" };

        var planning = new Planning
        {
            Name = "No Lesson Planning",
            PlanningItems = new List<PlanningItem>
            {
                new PlanningItem
                {
                    Index = 1,
                    Opdracht = new Opdracht
                    {
                        BeoordelingCritereas = new List<BeoordelingCriterea>
                        {
                            new BeoordelingCriterea { Leeruitkomsten = new List<Leeruitkomst> { leeruitkomst } }
                        }
                    }
                }
            }
        };

        var validator = new PlanningValidator(planning);

        // Act
        var result = validator.Validate();

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Planning: No Lesson Planning toetst Leeruitkomst1 voordat de leeruitkomst behandeld wordt in een les.", result.Messages);
    }
}
