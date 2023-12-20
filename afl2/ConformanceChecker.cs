namespace DCR;


public class ConformanceChecker {
    CSVReader csv;
    DCRGraph dcr_graph;
    
    public ConformanceChecker(string xml_path, string csv_path) {
        XMLreader xml = new(xml_path);
        dcr_graph = xml.ProcessXML();
        csv = new(csv_path);
    }

    public CheckConformity() {
        foreach (var kvp in csv.traces) {
            // Iterate through each event in each trace
            foreach (string activity in kvp.Value) {
                if (!dcr_graph.Enabled(dcr_graph.marking, activity))
            
    }   
}
    }
}



