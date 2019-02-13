using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

/// <summary>
/// writing logs in the log file
/// </summary>
public class CsvWriter : IDisposable
{

    private readonly StreamWriter _stream;

    public CsvWriter(string path)
    {
        _stream = new StreamWriter(path, append: true);
    }


    public void Write(object o)
    {
        var myType = o.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        var last = props[props.Count - 1];
        foreach (var prop in props)
        {
            _stream.Write(prop.GetValue(o, null));
            if (prop != last)
                _stream.Write(";");
        }
        _stream.Write(Environment.NewLine);
    }

    public void Dispose()
    {
        _stream.Close();
        GC.SuppressFinalize(this);
    }
}
