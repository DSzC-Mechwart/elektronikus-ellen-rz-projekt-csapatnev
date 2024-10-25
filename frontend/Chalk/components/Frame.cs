using System;
using API;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;
using AvaUtils;
using Chalk.components.pages;
using Chalk.components.pages.admin;
using Chalk.components.pages.student;
using Chalk.components.pages.teacher;
using static AvaUtils.ColorUtils;

namespace Chalk.components;

public class Frame : ComponentBase {
    protected override object Build() =>
        new Grid()
            .Cols("160,*")
            .Children(
                new Border()
                    .BorderThickness(right: 1)
                    .BorderBrush(HexBrush("#22ffffff"))
                    .Child(
                        new Grid()
                            .Rows("Auto,Auto,1*")
                            .Children(
                                new Border()
                                    .Row(0)
                                    .BorderThickness(bottom: 1)
                                    .BorderBrush(HexBrush("#22ffffff"))
                                    .Child(
                                        new TextBlock()
                                            .Text(() => ChalkAPI.Instance.CurrentUser?.role switch {
                                                "admin" => "Admin",
                                                "teacher" => "Tanári",
                                                "student" => "Diák",
                                                _ => "Ismeretlen"
                                            } + " nézet")
                                            .Margin(8)
                                            .FontWeight(FontWeight.Medium)
                                            .FontSize(16)
                                            .TextAlignment(TextAlignment.Center)
                                    ),
                                new StackPanel()
                                    .Row(1)
                                    .Orientation(Orientation.Vertical)
                                    .Spacing(6)
                                    .Margin(6)
                                    .Children(
                                        ChalkAPI.Instance.CurrentUser?.role switch {
                                            "student" => [
                                                new Button()
                                                    .Content("Home")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToStudentHome()),
                                                new Button()
                                                    .Content("Grades")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToStudentGrades()),
                                                new Button()
                                                    .Content("Settings")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToStudentSettings())
                                            ],
                                            "teacher" => [
                                                new Button()
                                                    .Content("Students")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToTeacherStudents()),
                                                new Button()
                                                    .Content("Subjects")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToTeacherSubjects()),
                                                new Button()
                                                    .Content("Stats")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToTeacherStats())
                                            ],
                                            "admin" => [
                                                new Button()
                                                    .Content("Students")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToAdminStudents()),
                                                new Button()
                                                    .Content("Subjects")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToAdminSubjects()),
                                                new Button()
                                                    .Content("Classes")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToAdminClasses()),
                                                new Button()
                                                    .Content("Teachers")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToAdminTeachers()),
                                                new Button()
                                                    .Content("Stats")
                                                    .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                    .OnClick(_ => NavigateToAdminStats()),
                                            ],
                                            _ => []
                                        }
                                    ),
                                new Border()
                                    .Row(2)
                                    .VerticalAlignment(VerticalAlignment.Bottom)
                                    .Child(
                                        new Border()
                                            .BorderThickness(top: 1)
                                            .BorderBrush(HexBrush("#22ffffff"))
                                            .Child(
                                                new StackPanel()
                                                    .Margin(6)
                                                    .Spacing(6)
                                                    .Children(
                                                        new TextBlock()
                                                            .Text(() => ChalkAPI.Instance.CurrentUser?.name ?? "")
                                                            .TextAlignment(TextAlignment.Center),
                                                        new Button()
                                                            .Content("Log out")
                                                            .HorizontalAlignment(HorizontalAlignment.Stretch)
                                                            .OnClick(_ => LogOut())
                                                    )
                                            )
                                    )
                            )
                    ),
                new SimpleNav()
                    .Col(1)
                    .Key("main")
                    .DefaultPage(
                        ChalkAPI.Instance.CurrentUser?.role switch {
                            "student" => new StudentHomePage(),
                            "teacher" => new TeacherStudentsPage(),
                            "admin" => new AdminStudentsPage(),
                            _ => new Control()
                        }
                    )
            );

    private static void NavigateToStudentHome() {
        Manager.SetCurrentPage("main", new StudentHomePage());
    }

    private static void NavigateToStudentGrades() {
        Manager.SetCurrentPage("main", new StudentGradesPage());
    }

    private static void NavigateToStudentSettings() {
        Manager.SetCurrentPage("main", new StudentSettingsPage());
    }


    private static void NavigateToTeacherStudents() {
        Manager.SetCurrentPage("main", new TeacherStudentsPage());
    }

    private static void NavigateToTeacherSubjects() {
        Manager.SetCurrentPage("main", new TeacherSubjectsPage());
    }

    private static void NavigateToTeacherStats() {
        Manager.SetCurrentPage("main", new TeacherStatsPage());
    }


    private static void NavigateToAdminStudents() {
        Manager.SetCurrentPage("main", new AdminStudentsPage());
    }

    private static void NavigateToAdminSubjects() {
        Manager.SetCurrentPage("main", new AdminSubjectsPage());
    }

    private static void NavigateToAdminClasses() {
        Manager.SetCurrentPage("main", new AdminClassesPage());
    }

    private static void NavigateToAdminTeachers() {
        Manager.SetCurrentPage("main", new AdminTeachersPage());
    }

    private static void NavigateToAdminStats() {
        Manager.SetCurrentPage("main", new AdminStatsPage());
    }


    private static void LogOut() {
        ChalkAPI.Instance.CurrentUser = null;
        Manager.SetCurrentPage("root", new LoginPage());
    }
}