namespace coffee_van;

public class Coffee
{
    public string Type { get; set; }
    public string State { get; set; }
    public decimal Cost { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }

    public Coffee(string type, string state, decimal cost, double weight, double volume)
    {
        Type = type;
        State = state;
        Cost = cost;
        Weight = weight;
        Volume = volume;
        
    }
}