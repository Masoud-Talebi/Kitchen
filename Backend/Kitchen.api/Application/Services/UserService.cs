using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Kitchen.api;

public class UserService : IUserService
{
    #region Field
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UserService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
        _mapper = mapper;
    }
    #endregion
    public async Task<bool> DeleteUser(int Id)
    {
        var User = await _context.Users.FindAsync(Id);
        User.deleted = true;
        _context.Update(User);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUser()
    {
        IEnumerable<User> Users = await _context.Users.Where(p => p.deleted == false).ToListAsync();
        IEnumerable<UserDTO> UserDTOs = _mapper.Map<IEnumerable<UserDTO>>(Users);
        return UserDTOs;
    }

    public async Task<UserDTO> GetUserById(int Id)
    {
        User user = await _context.Users.FindAsync(Id);
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }

    public async Task<string> LoginUser(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public async Task<UserDTO> Rigester(AddUserDTO addUserDTO)
    {
        User user = _mapper.Map<User>(addUserDTO);
        user.FullName = addUserDTO.FirstName + " " + addUserDTO.LastName;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }

    public async Task<UserDTO> UpdateUser(UpdateUserDTO UpdateUserDTO)
    {
        User user = _mapper.Map<User>(UpdateUserDTO);
        user.FullName = UpdateUserDTO.FirstName + " " + UpdateUserDTO.LastName;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }
    public async Task<bool> ChangeRole(int Id, string role)
    {
        var user = await _context.Users.FindAsync(Id);
        user.Role = role;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
