using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wep_comp : MonoBehaviour
{
    // stores effect for an action
    public string code;

    public weapon_type typeof_wep;
    public component_type comp_type;

    public List<seal_act> seals;
    public int act_limit;

    public AVATAR body;

    public void ACTION(bool inp){
        effect_cost efc = new effect_cost();
        List<effect> leff = new List<effect>();
        for(int i = 0; i < seals.Count; i++){
            if(seals[i] != null){
                efc.ADD(seals[i].a.cost); // aggregates all the costs of the actions
                foreach(effect e in seals[i].a.effects){
                    leff.Add(e); // aggregates all the effects
                }
            }
        }
        body.check_action(leff,efc,inp);
        body.check_effect(effect_trigger.on_action);
    }

    public bool add_seal(seal_act a){
        if(seals.Count < act_limit){
            seals.Add(a);
            // adds action
            return true;
        }
        return false;
    }

    public void remove_seal(seal_act a){
        seals.Remove(a);
        // removes action
    }

    void OnMouseOver(){
        if(body.antagonist != null){
            // only works in combat
            if(Input.GetMouseButtonDown(0)){
                ACTION(false);
            }
            if(Input.GetMouseButtonDown(1)){
                ACTION(true);
            }
        }
    }
}
