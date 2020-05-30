using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

	public Slider slider;
	public GameObject graphics;
	// public Gradient gradient;
	// public Image fill;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		// fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		slider.value = health;

		// fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public void Hide()
	{
		if (graphics != null)
		{
			graphics.SetActive(false);
		}
	}
}
