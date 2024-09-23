namespace lab1_coffee_van;
// Назва (наприклад, зернова кава, мелене тощо)
// Фізичний стан (зерна, мелена, розчинна в банках або пакетиках)
// Ціна
//     Вага
// Об’єм упаковки
// Якість

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