using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Infrastructure;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Presenters
{
    public interface IPresenter : IOutputPort<Response<User>>
    {
        IActionResult ActionResult { get; }
    }
}