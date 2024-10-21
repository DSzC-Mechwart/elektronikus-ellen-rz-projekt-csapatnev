using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Declarative;

namespace AvaUtils;

public class SimpleNav : ComponentBase {
    private bool hasRegistered;

    public object? DefaultPage {
        get => default;
        set {
            if (key == "") return;
            Manager.SetCurrentPage(key, value ?? Optional<object>.Empty);
        }
    }

    private string key = "";

    public string Key {
        get => key;
        set {
            key = value;
            if (hasRegistered) return;
            Manager.RegisterNav(
                key,
                SetPage
            );
            hasRegistered = true;
        }
    }

    public object? CurrentPage { get; set; }

    protected override object Build() =>
        new ContentControl()
            .Content(() => CurrentPage ?? new Control());

    public void SetPage(object? page) {
        CurrentPage = page;
        StateHasChanged();
    }
}