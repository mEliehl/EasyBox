using Xunit;

namespace CompositionRoot.Test
{
    public class ApplicationBootstrapTest
    {
        [Fact]
        public void Should_Return_Dependency_Injection_Container_Correctly_Configured()
        {
            var container = ApplicationBootstrap.Compose();
            container.Verify();
        }
    }
}
