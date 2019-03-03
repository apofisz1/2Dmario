using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil {
    // kulcs-érték pár tároló - adott típusú RecycleGameObjecthez object poolokat tárol
    private static readonly Dictionary<RecycleGameObject, ObjectPool> Pools =
        new Dictionary<RecycleGameObject, ObjectPool>();
    
    // új objektumot hoz létre prefab alapján
    public static GameObject Instantiate(GameObject prefab, Vector3 position) {
        GameObject instance;

        var recycleScript = prefab.GetComponent<RecycleGameObject>();
        if (recycleScript != null) {    // ha van a prefaben RecycleGameObject szkript -> pool kompatibilis
            var pool = GetObjectPool(recycleScript);
            instance = pool.NextObject(position).gameObject;
        } else {    // ellenkező esetben létrehozzuk a klasszikus módon
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;    
        }
        
        return instance;
    }
    
    // visszaadja az adott típusú prefabhez tartozó object poolt
    private static ObjectPool GetObjectPool(RecycleGameObject recycleScript) {
        ObjectPool pool;

        if (Pools.ContainsKey(recycleScript)) {    // ha a pool tartalmaz a szkripthez tartozó object poolt
            pool = Pools[recycleScript];           // beállítjuk a megfelelő poolt, amit később vissza is adunk
        } else {    // ellenkező esetben új poolt hozunk létre
            var poolContainer = new GameObject(recycleScript.gameObject.name + " Pool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.Prefab = recycleScript;
            Pools.Add(recycleScript, pool);
        }
        
        return pool;
    }

    public static void Destroy(GameObject gameObject) {
        var recycleScript = gameObject.GetComponent<RecycleGameObject>();

        if (recycleScript != null) {    // ha van a prefaben RecycleGameObject szkript -> pool kompatibilis
            recycleScript.Shutdown();    
        } else {    // ellenkező esetben megsemmisítjük a klasszikus módon
            GameObject.Destroy(gameObject);   
        }
    }
}