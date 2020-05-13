using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarineTestInput : MonoBehaviour
{
	PlayerControls controls;
	
	void Awake() 
	{
		controls = new PlayerControls();
		
		controls.GameplaySoldier.Jump.performed += ctx => jump();
	}
	
	void jump()
	{
		Debug.Log('2');
	}
	
	void Jump()
	{
		Debug.Log('2');
	}
	
	public void onJump(InputValue value) 
	{
		Debug.Log('j');
	}
	
	private void onJump() 
	{
		Debug.Log('k');
	}
}
