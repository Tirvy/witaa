using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;

	private void OnFire1(InputValue value)
	{
		Shoot();
	}
	
	private void Shoot() 
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}
