using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using UnityEngine;

public class MainMenu : MonoBehaviour
{

   public void Play()
    {
        SceneManager.LoadScene("Ocean");
    }

    public void Quit() 
    {
        Application.Quit();
    }
    
}
