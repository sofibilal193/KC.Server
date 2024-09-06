// using AutoMapper;
// using Kashmir.Captain.Server.Application.DTO;
// using Kashmir.Captain.Server.Common.Extensions;
// using Kashmir.Captain.Server.Data;
// using Kashmir.Captain.Server.Entities;
// using Kashmir.Captain.Server.Services;
// using Kashmir.Captain.Server.ViewModel;
// using Microsoft.EntityFrameworkCore;

// namespace Kashmir.Captain.Server.Repositories
// {
// 	public class UserRepository : IUserRepository
// 	{
// 		private readonly KcDbContext _context;
// 		private readonly IMapper _mapper;
// 		private readonly IPasswordService _passwordService;

// 		public UserRepository(KcDbContext context, IMapper mapper, IPasswordService passwordService)
// 		{
// 			_context = context;
// 			_mapper = mapper;
// 			_passwordService = passwordService;
// 		}

// 		public async Task<PagedList<UserDto>> GetUsersAsync(int pageNumber, int pageSize)
// 		{
// 			// var sortExpression = request.Sort?.TrimStart('-')?.ToLower();
// 			// var prefix = request.Sort?.StartsWith('-') == true ? "-" : "";
// 			// if (sortExpression == nameof(CreditAppListDto.LastUpdatedUtc).ToLower())
// 			// {
// 			//     sortExpression = prefix + nameof(CreditAppView.ModifyDateTimeUtc);
// 			// }
// 			// else
// 			// {
// 			//     sortExpression = $"-{nameof(CreditAppView.CreateDateTimeUtc)}";
// 			// }

// 			var query = _context.Users.AsQueryable();
// 			var xyz = await query.ToPagedListAsync(pageNumber, pageSize);
// 			var abc = _mapper.Map<PagedList<UserDto>>(xyz);

// 			return abc;
// 		}

// 		public async Task<int> UpsertUserAsync(RegisterUserModel request)
// 		{
// 			User? user;

// 			if (request.Role != UserRole.SuperAdmin)
// 			{
// 				var hashPassword = _passwordService.HashPassword(request.Password);
// 				if (!request.Id.HasValue)
// 				{
// 					var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
// 					if (existingUser != null)
// 					{
// 						throw new Exception("User Already Exists");
// 					}

// 					user = new User(request.FirstName, request.LastName, request.MobileNumber, request.Email, hashPassword, request.Role);
// 					await _context.Users.AddAsync(user);
// 				}
// 				else
// 				{
// 					user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id)
// 					   ?? throw new Exception("Not Found");

// 					user.Update(request.FirstName, request.LastName, request.MobileNumber, request.Email, hashPassword, request.Role);
// 				}
// 			}
// 			else
// 			{
// 				throw new Exception("SuperAdmin Already Exists!");
// 			}
// 			await _context.SaveChangesAsync();

// 			return user.Id;
// 		}

// 		public async Task DeleteUserAsync(int id)
// 		{
// 			var user = await _context.Users.FindAsync(id)
// 						?? throw new Exception("Not Found");

// 			_context.Users.Remove(user);
// 			await _context.SaveChangesAsync();
// 		}

// 		public async Task<UserDto> GetUserAsync(int id)
// 		{
// 			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id)
// 			?? throw new Exception("Not Found");

// 			return _mapper.Map<UserDto>(user);
// 		}
// 	}
// }
