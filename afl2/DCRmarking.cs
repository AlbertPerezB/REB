namespace DCR;

public class DCRMarking {
    public HashSet<string> executed = new();
    public HashSet<string> included = new();
    public HashSet<string> pending = new();

    public DCRMarking Clone(){
        return (DCRMarking)MemberwiseClone();
    }
}