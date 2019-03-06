using System.Threading.Tasks;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public interface IUpdateAssertionStep
    {
        Task<UpdateUserResponse> AssertForUpdate(UpdateUserRequest request);
        void SetSuccessor(IUpdateAssertionStep updateAssertionStep);
    }
}