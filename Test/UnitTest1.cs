using BlazorApp1.Components.Account.Pages;
using BlazorApp1.Components.Account.Shared;
using BlazorApp1.Components.Account;
using Bunit;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using BlazorApp1.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class RegisterTests : TestContext
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IEmailSender<ApplicationUser>> _emailSenderMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _claimsFactoryMock;
    private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
    private readonly Mock<NavigationManager> _navigationManagerMock;
    private readonly IdentityRedirectManager _redirectManager;

    

    [Fact]
    public void RenderRegisterconfirmation_ShouldDisplay()
    {
        {
            var cut = RenderComponent<RegisterConfirmation>();

            var loginLink = cut.Find("a[href='/Account/ResendEmailConfirmation']");
            var confirmationMessage = cut.Find("h1");
            
        }
    }
    [Fact]
    public async Task Register_ShouldSucceed_WhenValidInput()
    {
        var cut = RenderComponent<Register>();

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);

        cut.Find("input[name='Email']").Change("test@example.com");
        cut.Find("input[name='Password']").Change("Password123!");
        cut.Find("input[name='ConfirmPassword']").Change("Password123!");
        await cut.Find("form").SubmitAsync();
    }

    [Fact]
    public async Task Register_ShouldFail_WhenInvalidInput()
    {
        var cut = RenderComponent<Register>();

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error message" }));

        cut.Find("input[name='Email']").Change("invalid-email");
        cut.Find("input[name='Password']").Change("short");
        cut.Find("input[name='ConfirmPassword']").Change("notmatching");
        await cut.Find("form").SubmitAsync();

        var errorMessage = cut.Find(".text-danger");
        Assert.Contains("Error message", errorMessage.TextContent);
    }

}

public class ResetPasswordConfirmationTests : TestContext
{
    [Fact]
    public void RenderResetPasswordConfirmation_ShouldDisplayConfirmationMessage()
    {
        var cut = RenderComponent<ResetPasswordConfirmation>();
        var confirmationMessage = cut.Find("h1");

        Assert.Equal("Reset password confirmation", confirmationMessage.TextContent);
    }

    [Fact]
    public void RenderResetPasswordConfirmation_ShouldHaveLoginLink()
    {
        var cut = RenderComponent<ResetPasswordConfirmation>();

        var loginLink = cut.Find("a[href='Account/Login']");
        Assert.NotNull(loginLink);
        Assert.Equal("click here to log in", loginLink.TextContent.Trim());
    }
}
