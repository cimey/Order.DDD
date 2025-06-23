using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> userManager,
                          SignInManager<ApplicationUser> signInManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _config = config;
    }

    [HttpPost("addRole/{email}")]
    public async Task AddRole(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("User not found");

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);
    }


    [HttpPost("revoke/{email}")]
    public async Task RevokeRole(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("User not found");

        if (!await _roleManager.RoleExistsAsync(role))
            throw new Exception("Role does not exist");

        await _userManager.RemoveFromRoleAsync(user, role);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, string firstName, string lastName, string role = "User")
    {
        var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Auth:SigningKey")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.GetValue<string>("Auth:Issuer")!,
            audience: _config.GetValue<string>("Auth:Audience")!,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}