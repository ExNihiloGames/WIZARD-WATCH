using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICharacterPage : MonoBehaviour
{
	public GameObject CharacterSheet;
	public Image HeroImage;
	public TextMeshProUGUI HeroName;
	public TextMeshProUGUI HeroDescription;
	public Color ColorFire;
	public Color ColorHearth;
	public Color ColorIce;

	public void InitCharacterSheet(HeroID heroID)
	{
		HeroName.text = heroID.firstName +" "+ heroID.lastName;
		HeroDescription.text = "\n Aged " + heroID.age + "\n Faction: " + heroID.faction.factionName;
		switch (heroID.heroType)
		{
			case ElementalType.FIRE:
				HeroDescription.text += "\n This hero is type fire and fears ICE! ";
				HeroImage.color = ColorFire;
				break;
			case ElementalType.EARTH:
				HeroDescription.text += "\n This hero is type earth and fears FIRE! ";
				HeroImage.color = ColorHearth;
				break;
			case ElementalType.ICE:
				HeroDescription.text += "\n This hero is type ice and fears EARTH! ";
				HeroImage.color = ColorIce;
				break;
		}
		HeroImage.sprite = heroID.portrait;

		CharacterSheet.SetActive(true);
	}

	public void InitBlankPage()
	{
		CharacterSheet.SetActive(false);
	}
}
