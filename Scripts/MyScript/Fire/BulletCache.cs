using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletCache : MonoBehaviour
{
    static BulletCache StaticCache;
    public CacheObject[] Caches;
    private Hashtable actives;

    [System.Serializable]
    public class CacheObject
    {
        public GameObject Perfab;
        public int CacheSize = 10;
        private GameObject[] Objects;
        private int Index = 0;

        public void Init()
        {
            Objects = new GameObject[CacheSize];
            for (int i = 0; i < CacheSize; i++)
            {
                Objects[i] = Instantiate(Perfab);
                Objects[i].SetActive(false);
                Objects[i].name += 1;

            }
        }

        public GameObject GetNext()
        {
            GameObject next = null;
            for (int i = 0; i < CacheSize; i++)
            {
                next = Objects[Index];
                if (!next.activeSelf) break;
                Index = (Index + 1) % CacheSize;
            }
            // all active in cache, must destory the longest one
            if(next.activeSelf)
            {
                BulletCache.Destroy(next);
            }
            Index = (Index + 1) % CacheSize;
            return next;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        StaticCache = this;
        int count = 0;
        for (int i = 0; i < Caches.Length; i++)
        {
            Caches[i].Init();
            count += Caches[i].CacheSize;
        }
        actives = new Hashtable(count);
    }
    public static GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        CacheObject cache = null;
        if (StaticCache)
        {
            foreach (var item in StaticCache.Caches)
            {
                if (item.Perfab == prefab)
                {
                    cache = item;
                    break;
                }
            }
        }
        // If there's no cache for this prefab type, just instantiate normally
        if (cache == null)
        {
            return Instantiate(prefab, position, rotation);
        }
        GameObject obj = cache.GetNext();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        StaticCache.actives[obj] = true;

        return obj;

    }

    public static void Destroy(GameObject obj)
    {
        if(StaticCache && StaticCache.actives.ContainsKey(obj))
        {
            obj.SetActive(false);
            StaticCache.actives[obj] = false;
        } else
        {
            Object.Destroy(obj);
        }
    }
}
