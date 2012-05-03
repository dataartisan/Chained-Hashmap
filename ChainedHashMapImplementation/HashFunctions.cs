using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*Hash Functions list. These hash functions get delegated to the delegate object on the other class and called. 
 * HashModuloConstant is overwritten
 */
namespace ChainedHashMapImplementation
{
    internal class HashFunctions
    {
        public static int HashModuloConstant = 100;
        //LastTwoDigit method
        public static int LastTwoDigit(int key)
        {
            var num = key/100.0;
            var lastTwo = key.GetHashCode()%100.0;
            return Int32.Parse(lastTwo.ToString());
        }
        //LastThreeDigit method
        public static int LastThreeDigit(int key)
        {
            var num = key/1000.0;
            var lastThree = key.GetHashCode()%1000.0;
            return Int32.Parse(lastThree.ToString());
            //return lastThree;
        }
        //HashModulo method
        public static int HashModuloApply(int key)
        {
            var num = key;
            
            var hashModulo = key%HashModuloConstant;
            return hashModulo;

        }
        //SumDigits method
        public static int SumDigits(int key)
        {
            var temp = key;
            var sum = 0;
            while (temp > 0)
            {
                sum += temp%10;
                temp /= 10;
            }
            return sum;
        }

    }
}


