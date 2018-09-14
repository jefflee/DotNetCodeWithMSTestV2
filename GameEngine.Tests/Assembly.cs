using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Tests
{
    [TestClass]
    public class Assembly
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Console.WriteLine("AssemblyInit");
        }

        [AssemblyCleanup]
        public static void AssemblyClean()
        {
            Console.WriteLine("AssemblyClean");
        }
    }
}
