// Type: System.Collections.Generic.List`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class List<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    private static readonly T[] _emptyArray = new T[0];
    private T[] _items;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 4;

    [__DynamicallyInvokable]
    public int Capacity
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return this._items.Length;
      }
      [__DynamicallyInvokable] set
      {
        if (value < this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
        if (value == this._items.Length)
          return;
        if (value > 0)
        {
          T[] objArray = new T[value];
          if (this._size > 0)
            Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
          this._items = objArray;
        }
        else
          this._items = List<T>._emptyArray;
      }
    }

    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._size;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection.IsSynchronized
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object ICollection.SyncRoot
    {
      [__DynamicallyInvokable] get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    public T this[int index]
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        return this._items[index];
      }
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] set
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this._items[index] = value;
        ++this._version;
      }
    }

    static List()
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public List()
    {
      this._items = List<T>._emptyArray;
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public List(int capacity)
    {
      if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (capacity == 0)
        this._items = List<T>._emptyArray;
      else
        this._items = new T[capacity];
    }

    [__DynamicallyInvokable]
    public List(IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      ICollection<T> collection1 = collection as ICollection<T>;
      if (collection1 != null)
      {
        int count = collection1.Count;
        if (count == 0)
        {
          this._items = List<T>._emptyArray;
        }
        else
        {
          this._items = new T[count];
          collection1.CopyTo(this._items, 0);
          this._size = count;
        }
      }
      else
      {
        this._size = 0;
        this._items = List<T>._emptyArray;
        foreach (T obj in collection)
          this.Add(obj);
      }
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      else
        return false;
    }

    [__DynamicallyInvokable]
    object IList.get_Item(int index)
    {
      return (object) this[index];
    }

    [__DynamicallyInvokable]
    void IList.set_Item(int index, object value)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this[index] = (T) value;
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
    }

    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      this._items[this._size++] = item;
      ++this._version;
    }

    [__DynamicallyInvokable]
    int IList.Add(object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Add((T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
      return this.Count - 1;
    }

    [__DynamicallyInvokable]
    public void AddRange(IEnumerable<T> collection)
    {
      this.InsertRange(this._size, collection);
    }

    public ReadOnlyCollection<T> AsReadOnly()
    {
      return new ReadOnlyCollection<T>((IList<T>) this);
    }

    [__DynamicallyInvokable]
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      return Array.BinarySearch<T>(this._items, index, count, item, comparer);
    }

    [__DynamicallyInvokable]
    public int BinarySearch(T item)
    {
      return this.BinarySearch(0, this.Count, item, (IComparer<T>) null);
    }

    [__DynamicallyInvokable]
    public int BinarySearch(T item, IComparer<T> comparer)
    {
      return this.BinarySearch(0, this.Count, item, comparer);
    }

    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this._size > 0)
      {
        Array.Clear((Array) this._items, 0, this._size);
        this._size = 0;
      }
      ++this._version;
    }

    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      if ((object) item == null)
      {
        for (int index = 0; index < this._size; ++index)
        {
          if ((object) this._items[index] == null)
            return true;
        }
        return false;
      }
      else
      {
        EqualityComparer<T> @default = EqualityComparer<T>.Default;
        for (int index = 0; index < this._size; ++index)
        {
          if (@default.Equals(this._items[index], item))
            return true;
        }
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.Contains((T) item);
      else
        return false;
    }

    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
      if (converter == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
      List<TOutput> list = new List<TOutput>(this._size);
      for (int index = 0; index < this._size; ++index)
        list._items[index] = converter(this._items[index]);
      list._size = this._size;
      return list;
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public void CopyTo(T[] array)
    {
      this.CopyTo(array, 0);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      if (array != null)
      {
        if (array.Rank != 1)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      }
      try
      {
        Array.Copy((Array) this._items, 0, array, arrayIndex, this._size);
      }
      catch (ArrayTypeMismatchException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
      }
    }

    [__DynamicallyInvokable]
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Copy((Array) this._items, index, (Array) array, arrayIndex, count);
    }

    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int arrayIndex)
    {
      Array.Copy((Array) this._items, 0, (Array) array, arrayIndex, this._size);
    }

    private void EnsureCapacity(int min)
    {
      if (this._items.Length >= min)
        return;
      int num = this._items.Length == 0 ? 4 : this._items.Length * 2;
      if ((uint) num > 2146435071U)
        num = 2146435071;
      if (num < min)
        num = min;
      this.Capacity = num;
    }

    [__DynamicallyInvokable]
    public bool Exists(Predicate<T> match)
    {
      return this.FindIndex(match) != -1;
    }

    [__DynamicallyInvokable]
    public T Find(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    [__DynamicallyInvokable]
    public List<T> FindAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      List<T> list = new List<T>();
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          list.Add(this._items[index]);
      }
      return list;
    }

    [__DynamicallyInvokable]
    public int FindIndex(Predicate<T> match)
    {
      return this.FindIndex(0, this._size, match);
    }

    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, Predicate<T> match)
    {
      return this.FindIndex(startIndex, this._size - startIndex, match);
    }

    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      if ((uint) startIndex > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    [__DynamicallyInvokable]
    public T FindLast(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = this._size - 1; index >= 0; --index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    [__DynamicallyInvokable]
    public int FindLastIndex(Predicate<T> match)
    {
      return this.FindLastIndex(this._size - 1, this._size, match);
    }

    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      return this.FindLastIndex(startIndex, startIndex + 1, match);
    }

    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size == 0)
      {
        if (startIndex != -1)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      }
      else if ((uint) startIndex >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex - count + 1 < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      int num = startIndex - count;
      for (int index = startIndex; index > num; --index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    public void ForEach(Action<T> action)
    {
      if (action == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int num = this._version;
      for (int index = 0; index < this._size && (num == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5); ++index)
        action(this._items[index]);
      if (num == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        return;
      ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public List<T>.Enumerator GetEnumerator()
    {
      return new List<T>.Enumerator(this);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new List<T>.Enumerator(this);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new List<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    public List<T> GetRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      List<T> list = new List<T>(count);
      Array.Copy((Array) this._items, index, (Array) list._items, 0, count);
      list._size = count;
      return list;
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return Array.IndexOf<T>(this._items, item, 0, this._size);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    int IList.IndexOf(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.IndexOf((T) item);
      else
        return -1;
    }

    [__DynamicallyInvokable]
    public int IndexOf(T item, int index)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      return Array.IndexOf<T>(this._items, item, index, this._size - index);
    }

    [__DynamicallyInvokable]
    public int IndexOf(T item, int index, int count)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || index > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      return Array.IndexOf<T>(this._items, item, index, count);
    }

    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + 1, this._size - index);
      this._items[index] = item;
      ++this._size;
      ++this._version;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Insert(index, (T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
    }

    [__DynamicallyInvokable]
    public void InsertRange(int index, IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      ICollection<T> collection1 = collection as ICollection<T>;
      if (collection1 != null)
      {
        int count = collection1.Count;
        if (count > 0)
        {
          this.EnsureCapacity(this._size + count);
          if (index < this._size)
            Array.Copy((Array) this._items, index, (Array) this._items, index + count, this._size - index);
          if (this == collection1)
          {
            Array.Copy((Array) this._items, 0, (Array) this._items, index, index);
            Array.Copy((Array) this._items, index + count, (Array) this._items, index * 2, this._size - index);
          }
          else
          {
            T[] array = new T[count];
            collection1.CopyTo(array, 0);
            array.CopyTo((Array) this._items, index);
          }
          this._size += count;
        }
      }
      else
      {
        foreach (T obj in collection)
          this.Insert(index++, obj);
      }
      ++this._version;
    }

    [__DynamicallyInvokable]
    public int LastIndexOf(T item)
    {
      if (this._size == 0)
        return -1;
      else
        return this.LastIndexOf(item, this._size - 1, this._size);
    }

    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index)
    {
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      return this.LastIndexOf(item, index, index + 1);
    }

    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index, int count)
    {
      if (this.Count != 0 && index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this.Count != 0 && count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size == 0)
        return -1;
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      if (count > index + 1)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      return Array.LastIndexOf<T>(this._items, item, index, count);
    }

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      int index = this.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveAt(index);
      return true;
    }

    [__DynamicallyInvokable]
    void IList.Remove(object item)
    {
      if (!List<T>.IsCompatibleObject(item))
        return;
      this.Remove((T) item);
    }

    [__DynamicallyInvokable]
    public int RemoveAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int index1 = 0;
      while (index1 < this._size && !match(this._items[index1]))
        ++index1;
      if (index1 >= this._size)
        return 0;
      int index2 = index1 + 1;
      while (index2 < this._size)
      {
        while (index2 < this._size && match(this._items[index2]))
          ++index2;
        if (index2 < this._size)
          this._items[index1++] = this._items[index2++];
      }
      Array.Clear((Array) this._items, index1, this._size - index1);
      int num = this._size - index1;
      this._size = index1;
      ++this._version;
      return num;
    }

    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if ((uint) index >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      --this._size;
      if (index < this._size)
        Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
      this._items[this._size] = default (T);
      ++this._version;
    }

    [__DynamicallyInvokable]
    public void RemoveRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      if (count <= 0)
        return;
      this._size -= count;
      if (index < this._size)
        Array.Copy((Array) this._items, index + count, (Array) this._items, index, this._size - index);
      Array.Clear((Array) this._items, this._size, count);
      ++this._version;
    }

    [__DynamicallyInvokable]
    public void Reverse()
    {
      this.Reverse(0, this.Count);
    }

    [__DynamicallyInvokable]
    public void Reverse(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Reverse((Array) this._items, index, count);
      ++this._version;
    }

    [__DynamicallyInvokable]
    public void Sort()
    {
      this.Sort(0, this.Count, (IComparer<T>) null);
    }

    [__DynamicallyInvokable]
    public void Sort(IComparer<T> comparer)
    {
      this.Sort(0, this.Count, comparer);
    }

    [__DynamicallyInvokable]
    public void Sort(int index, int count, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Sort<T>(this._items, index, count, comparer);
      ++this._version;
    }

    [__DynamicallyInvokable]
    public void Sort(Comparison<T> comparison)
    {
      if (comparison == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size <= 0)
        return;
      Array.Sort<T>(this._items, 0, this._size, (IComparer<T>) new Array.FunctorComparer<T>(comparison));
    }

    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      T[] objArray = new T[this._size];
      Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
      return objArray;
    }

    [__DynamicallyInvokable]
    public void TrimExcess()
    {
      if (this._size >= (int) ((double) this._items.Length * 0.9))
        return;
      this.Capacity = this._size;
    }

    [__DynamicallyInvokable]
    public bool TrueForAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (!match(this._items[index]))
          return false;
      }
      return true;
    }

    internal static IList<T> Synchronized(List<T> list)
    {
      return (IList<T>) new List<T>.SynchronizedList(list);
    }

    [Serializable]
    internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
      private List<T> _list;
      private object _root;

      public int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
        }
      }

      public T this[int index]
      {
        get
        {
          lock (this._root)
            return this._list[index];
        }
        set
        {
          lock (this._root)
            this._list[index] = value;
        }
      }

      internal SynchronizedList(List<T> list)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public void Add(T item)
      {
        lock (this._root)
          this._list.Add(item);
      }

      public void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public bool Contains(T item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
        lock (this._root)
          this._list.CopyTo(array, arrayIndex);
      }

      public bool Remove(T item)
      {
        lock (this._root)
          return this._list.Remove(item);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        lock (this._root)
          return (IEnumerator) this._list.GetEnumerator();
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public int IndexOf(T item)
      {
        lock (this._root)
          return this._list.IndexOf(item);
      }

      public void Insert(int index, T item)
      {
        lock (this._root)
          this._list.Insert(index, item);
      }

      public void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }
    }

    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private List<T> list;
      private int index;
      private int version;
      private T current;

      [__DynamicallyInvokable]
      public T Current
      {
        [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
        {
          return this.current;
        }
      }

      [__DynamicallyInvokable]
      object IEnumerator.Current
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.list._size + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.Current;
        }
      }

      internal Enumerator(List<T> list)
      {
        this.list = list;
        this.index = 0;
        this.version = list._version;
        this.current = default (T);
      }

      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      [__DynamicallyInvokable]
      public bool MoveNext()
      {
        List<T> list = this.list;
        if (this.version != list._version || (uint) this.index >= (uint) list._size)
          return this.MoveNextRare();
        this.current = list._items[this.index];
        ++this.index;
        return true;
      }

      private bool MoveNextRare()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = this.list._size + 1;
        this.current = default (T);
        return false;
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = 0;
        this.current = default (T);
      }
    }
  }
}
