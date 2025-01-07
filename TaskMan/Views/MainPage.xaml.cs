using TaskMan.ViewModels;

namespace TaskMan
{
    public partial class MainPage : ContentPage
    {
        BaseViewModel vms;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as MainPageViewModel;
            vm.FetchTasks();
        }
    }

}
