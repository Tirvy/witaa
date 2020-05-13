using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MultiPlayerController2D : MonoBehaviour {
	
	private PixelCrown.CharacterMovement2D movementController;
	
	//PlayerControls controls;
	
	private int controlMovementHorizontal = 0;
	private bool isJumping;
	private bool isCrouching;
	
	void Awake() 
	{
		//controls = new PlayerControls();
	}
	
	void Start()
	{
		movementController = GetComponent<PixelCrown.CharacterMovement2D>();
	} 
	
	private void FixedUpdate()
	{
		if (!movementController) {
			return;
		}
		movementController.Move(controlMovementHorizontal, isCrouching, isJumping, false);
	}

    public void OnMove(InputValue value)
    {
		float horizontalValue = value.Get<float>();
		if (horizontalValue == 0)
		{
			controlMovementHorizontal = 0;
		}
		else if (horizontalValue > 0)
		{
			controlMovementHorizontal = 1;
		}
		else 
			controlMovementHorizontal = -1;
		 
    }

	public void OnJump(InputValue value)
	{
		if (value.isPressed)
			isJumping = true;
		else
			isJumping = false;
	}
}