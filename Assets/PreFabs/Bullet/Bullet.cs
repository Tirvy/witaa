using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public float damage = 20f;
	public Rigidbody2D rb;
	public GameObject impactEffect;
	public float projectileMass = 0.1f; 

	// Use this for initialization
	void Start () {
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D (	Collider2D hitInfo)
	{
		Creature enemy = hitInfo.GetComponent<Creature>();
		if (enemy == null) {
			enemy = hitInfo.transform.parent.GetComponent<Creature>();
		}
		
		if (enemy != null)
		{
			Vector3 hitOffset = transform.position - hitInfo.transform.position;
			Debug.Log(hitOffset);
			enemy.TakeDamage(new DamageSource(damage, "plasma", transform.position, rb.velocity * projectileMass));
		}

		Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);
	}
	
}
