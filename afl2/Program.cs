using DCR;

//Console.WriteLine("Hello, World!");
//DCRGraph graph = new();
XMLreader reader = new("REB1.xml");
HashSet<string> activities = reader.ReadEvents();
//HashSet<string> activities1 = reader.ReadEvents1();
Dictionary<string, HashSet<string>> conditions_for = reader.ReadConditions();
Dictionary<string, HashSet<string>> milestone_to = reader.ReadMilestones();
Dictionary<string, HashSet<string>> includes_to = reader.ReadIncludes();
Dictionary<string, HashSet<string>> excludes_to = reader.ReadExcludes();





System.Console.WriteLine("Milestones:\n");
PrintDictionary(milestone_to);

System.Console.WriteLine("Includes:\n");
PrintDictionary(includes_to);

System.Console.WriteLine("Excludes:\n");
PrintDictionary(excludes_to);