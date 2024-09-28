namespace coffee_van;

public class Coffee
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string State { get; set; }
    public double Cost { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public double Quality { get; set; }

    Coffee(string name, string type, string state, double cost, double weight, double volume, double quality)
    {
        Name = name;
        Type = type;
        State = state;
        Cost = cost;
        Weight = weight;
        Volume = volume;
        Quality = quality;
    }

    public double PricePerWeight => Cost / Weight;
    //вказувати вагу в грамах
    //обчислювати на 100г

    public override string ToString()
    {
        return $"Name: {Name}\nType: {Type}]\nState: {State}\nCost: {Cost}\nWeight: {Weight}\nVolume: {Volume}";
    }
}