using System;
using System.Threading.Tasks;

namespace MvcTestApp.Application.Commands.Users.Update
{
    public abstract class UpdateAssertionStepBase : IUpdateAssertionStep
    {
        protected IUpdateAssertionStep Successor;

        /// <inheritdoc />
        public async Task<UpdateUserResponse> AssertForUpdate(UpdateUserRequest request)
        {
            var actionResult = await AssertForUpdateAction(request);

            return actionResult ?? await Successor.AssertForUpdate(request);
        }

        /// <inheritdoc />
        public void SetSuccessor(IUpdateAssertionStep updateAssertionStep)
        {
            Successor = updateAssertionStep ?? throw new ArgumentNullException(nameof(updateAssertionStep));
        }

        protected abstract Task<UpdateUserResponse> AssertForUpdateAction(UpdateUserRequest request);
    }
}