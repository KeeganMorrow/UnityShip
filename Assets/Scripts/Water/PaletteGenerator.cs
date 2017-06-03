using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteGenerator : MonoBehaviour
{
    public Color leftColor;
    public Color rightColor;
    public int width;
    public int height;
    public void Awake()
    {
        Texture2D colourPalette = new Texture2D(256, 10, TextureFormat.ARGB32, false);

        for (int x = 0; x < width; x++)
        {
            float mixfactor = (float)x / (float)width;
            Color color = (1 - mixfactor) * leftColor + mixfactor * rightColor;
            for (int y = 0; y < height; y++)
            {
                colourPalette.SetPixel(x, y, color);
            }
        }
        colourPalette.filterMode = FilterMode.Point;
        colourPalette.wrapMode = TextureWrapMode.Clamp;
        colourPalette.Apply();
        GetComponent<Renderer>().material.SetTexture("_ColorRamp", colourPalette);
    }
}
