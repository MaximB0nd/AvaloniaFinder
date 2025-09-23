using System;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaFinder.Models;
using AvaloniaFinder.ViewModels;

namespace AvaloniaFinder.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnTreeViewDoubleTapped(object? sender, TappedEventArgs tappedEventArgs)
    {
        if (sender is TreeView treeView)
        {
            var selectedItem = treeView.SelectedItem;
            
            if (selectedItem is FinderObject fileSystemItem)
            {
                if (!fileSystemItem.IsDirectory) {
                    if (DataContext is MainWindowViewModel viewModel)
                    {
                        viewModel.OpenSelectedFinderObject();
                    }   
                }
            }
        }
    }
}