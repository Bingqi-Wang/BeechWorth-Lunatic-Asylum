using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private float timeVal;
    public float walkTime;
    public float runTime;

    // Start is called before the first frame update
    void Start()
    {
        walkTime = 0.4f;
        runTime = 0.3f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h!=0||v!=0)//有移动
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //跑步
                if (timeVal>=runTime)
                {
                    timeVal = 0;
                    audioSource.Stop();
                    audioSource.Play();
                }
            }
            else
            {
                //走路
                if (timeVal >= walkTime)
                {
                    timeVal = 0;
                    audioSource.Stop();
                    audioSource.Play();
                }
            }
            timeVal += Time.deltaTime;
        }
        else//移动停止
        {
            timeVal = walkTime;
        }
    }
}
