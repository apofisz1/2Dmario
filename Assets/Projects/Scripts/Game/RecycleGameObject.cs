using System.Collections.Generic;
using UnityEngine;

public class RecycleGameObject : MonoBehaviour {
    private List<IRecycle> _recycleComponents;    // újrahasznosítható komponensek listája

    private void Awake() {
        var components = GetComponents<MonoBehaviour>();
        _recycleComponents = new List<IRecycle>();
        foreach (var component in components) {    // adott GameObjecten megkeressük az újrahasznosítható komponenseket
            if (component is IRecycle) {
                _recycleComponents.Add(component as IRecycle);
            }
        }
    }

    public void Restart() {
        gameObject.SetActive(true);
        foreach (var recycleComponent in _recycleComponents) {    // a többi komponenst is újraindítjuk
            recycleComponent.Restart();
        }
    }

    public void Shutdown() {
        gameObject.SetActive(false);
        foreach (var recycleComponent in _recycleComponents) {    // a többi komponenst is leállítjuk
            recycleComponent.Shutdown();
        }
    }
}
