using Xunit;

namespace CompositionRoot.Test
{
    public class ApplicationBootstrapTest
    {
        [Fact]
        public void ShouldReturnDependencyInjectionContainerCorrectlyConfigured()
        {
            var container = ApplicationBootstrap.Compose();
            container.Verify();
        }
    }
}
