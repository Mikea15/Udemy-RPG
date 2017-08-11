using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_walkMoveStopRadius = 0.2f;
    [SerializeField] private float m_attackMoveStopRadius = 5f;


    ThirdPersonCharacter m_character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster m_cameraRaycaster;
    Vector3 m_currentDestination;

    private bool m_isInDirectMode = false;

    private Vector3 m_clickLocation;

    private void Start()
    {
        m_cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_character = GetComponent<ThirdPersonCharacter>();
        m_currentDestination = transform.position;
    }

    private void ProcessDirectMovement( )
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = Vector3.Scale( Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward + h * Camera.main.transform.right;

        m_character.Move(move, false, false);
    }

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        m_clickLocation = m_cameraRaycaster.hit.point;
    //        switch (m_cameraRaycaster.layerHit)
    //        {
    //            case Layer.Walkable:
    //                m_currentDestination = ShortDestination(m_clickLocation, m_walkMoveStopRadius);
    //                break;
    //            case Layer.Enemy:
    //                Debug.Log("Not moving to enemy");
    //                m_currentDestination = ShortDestination(m_clickLocation, m_attackMoveStopRadius);
    //                break;
    //            default:
    //                Debug.Log("Unexpected layer found");
    //                break;
    //        }
    //    }

    //    WalkToDestination();
    //}

    private void WalkToDestination()
    {
        Vector3 dir = m_currentDestination - transform.position;
        if (dir.sqrMagnitude >= 0)
        {
            m_character.Move(dir, false, false);
        }
        else
        {
            m_character.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination( Vector3 destination, float shorteningFactor )
    {
        Vector3 reducedVector = (destination - this.transform.position).normalized * shorteningFactor;
        return destination - reducedVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(this.transform.position, m_clickLocation);

        Gizmos.DrawSphere(m_currentDestination, 0.1f);
        Gizmos.DrawSphere(m_clickLocation, 0.15f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_attackMoveStopRadius);
    }
}

