using System.Collections.Generic;
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
        //[Ignore]
        public void BeInexperiencedWhenNew()
        {
            var sut = new PlayerCharacter();

            Assert.IsTrue(sut.IsNoob);
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        //[Ignore("Temporarily disabled for refactoring")]
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

        [DataTestMethod]
        [DataRow(1, 99)]
        [DataRow(0, 100)]
        [DataRow(100, 1)]
        [DataRow(101, 1)]
        [DataRow(50, 50)]
        [TestCategory("Player Health")]
        public void TakeDamage_DataDriven(int damage, int expectedHealth)
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }

        public static IEnumerable<object[]> Damages
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { 1, 99},
                    new object[] { 0, 100 },
                    new object[] { 100, 1 },
                    new object[] { 101, 1 },
                    new object[] { 50, 50 },
                    new object[] { 40, 60 }
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Damages))]
        [TestCategory("Player Health")]
        public void TakeDamage_DataDriven_FromProperty(int damage, int expectedHealth)
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }

        public static IEnumerable<object[]> GetDamages()
        {
            return new List<object[]>
            {
                new object[] { 1, 99},
                new object[] { 0, 100 },
                new object[] { 100, 1 },
                new object[] { 101, 1 },
                new object[] { 50, 50 },
                new object[] { 10, 90 }
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDamages), DynamicDataSourceType.Method)]
        [TestCategory("Player Health")]
        public void TakeDamage_DataDriven_FromMethod(int damage, int expectedHealth)
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }

        [DataTestMethod]
        [DynamicData(nameof(DamageData.GetDamages),
            typeof(DamageData),
            DynamicDataSourceType.Method)]
        [TestCategory("Player Health")]
        public void TakeDamage_DataDriven_FromClass(int damage, int expectedHealth)
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalHealthDamageTestData.TestData),
            typeof(ExternalHealthDamageTestData))]
        [TestCategory("Player Health")]
        public void TakeDamage_FromCsv(int damage, int expectedHealth)
        {
            var sut = new PlayerCharacter();

            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
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
