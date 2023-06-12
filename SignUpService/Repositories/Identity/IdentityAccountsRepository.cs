using Database.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace SignUpService.Repositories.Identity;

public class IdentityAccountRepository : IAccountRepository
{
    private readonly UserManager<Account> userManager;
    private readonly SignInManager<Account> signInManager;
    
    public IdentityAccountRepository(
        UserManager<Account> userManager,
        SignInManager<Account> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task CreateAccountAsync(
        string email, 
        string password)
    {
        Account? account = await userManager.FindByEmailAsync(email);
        if (account != null)
        {
            throw new ValidationException("User with specified email already exists.");
        }

        account = new Account
        {
            Email = email,
            UserName = email
        };

        ValidationException? validationException = await ValidatePasswordAsync(account, password);
        if (validationException != null)
        {
            throw validationException;
        }

        IdentityResult result = await userManager.CreateAsync(account, password);
        if (!result.Succeeded)
        {
            throw new Exception(result.ToString());
        }
    }

    public async Task<bool> SignInAsync(string email, string password)
    {
        SignInResult result = await signInManager.PasswordSignInAsync(
            email, 
            password, 
            isPersistent: false,
            lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }

    private async Task<ValidationException?> ValidatePasswordAsync(
        Account account,
        string password)
    {
        List<ValidationFailure> errors = new();
        
        foreach (var validator in userManager.PasswordValidators)
        {
            IdentityResult result = await validator.ValidateAsync(userManager, account, password);
            if (result.Succeeded)
            {
                continue;
            }

            errors.AddRange(result.Errors.Select(error => new ValidationFailure
            {
                ErrorMessage = error.Description
            }));
        }

        return errors.Count > 0
            ? new ValidationException("Invalid password format.", errors.ToArray())
            : null;
    }
}