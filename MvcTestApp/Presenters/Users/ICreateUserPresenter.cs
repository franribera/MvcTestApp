using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Presenters.Users
{
    public interface ICreateUserPresenter : IPresenter<Response<User>>
    {
    }
}