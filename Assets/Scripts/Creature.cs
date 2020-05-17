using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	
	public int maxHealth = 60;
	public float deathSpeed = 1;
	private Material material;
	public float deathProgress = 0;
	private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
		material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
		if (health <= 0) {
			deathProgress += deathSpeed * Time.deltaTime;
			material.SetFloat("_Progress", deathProgress);
			if (deathProgress > 1) {
				Debug.Log("Creature killed");
				Destroy(gameObject);
			}
		}
        
    }
	
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0) 
		{
		}
	}
}
