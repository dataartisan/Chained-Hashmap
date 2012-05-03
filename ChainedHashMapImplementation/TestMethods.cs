/* Purpose: Implement Unit Test Fixtures for IDictionary interface and underlying methods defined in
 *          http://msdn.microsoft.com/en-us/library/system.collections.idictionary.aspx
 * Author: Subhash Pant
 * Date Created: 09/11/2011
 * Date Modified: 
 * Modifications: 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace ChainedHashMapImplementation
{
    
    public class TestMethods
    {
        //declarations and initializations
        private readonly int REPS = 100000;
        private bool _containsKey;
        
        //Properties Test - these get invoted from UnitTest.cs
        public void TestCount(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(new KeyValuePair<int, object>(1, "Value1"));
            Assert.AreEqual(1, anyDictionary.Count);
        }

        public void IsReadOnlyTest(IDictionary<int, object> anyDictionary)
        {
            Assert.IsFalse(anyDictionary.IsReadOnly);
        }

        public void ItemsTest(IDictionary<int, object> anyDictionary)
        {
            object getterValue;
            anyDictionary.Add(1, "Value1");
            anyDictionary.TryGetValue(1, out getterValue);
            Assert.AreEqual("Value1", getterValue, "No Value Return!");
        }

        public void KeysTest(IDictionary<int, object> anyDictionary)
        {
            bool contains = true;
            ICollection<int> c = anyDictionary.Keys;
            anyDictionary.Add(1, "Value1");
            Assert.AreEqual(1, c.Count);
            Assert.IsTrue(contains, c.Contains(1).ToString());
        }

        public void ValuesTest(IDictionary<int, object> anyDictionary)
        {
            bool contains = true;
            ICollection<object> c = anyDictionary.Values;
            anyDictionary.Add(1, "Value1");
            Assert.AreEqual(1, c.Count);
            Assert.IsTrue(contains, c.Contains("Value1").ToString());
        }

        //Methods - these get invoted from UnitTest.cs
        public void AddTTest(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(new KeyValuePair<int, object>(1, "Value1"));
            Assert.AreEqual(1, anyDictionary.Count);
        }

        public void AddDuplicateTest(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(1, "Value1");
        }

        public void AddNullValueTest(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(1, null);
            anyDictionary.Add(2, "Value2");
            anyDictionary.Add(3, null);
            Assert.IsNull(anyDictionary[1]);
            Assert.IsNotNull(anyDictionary[2]);
        }

        public void AddDirectlyToCollectionTest(IDictionary<int, object> anyDictionary)
        {
            //this one fails with not supported exception. I just did some extra work to prove its worth.
            ICollection<int> collection = anyDictionary.Keys;
            collection.Add(1);
            collection.Add(1);
            Assert.AreEqual(1, collection.Contains(1));
            Assert.AreEqual(2, collection.Contains(2).ToString());
            Assert.AreNotEqual(3, collection.Contains(3).ToString());
        }

        public void AddKeyValueTest(IDictionary<int, object> anyDictionary, int size)
        {
            const string path = @"E:\results.txt";
            StreamWriter tr = File.AppendText(path);

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < size; i++)
            {
                anyDictionary.Add(i, "Value" + i);
            }

            sw.Stop();
            tr.WriteLine("AddKeyValueTest for " + anyDictionary.GetType().Name + " size: " + size + " took {0}ms",
                         sw.ElapsedMilliseconds.ToString());
            Console.WriteLine("Elapsed time for AddKeyValue is: {0}ms", sw.Elapsed);
            Assert.AreEqual(size, anyDictionary.Count);
            sw.Reset();
            tr.Close();
            tr.Dispose();
        }

        public void ClearItemsTest(IDictionary<int, object> anyDictionary)
        {
            //Lets add 3 items in the dictionary
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(2, "Value2");
            anyDictionary.Add(3, "Value3");
            anyDictionary.Clear();
            Assert.AreEqual(0, anyDictionary.Count);
        }

        public void ContainsTest(IDictionary<int, object> anyDictionary)
        {
            const bool contains = true;
            ICollection<object> collection = anyDictionary.Values;
            anyDictionary.Add(new KeyValuePair<int, object>(1, "Value1"));
            Assert.IsTrue(contains, collection.Contains("Value1").ToString());
        }

        public void ContainsKeyTest(IDictionary<int, object> anyDictionary, int size)
        {
            var random = new Random();
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            bool isContain = false;

            var a = random.Next(1000000);
            for (int i = 0; i < size; i++)
            {
                anyDictionary.Add(i, "Value" + i);
            }

            var s = new Stopwatch();
            s.Start();
            for (int r = 0; r < REPS; r++)
            {
                isContain = anyDictionary.ContainsKey(a);
            }
            // isContain = anyDictionary.ContainsKey(1015);
            Console.WriteLine("Elapsed time for Contains is: {0}ms", s.ElapsedMilliseconds);
            s.Stop();
            tr.WriteLine("ContainsKey test for " + anyDictionary.GetType().Name.ToString() + " size: " + size + " took {0}ms",
                         s.ElapsedMilliseconds);
            s.Reset();

            //We have not added Key3
            isContain = anyDictionary.ContainsKey(11);
            Assert.IsTrue(isContain);

            tr.Close();
            tr.Dispose();
        }

        public void CopyToTest(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(2, "Value2");
            anyDictionary.Add(3, "Value2");
            ICollection<int> myCollection = anyDictionary.Keys;
            var myArray = new int[myCollection.Count];
            myCollection.CopyTo(myArray, 0); //Copies starting form this array index
            Assert.AreEqual(3, myArray.Length);
            Assert.AreEqual(1, myArray[0]);
        }

        public void GetEnumeratorTest1(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(2, "Value2");
            anyDictionary.Add(3, "Value3");
            anyDictionary.Add(4, "Value4");
            IEnumerator e = anyDictionary.Keys.GetEnumerator();
            Assert.IsTrue(e.MoveNext(), 1.ToString());
        }

        public void GetEnumeratorTest2(IDictionary<int, object> anyDictionary)
        {
            anyDictionary.Add(1, "Value1");
            anyDictionary.Add(2, "Value2");
            anyDictionary.Add(3, "Value3");
            IEnumerator<KeyValuePair<int, object>> e = (anyDictionary).GetEnumerator();
            Assert.IsTrue(e.MoveNext(), 1.ToString());
            Assert.AreEqual("Value1", e.Current.Value, 2.ToString());
        }

        public void RemoveItemsFromCollection(IDictionary<int, object> anyDictionary)
        {
            const bool doesnotcontain = false;
            anyDictionary.Add(new KeyValuePair<int, object>(1, "Value1"));
            anyDictionary.Add(new KeyValuePair<int, object>(2, "Value2"));
            anyDictionary.Add(new KeyValuePair<int, object>(3, "Value3"));
            anyDictionary.Remove(1);
            anyDictionary.Remove(2);
            anyDictionary.Remove(3);
            ICollection<int> ic = anyDictionary.Keys;
            Assert.IsFalse(doesnotcontain, ic.Contains(1).ToString());
            Assert.IsFalse(doesnotcontain, ic.Contains(2).ToString());
            Assert.IsFalse(doesnotcontain, ic.Contains(3).ToString());
            Assert.AreEqual(0, ic.Count);
        }

        public void RemoveElementByKeyTest(IDictionary<int, object> anyDictionary, int size)
        {
            const string path = @"E:\results.txt";
            StreamWriter tr = File.AppendText(path);

            //Lets add 3 items in the dictionary
            for (int i = 0; i < size; i++)
            {
                anyDictionary.Add(i, "Value" + i);
            }
            Assert.AreEqual(size, anyDictionary.Count);
            var s = new Stopwatch();
            s.Start();
            for (int i = 0; i < size; i++)
            {
                anyDictionary.Remove(i);
            }
            //anyDictionary.Remove(99);
            s.Stop();
            Console.WriteLine("Elapsed time for RemoveKey is: {0}ms", s.Elapsed);
            tr.WriteLine("RemoveByKey for " + anyDictionary.GetType().Name + " size: " + size + " took {0}ms",
                         s.ElapsedMilliseconds);
            Assert.AreEqual(0, anyDictionary.Count);

            s.Reset();
            tr.Close();
            tr.Dispose();
        }

        public void TryGetValueTest(IDictionary<int, object> anyDictionary)
        {
            object value = "";
            Assert.IsFalse(anyDictionary.TryGetValue(1, out value), "Value1");
            Assert.IsNull(value, "Value1");
            anyDictionary.Add(1, "Value1");
            Assert.IsTrue(anyDictionary.TryGetValue(1, out value), 1.ToString());
        }
        public void AddRandomKeyValues(IDictionary<int, object> anyDictionary, int numOfItems)
        {
            //change this with random values adding
            //var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, size);
            var r = new Random();

            for (var i = 0; i < numOfItems; i++)
            {
                var myKey = i;
                var myValue = i;
                anyDictionary.Add(myKey, myValue);
                
            }
        }
        
        public void ContainsKeyPerformance(IDictionary<int, object> anyDictionary, int numOfItems)
        {

            AddRandomKeyValues(anyDictionary, numOfItems);
            for (var i = 0; i < REPS; i++)
            {
                var r = new Random();
                _containsKey = anyDictionary.ContainsKey(r.Next());
            }
        }

        public void TestContainsLastTwo100(IDictionary<int, object> anyDictionary)
        {
            const string path = @"E:\results.txt";
            StreamWriter tr = File.AppendText(path);
            var sw = new Stopwatch();
            Console.WriteLine("Elapsed time for Contains is: {0}ms", sw.ElapsedMilliseconds);
            sw.Stop();
            tr.WriteLine("ContainsKey test for " + anyDictionary.GetType().Name.ToString() + " size: " + 100 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            tr.Close();
            sw.Reset();


        }
        

        /*The methods below do the performance testings under different conditions.
         * Conditions as follows:
            ChainedHashMap, last two digits, size 100
            ChainedHashMap, last three digits, size 1000
            ChainedHashMap, digit sum, size 100
            ChainedHashMap, modulo 101, size 101
            ChainedHashMap, modulo 1009, size 1009
            ChainedHashMap, modulo 10007, size 10007
            ChainedHashMap, modulo 100003, size 100003
            and the following number of insertions
            100
            1000
            10000
            100000
        */
        //The tests below run independently
        [Test]
        public void LastTwoSize100Num100()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastTwoDigit, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for LastTwoDigits, Size 100, Num of insertions " + 100 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastTwoSize100Num1000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastTwoDigit, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for LastTwoDigits, Size 100, Num of insertions " + 1000 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastTwoSize100Num10000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastTwoDigit, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for LastTwoDigits, Size 100, Num of insertions " + 10000 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastTwoSize100Num100000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastTwoDigit, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for LastTwoDigits, Size 100, Num of insertions " + 100000 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastThreeSize1000Num100()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastThreeDigit, 1000);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for LastThreeDigits, Size 1000, Num of insertions " + 100 + " took {0}ms",
                         sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastThreeSize1000Num1000()
        {
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastThreeDigit, 1000);
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for LastThreeDigits, Size 1000, Num of insertions " + 1000 + " took {0}ms",
                        sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastThreeSize1000Num10000()
        {
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastThreeDigit, 1000);
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for LastThreeDigits, Size 1000, Num of insertions " + 10000 + " took {0}ms",
                        sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void LastThreeSize1000Num100000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.LastThreeDigit, 1000);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for LastThreeDigits, Size 1000, Num of insertions " + 100000 + " took {0}ms",
                        sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void DigiSum100Num100()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.SumDigits, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for Digi Sum, Size 100, Num of insertions " + 100 + " took {0}ms",
                        sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void DigiSum100Num1000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.SumDigits, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for Digi Sum, Size 100, Num of insertions " + 1000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void DigiSum100Num10000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.SumDigits, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for Digi Sum, Size 100, Num of insertions " + 10000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void DigiSum100Num100000()
        {
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.SumDigits, 100);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for Digi Sum, Size 100, Num of insertions " + 100000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
      
        [Test]
        public void HashModulo101Size101Num100()
        {
            HashFunctions.HashModuloConstant = 101;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 101);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo101, Size 101, Num of insertions " + 100 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo101Size101Num1000()
        {
            HashFunctions.HashModuloConstant = 101;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 101);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for HashModulo101, Size 101, Num of insertions " + 1000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo101Size101Num10000()
        {
            HashFunctions.HashModuloConstant = 101;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 101);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for HashModulo101, Size 101, Num of insertions " + 10000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo101Size101Num100000()
        {
            HashFunctions.HashModuloConstant = 101;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 101);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for HashModulo101, Size 101, Num of insertions " + 100000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo1009Size1009Num100()
        {
            HashFunctions.HashModuloConstant = 1009;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 1009);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo1009, Size 1009, Num of insertions " + 100 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo1009Size1009Num1000()
        {
            HashFunctions.HashModuloConstant = 1009;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 1009);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for HashModulo1009, Size 1009, Num of insertions " + 1000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo1009Size1009Num10000()
        {
            HashFunctions.HashModuloConstant = 1009;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 1009);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for HashModulo1009, Size 1009, Num of insertions " + 10000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo1009Size1009Num100000()
        {
            HashFunctions.HashModuloConstant = 1009;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 1009);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for HashModulo1009, Size 1009, Num of insertions " + 100000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo10007Size10007Num100()
        {
            HashFunctions.HashModuloConstant = 10007;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 10007);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo10007, Size 10007, Num of insertions " + 100 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo10007Size10007Num1000()
        {
            HashFunctions.HashModuloConstant = 10007;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 10007);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 1000);
            tr.WriteLine("ContainsKey test for HashModulo10007, Size 10007, Num of insertions " + 1000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo10007Size10007Num10000()
        {
            HashFunctions.HashModuloConstant = 10007;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 10007);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 10000);
            tr.WriteLine("ContainsKey test for HashModulo10007, Size 10007, Num of insertions " + 10000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo10007Size10007Num100000()
        {
            HashFunctions.HashModuloConstant = 10007;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 10007);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100000);
            tr.WriteLine("ContainsKey test for HashModulo10007, Size 10007, Num of insertions " + 100000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo100003Size100003Num100()
        {
            HashFunctions.HashModuloConstant = 100003;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 100003);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo100003, Size 100003, Num of insertions " + 100 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo100003Size100003Num1000()
        {
            HashFunctions.HashModuloConstant = 100003;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 100003);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo100003, Size 100003, Num of insertions " + 1000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo100003Size100003Num10000()
        {
            HashFunctions.HashModuloConstant = 100003;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 100003);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo100003, Size 100003, Num of insertions " + 10000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }
        [Test]
        public void HashModulo100003Size100003Num100000()
        {
            HashFunctions.HashModuloConstant = 100003;
            var anyDictionary = new ChainedHashMap<int, object>(HashFunctions.HashModuloApply, 100003);
            const string path = @"E:\results.txt";
            var tr = File.AppendText(path);
            var sw = new Stopwatch();
            sw.Start();
            ContainsKeyPerformance(anyDictionary, 100);
            tr.WriteLine("ContainsKey test for HashModulo10007, Size 100003, Num of insertions " + 100000 + " took {0}ms",
                       sw.ElapsedMilliseconds);
            sw.Stop();
            tr.Close();
            sw.Reset();
        }



    }
}
