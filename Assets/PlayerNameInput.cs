using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", nameInput.text);
    }   
}

