using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;

public class SpawnedObjectsContainer : MonoBehaviour {
    public IEnumerable<Transform> SpawnedObjects {
        get {    // visszaadja az objektum gyerekeinek Transform komponenseit, akik rendelkeznek LeanPoolable szkripttel 
            return GetComponentsInChildren<Transform>()
                .Where(t => t != transform && t.gameObject.GetComponent<LeanPoolable>() != null);
        }
    }
}
