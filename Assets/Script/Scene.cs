using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void btnHelpScene()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void btnGameScene()
    {
    
        SceneManager.LoadScene("GameScene");
    }
}
