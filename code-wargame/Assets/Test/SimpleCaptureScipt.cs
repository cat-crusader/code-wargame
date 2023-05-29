using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCaptureScipt : MonoBehaviour
{
    public GameObject WinLabel;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Win();
        Debug.Log("Win");
    }
    private void Win()
    {
        WinLabel.SetActive(true);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
