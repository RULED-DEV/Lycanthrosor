using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{

    // takes input check id
    // grabs list of all active effects
    // sorts list for effects with relevant id
    // activates effects

    public static void check_effect(effect_trigger ch_id){
        effect[] eff = Object.FindObjectsByType<effect>(FindObjectsSortMode.None);
        foreach(effect f in eff){
            if(f.tri_id.Contains(ch_id) || f.tri_id.Contains(effect_trigger.ALL)){ // * means activates on any check
                f.activate();
            }
        }
    }
}