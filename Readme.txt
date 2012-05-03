Due Date: Tue 4 Oct (final!)
Objectives

    Design with interfaces
    Design with inheritance
    Associative data structures
        Hash tables
    Strategy pattern
    C# delegates

Description
This project is a continuation of projects 1a and 1b. Your job is as follows.

    Develop your own implementation of the IDictionary interface (not including the extension methods) called ChainedHashMap, which uses a fixed-size hash table with chaining. 
        In your implementation, use one of C#'s list implementations and other predefined classes where appropriate. In other words, put together the functionality of the hash table but rely on available building blocks instead of building from scratch.
        Unlike the predefined implementations, your implementation must provide a constructor that allows you to pass the hash function as an argument when an instance your class is created. The constructor should also take the table size as another argument.
        Do not implement the methods Keys, Values, or GetEnumerator (see extra credit below).
    Create the following generic delegate to represent hash functions as objects:
        public delegate int ToInteger<T>(T item);
    Implement the following hash functions for integer keys:
        last two digits
        last three digits 
        digit sum (e.g. digitSum(1234) returns 10)
        modulo some positive number
    Using the same suitable reuse and parameterization techniques as in project 1b, create a specialized test suite for this implementation class based on the test suite you created for IDictionary.
    Also apply your performance evaluation test suite from project 1b to this implementation class. This time, measure only retrievals (ContainsKey method). Specifically, for each dictionary size and hash function (in the case of your implementation), write a test method that creates a suitable dictionary instance (parameterized with the given hash function) and performs the measurements for the various numbers of insertions. Do this for the following configurations:
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
        ...
    Choose the number of repeated retrievals, r, globally such that your smallest measurements are at least 100 milliseconds.
    Choose the maximum load of the table (maximum number of table entries) such that the longest running times stay around a minute or below.
    Interpret your findings and include your answers to the following questions in a write-up of about 300 words:
        How does the choice of table size impact performance?
        How does the choice of hash function impact performance?
        Overall, how does the performance of your implementation compare to the ones included in the .NET framework (Dictionary, SortedDictionary)?
    Be sure to provide sufficient documentation in the form of a readme file, inline comments, and XML doc comments.

Grading

    8 chained hash table implementation/correctness test
        1 genericity
        1 parameterization with hash function
        0.5 points for each of 3 IDictionary properties (i.e., not counting Keys, Values)
        0.5 points for each of 9 IDictionary methods (i.e., not counting GetEnumerator)
    1.5 hash function implementations
    1.5 performance test
        0.5 Add/Remove
        1 ContainsKey (in isolation, for globally fixed number of reps, should grow with # items)
    2 written part
        0.5 describe results 
        0.5 compare with platform-provided implementations
        1 interpret results e.g. Dict hash, SortedDict tree
    2 documentation

TOTAL 15
