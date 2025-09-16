using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaFinder.Models;
using AvaloniaFinder.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FinderObject> _finderObjects;

    private DriverInfoService _driverInfoService = DriverInfoService.Shared;

    public MainWindowViewModel()
    {
        _finderObjects = _driverInfoService.GetAllDriverFinderObject();
    }
}
