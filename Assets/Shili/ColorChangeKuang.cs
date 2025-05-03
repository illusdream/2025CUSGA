using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorChangeKuang : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.black;
    public float speed = 1.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ê¹ÓÃ Time.unscaledTime ´úÌæ Time.time
        float t = Mathf.PingPong(Time.unscaledTime * speed, 1);
        spriteRenderer.color = Color.Lerp(startColor, endColor, t);
    }
}