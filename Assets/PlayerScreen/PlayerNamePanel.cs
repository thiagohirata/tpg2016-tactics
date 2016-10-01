using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerNamePanel : MonoBehaviour {
    InputField playerNameInputField;

	// Use this for initialization
	void Start () {
        playerNameInputField = GetComponentInChildren<InputField>();

        playerNameInputField.text = PlayerPrefs.GetString("PlayerName");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Jogar()
    {
        PlayerPrefs.SetString("PlayerName", playerNameInputField.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Stage1");
    }
}
