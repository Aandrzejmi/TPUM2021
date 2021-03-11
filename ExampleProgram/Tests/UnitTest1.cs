using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        Program.ExampleClass obj;

        [SetUp]
        public void Setup()
        {
            obj = new Program.ExampleClass("Test");
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(obj.Text, "Test");
        }

        [Test]
        public void Test2()
        {
            Assert.IsTrue(obj.Text is string);
        }
    }
}