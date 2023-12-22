namespace DCR;
using System;

public class ConformanceChecker {
    CSVReader csv;
    XMLreader xml;
    DCRGraph dcr_Graph;
    int failed_Counter = 0;    

    public ConformanceChecker(string xml_path, string csv_path) {
        xml = new(xml_path);
        csv = new(csv_path);
        dcr_Graph = xml.ProcessXML();
        // System.Console.WriteLine("inc:");
        // // PrintDictionary(dcr_Graph.marking.included);
        // Console.WriteLine(string.Join(", ", dcr_Graph.marking.included));
    }

    // Counts the amount of times a non-enabled action was in the log then returns ok count and failed count
    public (int, int) CheckConformity() {
        foreach (var kvp in csv.traces) {
            dcr_Graph = xml.ResetMarkings();
            // Iterate through each event in each trace
            int count = kvp.Value.Count;
            foreach (string activity in kvp.Value) {
                // If the next activity is enabled, then it's ok, and we update the marking.
                if (dcr_Graph.Enabled(dcr_Graph.marking, activity)) {
                    dcr_Graph.marking = dcr_Graph.Execute(dcr_Graph.marking, activity);
                    //Console.WriteLine(string.Join(", ",dcr_Graph.marking.included));
                    if (--count == 0) { // If its the last activity the graph needs to be accepting
                        // System.Console.WriteLine($"activity: {activity}");
                        if (!dcr_Graph.IsAccepting()) {
                            failed_Counter++; 
                        }
                    }
                } else {
                    failed_Counter++;
                    break;
                }
            }   
        }
        return (csv.traces.Count - failed_Counter, failed_Counter);
    }

    static void PrintDictionary(Dictionary<string, HashSet<string>> dictionary) {
        foreach (var kvp in dictionary){
            Console.WriteLine($"Key: {kvp.Key}");
            Console.WriteLine("Values:");

            foreach (var value in kvp.Value){
                Console.WriteLine($"  {value}");
            }
            Console.WriteLine(""); // Separate key-value pairs with an empty line
        }
    }
}