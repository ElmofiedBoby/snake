using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartGame() {

        string currentScene = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(currentScene);
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void Die() {
        SceneManager.LoadScene("DeathMenu");
    }
}
