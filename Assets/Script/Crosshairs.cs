using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public SpriteRenderer dot;
    [SerializeField] public Color doHighlightColour;

    Color originalDotColour;

    private void Start()
    {
        Cursor.visible = false;
        originalDotColour = dot.color;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * 40 * Time.deltaTime);
    }

    public void DetectTargets(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            dot.color = doHighlightColour;
        }
        else
        {
            dot.color = originalDotColour;
        }
    }
}
