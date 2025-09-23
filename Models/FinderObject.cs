using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaFinder.Models;

public class FinderObject : ObservableObject
{
    public string Path { get; }

    public string? Title { get {
        if (File.Exists(Path))
        {
            return new FileInfo(Path).Name;
        }
        if (Directory.Exists(Path))
        {
            return new DirectoryInfo(Path).Name;
        }

        return null;
        }
    }

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


        foreach (var directory in Directory.EnumerateDirectories(Path, "*", options))
        {
            try
            {
                SubObjects.Add(new FinderObject(directory));
            }
            catch {}

        }

        
        foreach (var file in Directory.EnumerateFiles(Path, "*", options))
        {
            try
            {
                SubObjects.Add(new FinderObject(file));
            }
            catch {}

        }
    }

    public string IconPlusTitle {
        get {
            if (IsDirectory)
            {
                if (IsExpanded)
                {
                    return "📂" + Title;
                } 
                else
                {
                    return "📁" + Title;
                }
            }
            else
            {
                return "📄" + Title;
            }
        }
    }

    public FinderObject(FinderObject obj)
    {
        this.Path = obj.Path;
    }

    public FinderObject GetCopy()
    {
        return new FinderObject(this);
    }
}