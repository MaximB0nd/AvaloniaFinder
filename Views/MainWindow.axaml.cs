using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaFinder.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnTreeViewDoubleTapped(object? sender, TappedEventArgs tappedEventArgs) {
        Console.WriteLine("Double click");
    }
}