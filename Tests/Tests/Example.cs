using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Tests
{
    /*
     * Each Test class should only test one fixture class. If there exist 3 fixture class there should be 3 test classes.
     * Each test class should end in 'Tests'. Each method that does testing, should start with 'Test'.
     * There should be a Test Initializer and Cleanup in each test class.
     * e.g
     */


    [TestClass]
    public class SomeClassToTestTests
    {
        [TestInitialize]
        public void Init()
        {

        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void TestSomePrivateMethodThatNeedTesting()
        {
            // ...
        }
    }
    
}
