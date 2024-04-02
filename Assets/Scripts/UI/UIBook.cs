using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : MonoBehaviour
{
	public GameObject GameObjectToSetActive;

	public void OnClick()
	{
		GameObjectToSetActive.SetActive(true);
	}

	public void OnMouseDown()
	{
		//GameObjectToSetActive.SetActive(true);
	}
}
