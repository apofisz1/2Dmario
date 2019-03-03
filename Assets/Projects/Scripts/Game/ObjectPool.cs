using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public RecycleGameObject Prefab;    // a pool ilyen típusú objektumokat tárol
    
    // a poolban lévő példányok
    private List<RecycleGameObject> _poolInstances = new List<RecycleGameObject>();

    private RecycleGameObject CreateInstance(Vector3 position) {
        var clone = Instantiate(Prefab);
        clone.transform.position = position;
        clone.transform.parent = transform;
        
        _poolInstances.Add(clone);

        return clone;
    }

    // visszaadja a következő szabad példányt a poolból
    public RecycleGameObject NextObject(Vector3 position) {
        RecycleGameObject instance = null;

        // végigmegyünk az összes példányon a poolban
        foreach (var poolInstance in _poolInstances) {
            if (poolInstance.gameObject.activeSelf) continue;    // ha aktív, akkor épp használatban van
            instance = poolInstance;
            instance.transform.position = position;
        }

        // ha nem találtunk szabad példányt, létrehozunk egyet
        if (instance == null) instance = CreateInstance(position);
        
        // inicializáljuk a szabad példányt
        instance.Restart();
        return instance;
    }
}
