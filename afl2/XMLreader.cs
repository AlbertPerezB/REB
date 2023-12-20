namespace DCR;

using System;
using System.Xml;
using System.Linq;

// XML reader class creates DCRGraph from XML file.
public class XMLreader {
    // XML document we read from later on. Loaded from file in constructor.
    private readonly XmlDocument xml_doc = new();

    // Dictionary mapping event label to event id.
    private Dictionary<string, string> label_mapping = new();
    
    //Dictionary to keep track of groups (groupname, group members)
    public Dictionary<string, HashSet<string>> groups = new();

    // Constructor. Loads xml file from path
    public XMLreader(string xml_path) {
        try {
            xml_doc.Load(xml_path);
        } catch (Exception ex) {
            Console.WriteLine ("Error: " + ex.Message);
        }
    }
  
    // Method for reading events from XML document
    private void ReadMappingsAndEvents(HashSet<string> activities) {
        XmlNodeList? event_nodes = xml_doc.SelectNodes("//labelMapping");
        if (event_nodes != null) {
            foreach (XmlNode event_node in event_nodes) {
                if (event_node.Attributes != null) {
                    XmlAttribute? name = event_node.Attributes["labelId"];
                    XmlAttribute? id = event_node.Attributes["eventId"];
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
    }

    // Fills out dictionary to map groups to group members.
    private void ReadGroups() {
        XmlNodeList? group_nodes = xml_doc.SelectNodes("//event[@type='subprocess']");
        if (group_nodes != null) {
            foreach (XmlNode group_node in group_nodes) {
                HashSet<string> events = new(); // Placeholder for group's events
                
                // Extract name of group
                XmlAttribute? group_id = group_node.Attributes?["id"]; 
                if (group_id != null) {
                    string group_name = label_mapping[group_id.Value];

                    // Iterate over each event in the group
                    XmlNodeList? event_nodes = group_node.ChildNodes;
                    if (event_nodes != null) {
                        foreach (XmlNode event_node in event_nodes) {
                            // Get name of each sub event
                            XmlAttribute? event_id = event_node.Attributes?["id"];
                            if (event_id != null) {
                                string event_name = label_mapping[event_id.Value];
                                events.Add(event_name);
                            }
                        }
                    }
                groups.Add(group_name, events);
                }
            }
        } else {
            Console.WriteLine("No groups");
        }
    }
    
    // Method for adding a relation to a dictionary checking if the source is already in dictionary.
    private void AddRelation(Dictionary<string, HashSet<string>> result_dict, string source_name, 
                        string target_name) {
        // If the source is not already in dictionary, create <string, hashset> entry.
        if (!result_dict.ContainsKey(source_name)) { 
            HashSet<string> targets = new() {target_name};
            result_dict.Add(source_name, targets);
        } else { // If source already exists, add target name to HashSet in dictionary.
            result_dict[source_name].Add(target_name);
        }
    }

    // Method for reading relations from XML document and adding it do a given dictionary.
    private void ReadRelation(
                    Dictionary<string, HashSet<string>> relation_dict, string node_address) {
        XmlNodeList? relation_nodes = xml_doc.SelectNodes(node_address);
        if (relation_nodes != null) {
            foreach (XmlNode relation_node in relation_nodes) {
                if (relation_node.Attributes != null) {
                    XmlAttribute? source = relation_node.Attributes["sourceId"];
                    XmlAttribute? target = relation_node.Attributes["targetId"];
                    if (source != null && target != null) {
                        string source_name = label_mapping[source.Value];
                        string target_name = label_mapping[target.Value];
                        // If the source is a group head
                        if (groups.ContainsKey(source_name)) {
                            // Add relation for each group member
                            foreach (string group_member in groups[source_name]) {
                                AddRelation(relation_dict, group_member, target_name);
                            }
                        // If the target it is group head
                        } else if (groups.ContainsKey(target_name)) {
                                foreach (string group_member in groups[target_name]) {
                                    AddRelation(relation_dict, source_name, group_member);
                                }
                        } else {
                            AddRelation(relation_dict, source_name, target_name);
                        }
                    }
                }
            }
        } else {
            Console.WriteLine("No nodes found at {node_address}");
        }
    }

    // Add the marked events to given HashShet
    private void AddMarking (HashSet<string> marking, string node_address){
        XmlNodeList? marked_nodes = xml_doc.SelectNodes(node_address);
        if (marked_nodes != null) {
            foreach (XmlNode marked_node in marked_nodes) {
                if (marked_node.Attributes != null) {
                    XmlAttribute? id = marked_node.Attributes["id"];
                    if (id != null) {
                        string name = label_mapping[id.Value];
                        marking.Add(name);
                    }
                }
            }
        }
    }

    // Read the markings of the events from XML document and update DCRMarking object.
    private void ReadMarkings(DCRMarking markings){
        AddMarking(markings.executed, "//executed/event");
        AddMarking(markings.included, "//included/event");
        AddMarking(markings.pending, "//pendingResponses/event");

    }

    // Call the other methods
    public DCRGraph ProcessXML () {
        DCRGraph dcr_graph = new();

        ReadMappingsAndEvents(dcr_graph.events);
        ReadGroups();
        // In the set of events, delete the ones that are actually groups.
        dcr_graph.events = dcr_graph.events.Where(activity => !groups.ContainsKey(activity)).ToHashSet();

        ReadRelation(dcr_graph.conditions_For, "//condition");
        ReadRelation(dcr_graph.milestones_For, "//milestone");
        ReadRelation(dcr_graph.excludes_To, "//exclude");
        ReadRelation(dcr_graph.excludes_To, "//exclude");
        ReadRelation(dcr_graph.includes_To, "//include");
        ReadRelation(dcr_graph.responses_To, "//response");
        ReadMarkings(dcr_graph.marking);
        return dcr_graph;
    }
}