using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;  // Tambahkan ini untuk menggunakan FirstOrDefaultAsync


namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository : IUserAccount
    {
        private readonly IOptions<JwtSection> _config;
        private readonly AppDbContext _appDbContext;

        public UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "Modal is empty");

            var checkUser = await FindUserByEmail(user.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User Registered");

            // Save User
            var applicationUser = await AddToDatabase(new ApplicationUser()
            {
                FullName = user.Fullname,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });


            // check, create and assign role
            var checkAdminRole = await _appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if(checkAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account Crated Successfuly");
            }

            // check, create and assign role
            var checkUserRole = await _appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            if (checkUserRole is null)
            {
                var createUserRole = await AddToDatabase(new SystemRole() { Name = Constants.User });
                await AddToDatabase(new UserRole() { RoleId = createUserRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account Crated Successfuly");
            } else
            {
                await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });
            }

            return new GeneralResponse(true, "Account User Created Successfuly");

        }

        public Task<LoginResponse> CreateAsync(Login user)
        {
            throw new NotImplementedException();
        }

        // Assuming you have a method to find the user by email
        private Task<ApplicationUser> FindUserByEmail(string email)
        {
            return _appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = _appDbContext.Add(model!);
            await _appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}
