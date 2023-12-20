namespace DCR;

using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;


public class CSVReader {
    public Dictionary<string, List<string>> traces = new();

    public CSVReader(string file_name) {
        StreamReader stream_reader = new(file_name);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
            Delimiter = ";"
        };
        CsvReader reader = new(stream_reader, config);
        
        // Read the records
        var records = reader.GetRecords<dynamic>().ToList();

        // Process each record
        foreach (var record in records) {
            // Iterate over properties dynamically
            string id = record.ID;
            string name = record.EventName;
            // If this is the first mention of the id, add string, List<string> entry
            if (!traces.ContainsKey(id)) {
                traces.Add(id, new List<string> {name});
            // If the id is already in the Dictionary
            } else {
                traces[id].Add(name);
            }
        }   
    }
}