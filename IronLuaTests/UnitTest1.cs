using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronLuaCompiler;
using System;

namespace IronLuaTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = IronLuaParser.Parse(
                "function test()\n" +
                "    x = 10" +
                "end");
            Assert.AreEqual("test--", result);
        }
    }
}
