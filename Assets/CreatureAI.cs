using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CreatureAI : MonoBehaviour
{
	private PixelCrown.CharacterMovement2D movementController;

	public Transform target;
	
	public float nextWaypointDistance = 3f;
	public float minVerticalDistance = 1f;
	public float minHorizontalDistance = 1f;
	
	Path path;
	int currentWaypoint = 0;
	bool inMotion = true;
	
	Seeker seeker;
	Rigidbody2D rb;
	
    // Start is called before the first frame update
    void Start()
    {
		movementController = GetComponent<PixelCrown.CharacterMovement2D>();
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		InvokeRepeating("UpdatePath", 0f, 0.5f);
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
			if (movementController != null) {
				float horizontalMovement = 0;
				if (Mathf.Abs(direction.x) >= minHorizontalDistance)
					horizontalMovement = Mathf.Sign(direction.x);

				bool jumping = direction.y > minVerticalDistance;

				movementController.Move(horizontalMovement, false, jumping, false);
			}
			
			float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
			
			if (distance < nextWaypointDistance)
			{
				currentWaypoint++;
			}
		} 
    }
}
