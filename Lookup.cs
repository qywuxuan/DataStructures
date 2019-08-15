using System.Collections.Generic;
using System;

namespace StalkerME.DataStructures
{
    public class Lookup<T, U> : CompoundDictionary<T, U, List<U>>
	{
		public void Add(T key, U value)
		{
            var list = GetIEnumerable(key);

			if (list.Contains (value))
			{
				//do nothing
			}
			else
			{
				list.Add (value);
			}
		}

		public bool Remove(T key, U item)
		{
			if (ContainsKey (key))
			{
                var list = GetIEnumerable(key);

				return list.Remove (item);
			}
			else
			{
				return false;
			}
		}

		public U Find(T key, Predicate<U> match)
		{
			if (ContainsKey (key))
			{
                var list = GetIEnumerable(key);;

				return list.Find (match);
			}
			else
			{
				return default(U);
			}
		}

		public bool Contains(T key, U item)
		{
			if (ContainsKey (key))
			{
                var list = GetIEnumerable(key);;

				return list.Contains (item);
			}
			else
			{
				return false;
			}
		}
	}
}