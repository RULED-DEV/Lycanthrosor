using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seal : MonoBehaviour
{
    public Color fade;
    public Color full;

    public seal_act act;
    public bool select;
    seal sl;
    public seal_act obj;

    void Awake(){
        sl = gameObject.GetComponent<seal>();
    }

    void OnMouseOver(){
        if(!FindObjectsByType<player_control>(FindObjectsSortMode.None)[0].iscombat){
            // only outside combat
            if(Input.GetMouseButtonDown(0) && !select){
                // pickup seal
                FindObjectsByType<player_control>(FindObjectsSortMode.None)[0].pickup(sl);
                select = true;
            }
            if(Input.GetMouseButtonDown(1) && select){
                // return
                return_seal();
            }
        }
    }

    public void doesfade(bool b){
        if(b){
            GetComponent<SpriteRenderer>().color = fade;
        }
        else{
            GetComponent<SpriteRenderer>().color = full;
        }
    }

    public void return_seal(){
        // dequip
        select = false;
        //obj.c.remove_seal(obj);
        Destroy(obj.gameObject);
        doesfade(false);
    }
}
