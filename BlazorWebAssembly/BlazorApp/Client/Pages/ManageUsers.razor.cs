using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages;

public partial class ManageUsers
{
    [Inject]
    private HttpClient HttpClient { get; set; }

    private IList<Cell> userActorState = Enumerable.Empty<Cell>().ToArray();

    protected override async Task OnInitializedAsync()
    {
        var q = from a in await HttpClient.GetFromJsonAsync<IEnumerable<ActorState<UserActorState>>>("b/ManageUsers")
                select new Cell
                {
                    Checked = false,
                    State = a
                };

        userActorState = q.ToList();
        base.OnInitializedAsync();
    }

    private void DeleteUserHandle()
    {

    }

    private void ClickCheckboxHandle(ChangeEventArgs e, string id)
    {
        var q = from a in userActorState
                where a.State.Id == id
                select a;

        foreach (var item in q)
        {
            item.Checked = (bool)e.Value;
        }

        disabled = userActorState.Any(x => x.Checked) is not true;

        StateHasChanged();
    }

    private bool disabled = true;
}

public class Cell
{
    public bool Checked { get; set; }
    public ActorState<UserActorState> State { get; init; }
}

