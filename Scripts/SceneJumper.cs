using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene(0);
    }
}
