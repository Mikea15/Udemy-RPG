using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_maxHealthPoints = 100f;
    [SerializeField] private float m_attackRadius = 4f;

    private float m_currentHealthPoints = 100f;

    public float healthAsPercentage { get { return m_currentHealthPoints / m_maxHealthPoints; } }

    private AICharacterControl m_aiCharacterControl = null;

    private GameObject m_player;

    void Start ()
    {
        m_aiCharacterControl = this.GetComponent<AICharacterControl>();

        m_player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distanceToPlayer = Vector3.Distance(m_player.transform.position, this.transform.position);
		if( distanceToPlayer <= m_attackRadius )
        {
            m_aiCharacterControl.SetTarget(m_player.transform);
        }
        else
        {
            m_aiCharacterControl.SetTarget(this.transform);
        }
	}
}
