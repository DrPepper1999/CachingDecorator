using NUnit.Framework;


namespace CachingDecorator.Samples.Expressions.Tests
{
    [TestFixture]
    public class CachedCustomProviderTests
    {
        [TestCaseSource(typeof(CustomProviderObjectMother), "GetCustomProviderTestData")]
        public void Test_Calling_Method_Twice_Will_Return_The_Same_Result(string method, object[] arguments)
        {
            // Arrange
            ICustomProvider customProvider = new CachedCustomProvider(new CustomProvider());
            var methodInfo = typeof (ICustomProvider).GetMethod(method);
            Assert.That(methodInfo != null, $"ICustomProvider does not contain method '{method}'");

            // Act
            // Вызываем наш метод дважды подряд
            var task1 = (Task) methodInfo.Invoke(customProvider, arguments);
            var task2 = (Task) methodInfo.Invoke(customProvider, arguments);

            // Assert
            Assert.That(ReferenceEquals(task1, task2),
                    "Two subsequent calls to CachedCustomProvider should return the same objects");
        }
    }
}