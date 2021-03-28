using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class SavePrefs : MonoBehaviour
{
    int powerChoice;

 	

public void SetPowerChoice(int choice) {
    powerChoice = choice;
}
 	
public void SaveGame()
{
	PlayerPrefs.SetInt("SavedInteger", powerChoice);
	PlayerPrefs.Save();
    SceneManager.LoadScene (sceneName:"Level1");
	
}

}
