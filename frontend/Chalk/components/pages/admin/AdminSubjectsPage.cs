using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using API;
using API.models;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;

namespace Chalk.components.pages.admin;

public class AdminSubjectsPage : ComponentBase {
    public ObservableCollection<SubjectJSON> AllSubjects { get; set; } = [];

    public AdminSubjectsPage() {
        Task.Run(async () => {
            var res = await ChalkAPI.Instance.Subject.GetAll();
            res.Data?.subjects?.ToList().ForEach(AllSubjects.Add);
        });
    }

    protected override object Build() =>
        new Grid()
            .Rows("40,*")
            .Margin(24)
            .Children(
                new TextBlock()
                    .Text("Subjects")
                    .VerticalAlignment(VerticalAlignment.Center),
                new ItemsControl()
                    .Row(1)
                    .ItemsSource(@AllSubjects)
                    .ItemTemplate<SubjectJSON, ItemsControl>(item =>
                        new FuncComponent<SubjectJSON>(item, x =>
                            new TextBlock()
                                .Text(() => x.name)
                        )
                    )
            );
}