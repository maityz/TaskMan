using TaskMan.Models;
using TaskMan.ViewModels;

namespace TaskMan.Views;



public partial class AddPage : ContentPage
{
  public AddPage(AddPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}