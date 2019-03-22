using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Fixtures
{
    /*
     * For each class you want to test a fixture class should be created that opens up private methods for testing.
     * Each Fixture class should end in 'Fixture'. All fixture classes should be internal, so that they can only be used from within this project.
     * Any 'T's in the example should be of the corresponding type that the original method has.
     * e.g
     */

    /*
    internal class SomeClassToTestFixture : SomeClassToTest
    {
        new public T SomePrivateMethodThatNeedTesting(T input1, T input2...)
        {
            return base.SomeMethodThatNeedTesting(T input1, T input2...)
        }
    }
    */
}
