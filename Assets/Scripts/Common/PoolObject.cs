using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolObject 
{
    private static Dictionary<string, List<GameObject>> _Pools;
    public static GameObject GetPool(string key, GameObject prefab =null)
    {
        if(_Pools ==null) _Pools = new Dictionary<string, List<GameObject>>();
        GameObject obj;

        if (!_Pools.ContainsKey(key))
        {
            _Pools[key] = new List<GameObject>();
        }
        if (_Pools[key].Count <= 0)
        {
            //create
            obj = (prefab != null) ? UnityEngine.Object.Instantiate(prefab)
                                : new GameObject("Poolobj_" + key);
            return obj;
        }
        obj = _Pools[key][0];
        _Pools[key].Remove(obj);

        return obj; 
    }

    public static void DeActiveObj(GameObject obj, string key)
    {
        obj.SetActive(false);
        _Pools[key].Add(obj);
    }
}
