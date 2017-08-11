using System;
using UnityEngine;
using UnityEditor.AI;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_walkMoveStopRadius = 0.2f;
    [SerializeField] private float m_attackMoveStopRadius = 5f;

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;
    [SerializeField] const int unknownLayerNumber = 10;


    ThirdPersonCharacter m_character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster m_cameraRaycaster;
    Vector3 m_currentDestination;

    AICharacterControl m_aiCharacterControl = null;

    private bool m_isInDirectMode = false;

    private Vector3 m_clickLocation;

    GameObject walkTarget = null;

    private void Start()
    {
        m_cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_cameraRaycaster.notifyMouseClickObservers += CameraRaycasterMouseClickObservers;
        m_character = GetComponent<ThirdPersonCharacter>();
        m_currentDestination = transform.position;

        m_aiCharacterControl = this.GetComponent<AICharacterControl>();

        walkTarget = new GameObject("Walk Target");
    }

    private void CameraRaycasterMouseClickObservers(RaycastHit raycastHit, int layerHit)
    {
        switch( layerHit )
        {
            case enemyLayerNumber:
                m_aiCharacterControl.SetTarget(raycastHit.collider.gameObject.transform);
                break;

            case walkableLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                m_aiCharacterControl.SetTarget(walkTarget.transform);
                break;

            case unknownLayerNumber:
            default:
                m_aiCharacterControl.SetTarget(this.transform);
                break;
        }
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

}

