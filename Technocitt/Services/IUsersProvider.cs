using Technocitt.AssistantControllers;
using Technocitt.Data;
using Technocitt.mydb;

namespace Technocitt.Services
{

    public interface IUsersProvider
    {
        Task<CustomUser> RegisterUser(string FName, string LName, string Email, string mobile, string Password);
        Task<CustomUser> GetUserWithUsername(string email);
        Task<IEnumerable<CustomUser>> GetAllUsers( );
    }

    public class  UsersProvider : IUsersProvider
    {
        private readonly AppDBContext myDb;

        public UsersProvider(AppDBContext myDb)
        {
            this.myDb = myDb;
        }

        public Task<IEnumerable<CustomUser>> GetAllUsers()
        {
            TaskCompletionSource<IEnumerable<CustomUser>> taskCompletionSource = new TaskCompletionSource<IEnumerable<CustomUser>>();
            Task.Run(() =>
            {
                IEnumerable<CustomUser> usr = myDb.TechnoUsers;
                taskCompletionSource.SetResult(usr);
            });
            return taskCompletionSource.Task;

        }

        public Task<CustomUser> GetUserWithUsername(string email)
        {
            TaskCompletionSource<CustomUser> taskCompletionSource = new TaskCompletionSource<CustomUser>();
            Task.Run(() =>
            {
                CustomUser usr = myDb.TechnoUsers
                .FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
                taskCompletionSource.SetResult(usr);
            });
            return taskCompletionSource.Task;

        }
        public Task<CustomUser> RegisterUser(string FName, string LName, string email, string mobile, string password)
        {
            TaskCompletionSource<CustomUser> taskCompletionSource = new TaskCompletionSource<CustomUser>();
            Task.Run(() =>
            {
                CustomUser myuser = new CustomUser();
                myuser.FirstName = FName;
                myuser.LastName = LName;
                myuser.Email = email;
                myuser.Mobile = mobile; 
                myuser.Password = sysController.CreateMD5(password);
                myDb.TechnoUsers.Add(myuser);
                myDb.SaveChanges();
                taskCompletionSource.SetResult(myuser);
            });
            return taskCompletionSource.Task;
        }

        
    }
}
