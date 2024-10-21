using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;
using AvaUtils;
using Chalk.components.pages;
using Chalk.components.pages.admin;

namespace Chalk.components;

public class Frame : ComponentBase {
    protected override object Build() =>
        new Grid()
            .Cols("160,*")
            .Children(
                new Border()
                    .BorderThickness(right: 1d)
                    .BorderBrush(new SolidColorBrush(Color.Parse("#22ffffff")))
                    .Child(
                        new StackPanel()
                            .Orientation(Orientation.Vertical)
                            .Spacing(6)
                            .Margin(6)
                            .Children(
                                new Button()
                                    .Content("Home")
                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                    .OnClick(_ => NavigateToHome()), new Button()
                                    .Content("Subjects")
                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                    .OnClick(_ => NavigateToAdminSubjects())
                            )
                    ),
                new SimpleNav()
                    .Col(1)
                    .Key("main")
                    .DefaultPage(new HomePage())
            );

    private static void NavigateToHome() {
        Manager.SetCurrentPage("main", new HomePage());
    }

    private static void NavigateToAdminSubjects() {
        Manager.SetCurrentPage("main", new AdminSubjectsPage());
    }
}