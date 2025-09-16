using System.Collections.ObjectModel;

namespace AvaloniaFinder.Models;

public class FinderObject
{
    public string Title { get; }
    public ObservableCollection<FinderObject> SubObjects { get; }
  
    public FinderObject(string title, ObservableCollection<FinderObject> subObjects)
    {
        this.Title = title;
        this.SubObjects = subObjects;
    }
}