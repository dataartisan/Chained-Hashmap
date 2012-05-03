using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

//Tutorial about linkedlistnode at https://www-927.ibm.com/ibm/cas/hspc/Resources/Tutorials/linkedListNode.shtml. did not land up using it.

namespace ChainedHashMapImplementation
{
    public class ChainedHashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        //delegate the hash function.
        public delegate int ToInteger<T>(T item);
        private ToInteger<int> Hashfunction { get; set; } //autoimplement c#
        //tableSize
        public int TableSize;
        //using list<linkedlist>
        private List<LinkedList<KeyValuePair<TKey, TValue>>> ListLinkedList { get; set; }//autoimplement c#
       
        
       public ChainedHashMap(ToInteger<int> hasher, int size)
        {
            this.Hashfunction = hasher;
            this.TableSize = size;
            this.ListLinkedList
                = new List<LinkedList<KeyValuePair<TKey, TValue>>>(TableSize);
           //initialize this so that it is not empty! missing me made me go nuts!!
           for(var i=0; i< size; i++)
           {
               ListLinkedList.Add(null);
           }
           
        }
        public int GetHashCode(int key)
        {
            //int hashKey = _hashfunction.Apply(key); //initial to make it work!!! comment after delegate has been completed.
            var hashKey = Hashfunction.Invoke(key) % TableSize;     //key % TableSize; initially to make it work! use delegate here!
            return hashKey;
        }
        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var values = new List<KeyValuePair<TKey, TValue>>();
            for (var i = 0; i < TableSize; i++)
            {
                values.AddRange((ListLinkedList[i]));   
            }
            return values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ListLinkedList.GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
           Add(item.Key, item.Value);
            
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            for (var i = 0; i < TableSize; i++)
            {
                ListLinkedList[i] = null;
                //just in case that does not take care of it.
                ListLinkedList[i].Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var myKey = GetHashCode(Int32.Parse(item.Key.ToString()));
            var myPos = GetHashCode(myKey);
            return ListLinkedList[myPos] != null && ListLinkedList[myPos].Select(myKeyValuePair => myKeyValuePair.Key.Equals(myKey)).FirstOrDefault();
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < TableSize; i++)
            {
                if (ListLinkedList[i] == null)
                {
                    continue;  
                }
                else
                {
                    foreach (var kvPair in ListLinkedList[i])
                    {
                        array[arrayIndex++] = kvPair;
                    }
                }  
                
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            //var myKey = Int32.Parse(item.Key.ToString());
            var myPos = GetHashCode(Int32.Parse(item.Key.ToString()));
            return ListLinkedList[myPos] != null && ListLinkedList[myPos].Remove(item);

            //return Remove(item.Key);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get
            {
                var count = 0;
                for (var i = 0; i < TableSize; i++ )
                {
                    if (ListLinkedList[i] != null)
                    {
                        count += ListLinkedList[i].Count;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IDictionary<TKey,TValue>

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool ContainsKey(TKey key)
        {
            var myKey = Int32.Parse(key.ToString());
            var myPos = GetHashCode(myKey);
            return ListLinkedList[myPos] != null && ListLinkedList[myPos].Any(kvPair => kvPair.Key.Equals(key));
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param><param name="value">The object to use as the value of the element to add.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</exception><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
        public void Add(TKey key, TValue value)
        {
            var myKey = Int32.Parse(key.ToString());
            var myPos = GetHashCode(myKey);
            //var found = false;
            
            if (myPos < TableSize)
            {
                if(ListLinkedList[myPos] == null)
                {
                    ListLinkedList[myPos] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    ListLinkedList[myPos].AddFirst(new KeyValuePair<TKey, TValue>(key, value));
                    return;
                }
                else
                {
                    var myListLinkedList = ListLinkedList[myPos];
                    if (myListLinkedList.Any(kvPair => kvPair.Key.Equals(key)))
                    {
                        throw new Exception();
                    }
                    myListLinkedList.AddFirst(new KeyValuePair<TKey, TValue>(key, value));
                    
                }
               
            }

        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key"/> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <param name="key">The key of the element to remove.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
        public bool Remove(TKey key)
        {
            var myKey = Int32.Parse(key.ToString());
            var myKeyPos = GetHashCode(myKey);
            var ll = ListLinkedList[myKeyPos];
            return ll != null && (from kvPair in ll where kvPair.Key.Equals(key) select Remove(kvPair)).FirstOrDefault();
        }

    

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value to get.</param><param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var myKey = Int32.Parse(key.ToString());
            var keyPos = GetHashCode(myKey);
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            var myPos = GetHashCode(myKey);
            var ll = ListLinkedList[myPos];
            if (ll != null)
            {
                foreach (var kvPair in ll.Where(kvPair => kvPair.Key.Equals(key)))
                {
                    value = kvPair.Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
            
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> is not found.</exception><exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
        public TValue this[TKey key]
        {
            get
            {
                var myKey = Int32.Parse(key.ToString());
                var myPos = GetHashCode(myKey);
                var ll = ListLinkedList[myPos];
                if (ll == null)
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    foreach (var kvPair in ll.Where(kvPair => kvPair.Key.Equals(key)))
                    {
                        return kvPair.Value;
                    }
                }
                return default(TValue);
            }
          
            
            set
            {
                this[key] = value;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TKey> Keys
        {
            get
            {
                var myKeys = new LinkedList<TKey>();
                for (var i = 0; i < TableSize; i++)
                {
                    if (ListLinkedList[i] == null) 
                        continue;
                    foreach (var kvPair in ListLinkedList[i])
                    {
                        myKeys.AddLast(kvPair.Key);
                    }
                }
                return myKeys;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// doing same thing but return value;
        public ICollection<TValue> Values
        {
            get
            {
                var myValues = new LinkedList<TValue>();
                for (var i = 0; i < TableSize; i++)
                {
                    if (ListLinkedList[i] == null) 
                        continue;
                    foreach (var kvPair in ListLinkedList[i])
                    {
                        myValues.AddLast(kvPair.Value);
                    }
                }
                return myValues;
            }
        }

        #endregion
    }
}

