using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
	public GameObject BookClosed;
	public GameObject BookOpen;

	public void OnClickPlay()
	{
		SceneManager.LoadScene("MainGame");
	}

	public void OnClickQuit()
	{
		Application.Quit();	
	}

	public void OnClickCredits()
	{
		BookOpen.SetActive(true);
		BookClosed.SetActive(false);
	}

	public void OnClickCloseCredits()
	{
		BookOpen.SetActive(false);
		BookClosed.SetActive(true);
	}
}
