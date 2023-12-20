namespace DCR;


public class ConformanceChecker {
    CSVReader csv;
    XMLreader xml;
    DCRGraph dcr_Graph;
    int failed_Counter = 0;    
    public ConformanceChecker(string xml_path, string csv_path) {
        xml = new(xml_path);
        csv = new(csv_path);
        dcr_Graph = xml.ProcessXML();
    }

    public int CheckConformity() {
        foreach (var kvp in csv.traces) {
            dcr_Graph = xml.ResetMarkings();
            // Iterate through each event in each trace
            foreach (string activity in kvp.Value) {
                if (dcr_Graph.Enabled(dcr_Graph.marking, activity)) {
                    dcr_Graph.marking = dcr_Graph.Execute(dcr_Graph.marking, activity);
                } else {
                    failed_Counter += 1;
                    break;
                }
            }   
        }
        return failed_Counter;
    }
}