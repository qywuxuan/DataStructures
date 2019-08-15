using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace StalkerME.DataStructures
{
	public class ImprovedDictionary<T, U> : IDictionary<T, U>
	{
		List<T> keys;
		List<U> values;

		KeyNotFoundException keyNotFoundException;
        ArgumentException KeyAlreadyExistException;

		internal List<KeyValuePair<T,U>> pairList;

		public ImprovedDictionary()
		{
			pairList = new List<KeyValuePair<T,U>> ();
			keys = new List<T> ();
			values = new List<U> ();

			keyNotFoundException = new KeyNotFoundException ("The given key was not present in the dictionary.");
            KeyAlreadyExistException = new ArgumentException("An element with the same key already exists in the dictionary.");
        }

		public bool ContainsValue(U value)
		{
			return pairList.Exists (pair => 
			{
				return pair.Value.Equals(value);
			});
		}

		public bool ContainsKey (T key)
		{
			return pairList.Exists (pair => 
			{
				return pair.Key.Equals(key);
			});
		}

		public void Add (T key, U value)
		{
			var item = new KeyValuePair<T, U> (key, value);

			Add (item);
		}

        public void Put(T key, U value)
        {
            try
            {
                Add(key, value);
            }
            catch (ArgumentException exception)
            {
                if (exception.Equals(KeyAlreadyExistException))
                {
                    this[key] = value;
                }
                else
                {
                    throw exception;
                }
            }
        }

		public bool Remove (T key)
		{
			if (ContainsKey (key))
			{
				var keyIndex = keys.FindIndex (_key =>
				{
					return _key.Equals (key);
				});

				var value = values [keyIndex];

				var pair = new KeyValuePair<T, U> (key, value);

				return Remove (pair);
			}
			else
			{
				return false;
			}
		}

		public bool TryGetValue (T key, out U value)
		{
			if (ContainsKey (key))
			{
				value = this [key];

				return true;
			}
			else
			{
				value = default(U);

				return false;
			}
		}

		public U this [T index]
		{
			get
			{
				if (ContainsKey (index))
				{
					var targetPair = pairList.Find (pair =>
					{
						return pair.Key.Equals (index);
					});

					return targetPair.Value;
				}
				else
				{
					throw keyNotFoundException;
				}
			}

			set
			{
				if (ContainsKey (index))
				{
					var pairIndex = pairList.FindIndex (pair =>
					{
						return pair.Key.Equals (index);
					});

					var valueIndex = keys.FindIndex (_key =>
					{
						return _key.Equals (index);
					});

					pairList [pairIndex] = new KeyValuePair<T, U> (index, value);;
					values [valueIndex] = value;
				}
				else
				{
					throw keyNotFoundException;
				}
			}
		}

		public ICollection<T> Keys
		{
			get
			{
				return keys;
			}
		}

		public ICollection<U> Values
		{
			get
			{
				return values;
			}
		}

		public void Add (KeyValuePair<T, U> item)
		{
			var key = item.Key;
			var value = item.Value;

			if(ContainsKey (key))
			{
				throw KeyAlreadyExistException;
			}
			else
			{
				pairList.Add (item);
				keys.Add (key);
				values.Add (value);
			}
		}

		public void Clear ()
		{
			pairList.Clear ();
			keys.Clear ();
			values.Clear ();
		}

		public bool Contains (KeyValuePair<T, U> item)
		{
			return pairList.Contains (item);
		}

		public void CopyTo (KeyValuePair<T, U>[] array, int arrayIndex)
		{
			pairList.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<T, U> item)
		{
			var isSuccessful = pairList.Remove (item);

			if (isSuccessful)
			{
				var key = item.Key;
				var value = item.Value;

				keys.Remove (key);
				values.Remove (value);
			}
			else
			{
				//do nothing
			}

			return isSuccessful;
		}

		public int Count
		{
			get
			{
				return pairList.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public IEnumerator<KeyValuePair<T, U>> GetEnumerator ()
		{
			return pairList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this.GetEnumerator();
		}
	}

	public static class ImprovedDictionaryExpend
	{
		public delegate void DictionaryForeachHandler<T, U>(T key, U value);

		public static void DictionaryForeach<T, U>(this ImprovedDictionary<T, U> improvedDictionary, DictionaryForeachHandler<T, U> handler)
		{
			if (handler == null)
				throw new Exception ("字典 handler 不能为空");

			var pairList = improvedDictionary.pairList;

			for (int i = 0; i < improvedDictionary.Count; i++)
			{
				var pair = pairList [i];
				var key = pair.Key;
				var value = pair.Value;

				handler (key, value);
			}
		}
	}
}