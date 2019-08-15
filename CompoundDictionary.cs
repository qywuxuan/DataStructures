using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace StalkerME.DataStructures
{
    /// <summary>
    /// T:KeyType
    /// U:ValueType
    /// V:IEnumerableType
    /// </summary>
    public class CompoundDictionary<T, U, V> : ImprovedDictionary<T, V> where V : class, IEnumerable<U>, new()
    {
        protected V GetIEnumerable(T key)
        {
            V enumerable = null;

            if (ContainsKey (key))
            {
                enumerable = this[key];
            }
            else
            {
                enumerable = new V();
                Add (key, enumerable);
            }

            return enumerable;
        }
    }
}