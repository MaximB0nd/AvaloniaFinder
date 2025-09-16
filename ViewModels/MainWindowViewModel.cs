using System.Collections.ObjectModel;
using System.IO;
using AvaloniaFinder.Models;
using AvaloniaFinder.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DriverInfoService _driverInfoService = DriverInfoService.Shared;
    [ObservableProperty] private ObservableCollection<FinderObject> _finderObjects;

    [ObservableProperty] private FinderObject? _selectedFinderObject;
    [ObservableProperty] private DriveInfo? _selectedDrive;
    [ObservableProperty] private FileSystemInfo? _selectedFinderObjectInfo;

    public MainWindowViewModel()
    {
        _finderObjects = _driverInfoService.GetAllDriverFinderObject();
    }

    partial void OnSelectedFinderObjectChanged(FinderObject? value)
    {
        if (value is null)
        {
            SelectedDrive = null;
            SelectedFinderObjectInfo = null;
            return;
        }

        SelectedDrive = _driverInfoService.GetParentDiskInfo(value);
        SelectedFinderObjectInfo = _driverInfoService.GetObjectInfo(value.Path);
    }
}
