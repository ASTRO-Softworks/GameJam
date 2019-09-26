using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    float timer;
    bool flag = false;
    public AudioSource ss;
    //AudioSource
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TogglePlay()
    {
        flag = !flag;
        if (flag) ss.Play();
        else ss.Stop();
    }
}
