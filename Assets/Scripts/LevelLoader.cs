using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void LoadLevel() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
