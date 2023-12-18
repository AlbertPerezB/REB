using DCR;

//Console.WriteLine("Hello, World!");
//DCRGraph graph = new();
XMLreader reader = new("REB1.xml");
HashSet<string> activities = reader.ReadEvents();
// Dictionary<string, HashSet<string>> conditions_for = reader.ReadConditions();


// static void PrintDictionary(Dictionary<string, HashSet<string>> dictionary)
//     {
//         foreach (var kvp in dictionary)
//         {
//             Console.WriteLine($"Key: {kvp.Key}");
//             Console.WriteLine("Values:");

//             foreach (var value in kvp.Value)
//             {
//                 Console.WriteLine($"  {value}");
//             }

//             Console.WriteLine(""); // Separate key-value pairs with an empty line
//         }
//     }
Console.WriteLine(string.Join(", ", activities));
// PrintDictionary(conditions_for);