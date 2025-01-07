using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace TaskMan.Models;
[SQLite.Table("Tasks")]
public partial class TaskModel : ObservableObject
{
    [SQLite.Column("Id")]
    [AutoIncrement]
    [PrimaryKey]
    public int Id { get; set; }
    [ObservableProperty]
    string _taskName;
    [ObservableProperty]
    string _taskStatus;
    [ObservableProperty]
    string _taskDescription;
    [ObservableProperty]
    string _taskModified;
    
}