using MyBarberShop.Repositories;
using MyBarberShop.Entities;
using MyBarberShop.Models;
using System.Linq.Expressions;

namespace MyBarberShop.Services
{
    public class UserService(GenericRepository <User> _userRepository)
    {

        public async Task<UserVm> Login(LoginVM loginVM)
        {
            var conditions = new List<Expression<Func<User, bool>>>()
            {
                x => x.Email == loginVM.Email,
                x => x.password == loginVM.Password,

            };

            var found = await _userRepository.GetByFilter(conditions: conditions.ToArray());

            var userVM = new UserVm();
            if (found != null) { 
                userVM.UserId = found.UserId;
                userVM.FullName = found.FullName;
                userVM.Email = found.Email;
                userVM.type = found.type;
                
            }
            return userVM;
        }

        public async Task Register(UserVm userVM)
        {
            if (userVM.password != userVM.Repeatpassword)
                throw new InvalidOperationException("Las Contraseñas no coinciden");

            var conditions = new List<Expression<Func<User, bool>>>()
            {
                x => x.Email == userVM.Email
            };

            var foundEmail = await _userRepository.GetByFilter(conditions:conditions.ToArray());
            if (foundEmail != null)
                throw new InvalidOperationException("La direccion de correo ya se encuentra registrada");

            var entity = new User()
            {
                FullName = userVM.FullName,
                Email = userVM.Email,
                type = userVM.type,
                password = userVM.password

            };
            await _userRepository.AddAsync(entity);
            
        }
    }
}
