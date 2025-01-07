using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TaskMan.Interfaces;
using TaskMan.Models;
using TaskMan.Views;

namespace TaskMan.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    public readonly ISqliteService SqliteService;



    public MainPageViewModel(ISqliteService _sqliteService):base(_sqliteService)
	{

        SqliteService = _sqliteService;
        FetchTasks();
       
    }
    
    [RelayCommand]
    public async void Edit(TaskModel ob)
    {
        
        
            var Params = new ShellNavigationQueryParameters
    {
                { "Sel", ob }
            };

       
        await Shell.Current.GoToAsync($"AddPage", Params);
     

    }
    
    [RelayCommand]
    public async void Delete(TaskModel ob)
    {
        try
        {
            var dbconnection = await SqliteService.GetConnectionAsync();
            var query = $"Delete from tasks where id={ob.Id}";
            var results = await dbconnection.ExecuteAsync(query);
            
            Tasks.Remove(ob);
            FetchTasks();
            Counts();

        }
        catch (Exception ex)
        {

        }

    }
}
