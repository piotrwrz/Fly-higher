using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenes : MonoBehaviour
{
    public void ChangeToNormal()
    { 
        SceneManager.LoadScene(0);
    }
}
