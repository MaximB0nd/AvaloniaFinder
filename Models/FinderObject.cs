using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.Models;

public class FinderObject : ObservableObject
{
    public string Path { get; }
    public bool IsDirectory { get; }
    public ObservableCollection<FinderObject> SubObjects { get; } = new();

    private bool _isLoaded;
    private bool _isExpanded;

    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            SetProperty(ref _isExpanded, value);
            if (value)
            {
                LoadChildrenIfNeeded();
            }
        }
    }

    public FinderObject(string path)
    {
        Path = path;
        IsDirectory = Directory.Exists(path);

        if (IsDirectory)
        {
            SubObjects.Add(CreatePlaceholder());
        }
    }

    private FinderObject(string label, bool isDirectory)
    {
        Path = label;
        IsDirectory = isDirectory;
    }

    private static FinderObject CreatePlaceholder() => new FinderObject("(loading)", false);

    private void LoadChildrenIfNeeded()
    {
        if (!IsDirectory || _isLoaded)
        {
            return;
        }

        _isLoaded = true;

        SubObjects.Clear();

        var options = new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,
            ReturnSpecialDirectories = false
        };

        try
        {
            foreach (var directory in Directory.EnumerateDirectories(Path, "*", options))
            {
                SubObjects.Add(new FinderObject(directory));
            }
        }
        catch 
        {
            // ignore
        }

        try
        {
            foreach (var file in Directory.EnumerateFiles(Path, "*", options))
            {
                SubObjects.Add(new FinderObject(file));
            }
        }
        catch
        {
            // ignore
        }
    }
}