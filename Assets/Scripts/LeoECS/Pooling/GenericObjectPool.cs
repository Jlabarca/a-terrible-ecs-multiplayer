using System.Collections.Generic;
using UnityEngine;

namespace LeoECS.Pooling
{
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        private Queue<T> objects = new Queue<T>();
        private T prefab;
        public void Prewarm(int count, T prefab)
        {
            this.prefab = prefab;
            if (objects.Count >= count)
            {
                Debug.LogWarning("Pool is already big enough!");
                return;
            }

            int numberToPrewarm = count - objects.Count;
            AddObjects(numberToPrewarm, prefab);

        }

        public virtual T Get(T prefab)
        {
            if (objects.Count == 0)
                AddObjects(1, prefab);
            T objectFromPool = objects.Dequeue();
            return objectFromPool;
        }

        public virtual T Get()
        {
            if (objects.Count == 0)
                AddObjects(1, prefab);
            T objectFromPool = objects.Dequeue();
            return objectFromPool;
        }

        private void AddObjects(int count, T prefab)
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = GameObject.Instantiate(prefab);
                newObject.gameObject.SetActive(false);
                newObject.transform.SetParent(transform);
                objects.Enqueue(newObject);
            }
        }

        public virtual void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            objects.Enqueue(objectToReturn);
        }
    }
}
