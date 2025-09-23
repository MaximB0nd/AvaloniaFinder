using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaFinder.Models;
using AvaloniaFinder.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DriverInfoService _driverInfoService = DriverInfoService.Shared;
    private readonly OpenHistoryService _historyService = OpenHistoryService.Shared;
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

    public async Task OpenSelectedFinderObject()
    {
        var selectedFinderObject = _selectedFinderObject.GetCopy();

        try
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "open",
                Arguments = $"\"{selectedFinderObject.Path}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            
            process.Start();

            _historyService.SetOpenFile(selectedFinderObject.Path);

            await Task.Delay(3000);

            while (true) {
                if (!isFileOpened(selectedFinderObject.Path)) {
                    _historyService.SetCloseFile(selectedFinderObject.Path);
                    break;
                }
                await Task.Delay(500);
            }
        } catch {}

        
    }

    private bool isFileOpened(string path) {
        var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "/usr/sbin/lsof",
            Arguments = $"\"{path}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit(1000); 

        return !string.IsNullOrWhiteSpace(output);
    }
}
