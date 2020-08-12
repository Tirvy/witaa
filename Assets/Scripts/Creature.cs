using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	
	[Tooltip("Initial Max Health")]
	public float maxHealth = 60f;
	
    [Range(0.1f, 5f)]
	[Tooltip("Speed of death animation")]
	public float deathSpeed = 1;
	
	[Tooltip("Body with Sprite and Collider")]
	public GameObject creatureBody;
	
	[Tooltip("Body with Sprite and Collider")]
	private float deathProgress = 0;

	[Tooltip("Healthbar ref")]
	public Healthbar healthbar;
	
	
	private Material material;
	private float health;
	private List<Vector2> frameHitPushes = new List<Vector2>();
	private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
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

	private void FixedUpdate() {
		applyHitPushes();
	}
	
	private void applyHitPushes() {
		if (frameHitPushes.Count > 0) {
			Vector2 pushSum = Vector2.zero;
			frameHitPushes.ForEach(delegate(Vector2 push)
			{
				pushSum += push;
			});
			rb.AddForce(pushSum * 10);
			frameHitPushes.Clear();
		}
	}

	private void ApplyDamage(float damage)
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

	private void ApplyHit(Vector3 position, Vector2 impulse) {
		material.SetVector("_DamageSource", position);
		frameHitPushes.Add(impulse);
		// ObjectPooler.Instance.SpawnFromPool("Hitmark", transform.position + position);
		ObjectPooler.Instance.SpawnFromPool("Hitmark", position);
	}
	
	public void TakeDamage(DamageSource damageSource)
	{
		ApplyDamage(damageSource.power);
		ApplyHit(damageSource.position, damageSource.impulse);
	}
}
