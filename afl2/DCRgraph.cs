using System.Dynamic;

namespace DCR;



public class DCRGraph {
    // Events
    public HashSet<string> events = new HashSet<string>();
        

    // Relations
    public Dictionary<string, HashSet<string>> conditions_For = new();
    public Dictionary<string, HashSet<string>> milestones_For = new();
    public Dictionary<string, HashSet<string>> responses_To = new();
    public Dictionary<string, HashSet<string>> excludes_To = new();
    public Dictionary<string, HashSet<string>> includes_To = new();
    
    // Marking
    public DCRMarking marking = new();



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

    public DCRMarking Execute(DCRMarking marking, string activity) {
        // Check if the event exists
        if (!events.Contains(activity)) {
            return marking;
        }

        // Check if the event is enabled
        if (!Enabled(marking, activity)) {
            return marking;
        }

        // Create a new marking
        DCRMarking result = marking.Clone();

        // Add the event to the set of executed events
        result.executed.Add(activity);
    
        // Remove the event from the set of pending events.
        result.pending.Remove(activity);

        // Add all new responses
        result.pending.UnionWith(responses_To[activity]);

        // Remove all excluded events
        result.included.UnionWith(excludes_To[activity]);

        // Add all included events
        result.included.UnionWith(includes_To[activity]);

        return result;
    }
    
    public HashSet<string> getIncludedPending() {
        // Select those included events that are also pending
        HashSet<string> result = new HashSet<string>(marking.included);
        result = (HashSet<string>)result.Intersect(marking.pending);
        return result;
    }

    
    public bool IsAccepting() {
        // Check if there are any included events that are also pending responses
        return getIncludedPending().Count == 0;
    }
}