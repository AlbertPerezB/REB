using DCR;

// Put XML files here
string folder_Path = "Patterns";

string[] file_Names = Directory.GetFiles(folder_Path);

for (int i = 0; i < file_Names.Count(); i++) {
    ConformanceChecker confchecker = new(file_Names[i], "log.csv");
    (int ok_count, int failed_count) = confchecker.CheckConformity();
    string name = Path.GetFileNameWithoutExtension(file_Names[i]);
    Console.WriteLine($"{name} | Failed count {failed_count} | Ok count {ok_count}");
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

