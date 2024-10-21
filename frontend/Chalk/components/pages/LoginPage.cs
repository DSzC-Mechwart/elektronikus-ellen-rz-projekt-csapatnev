using API;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;
using Avalonia.Styling;
using AvaUtils;
using static AvaUtils.ColorUtils;

namespace Chalk.components.pages;

public class LoginPage : ComponentBase {
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string ErrorMessage { get; set; } = "";

    protected override StyleGroup BuildStyles() => [
        new Style<TextBox>() {
                Children = {
                    new Style(x => x.Nesting().Class(":pointerover")) {
                        Children = {
                            new Style(x =>
                                    x.Nesting().Template().OfType<Border>().Name("PART_BorderElement"))
                                .Setter(Border.BorderBrushProperty, HexBrush("#33ffffff"))
                        }
                    }
                }
            }
            .BorderBrush(HexBrush("#18ffffff"))
            .CornerRadius(8),
        new Style<Button>() {
                Children = {
                    new Style(x =>
                            x.Nesting().Class(":pointerover").Template().OfType<ContentPresenter>()
                                .Name("PART_ContentPresenter"))
                        .Setter(ContentPresenter.CursorProperty, Cursor.Parse("Hand"))
                        .Setter(ContentPresenter.BackgroundProperty, HexBrush("#027ee3"))
                }
            }
            .Background(HexBrush("#026fc7"))
            .CornerRadius(8),
    ];

    protected override object Build() =>
        new Grid()
            .Cols("*,2*")
            .Children(
                new Border()
                    .Col(0)
                    .BorderThickness(1)
                    .BorderBrush(HexBrush("#18ffffff"))
                    .CornerRadius(12)
                    .Background(HexBrush("#0dffffff"))
                    .Margin(12)
                    .Padding(24)
                    .Child(
                        new StackPanel()
                            .VerticalAlignment(VerticalAlignment.Center)
                            .Spacing(8)
                            .Children(
                                new TextBlock()
                                    .Text("LOGIN")
                                    .HorizontalAlignment(HorizontalAlignment.Center)
                                    .FontWeight(FontWeight.Regular)
                                    .Foreground(HexBrush("#77ffffff"))
                                    .LetterSpacing(2)
                                    .FontSize(20)
                                    .Margin(bottom: 8),
                                new TextBox()
                                    .Text(() => Username)
                                    .OnTextChanged(args => Username = (args.Source as TextBox)?.Text ?? "")
                                    .Watermark("Username"),
                                new TextBox()
                                    .Text(() => Password)
                                    .OnTextChanged(args => Password = (args.Source as TextBox)?.Text ?? "")
                                    .Watermark("Password")
                                    .PasswordChar('•'),
                                new Button()
                                    .Content("Submit")
                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                    .HorizontalContentAlignment(HorizontalAlignment.Center)
                                    .OnClick(_ => Submit())
                                    .HotKey(KeyGesture.Parse("Enter"))
                            )
                    ),
                new Border()
                    .Col(0)
                    .BorderThickness(1)
                    .BorderBrush(HexBrush("#33ff0000"))
                    .Margin(24)
                    .VerticalAlignment(VerticalAlignment.Bottom)
                    .IsVisible(() => ErrorMessage != "")
                    .Padding(12)
                    .CornerRadius(8)
                    .Background(HexBrush("#0dff0000"))
                    .Child(
                        new StackPanel().Spacing(6)
                            .Children(
                                new TextBlock()
                                    .Text("Something went wrong :(")
                                    .FontWeight(FontWeight.Medium)
                                    .Foreground(HexBrush("#ccffffff")),
                                new TextBlock()
                                    .Text(() => ErrorMessage)
                                    .TextWrapping(TextWrapping.Wrap)
                                    .Foreground(HexBrush("#77ffffff"))
                                    .FontSize(12)
                            )
                    ),
                new Grid()
                    .Col(1)
                    .OpacityMask(
                        new RadialGradientBrush()
                            .RadiusX(RelativeScalar.Parse("100%"))
                            .RadiusY(RelativeScalar.Parse("90%"))
                            .Center(x: 1)
                            .GradientStops(new GradientStops() {
                                new GradientStop()
                                    .Color(Colors.Black)
                                    .Offset(0),
                                new GradientStop()
                                    .Color(Colors.Transparent)
                                    .Offset(1)
                            })
                    )
                    .Children(
                        new BackgroundGrid()
                            .GridSize(45)
                            .GridLineBrush(HexBrush("#99026fc7"))
                            .OffsetX(0)
                            .OffsetY(20)
                    ),
                new StackPanel()
                    .Col(1)
                    .HorizontalAlignment(HorizontalAlignment.Left)
                    .VerticalAlignment(VerticalAlignment.Bottom)
                    .Margin(42)
                    .Children(
                        new TextBlock()
                            .Text("Chalk")
                            .FontSize(64)
                            .LetterSpacing(2)
                            .FontWeight(FontWeight.SemiBold),
                        new TextBlock()
                            .Text("Student Information System")
                            .FontSize(18)
                            .FontWeight(FontWeight.Regular)
                    )
            );


    private async void Submit() {
        var res = await ChalkAPI.Instance.Auth.Login(Username, Password);
        if (res.Ok) {
            Manager.SetCurrentPage("root", new Frame());
        }

        ErrorMessage = res.Error ?? "Unknown error";
    }
}