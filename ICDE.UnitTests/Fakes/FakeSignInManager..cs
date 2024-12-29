using ICDE.Data.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ICDE.UnitTests.Fakes;
public class FakeUserManager : UserManager<User>
{
    public FakeUserManager()
        : base(new Mock<IUserStore<User>>().Object,
          new Mock<IOptions<IdentityOptions>>().Object,
          new Mock<IPasswordHasher<User>>().Object,
          new IUserValidator<User>[0],
          new IPasswordValidator<User>[0],
          new Mock<ILookupNormalizer>().Object,
          new Mock<IdentityErrorDescriber>().Object,
          new Mock<IServiceProvider>().Object,
          new Mock<ILogger<UserManager<User>>>(MockBehavior.Loose).Object)
    { }
}

public class FakeSignInManager : SignInManager<User>
{
    public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object,
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<User>>>(MockBehavior.Loose).Object,
                 new Mock<IAuthenticationSchemeProvider>().Object)
    { }
}