using DCR;

List<string> files = new() {"Pattern1.xml", "Pattern2.xml", "Pattern3.xml", "Pattern4.xml", "Pattern5.xml", 
    "Pattern6.xml", "Pattern7.xml", "Pattern8.xml", "REB1.xml"};

foreach (string graph in files) {
    ConformanceChecker confchecker = new(graph, "log.csv");
    int failed_count = confchecker.CheckConformity();
    Console.WriteLine($"Graph: {(string)graph.Except("Pattern.xml")} | Failed count {failed_count}");
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
// System.Console.WriteLine("Groups:\n");
// PrintDictionary(reader.groups);

// System.Console.WriteLine("Milestones:\n");
// PrintDictionary(dcr_graph.milestones_For);

// System.Console.WriteLine("responses:\n");
// PrintDictionary(dc.responses_To);

//System.Console.WriteLine(":\n");
//PrintDictionary(dc.);

// System.Console.WriteLine("Conditions:\n");
// PrintDictionary(dc.conditions_For);
