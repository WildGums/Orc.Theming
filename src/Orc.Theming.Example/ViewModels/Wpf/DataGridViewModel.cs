namespace Orc.Theming.Example.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Catel.MVVM;

public class DataGridViewModel : ViewModelBase
{
    private const int RecordCount = 100;
    private readonly Random _rand;

    public DataGridViewModel()
    {
        _rand = new Random();

        Records = new ObservableCollection<Record>();

        for (var i = 0; i < RecordCount; i++)
        {
            var record = new Record
            {
                String = GenerateString(_rand.Next(3, 15)),
                DateTime = GenerateDateTime(),
                Bool = GenerateBool(),
                Double = _rand.NextDouble(),
                Int = _rand.Next()
            };

            Records.Add(record);
        }
    }

    public ObservableCollection<Record> Records { get; set; }

    private string GenerateString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_rand.Next(s.Length)]).ToArray());
    }

    private DateTime GenerateDateTime()
    {
        var start = new DateTime(1995, 1, 1);
        var range = (DateTime.Today - start).Days;
        return start.AddDays(_rand.Next(range));
    }

    private bool GenerateBool()
    {
        var prob = _rand.Next(100);
        return prob <= 49;
    }
}