namespace SeatsProject.repostiory_folder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SeatsProject.Models;
    public interface ILogin
    {
        Task<IEnumerable<UserLogin>> getUser();
        Task<UserLogin> AuthenticateUser(string username, string passcode);
    }
}
