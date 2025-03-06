using CachingDecorator.Expressions;
using CrazyStuff.Samples.Expressions;

namespace CachingDecorator.Samples.Expressions
{
    public class CustomProvider : ICustomProvider
    {
        private readonly Dictionary<MethodCallInfo, Task> _outstandingTasks = new Dictionary<MethodCallInfo, Task>();
        private readonly object _syncRoot = new object();
        private static int _id;

        public Task<int> GetNextId()
        {
            return Task.Run(() =>
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        return Interlocked.Increment(ref _id);
                    });
        }

        public Task<string> GetCustomerName(int id)
        {
            return Task.Run(() =>
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        return id.ToString();
                    });
        }

        public Task<Order> GetOrder(int orderId, string customerName)
        {
            Console.WriteLine("GetOrder");
            return Task.Run(() =>
                    {
                        // Эмулируем длительную операцию
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        return new Order(orderId, customerName);
                    });
        }

    }
}