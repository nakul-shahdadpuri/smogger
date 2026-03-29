using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private int size = 20;
    [SerializeField] private int ringThickness = 2;
    [SerializeField] private Color color = new Color(0.7f, 0.7f, 0.7f, 0.9f);

    private Texture2D crosshairTex;

    private void Start()
    {
        crosshairTex = new Texture2D(size, size);
        crosshairTex.filterMode = FilterMode.Bilinear;

        float center = size / 2f;
        float outerRadius = size / 2f - 0.5f;
        float innerRadius = outerRadius - ringThickness;

        for (int x = 0; x < size; x++)
        for (int y = 0; y < size; y++)
        {
            float dist = Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), new Vector2(center, center));
            bool onRing = dist >= innerRadius && dist <= outerRadius;
            crosshairTex.SetPixel(x, y, onRing ? color : Color.clear);
        }

        crosshairTex.Apply();
    }

    private void OnGUI()
    {
        float x = Screen.width / 2f - size / 2f;
        float y = Screen.height / 2f - size / 2f;
        GUI.DrawTexture(new Rect(x, y, size, size), crosshairTex);
    }
}
