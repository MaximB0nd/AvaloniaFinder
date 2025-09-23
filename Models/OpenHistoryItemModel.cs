using System;

namespace AvaloniaFinder.Models;

public struct OpenHistoryItemModel
{
    public string Path { get; set; }
    public DateTime OpenDate { get; set; }

    public OpenHistoryItemModel(string path, DateTime openDate)
    {
        Path = path;
        OpenDate = openDate;
    }

    public OpenHistoryItemModel(string path)
    {
        Path = path;
        OpenDate = DateTime.Now;
    }
}

