using System.Dynamic;

namespace DCR;



public class DCRGraph {
    // Events
    protected HashSet<string> events = new HashSet<string>();
        

    // Relations
    private Dictionary<string, HashSet<string>> conditions_For = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> milestones_For = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> responses_To = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> excludes_To = new Dictionary<string, HashSet<string>>();
    private Dictionary<string, HashSet<string>> includes_To = new Dictionary<string, HashSet<string>>();
    
    // Marking
    public DCRMarking? marking;



    public bool Enabled(DCRMarking marking, string activity) {
        // Open world assumption
        if (!events.Contains(activity)) return true;

        // Check included
        if (!marking.included.Contains(activity)) return false;

        // Filter only included conditions
        HashSet<string> included_Conditions = new HashSet<string>(conditions_For[activity]); // All conditions 
        included_Conditions = (HashSet<string>) included_Conditions.Intersect(marking.included); 
        
        // Return false if all conditions have not been executed
        if (!included_Conditions.All(marking.executed.Contains)) return false;

        // Select only included milestones
        HashSet<string> included_Milestones = new HashSet<string>(milestones_For[activity]); // All milestones 
        included_Milestones = (HashSet<string>) included_Milestones.Intersect(marking.included); 
        // Check if any included milestone has a pending response
        foreach (string  p in marking.pending) {
            if (included_Milestones.Contains(p)) return false ;
        }
        return true;
    }
}
