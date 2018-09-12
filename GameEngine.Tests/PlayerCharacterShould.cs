using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
    [TestClass]
    public class PlayerCharacterShould
    {
        [TestMethod]
        public void BeInexperiencedWhenNew()
        {
            var sut = new PlayerCharacter();

            Assert.IsTrue(sut.IsNoob);
        }
    }
}
