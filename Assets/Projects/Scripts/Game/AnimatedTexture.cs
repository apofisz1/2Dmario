using UnityEngine;

public class AnimatedTexture : MonoBehaviour {
    public Vector2 Speed;

    private Material _material;    // a mozgatni kívánt textúrához tartozó material
    private Vector2 _offset;       // a textúra aktuális eltolása

    private void Awake() {
        _material = GetComponent<Renderer>().material;
        _offset = _material.GetTextureOffset("_MainTex");
    }

    private void Update() {
        _offset += Speed * Time.deltaTime;                        // az eltolást növeljük a sebességgel
        _material.SetTextureOffset("_MainTex", _offset);          // beállítjuk az eltolást a textúrához
    }
}