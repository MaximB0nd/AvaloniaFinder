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
}
