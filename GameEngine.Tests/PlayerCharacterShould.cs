using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
    [TestClass]
    public class PlayerCharacterShould
    {
        [TestMethod]
        [TestCategory("Player Defaults")]
        public void BeInexperiencedWhenNew()
        {
            var sut = new PlayerCharacter();

            Assert.IsTrue(sut.IsNoob);
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        public void NotHaveNickNameByDefault()
        {
            var sut = new PlayerCharacter();

            Assert.IsNull(sut.Nickname);
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        public void StartWithDefaultHealth()
        {
            var sut = new PlayerCharacter();

            Assert.AreEqual(100, sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        public void TakeDamage()
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(1);

            Assert.AreEqual(99, sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        public void TakeDamage_NotEqual()
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(1);

            Assert.AreNotEqual(100, sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        [TestCategory("Another Category")]
        public void IncreaseHealthAfterSleeping()
        {
            var sut = new PlayerCharacter();

            sut.Sleep(); // Expect increase between 1 to 100 inclusive

            Assert.IsTrue(sut.Health >= 101 && sut.Health <= 200);
        }

        [TestMethod]
        public void CalculateFullName()
        {
            var sut = new PlayerCharacter();

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.AreEqual("SARAH SMITH", sut.FullName, true);
        }

        [TestMethod]
        public void HaveFullNameStartingWithFirstName()
        {
            var sut = new PlayerCharacter();

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            //Assert.IsTrue(sut.FullName.StartsWith("Sarah"));

            StringAssert.StartsWith(sut.FullName, "Sarah");
        }

        [TestMethod]
        public void HaveFullNameEndingWithLastName()
        {
            var sut = new PlayerCharacter();

            sut.LastName = "Smith";

            StringAssert.EndsWith(sut.FullName, "Smith");
        }

        [TestMethod]
        public void CalculateFullName_SubstringAssertExample()
        {
            var sut = new PlayerCharacter();

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            StringAssert.Contains(sut.FullName, "ah Sm");
        }

        [TestMethod]
        public void CalculateFullNameWithTitleCase()
        {
            var sut = new PlayerCharacter();

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            StringAssert.Matches(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            //StringAssert.DoesNotMatch(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        }

        [TestMethod]
        public void HaveALongBow()
        {
            var sut = new PlayerCharacter();

            CollectionAssert.Contains(sut.Weapons, "Long Bow");
        }

        [TestMethod]
        public void NotHaveAStaffOfWonder()
        {
            var sut = new PlayerCharacter();

            CollectionAssert.DoesNotContain(sut.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {
            var sut = new PlayerCharacter();

            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            CollectionAssert.AreEqual(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveAllExpectedWeapons_AnyOrder()
        {
            var sut = new PlayerCharacter();

            var expectedWeapons = new[]
            {
                "Short Bow",
                "Long Bow",
                "Short Sword"
            };

            CollectionAssert.AreEquivalent(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveNoDuplicateWeapons()
        {
            var sut = new PlayerCharacter();

            //sut.Weapons.Add("Short Bow");

            CollectionAssert.AllItemsAreUnique(sut.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneKindOfSword()
        {
            var sut = new PlayerCharacter();

            Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));
            // custom assert later
        }

        [TestMethod]
        public void HaveNoEmptyDefaultWeapons()
        {
            var sut = new PlayerCharacter();

            Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
            // custom assert later
        }
    }
}
