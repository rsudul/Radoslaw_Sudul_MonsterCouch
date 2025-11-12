using System.Collections.Generic;
using UnityEngine;

namespace MonsterCouchTest.Utilities
{
    public sealed class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _stack = new();

        public ObjectPool(T prefab, int prewarm = 0, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            if (prewarm > 0)
            {
                Prewarm(prewarm);
            }
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = Object.Instantiate(_prefab, _parent);
                obj.gameObject.SetActive(false);
                _stack.Push(obj);
            }
        }

        public T Get()
        {
            var obj = _stack.Count > 0 ? _stack.Pop() : Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent, worldPositionStays: false);
            _stack.Push(obj);
        }

        public void Clear()
        {
            while (_stack.Count > 0)
            {
                var obj = _stack.Pop();
                if (obj)
                {
                    Object.Destroy(obj.gameObject);
                }
            }
        }
    }
}
