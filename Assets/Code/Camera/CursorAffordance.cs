using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    private CameraRaycaster m_camRaycaster;

    [SerializeField] private Texture2D m_moveCursor;
    [SerializeField] private Texture2D m_targetCursor;
    [SerializeField] private Texture2D m_unknownCursor;

    [SerializeField] private Vector2 m_cursorHotspot = new Vector2(0, 0);



    void Awake( )
    {
        m_camRaycaster = this.GetComponent<CameraRaycaster>();
        
        m_camRaycaster.OnLayerChanged += SetCursor;
    }

    private void SetCursor(Layer newLayer)
    {
        Texture2D cursor = m_unknownCursor;

        switch (newLayer)
        {
            case Layer.Enemy:
                cursor = m_targetCursor;
                break;
            case Layer.Walkable:
                cursor = m_moveCursor;
                break;
            case Layer.RaycastEndStop:
            default:
                break;
        }

        Cursor.SetCursor(cursor, m_cursorHotspot, CursorMode.Auto);

        Debug.Log("Setting Cursor");
    }
}
