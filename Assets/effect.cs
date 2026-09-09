using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    public List<effect_IDS> eff_id = new List<effect_IDS>();
    // groups effects by type

    public List<effect_trigger> tri_id = new List<effect_trigger>();
    // activation condition

    public List<effect_stat> effects = new List<effect_stat>();
    // all the effects when we do actually activate

    public AVATAR b;
    public AVATAR e;
    // body the effect effects

    public bool apply_on_enemy;
    public bool temp;
    // extra info if needed

    public void activate(){
        AVATAR body = b;
        if(apply_on_enemy){
            body = e;
        }
        for(int i = 0; i < effects.Count; i++){
            effect_stat f = effects[i]; 
            // goes through every effect
            switch (f.effect_ID){
                // checks for each effect type
                // // // // COST STATS // // // // 
                    case effect_type.Cost_strain_adrenaline:
                        body.adrenaline_strain_tot -= (int)f.potency;
                        break;
                    case effect_type.Cost_strain_endorphin:
                        body.endorphins_strain_tot -= (int)f.potency;
                        break;
                    case effect_type.Cost_blood_human:
                        body.human_blood_tot -= (int)f.potency;
                        break;
                    case effect_type.Cost_blood_beast:
                        body.beast_blood_tot -= (int)f.potency;
                        break;
                    case effect_type.Cost_psyche_rational:
                        body.rational_psyche_tot -= (int)f.potency;
                        break;
                    case effect_type.Cost_psyche_delusional:
                        body.delusional_psyche_tot -= (int)f.potency;
                        break;
                // // // // BALANCE STATS // // // //
                    case effect_type.Exchange_adrenaline_endorphin:
                        body.adrenaline_strain_tot += (int)f.potency;
                        body.endorphins_strain_tot -= (int)f.potency;
                        break;
                    case effect_type.Exchange_human_beast:
                        body.human_blood_tot += (int)f.potency;
                        body.beast_blood_tot -= (int)f.potency;
                        break;
                    case effect_type.Exchange_rational_delusional:
                        body.rational_psyche_tot += (int)f.potency;
                        body.delusional_psyche_tot -= (int)f.potency;
                        break;
                    
                // // // // DIMINISH STATS // // // // 
                    case effect_type.Diminish_strain:
                        body.fluid_strain_amount -= (int)f.potency;
                        break;
                    case effect_type.Diminish_blood:
                        body.fluid_blood_amount -= (int)f.potency;
                        break;
                    case effect_type.Diminish_psyche:
                        body.fluid_psyche_amount -= (int)f.potency;
                        break;

                // // // // PASSIVE EFFECT // // // //
                    case effect_type.Effect_add:
                        // add effect
                        body.apply_effect_change(f,"add");
                        break;
                    case effect_type.Effect_remove:
                        // remove effect
                        body.apply_effect_change(f,"remove");
                        break;
                    case effect_type.Effect_change:
                        body.apply_effect_change(f,"change");
                        break;
            }
        }
        if(temp){
            Invoke("remove",0.0001f); // deletes object next frame after the check is done
        }
        body.check_stats(); // prevents stats from falling too low
    }

    void remove(){
        b.passive_effects.Remove(gameObject.GetComponent<effect>());
        Destroy(gameObject);
    }
}
