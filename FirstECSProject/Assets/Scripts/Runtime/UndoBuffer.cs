using System;
using System.Collections.Generic;

namespace Kulikova
{
	public class UndoBuffer<T>
	{
		private readonly T[] _undoBuffer;
		private readonly List<T> _addList = new List<T>();

		private int _undoCount;
		public int UndoCount { get => _undoCount; }

		private int _redoCount;
		public int RedoCount { get => _redoCount; }

		private int _addCount;

		public UndoBuffer(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentException("Ring buffer cannot have negative or zero capacity.", nameof(capacity));
			} else if (capacity % 2 == 0)
			{
				_undoBuffer = new T[capacity];
			}
			else
			{
				throw new ArgumentException("The capacity of the ring buffer must be a multiple of two.", nameof(capacity));
			}
		}

		public void Add(T item)
		{
			_addCount += 1;
			if (_addCount <= _undoBuffer.Length)
			{
				_undoCount += 1;
			}
			_addList.Add(item);
		}

		public bool TryGetUndo(out T item)
		{
			item = _addList[_addList.Count - 1];
			if (_undoCount != 0) 
			{
				_undoCount -= 1;
				_redoCount += 1;
                return true;
            }
            else
            {
                return false;
            }
		}

		public bool TryGetRedo(out T item)
		{
			item = _addList[_addList.Count - 1];
			if (_undoCount != 0)
			{
				_redoCount -= 1;
				_undoCount += 1;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}