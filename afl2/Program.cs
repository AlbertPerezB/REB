using DCR;

//Console.WriteLine("Hello, World!");
//DCRGraph graph = new();
XMLreader reader = new("REB1.xml");
DCRGraph dcr_graph = reader.ProcessXML();

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

System.Console.WriteLine("Milestones:\n");
PrintDictionary(dcr_graph.milestones_For);

System.Console.WriteLine("Excludes:\n");
PrintDictionary(dcr_graph.excludes_To);

System.Console.WriteLine("Conditions:\n");
PrintDictionary(dcr_graph.conditions_For);
