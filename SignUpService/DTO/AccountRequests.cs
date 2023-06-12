namespace SignUpService.DTO;

public class SignUpRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
