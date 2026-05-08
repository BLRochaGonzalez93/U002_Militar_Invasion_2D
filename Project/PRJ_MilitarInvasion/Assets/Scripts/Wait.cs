using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{

    public float waitTime = 14f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForIntro());   
    }

    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
