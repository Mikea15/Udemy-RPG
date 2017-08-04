using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_maxHealthPoints = 100f;

    private float m_currentHealthPoints = 100f;

    public float healthAsPercentage { get { return m_currentHealthPoints / m_maxHealthPoints; } }
                                      
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
