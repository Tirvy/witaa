using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CreatureAI : MonoBehaviour
{
	public Transform target;
	
	public float speed;
	public float nextWaypointDistance = 3f;
	
	Path path;
	int currentWaypoint = 0;
	bool inMotion = true;
	
	Seeker seeker;
	Rigidbody2D rb;
	
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		InvokeRepeating("UpdatePath", 0f, 1f);
    }
	
	void UpdatePath()
	{
		if (seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}
	
	void OnPathComplete(Path newPath)
	{
		if (!newPath.error)
		{
			path = newPath;
			currentWaypoint = 0;
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (path != null) 
		{
			if (currentWaypoint >= path.vectorPath.Count)
			{
				inMotion = false;
				return;
			} else
			{
				inMotion = true;
			}
			
			Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
			Vector2 force = new Vector2(direction.x * speed * Time.deltaTime, 0f);
			
			rb.AddForce(force);
			
			float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
			
			if (distance < nextWaypointDistance)
			{
				currentWaypoint++;
			}
		} 
    }
}
