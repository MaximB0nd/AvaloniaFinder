using System.Collections.ObjectModel;
using System.IO;
using AvaloniaFinder.Models;

namespace AvaloniaFinder.Services;

class DriverInfoService
{

    public static DriverInfoService Shared = new DriverInfoService();

    private DriverInfoService() {

    }

    public ObservableCollection<FinderObject>  GetAllDriverFinderObject() {
       var drivers = DriveInfo.GetDrives();

       var finderObjects = new ObservableCollection<FinderObject>();

        foreach (var driveInfo in drivers)
        {
            finderObjects.Add(new FinderObject(driveInfo.Name));
        }

        return finderObjects;

    }

    public DriveInfo GetParentDiskInfo(FinderObject finderObject)
    {
        string? rootDirectory = Path.GetPathRoot(finderObject.Path);
   
        DriveInfo drive = new DriveInfo(rootDirectory);
    
        return drive;
    }

    public FileSystemInfo? GetObjectInfo(string? path) {
        if (path is null)
        {
            return null;
        }
        if (File.Exists(path))
        {
            return new FileInfo(path);
        }
        if (Directory.Exists(path))
        {
            return new DirectoryInfo(path);
        }
        return null;
    }

}
