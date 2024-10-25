using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;

namespace Chalk.components.pages.admin;

public class AdminClassesPage : ComponentBase {
    protected override object Build() =>
        new TextBlock()
            .Text("Classes")
            .HorizontalAlignment(HorizontalAlignment.Center)
            .VerticalAlignment(VerticalAlignment.Center)
            .FontSize(32)
            .FontWeight(FontWeight.SemiBold);
}