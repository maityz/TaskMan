using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskMan.Interfaces;
using TaskMan.Models;

namespace TaskMan.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public readonly ISqliteService SqliteService;
    [ObservableProperty]
    ObservableCollection<TaskModel> _tasks = new();
    [ObservableProperty]
    string _inPendCount;

    [ObservableProperty]
    string _inProgCount;

    [ObservableProperty]
    string _compCount;

    [ObservableProperty]
    string _itemsCount;
    public BaseViewModel(ISqliteService _sqliteService)
	{
        SqliteService = _sqliteService;
    }

    public async Task FetchTasks()
    {
        try
        {
            var dbconnection = await SqliteService.GetConnectionAsync();
            var query = "select * from Tasks";
            var results = await dbconnection.QueryAsync<TaskModel>(query);
            if (results.Count == 0)
            {
                //display empty
            }
            else
            {
                Tasks = new ObservableCollection<TaskModel>(results);

            }
            Counts();
        }
        catch (Exception ex) { }

    }
    public void Counts()
    {
        try
        {
            var progcount = Tasks.Where(a => a.TaskStatus.ToLower() == "inprogress").Count();
            var pendcount = Tasks.Where(a => a.TaskStatus.ToLower() == "pending").Count();
            var compcount = Tasks.Where(a => a.TaskStatus.ToLower() == "completed").Count();

            InProgCount = $"({progcount})";
            InPendCount = $"({pendcount})";
            CompCount = $"({compcount})";
            ItemsCount = $"{progcount + pendcount + compcount} items";
        }
        catch (Exception ex)
        {

        }
    }
}