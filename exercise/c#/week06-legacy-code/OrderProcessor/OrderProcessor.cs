namespace OrderProcessor;

public class OrderProcessor
{
    public static void ProcessOrders(List<Order> orders) 
        => orders
            .Where(o => o.IsUnProcessed)
            .ToList()
            .ForEach(o => o.Process());
}