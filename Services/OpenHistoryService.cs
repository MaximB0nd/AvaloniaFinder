using System;
using System.Timers;
using AvaloniaFinder.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaFinder.Services;

class OpenHistoryService
{
    public static OpenHistoryService Shared = new OpenHistoryService();
    private Timer _timer;
    private readonly object _lockObject = new object();

    private List<OpenHistoryItemModel> _openedFilesList = new List<OpenHistoryItemModel>();

    private List<OpenHistoryItemModel> _recentlyOpenedFilesList = new List<OpenHistoryItemModel>();

    private string _jsonPath = "Cache/OpenHistory.json";

    private OpenHistoryService()
    {
        _timer = new Timer(1000);
        _timer.AutoReset = true;
        _timer.Elapsed += OnTimedEvent;

        _timer.Start();
    }

    ~OpenHistoryService()
    {
        _timer.Stop();
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {

        lock (_lockObject)
        {
            try
            {   Console.WriteLine(_recentlyOpenedFilesList.Count + _openedFilesList.Count);
                _recentlyOpenedFilesList.RemoveAll(model => model.OpenDate.AddSeconds(10) < DateTime.Now);
                
                var jsonObjects = new List<OpenHistoryItemModel>();
                foreach (var openHistoryItemModel in _openedFilesList)
                {
                    var updatedOpenHistoryItemModel = new OpenHistoryItemModel(path: openHistoryItemModel.Path);
                    jsonObjects.Add(updatedOpenHistoryItemModel);
                }
                foreach (var openHistoryItemModel in _recentlyOpenedFilesList)
                {
                    jsonObjects.Add(openHistoryItemModel);
                    Console.WriteLine(openHistoryItemModel.OpenDate);
                }

                Console.WriteLine(jsonObjects.Count);
                string json = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                System.IO.File.WriteAllText(_jsonPath, json);
                Console.WriteLine($"Данные сохранены {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }

    public void SetOpenFile(string path)
    {
        _openedFilesList.Add(new OpenHistoryItemModel(path, DateTime.Now));
    }

    public void SetCloseFile(string path)
    {
        _openedFilesList.RemoveAll(model => model.Path == path);
        _recentlyOpenedFilesList.Add(new OpenHistoryItemModel(path, DateTime.Now.Date));
    }


}