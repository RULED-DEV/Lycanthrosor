using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_control : MonoBehaviour
{
    public AVATAR av;

    float timer;

    // for now just does debug
    void Update()
    {
        if(av.isturn){
            // does control only on turn
            if(timer < Time.time){
                // end turn
                timer = Time.time + 6;
                av.endturn();
            }
        }
    }
}
