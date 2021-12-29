using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages;

public partial class ManageUsers
{
    [Inject]
    private HttpClient HttpClient { get; set; }

    private IList<(bool Check, ActorState<UserActorState> State)> userActorState = Enumerable.Empty<(bool, ActorState<UserActorState>)>().ToArray();

    protected override async Task OnInitializedAsync()
    {
        var q = from a in await HttpClient.GetFromJsonAsync<IEnumerable<ActorState<UserActorState>>>("b/ManageUsers")
                select (false, a);

        userActorState = q.ToList();
        base.OnInitializedAsync();
    }

    private void DeleteUserHandle()
    {

    }

    private void ClickCheckboxHandle()
    {
        if(userActorState.Any(x => x.Check is true))
        {
            disabled = "";
        }
        else
        {
            disabled = "disabled";
        }

        StateHasChanged();
    }

    string disabled = "disabled";
}
