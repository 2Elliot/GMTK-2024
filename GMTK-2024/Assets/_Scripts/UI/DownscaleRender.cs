using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DownscaleRender : MonoBehaviour
{
    public Camera camera;
    public CustomRenderTexture texture;
    public Vector2Int downscaleValue;
    public Material material;
    public RawImage raw;
    // Start is called before the first frame update
    void Start()
    {
        texture = new CustomRenderTexture(downscaleValue.x, downscaleValue.y);
        texture.material = material;
        texture.filterMode = FilterMode.Point;

        camera.targetTexture = texture;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0, 0, 0, 0);

        raw.texture = texture;
        raw.material = material;
    }
}
