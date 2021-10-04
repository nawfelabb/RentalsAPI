using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace Rentals.Tests.Base
{
    public abstract class BaseTestClass
    {
        protected IFixture _fixture;

        protected BaseTestClass()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected Mock<TInterface> GetMock<TInterface>() where TInterface : class
        {
            return _fixture.Freeze<Mock<TInterface>>();
        }
    }

    public abstract class BaseTestClass<T> : BaseTestClass where T : class
    {
        private T _sut;
        protected T Sut => _sut ??= _fixture.Create<T>();
    }
}
