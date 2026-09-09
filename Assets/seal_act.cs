using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seal_act : MonoBehaviour
{
    public action a;

    public seal s;

    public wep_comp c;

    public bool check(){
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.enabled = true;
        List<Collider2D> cols = new List<Collider2D>();
        // retrieves all the wepon components
        col.Overlap(cols);
        foreach(Collider2D co in cols){
            if(co.GetComponent<wep_comp>() != null){
                if(co.GetComponent<wep_comp>().add_seal(gameObject.GetComponent<seal_act>())){
                    // adds seal
                    c = co.GetComponent<wep_comp>();
                    return true;
                }
            }
        }
        col.enabled = false;
        return false;
    }

    void OnMouseOver(){
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if(col.enabled){
            if(Input.GetMouseButtonDown(0)){
                // reselect
                remove_from_wep();
                FindObjectsByType<player_control>(FindObjectsSortMode.None)[0].pickup(s);
            }
            if(Input.GetMouseButtonDown(1)){
                // return
                remove_from_wep();
                s.return_seal();
            }
        }
    }

    public void remove_from_wep(){
        c.remove_seal(gameObject.GetComponent<seal_act>());
        // removes seal
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}