using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.Design;
using System.Net;
using System.Threading.Tasks;
using TaskMan.Interfaces;
using TaskMan.Models;



namespace TaskMan.ViewModels;

public partial class AddPageViewModel :BaseViewModel, IQueryAttributable
{

    public readonly ISqliteService SqliteService;

    TaskModel Sel { get; set; }

    [ObservableProperty]
    string _taskName;
    [ObservableProperty]
    string _status;
    [ObservableProperty]
    string _taskSta;
    [ObservableProperty]
    string _taskModified;
    [ObservableProperty]
    string _taskDescription;

    [ObservableProperty]
    bool _isErrorvisible;
    List<string> TaskStatus=new List<string>() { "InProgress","Pending","Completed"};
	public AddPageViewModel(ISqliteService _sqliteService): base(_sqliteService)
    {

        SqliteService = _sqliteService;
        

    }

	[RelayCommand]
    public async Task Save()
    {
        try
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(TaskName) && !string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(Status))
            {
                if (Sel == null)
                {
                    if (Status == "0")
                        TaskSta = "InProgress";
                    else if (Status == "1")
                        TaskSta = "Pending";
                    else if (Status == "2")
                        TaskSta = "Completed";
                    var dbconnection = await SqliteService.GetConnectionAsync();
                    var query = $"insert into Tasks(Taskname,Taskdescription,taskstatus,taskmodified) Values('{TaskName}','{TaskDescription}','{TaskSta}','{DateTime.Now.ToString()}')";
                    var results = await dbconnection.ExecuteAsync(query);
                    if (results == 1)
                    {
                        //success
                        isSuccess = true;
                    }
                }
                else
                {
                    if (Status == "0")
                        TaskSta = "InProgress";
                    else if (Status == "1")
                        TaskSta = "Pending";
                    else if (Status == "2")
                        TaskSta = "Completed";
                    var dbconnection = await SqliteService.GetConnectionAsync();
                    var query = $"update tasks set Taskname='{TaskName}',Taskdescription='{TaskDescription}',taskstatus='{TaskSta}',taskmodified='{DateTime.Now.ToString()}' where id={Sel.Id}";
                    var results = await dbconnection.ExecuteAsync(query);
                    if (results == 1)
                    {
                        isSuccess = true;
                    }
                }
            }
           

            if (isSuccess)
            {
              
                await Shell.Current.GoToAsync("//MainPage",false);
               
                await FetchTasks();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error","Some fields are missing","OK");
            }

        }
        catch (Exception ex)
        {

        }

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Sel = query["Sel"] as TaskModel;
       
        if (Sel != null)
        {

            TaskName=Sel.TaskName;
            TaskDescription=Sel.TaskDescription;
            if (Sel.TaskStatus != null && Sel.TaskStatus.ToLower()=="pending")
            {
                Status = "1";
            }
            else if (Sel.TaskStatus != null && Sel.TaskStatus.ToLower()== "inprogress")
            {
                Status = "0";
            }
            else
            {
                Status = "2";   
            }
        }
        
    }
}