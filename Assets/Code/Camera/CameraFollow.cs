using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        this.transform.position = m_target.position;
    }
}
