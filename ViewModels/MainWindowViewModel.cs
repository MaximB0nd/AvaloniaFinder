using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaFinder.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FinderObject> _finderObjects;

    public MainWindowViewModel()
    {
        _finderObjects = new ObservableCollection<FinderObject>
        {
            new FinderObject(
                "Folder1", 
                [
                    new FinderObject(
                        "Folder1.1", 
                        [
                            new FinderObject("File1", []),
                             new FinderObject("File11", [])
                        ])]),
            new FinderObject(
                "Folder2", 
                [
                    new FinderObject(
                        "Folder2.1", 
                        [new FinderObject("File2", []), new FinderObject("File21", [])])])
            
        };
    }
}
