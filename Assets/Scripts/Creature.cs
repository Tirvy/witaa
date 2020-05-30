using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	
	[Tooltip("Initial Max Health")]
	public int maxHealth = 60;
	
    [Range(0.1f, 5f)]
	[Tooltip("Speed of death animation")]
	public float deathSpeed = 1;
	
	[Tooltip("Body with Sprite and Collider")]
	public GameObject creatureBody;
	
	[Tooltip("Body with Sprite and Collider")]
	private float deathProgress = 0;

	public Healthbar healthbar;
	
	
	private Material material;
	private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
		if (healthbar != null)
		{
			healthbar.SetMaxHealth(maxHealth);
		}
		material = creatureBody.GetComponent<SpriteRenderer>().material;
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
		if (healthbar != null)
		{
			healthbar.SetHealth(health);
		}

		if (health <= 0) 
		{
			healthbar.Hide();
		}
	}
}
