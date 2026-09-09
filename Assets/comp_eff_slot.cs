using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comp_eff_slot : MonoBehaviour
{
    // manages the slots that actions are placed into
    
    public action act;

    public void add_action(action a){
        act = a;
        gameObject.GetComponent<SpriteRenderer>().sprite = a.act_spr; // sets display sprite
    }

    public void remove_action(){
        act = null;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
