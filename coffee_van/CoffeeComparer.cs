using System.Xml.Linq;
public class CoffeeComparer : IComparer<XElement>
{
    public int Compare(XElement? x, XElement? y)
    {
        if (x == null || y == null)
            throw new InvalidOperationException();
        return CoffeeManager.PricePerWeight(x).CompareTo(CoffeeManager.PricePerWeight(y));
    }
}