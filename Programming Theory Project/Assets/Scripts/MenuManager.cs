using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string playerName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName()
    {
        playerName = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log(playerName);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
