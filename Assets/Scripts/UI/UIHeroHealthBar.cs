using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroHealthBar : MonoBehaviour
{
	public Image BackgroundImage;
	public Image CurrentHealth;

	Hero _hero;

	public void Init(Hero hero)
	{
		_hero = hero;
		// hero.currentHP
	}
}
