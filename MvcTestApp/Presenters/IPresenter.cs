using Microsoft.AspNetCore.Mvc;
using MvcTestApp.Application.Infrastructure;

namespace MvcTestApp.Presenters
{
    public interface IPresenter<in TResponse> : IOutputPort<TResponse>
    {
        IActionResult ActionResult { get; }
    }
}