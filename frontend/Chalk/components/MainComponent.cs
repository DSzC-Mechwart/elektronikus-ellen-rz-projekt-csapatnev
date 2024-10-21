using Avalonia.Markup.Declarative;
using AvaUtils;
using Chalk.components.pages;

namespace Chalk.components;

public class MainComponent : ComponentBase {
    protected override object Build() =>
        new SimpleNav()
            .Key("root")
            .DefaultPage(new LoginPage());
}