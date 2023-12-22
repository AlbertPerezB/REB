using System.Diagnostics;
using DCR;


static string CurrentDirectory() {
    if (!Debugger.IsAttached) return Directory.GetCurrentDirectory();
    else return Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
}

string[] file_Names = Directory.GetFiles(Path.Combine(CurrentDirectory(),"Patterns"));

List <string> fileNameList = new List<string>(file_Names);
fileNameList.Sort();

for (int i = 0; i < fileNameList.Count(); i++) {
    ConformanceChecker confchecker = new(fileNameList[i], Path.Combine(CurrentDirectory(),"log.csv"));
    string name = Path.GetFileNameWithoutExtension(fileNameList[i]);
    (int ok_count, int failed_count) = confchecker.CheckConformity();
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

