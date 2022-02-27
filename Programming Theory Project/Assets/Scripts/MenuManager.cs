using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string playerName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName()
    {
        playerName = inputField.GetComponent<TMP_InputField>().text;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
