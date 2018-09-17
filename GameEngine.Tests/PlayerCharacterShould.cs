using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngine.Tests
{
    [TestClass]
    public class PlayerCharacterShould
    {
        private PlayerCharacter _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new PlayerCharacter
            {
                FirstName = "Sarah",
                LastName = "Smith"
            };
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        //[Ignore]
        public void BeInexperiencedWhenNew()
        {
            Assert.IsTrue(_sut.IsNoob);
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        //[Ignore("Temporarily disabled for refactoring")]
        public void NotHaveNickNameByDefault()
        {
            Assert.IsNull(_sut.Nickname);
        }

        [TestMethod]
        [TestCategory("Player Defaults")]
        public void StartWithDefaultHealth()
        {
            Assert.AreEqual(100, _sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        public void TakeDamage()
        {
            _sut.TakeDamage(1);

            Assert.AreEqual(99, _sut.Health);
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
            _sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, _sut.Health);
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
            _sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, _sut.Health);
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
            _sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, _sut.Health);
        }

        [DataTestMethod]
        [DynamicData(nameof(DamageData.GetDamages),
            typeof(DamageData),
            DynamicDataSourceType.Method)]
        [TestCategory("Player Health")]
        public void TakeDamage_DataDriven_FromClass(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, _sut.Health);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalHealthDamageTestData.TestData),
            typeof(ExternalHealthDamageTestData))]
        [TestCategory("Player Health")]
        public void TakeDamage_FromCsv(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, _sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        public void TakeDamage_NotEqual()
        {
            _sut.TakeDamage(1);

            Assert.AreNotEqual(100, _sut.Health);
        }

        [TestMethod]
        [TestCategory("Player Health")]
        [TestCategory("Another Category")]
        public void IncreaseHealthAfterSleeping()
        {
            _sut.Sleep(); // Expect increase between 1 to 100 inclusive

            Assert.IsTrue(_sut.Health >= 101 && _sut.Health <= 200);
        }

        [TestMethod]
        public void CalculateFullName()
        {
            Assert.AreEqual("SARAH SMITH", _sut.FullName, true);
        }

        [TestMethod]
        public void HaveFullNameStartingWithFirstName()
        {

            //Assert.IsTrue(sut.FullName.StartsWith("Sarah"));

            StringAssert.StartsWith(_sut.FullName, "Sarah");
        }

        [TestMethod]
        public void HaveFullNameEndingWithLastName()
        {
            StringAssert.EndsWith(_sut.FullName, "Smith");
        }

        [TestMethod]
        public void CalculateFullName_SubstringAssertExample()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            StringAssert.Contains(_sut.FullName, "ah Sm");
        }

        [TestMethod]
        public void CalculateFullNameWithTitleCase()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            StringAssert.Matches(_sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            //StringAssert.DoesNotMatch(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        }

        [TestMethod]
        public void HaveALongBow()
        {
            CollectionAssert.Contains(_sut.Weapons, "Long Bow");
        }

        [TestMethod]
        public void NotHaveAStaffOfWonder()
        {
            CollectionAssert.DoesNotContain(_sut.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new[]
              {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            CollectionAssert.AreEqual(expectedWeapons, _sut.Weapons);
        }

        [TestMethod]
        public void HaveAllExpectedWeapons_AnyOrder()
        {
            var expectedWeapons = new[]
             {
                "Short Bow",
                "Long Bow",
                "Short Sword"
            };

            CollectionAssert.AreEquivalent(expectedWeapons, _sut.Weapons);
        }

        [TestMethod]
        public void HaveNoDuplicateWeapons()
        {
            //_sut.Weapons.Add("Short Bow");

            CollectionAssert.AllItemsAreUnique(_sut.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneKindOfSword()
        {
            Assert.IsTrue(_sut.Weapons.Any(weapon => weapon.Contains("Sword")));
            // custom assert later
        }

        [TestMethod]
        public void HaveNoEmptyDefaultWeapons()
        {
            Assert.IsFalse(_sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
            // custom assert later
        }
    }
}
