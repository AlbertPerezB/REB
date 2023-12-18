namespace DCR;

using System;
using System.Xml;

public class XMLreader {
    // XML document we read from later on. Loaded from file in constructor.
    private readonly XmlDocument xml_doc = new();
    // Dictionary mapping event label to event id.
    private Dictionary<string, string> label_mapping = new();

    // Constructor. Loads xml file from path
    public XMLreader(string xml_path) {
        try {
            xml_doc.Load(xml_path);
        }
        catch (Exception ex) {Console.WriteLine ("Error: " + ex.Message);}
    }
    
    // Method for reading events from XML document
    public HashSet<string> ReadEvents() {
        HashSet<string> activities = new HashSet<string>();
        XmlNodeList? event_nodes = xml_doc.SelectNodes("//labelMappings");
        if (event_nodes != null) {
            foreach (XmlNode event_node in event_nodes) {
                    if (event_node.Attributes != null) {
                        Console.WriteLine(string.Join(", ", event_node.Attributes));
                        XmlAttribute? name = event_node.Attributes["labelId"];
                        XmlAttribute? id = event_node.Attributes["eventId"];
                        Console.WriteLine("name: " + name);
                        Console.WriteLine("id: " + id);
                        if (name != null && id != null) {
                            string activity_name = name.Value;
                            string activity_id = id.Value;
                            // Add label to HashSet for later use.
                            activities.Add(activity_name);
                            // Add mapping to private dictionary to "translate" id's.
                            label_mapping.Add(activity_id, activity_name);
                        }
                    }
            }
        }
        return activities;
    }
    
    // // Method for reading conditions from XML document
    // public Dictionary<string, HashSet<string>> ReadConditions() {
    //     Dictionary<string, HashSet<string>> conditions_For = new();
    //     XmlNodeList? condition_nodes = xml_doc.SelectNodes("//conditions");
    //     if (condition_nodes != null) {
    //         foreach (XmlNode condition_node in condition_nodes) {
    //                 if (condition_node.Attributes != null) {
    //                     XmlAttribute? source = condition_node.Attributes["sourceId"];
    //                     XmlAttribute? target = condition_node.Attributes["targetId"];
    //                     if (source != null && target != null) {
    //                         string source_name = label_mapping[source.Value];

    //                         // If the source is not already in dictionary, create <string, hashset> entry.
    //                         if (!conditions_For.ContainsKey(source_name)) { 
    //                             //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
    //                             System.Console.WriteLine("source doesn't exist");
    //                             HashSet<string> target_name = new();
    //                             target_name.Add(label_mapping[target.Value]);
    //                             conditions_For.Add(source_name, target_name);
    //                         } else { // If source already exists, add target name to HashSet in dictionary.
    //                             System.Console.WriteLine("source already exists");
    //                             string target_name = target.Value;
    //                             conditions_For[source_name].Add(target_name);
    //                         }
    //                     }
    //                 }
    //         }
    //     }
    //     return conditions_For;
    // }

    // // Method for reading conditions from XML document
    // public Dictionary<string, HashSet<string>> ReadMilestones() {
    //     Dictionary<string, HashSet<string>> milestones_For = new();
    //     XmlNodeList? milestone_nodes = xml_doc.SelectNodes("//milestones");
    //     if (milestone_nodes != null) {
    //         foreach (XmlNode milestone_node in milestone_nodes) {
    //                 if (milestone_node.Attributes != null) {
    //                     XmlAttribute? source = milestone_node.Attributes["sourceId"];
    //                     XmlAttribute? target = milestone_node.Attributes["targetId"];
    //                     if (source != null && target != null) {
    //                         string source_name = label_mapping[source.Value];

    //                         // If the source is not already in dictionary, create <string, hashset> entry.
    //                         if (!milestones_For.ContainsKey(source_name)) { 
    //                             HashSet<string> target_name = new();
    //                             target_name.Add(label_mapping[target.Value]);
    //                             milestones_For.Add(source_name, target_name);
    //                         } else { // If source already exists, add target name to HashSet in dictionary.
    //                             string target_name = target.Value;
    //                             milestones_For[source_name].Add(target_name);
    //                         }
    //                     }
    //                 }
    //         }
    //     }
    //     return milestones_For;
    // }

    // public Dictionary<string, HashSet<string>> ReadExcludes() {
    //     Dictionary<string, HashSet<string>> excludes_To = new();
    //     XmlNodeList? exclusion_nodes = xml_doc.SelectNodes("//excludes");
    //     if (exclusion_nodes != null) {
    //         foreach (XmlNode exclusion_node in exclusion_nodes) {
    //                 if (exclusion_node.Attributes != null) {
    //                     XmlAttribute? source = exclusion_node.Attributes["sourceId"];
    //                     XmlAttribute? target = exclusion_node.Attributes["targetId"];
    //                     if (source != null && target != null) {
    //                         string source_name = label_mapping[source.Value];

    //                         // If the source is not already in dictionary, create <string, hashset> entry.
    //                         if (!excludes_To.ContainsKey(source_name)) { 
    //                             //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
    //                             HashSet<string> target_name = new();
    //                             target_name.Add(label_mapping[target.Value]);
    //                             excludes_To.Add(source_name, target_name);
    //                         } else { // If source already exists, add target name to HashSet in dictionary.
    //                             string target_name = target.Value;
    //                             excludes_To[source_name].Add(target_name);
    //                         }
    //                     }
    //                 }
    //         }
    //     }
    //     return excludes_To;
    // }
}