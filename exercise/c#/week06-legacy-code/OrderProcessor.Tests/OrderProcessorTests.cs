namespace OrderProcessor.Tests;

public class OrderProcessorTests
{
    [Fact]
    public void ProcessOrderSuccessfully()
    {
        // Prepare
        var orders = new List<Order>()
        {
            new(OrderStatus.Unprocessed, 7, 100),
            new(OrderStatus.Processed, 7, 100),
            new(OrderStatus.Unprocessed, 3, 100)
        };
        
        // Act
        OrderProcessor.ProcessOrders(orders);

        // Assert
        Assert.All(orders, 
            order => Assert.True(order.IsProcessed));
        
        Assert.Equal(90, orders[0].Total);
        Assert.Equal(100, orders[1].Total);
        Assert.Equal(100, orders[2].Total);
    }
}