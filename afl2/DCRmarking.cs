namespace DCR;

public class DCRMarking {
    public HashSet<string> executed = new HashSet<string>();
    public HashSet<string> included = new HashSet<string>();
    public HashSet<string> pending = new HashSet<string>();
}