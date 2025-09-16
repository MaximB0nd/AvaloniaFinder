using System.Collections.ObjectModel;
using AvaloniaFinder.Models;
using AvaloniaFinder.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FinderObject> _finderObjects;

    [ObservableProperty]
    private FinderObject? _selectedFinderObject;

    private DriverInfoService _driverInfoService = DriverInfoService.Shared;

    public MainWindowViewModel()
    {
        _finderObjects = _driverInfoService.GetAllDriverFinderObject();
    }
}
