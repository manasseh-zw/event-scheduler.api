using event_scheduler.api.Data.Models;
using event_scheduler.api.Data.Repository;
using event_scheduler.api.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace event_scheduler.api.Features.Auth;

public interface IAuthService
{
    Task<GlobalResponse<AuthResponseDto>> Register(RegisterRequestDto registerRequest);
    Task<GlobalResponse<AuthResponseDto>> Login(LoginRequestDto loginRequest);
}



public class AuthService : IAuthService
{
    private readonly RepositoryContext _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenManager _jwtTokenManager;



    public AuthService(RepositoryContext repository, IPasswordHasher<User> passwordHasher, IJwtTokenManager jwtTokenManager)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtTokenManager = jwtTokenManager;
    }

    public async Task<GlobalResponse<AuthResponseDto>> Login(LoginRequestDto loginRequest)
    {
        var user = await _repository.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

        if (user == null)
        {
            return new GlobalResponse<AuthResponseDto>(false, "user login failed", errors: ["bad credentials"]);
        }

        var passwordVerifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

        if (passwordVerifyResult == PasswordVerificationResult.Failed)
        {
            return new GlobalResponse<AuthResponseDto>(false, "user login failed", errors: ["bad credentials"]);
        }

        var token = _jwtTokenManager.GenerateToken(user);

        return new GlobalResponse<AuthResponseDto>(true, "user login success", new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        });


    }

    public async Task<GlobalResponse<AuthResponseDto>> Register(RegisterRequestDto registerRequest)
    {
        var isEmailTaken = await _repository.Users.AnyAsync(u => u.Email == registerRequest.Email);
        if (isEmailTaken)
        {
            return new GlobalResponse<AuthResponseDto>(false, "register user failed", errors: ["email is already taken"]);
        }

        var validationResult = new AuthValidator().Validate(registerRequest);
        if (!validationResult.IsValid)
        {
            return new GlobalResponse<AuthResponseDto>(false, "register user failed", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var user = new User
        {
            FullName = registerRequest.Fullname,
            Email = registerRequest.Email,
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, registerRequest.Password);

        await _repository.Users.AddAsync(user);
        await _repository.SaveChangesAsync();

        var token = _jwtTokenManager.GenerateToken(user);

        return new GlobalResponse<AuthResponseDto>(true, "register user success",
        new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        });

    }
}