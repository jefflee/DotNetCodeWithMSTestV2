using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Tests
{
    [TestClass]
    public class Lifecycle
    {
        static string SomeTestContext;

        [TestInitialize]
        public void LifecycleInit()
        {
            Console.WriteLine("    TestInitialize Lifecycle");
        }

        [TestCleanup]
        public void LifecycleClean()
        {
            Console.WriteLine("    TestCleanup Lifecycle");
        }

        [ClassInitialize]
        public static void LifecycleClassInit(TestContext context)
        {
            Console.WriteLine("  ClassInitialize Lifecycle");

            Console.WriteLine("  data loaded from disk or some expensive object creation");
            SomeTestContext = "42";
        }

        [ClassCleanup]
        public static void LifecycleClassClean()
        {
            Console.WriteLine("  ClassCleanup Lifecycle");
        }


        [TestMethod]
        public void TestA()
        {
            Console.WriteLine("      Test A starting");
            Console.WriteLine($"      Shared test context: {SomeTestContext}");
        }


        [TestMethod]
        public void TestB()
        {
            Console.WriteLine("      Test B starting");
            Console.WriteLine($"      Shared test context: {SomeTestContext}");
        }
    }
}
