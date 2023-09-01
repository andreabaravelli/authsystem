namespace SeatsProject.repostiory_folder
{
    using SeatsProject.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    public class AuthenticateLogin: ILogin
    {
        private readonly SeatsProjectContext _context;

        public AuthenticateLogin(SeatsProjectContext context)// we take the context from the original class and we pass in the constructor of this class.
        {
            _context = context;
        }
        public async Task<UserLogin> AuthenticateUser(string username, string passcode)
        {
           var succeeded = await _context.UserLogin.FirstOrDefaultAsync(AuthUser=> AuthUser.userName == username && AuthUser.passcode== passcode);
            return succeeded;
        }

        public async Task<IEnumerable<UserLogin>> getUser()
        {
            return await _context.UserLogin.ToListAsync();
        }
    }
}
