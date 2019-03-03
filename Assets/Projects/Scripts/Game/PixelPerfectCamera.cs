using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
    public float PixelPerUnit;
    public float Scale;

    // felbontás, amire "szabva lett" a játék (x a szélesség, y a magasság)
    public Vector2 NativeResolution;

    private void Awake() {
        var gameCamera = GetComponent<Camera>();
        if (gameCamera.orthographic) {
            Scale = Screen.height / NativeResolution.y;    // mostani képernyő magasság / natív magasság
            PixelPerUnit *= Scale;    // az aránnyal skálázzúk a Pixel Per Unitot (ezzel tudunk koordinátarendszer - pixel konverziót végezni)
            gameCamera.orthographicSize = Screen.height / 2f / PixelPerUnit;
        }
    }
}
