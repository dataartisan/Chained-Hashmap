using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
/*Regular tests to ensure that the dictionary functions are working using a dictionary!*/

namespace ChainedHashMapImplementation
{
    [TestFixture]
    internal class UnitTest
    {
        private TestMethods _t = new TestMethods();

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void AddDirectlyToDictionaryTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddDirectlyToCollectionTest(dObject);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddDuplicateTest(dObject);
        }

        [Test]
        public void AddKeyValueTest100()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddKeyValueTest(dObject, 100);
        }

        [Test]
        public void AddKeyValueTest1000()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddKeyValueTest(dObject, 1000);
        }

        [Test]
        public void AddKeyValueTest10000()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddKeyValueTest(dObject, 10000);
        }

        [Test]
        public void AddKeyValueTest100000()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddKeyValueTest(dObject, 100000);
        }

        [Test]
        public void AddNullValueTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddNullValueTest(dObject);
        }

        [Test]
        public void AddtoTTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.AddTTest(dObject);
        }

        [Test]
        public void ClearItemsTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.ClearItemsTest(dObject);
        }

        [Test]
        public void ContainsKeyTest100()
        {
            Thread.Sleep(100);
            var dObject = new Dictionary<int, object>();

            _t.ContainsKeyTest(dObject, 100);
        }

        [Test]
        public void ContainsKeyTest1000()
        {
            Thread.Sleep(100);
            var dObject = new Dictionary<int, object>();

            _t.ContainsKeyTest(dObject, 1000);
        }

        [Test]
        public void ContainsKeyTest10000()
        {
            Thread.Sleep(1000);
            var dObject = new Dictionary<int, object>();
            _t.ContainsKeyTest(dObject, 10000);
        }

        [Test]
        public void ContainsKeyTest1000000()
        {
            Thread.Sleep(1000);
            var dObject = new Dictionary<int, object>();

            _t.ContainsKeyTest(dObject, 1000000);
        }
        [Test]
        public void ContainsKeyTest10000000()
        {
            Thread.Sleep(1000);
            var dObject = new Dictionary<int, object>();

            _t.ContainsKeyTest(dObject, 10000000);
        }

        [Test]
        public void ContainsKeyTest100000()
        {
            Thread.Sleep(1000);
            var dObject = new Dictionary<int, object>();

            _t.ContainsKeyTest(dObject, 100000);
        }

        [Test]
        public void ContainsTest()
        {
            var dboject = new Dictionary<int, object>();
            _t.ContainsTest(dboject);
        }

        [Test]
        public void CopyToTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.CopyToTest(dObject);
        }

        [Test]
        public void CountTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.TestCount(dObject);
        }


        [Test]
        public void EnumeratorTest1()
        {
            var dObject = new Dictionary<int, object>();
            _t.GetEnumeratorTest1(dObject);
        }

        [Test]
        public void EnumeratorTest2()
        {
            var dObject = new Dictionary<int, object>();
            _t.GetEnumeratorTest2(dObject);
        }
        
        [Test]
        public void IsReadOnlyTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.IsReadOnlyTest(dObject);
        }
        
        [Test]
        public void ItemsTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.ItemsTest(dObject);
        }

        [Test]
        public void KeysTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.KeysTest(dObject);
        }

        [Test]
        public void RemoveElementByKey100()
        {
            var dObject = new Dictionary<int, object>();
            _t.RemoveElementByKeyTest(dObject, 100);
        }

        [Test]
        public void RemoveElementByKey1000()
        {
            var dObject = new Dictionary<int, object>();
            _t.RemoveElementByKeyTest(dObject, 1000);
        }

        [Test]
        public void RemoveElementByKey10000()
        {
            var dObject = new Dictionary<int, object>();
            _t.RemoveElementByKeyTest(dObject, 10000);
        }

        [Test]
        public void RemoveElementByKey10000000()
        {
            var dObject = new Dictionary<int, object>();
            _t.RemoveElementByKeyTest(dObject, 10000000);
        }

        [Test]
        public void RemoveFromDictionaryDirect()
        {
            var dObject = new Dictionary<int, object>();
            _t.RemoveItemsFromCollection(dObject);
        }

        [Test]
        public void TryGetValueTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.TryGetValueTest(dObject);
        }

        [Test]
        public void ValuesTest()
        {
            var dObject = new Dictionary<int, object>();
            _t.ValuesTest(dObject);
        }
        
    }
}