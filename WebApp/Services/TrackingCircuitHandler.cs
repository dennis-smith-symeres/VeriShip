using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace VeriShip.WebApp.Services
{
    public class TrackingCircuitHandler(
        IUsersStateContainer usersStateContainer,
        AuthenticationStateProvider authenticationStateProvider)
        : CircuitHandler
    {
        public override async Task OnConnectionUpAsync(Circuit circuit, 
            CancellationToken cancellationToken)
        {
            var state =  await authenticationStateProvider.GetAuthenticationStateAsync();
            usersStateContainer.Update(circuit.Id, state.User.Identity.Name);

            return ;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, 
            CancellationToken cancellationToken)
        {
            usersStateContainer.Remove(circuit.Id);

            return Task.CompletedTask;
        }

       
    }
}