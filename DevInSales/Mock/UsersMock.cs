using DevInSales.Models;
using DevInSales.Enums;

namespace DevInSales.Mock
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UsersMock
    {
        public static List<User> ListUsers()
        {
            var users = new List<User>
            {
                new User() { Id = 1, UserName = "Victor", Password = "123", Role = "funcionario", Permition = Permitions.Funcionario },
                new User() { Id = 2, UserName = "Cris", Password = "456", Role = "gerente", Permition = Permitions.Gerente },
                new User() { Id = 3, UserName = "Maria", Password = "789", Role = "administrador", Permition = Permitions.Administrador },
                new User() { Id = 4, UserName = "Test", Password = "000", Role = "administrador", Permition = Permitions.Administrador },
            };

            return users;
        }

        public static User GetUserMock(string username, string password)
        {
            var list = ListUsers();

            return list.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }

    }
}
