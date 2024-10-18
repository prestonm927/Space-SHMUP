using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
// To type the next 4 lines, start by typing /// and then Tab.
/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [ 0, 0, 0 ].
/// </summary>
public class BoundsCheck : MonoBehaviour
{   // a
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;
    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;
    private Vector3 boundSize;

    public object Vector3boundSize { get; private set; }

    void Awake()
    {
        camHeight = Camera.main.orthographicSize; // b
        camWidth = camHeight * Camera.main.aspect; // c
    }
    void LateUpdate()
    { // d
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
            isOnScreen = false;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
            isOnScreen = false;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
            isOnScreen = false;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
            isOnScreen = false;
        }
        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }
    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos()
    { // e
    if (!Application.isPlaying) return;
        Vector3boundSize = newVector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

    private object newVector3(float v1, float v2, float v3)
    {
        throw new NotImplementedException();
    }
}
