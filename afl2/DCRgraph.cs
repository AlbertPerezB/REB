namespace DCR;



public class DCRGraph {
    // Events
    protected HashSet<string> events = new HashSet<string>();
        

    // Relations
    private Dictionary<string, HashSet<string>> conditionsFor = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> milestonesFor = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> responsesTo = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> excludesTo = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> includesTo = new Dictionary<string, HashSet<string>>();
    
    // Marking
    public DCRMarking marking;

}