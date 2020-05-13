using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotImpact : MonoBehaviour
{
	public void Exploded () 
	{
		Destroy(gameObject);
	}
}
