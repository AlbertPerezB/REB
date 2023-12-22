namespace DCR;

public class DCRMarking {
    public HashSet<string> executed = new();
    public HashSet<string> included = new();
    public HashSet<string> pending = new();

    public DCRMarking Clone(){
        return (DCRMarking)MemberwiseClone();
    }

    public void printDCR() {
        System.Console.WriteLine("included");
        Console.WriteLine(string.Join(", ", included));
    }
}