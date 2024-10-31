namespace OrderProcessor;

public class Order(OrderStatus status, int numberOfItems, double total)
{
    private const int MinItemsForDiscount = 5;
    private const double Discount = 0.9;
    
    private OrderStatus _status = status;
    
    public double Total { get; private set; } = total;
    public bool IsProcessed => _status == OrderStatus.Processed;
    public bool IsUnProcessed => !IsProcessed;
    
    public void Process()
    {
        if (IsEligibleForDiscount) 
            ApplyBulkDiscount();
        
        _status = OrderStatus.Processed;
    }
    private void ApplyBulkDiscount() => Total *= Discount;
    private bool IsEligibleForDiscount => numberOfItems > MinItemsForDiscount;
}