using Xunit;

namespace CompositionRoot.Test
{
    public class CompositionRootBootstrapTest
    {
        [Fact]
        public void ShouldReturnDependencyInjectionContainerCorrectlyConfiguredForRavenDB()
        {
            var container = CompositionRootBootstrap.BuildForRavenDB();
            container.Verify();
        }

        [Fact]
        public void ShouldReturnDependencyInjectionContainerCorrectlyConfiguredForRavenInMemoryDB()
        {
            var container = CompositionRootBootstrap.BuildForInMemoryDB();
            container.Verify();
        }
    }
}
