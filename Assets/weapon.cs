using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public AVATAR body;
    public weapon_type type;

    public int size;

    // needs to hold a set of components for knocker, haft and head
    // needs to grab a list of all valid components for the weapon type
    // needs to be able to quickly swap between components

    // components holds a list of actions which the player needs to be able to change

    public wep_comp head;
    public wep_comp haft;
    public wep_comp knocker;

    public void change_comp(int dir, component_type ct){
        REPOSITORY r = FindObjectsByType<REPOSITORY>(FindObjectsSortMode.None)[0];
        List<wep_comp> list = r.getlist(type); // full list
        int pos = 0;
        
        wep_comp comp = null;
        if(ct == component_type.head){comp = head;}
        if(ct == component_type.haft){comp = haft;}
        if(ct == component_type.knocker){comp = knocker;}

        for(int i = 0; i < list.Count; i++){
            wep_comp ce = list[i];
            if(ce.code == comp.code){
                pos = i+dir;
            }
            if(ce.comp_type != ct){
                // if the wep_comp is not one we want
                list.Remove(ce);
                i--; // backpedals
            }
        }
        // culls list of irrelevant items

        if(pos < 0){pos = list.Count-1;}
        if(pos >= list.Count){pos = 0;}
        // prevents it from falling out of range

        wep_comp c = Instantiate(list[pos].gameObject).GetComponent<wep_comp>();
        if(ct == component_type.head){
            Destroy(head.gameObject);
            head = c;
        }
        if(ct == component_type.haft){
            Destroy(haft.gameObject);
            haft = c;
        }
        if(ct == component_type.knocker){
            Destroy(knocker.gameObject);
            knocker = c;
        }
        c.transform.parent = transform;
        c.transform.localPosition = Vector2.zero;
        c.body = body;
    }

    // needs a button, reference to AVATAR and to do actions when pressed/prompted
}