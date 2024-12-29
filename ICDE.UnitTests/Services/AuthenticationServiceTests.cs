using ICDE.Data.Entities.Identity;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Services;
using ICDE.UnitTests.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ICDE.UnitTests.Services;

public class AuthenticationServiceTests
{
    private MockRepository mockRepository;
    private Mock<FakeSignInManager> mockSignInManager;
    private Mock<IUserRepository> mockUserRepository;

    public AuthenticationServiceTests()
    {
        this.mockRepository = new MockRepository(MockBehavior.Strict);
        this.mockSignInManager = this.mockRepository.Create<FakeSignInManager>();
        this.mockUserRepository = this.mockRepository.Create<IUserRepository>();

        var resz = mockSignInManager.Object;
    }

    private AuthenticationService CreateService()
    {
        return new AuthenticationService(
            this.mockSignInManager.Object,
            this.mockUserRepository.Object);
    }

    [Fact]
    public async Task Login_CorrectCredentials_ShouldSucceed()
    {
        // Arrange
        var service = this.CreateService();
        string username = "root";
        string password = "root";

        var user = new User();

        this.mockUserRepository
           .Setup(x => x.GetByName(It.Is<string>(x => x == username)))
           .ReturnsAsync(user);

        this.mockSignInManager
            .Setup(x => x.PasswordSignInAsync(
                It.Is<User>(x => x == user),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>())
            )
            .ReturnsAsync(SignInResult.Success);

        // Act
        var result = await service.Login(
            username,
            password);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task LogoutUser_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();

        // Act
        await service.LogoutUser();

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Register_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = this.CreateService();
        string username = null;
        string password = null;
        string rol = null;

        var user = new User();

        this.mockUserRepository
            .Setup(x => x.CreateUser(It.Is<string>(x => x == username), It.Is<string>(x => x == password)))
            .ReturnsAsync(user);

        this.mockUserRepository
           .Setup(x => x.GetByName(It.Is<string>(x => x == username)))
           .ReturnsAsync(user);

        this.mockUserRepository
            .Setup(x => x.AddUserClaim(It.Is<User>(x => x == user), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        this.mockUserRepository
            .Setup(x => x.AddUserRole(It.Is<User>(x => x == user), It.Is<string>(x => x == rol)))
            .ReturnsAsync(true);

        // Act
        var result = await service.Register(
            username,
            password,
            rol);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
