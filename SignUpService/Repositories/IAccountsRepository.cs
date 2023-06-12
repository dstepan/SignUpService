namespace SignUpService.Repositories;

/// <summary>
/// Manages user accounts.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Creates user account if one doesn't exist.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="password">User password.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Is thrown if user account already exists, invalid email or password provided.
    /// </exception>
    public Task CreateAccountAsync(
        string email,
        string password);
    
    /// <summary>
    /// Signs user in. Does nothing if user already signed in.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="password">User password.</param>
    /// <returns>Returns 'true' if the user is signed in, 'false' otherwise.</returns>
    public Task<bool> SignInAsync(string email, string password);

    /// <summary>
    /// Signs current user out. Does nothing if user is not signed in.
    /// </summary>
    public Task SignOutAsync();
}