using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePrefs : MonoBehaviour
{

    //Tracks player choice in intro scene
    int powerChoice;

//This is used on the button game object as a function to call when each button is clicked
public void SetPowerChoice(int choice) {
    powerChoice = choice;
}
 	
//We can save the player choice at the end of the customization
public void SaveGame()
{
    //Using playerprefs to save data and load it in the other scene
	PlayerPrefs.SetInt("SavedInteger", powerChoice);
	PlayerPrefs.Save();

    //We proceed to load the first scene
    SceneManager.LoadScene (sceneName:"Level1");
	
}

}
