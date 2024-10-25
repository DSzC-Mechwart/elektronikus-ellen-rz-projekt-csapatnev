using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;

namespace Chalk.components.pages.teacher;

public class TeacherStudentsPage : ComponentBase {
    protected override object Build() =>
        new TextBlock()
            .Text("Students")
            .HorizontalAlignment(HorizontalAlignment.Center)
            .VerticalAlignment(VerticalAlignment.Center)
            .FontSize(32)
            .FontWeight(FontWeight.SemiBold);
}