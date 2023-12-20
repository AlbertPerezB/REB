using DCR;
ConformanceChecker confchecker = new("REB1.xml", "log.csv");
int failed_count = confchecker.CheckConformity();
System.Console.WriteLine($"Failed count {failed_count}");

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
