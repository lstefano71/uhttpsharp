using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace uhttpsharp.Tests
{
    public class HttpMethodProviderCacheTests
    {
        private const string MethodName = "Hello World";

        private static IHttpMethodProvider GetTarget(IHttpMethodProvider child)
        {
            return new HttpMethodProviderCache(child);
        }

        [Test]
        public void Should_Call_Child_With_Right_Parameters()
        {
            // Arrange
            var mock = Substitute.For<IHttpMethodProvider>();
            var target = GetTarget(mock);

            // Act
            target.Provide(MethodName);

            // Assert
            mock.Received(1).Provide(MethodName);
        }

        [Test]
        public void Should_Return_Same_Child_Value()
        {
            // Arrange
            const HttpMethods expectedMethod = HttpMethods.Post;

            var mock = Substitute.For<IHttpMethodProvider>();
            mock.Provide(MethodName).Returns(expectedMethod);
            var target = GetTarget(mock);


            // Act
            var actual = target.Provide(MethodName);

            // Assert
            actual.ShouldBe(expectedMethod);
        }

        [Test]
        public void Should_Cache_The_Value()
        {
            // Arrange
            var mock = Substitute.For<IHttpMethodProvider>();
            var target = GetTarget(mock);

            // Act
            target.Provide(MethodName);
            target.Provide(MethodName);
            target.Provide(MethodName);

            // Assert
            mock.Received(1).Provide(MethodName);
        }

    }
}
