namespace DCR;

using System;
using System.Xml;
using System.Linq;

public class XMLreader {
    // XML document we read from later on. Loaded from file in constructor.
    private readonly XmlDocument xml_doc = new();
    // Dictionary mapping event label to event id.
    public Dictionary<string, string> label_mapping = new();
    public Dictionary<string, HashSet<string>> groups = new();

    // Constructor. Loads xml file from path
    public XMLreader(string xml_path) {
        try {
            xml_doc.Load(xml_path);
        }
        catch (Exception ex) {Console.WriteLine ("Error: " + ex.Message);}
    }
  
    // Method for reading events from XML document
    public HashSet<string> ReadMappings() {
        HashSet<string> activities = new HashSet<string>();
        XmlNodeList? event_nodes = xml_doc.SelectNodes("//labelMapping");
        if (event_nodes != null) {
            foreach (XmlNode event_node in event_nodes) {
                    if (event_node.Attributes != null) {
                        //Console.WriteLine(event_node.Attributes["labelId"].GetType());
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
        return activities;
    }

    public void ReadGroups() {
        XmlNodeList? group_nodes = xml_doc.SelectNodes("//event[@type='subprocess']");
        if (group_nodes != null) {
            foreach (XmlNode group_node in group_nodes) {
                XmlAttribute? group_id = group_node.Attributes["id"];
                group_name = label_mapping[group_id.Value];
                XmlNodeList? event_nodes = group_node.SelectNodes("//event");
                foreach (XmlNode event_node in event_nodes) {
                
                XmlAttribute? id = event_node.Attributes["id"];

                }

                if (name != null && id != null) {
                    string activity_name = name.Value;
                    string activity_id = id.Value;
                    
                    if (!groups.ContainsKey(activity_name)) { 
                        //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
                        HashSet<string> target_name = new();
                        target_name.Add(label_mapping[target.Value]);
                        conditions_For.Add(source_name, target_name);
                    } else { // If source already exists, add target name to HashSet in dictionary.
                        string target_name = label_mapping[target.Value];
                        conditions_For[source_name].Add(target_name);
                    }
                }
            }
            }
        }
    }
    
    // Method for reading conditions from XML document
    public Dictionary<string, HashSet<string>> ReadConditions() {
        Dictionary<string, HashSet<string>> conditions_For = new();
        XmlNodeList? condition_nodes = xml_doc.SelectNodes("//condition");
        if (condition_nodes != null) {
            foreach (XmlNode condition_node in condition_nodes) {
                    if (condition_node.Attributes != null) {
                        XmlAttribute? source = condition_node.Attributes["sourceId"];
                        XmlAttribute? target = condition_node.Attributes["targetId"];
                        if (source != null && target != null) {
                            string source_name = label_mapping[source.Value];

                            // If the source is not already in dictionary, create <string, hashset> entry.
                            if (!conditions_For.ContainsKey(source_name)) { 
                                //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
                                HashSet<string> target_name = new();
                                target_name.Add(label_mapping[target.Value]);
                                conditions_For.Add(source_name, target_name);
                            } else { // If source already exists, add target name to HashSet in dictionary.
                                string target_name = label_mapping[target.Value];
                                conditions_For[source_name].Add(target_name);
                            }
                        }
                    }
            }
        }
        return conditions_For;
    }

    // Method for reading milestones from XML document
    public Dictionary<string, HashSet<string>> ReadMilestones() {
        Dictionary<string, HashSet<string>> milestones_For = new();
        XmlNodeList? milestone_nodes = xml_doc.SelectNodes("//milestone");
        if (milestone_nodes != null) {
            foreach (XmlNode milestone_node in milestone_nodes) {
                    if (milestone_node.Attributes != null) {
                        XmlAttribute? source = milestone_node.Attributes["sourceId"];
                        XmlAttribute? target = milestone_node.Attributes["targetId"];
                        if (source != null && target != null) {
                            string source_name = label_mapping[source.Value];

                            // If the source is not already in dictionary, create <string, hashset> entry.
                            if (!milestones_For.ContainsKey(source_name)) { 
                                HashSet<string> target_name = new();
                                target_name.Add(label_mapping[target.Value]);
                                milestones_For.Add(source_name, target_name);
                            } else { // If source already exists, add target name to HashSet in dictionary.
                                string target_name = label_mapping[target.Value];
                                milestones_For[source_name].Add(target_name);
                            }
                        }
                    }
            }
        }
        return milestones_For;
    }

    // Method for reading exclusions from XML document
    public Dictionary<string, HashSet<string>> ReadExcludes() {
        Dictionary<string, HashSet<string>> excludes_To = new();
        XmlNodeList? exclusion_nodes = xml_doc.SelectNodes("//exclude");
        if (exclusion_nodes != null) {
            foreach (XmlNode exclusion_node in exclusion_nodes) {
                    if (exclusion_node.Attributes != null) {
                        XmlAttribute? source = exclusion_node.Attributes["sourceId"];
                        XmlAttribute? target = exclusion_node.Attributes["targetId"];
                        if (source != null && target != null) {
                            string source_name = label_mapping[source.Value];

                            // If the source is not already in dictionary, create <string, hashset> entry.
                            if (!excludes_To.ContainsKey(source_name)) { 
                                //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
                                HashSet<string> target_name = new();
                                target_name.Add(label_mapping[target.Value]);
                                excludes_To.Add(source_name, target_name);
                            } else { // If source already exists, add target name to HashSet in dictionary.
                                string target_name = label_mapping[target.Value];
                                excludes_To[source_name].Add(target_name);
                            }
                        }
                    }
            }
        }
        return excludes_To;
    }

    // Method for reading inclusions from XML document
    public Dictionary<string, HashSet<string>> ReadIncludes() {
        Dictionary<string, HashSet<string>> includes_To = new();
        XmlNodeList? inclusion_nodes = xml_doc.SelectNodes("//include");
        if (inclusion_nodes != null) {
            foreach (XmlNode inclusion_node in inclusion_nodes) {
                    if (inclusion_node.Attributes != null) {
                        XmlAttribute? source = inclusion_node.Attributes["sourceId"];
                        XmlAttribute? target = inclusion_node.Attributes["targetId"];
                        if (source != null && target != null) {
                            string source_name = label_mapping[source.Value];

                            // If the source is not already in dictionary, create <string, hashset> entry.
                            if (!includes_To.ContainsKey(source_name)) { 
                                //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
                                HashSet<string> target_name = new();
                                target_name.Add(label_mapping[target.Value]);
                                includes_To.Add(source_name, target_name);
                            } else { // If source already exists, add target name to HashSet in dictionary.
                                string target_name = label_mapping[target.Value];
                                includes_To[source_name].Add(target_name);
                            }
                        }
                    }
            }
        }
        return includes_To;
    }

    // Method for reading responses from XML document
    public Dictionary<string, HashSet<string>> ReadResponses() {
        Dictionary<string, HashSet<string>> responses_To = new();
        XmlNodeList? response_nodes = xml_doc.SelectNodes("//response");
        if (response_nodes != null) {
            foreach (XmlNode response_node in response_nodes) {
                    if (response_node.Attributes != null) {
                        XmlAttribute? source = response_node.Attributes["sourceId"];
                        XmlAttribute? target = response_node.Attributes["targetId"];
                        if (source != null && target != null) {
                            string source_name = label_mapping[source.Value];

                            // If the source is not already in dictionary, create <string, hashset> entry.
                            if (!responses_To.ContainsKey(source_name)) { 
                                //conditions_For[source_name] = new HashSet<string> { label_mapping[target.Value] };
                                HashSet<string> target_name = new();
                                target_name.Add(label_mapping[target.Value]);
                                responses_To.Add(source_name, target_name);
                            } else { // If source already exists, add target name to HashSet in dictionary.
                                string target_name = label_mapping[target.Value];
                                responses_To[source_name].Add(target_name);
                            }
                        }
                    }
            }
        }
        return responses_To;
    }

    // Read the markings of the events from XML document. Returns a DCRMarking object.
    public DCRMarking ReadMarkings(){
        DCRMarking markings = new();
        // Access the executed events
        XmlNodeList? executed_nodes = xml_doc.SelectNodes("//executed/event");
        if (executed_nodes != null) {
            foreach (XmlNode executed_node in executed_nodes) {
                    if (executed_node.Attributes != null) {
                        XmlAttribute? id = executed_node.Attributes["id"];
                        if (id != null) {
                            string name = label_mapping[id.Value];
                            markings.executed.Add(name);
                        }
                    }
            }
        }

        // Access the included events
        XmlNodeList? included_nodes = xml_doc.SelectNodes("//included/event");
        if (included_nodes != null) {
            foreach (XmlNode included_node in included_nodes) {
                    if (included_node.Attributes != null) {
                        XmlAttribute? id = included_node.Attributes["id"];
                        if (id != null) {
                            string name = label_mapping[id.Value];
                            markings.included.Add(name);
                        }
                    }
            }
        }

        // Access the pending events
        XmlNodeList? pending_nodes = xml_doc.SelectNodes("//pendingResponses/event");
        if (pending_nodes != null) {
            foreach (XmlNode pending_node in pending_nodes) {
                    if (pending_node.Attributes != null) {
                        XmlAttribute? id = pending_node.Attributes["id"];
                        if (id != null) {
                            string name = label_mapping[id.Value];
                            markings.pending.Add(name);
                        }
                    }
            }
        }
        return markings;
    }

    public void ProcessXML (){
        ReadMappings();
        ReadGroups();
        ReadConditions();
        ReadMilestones();
        ReadExcludes();
        ReadIncludes();
        ReadResponses();
        ReadMarkings();
    }
}