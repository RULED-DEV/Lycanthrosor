using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVATAR : MonoBehaviour
{
    // manages liquids
    // manages active effects (buffs/debuffs)
    // manages any other relevant things

    public bool isturn;

    public AVATAR antagonist;

    [Header("STRAIN")] 
    public int STRAIN_CAP; // max limit

    public int fluid_strain_amount; // amount we have
    public int adrenaline_strain_tot; // percentage of each humour type // pls rename i am begging you
    public int endorphins_strain_tot;

    public int adrenaline_spelt_tot;
    public int endorphin_spelt_tot;

    [Header("BLOOD")] 
    public int BLOOD_CAP;

    public int fluid_blood_amount;
    public int human_blood_tot; 
    public int beast_blood_tot;

    public int human_spelt_tot;
    public int beast_spelt_tot;

    [Header("PSYCHE")] 
    public int PSYCHE_CAP;

    public int fluid_psyche_amount;
    public int rational_psyche_tot;
    public int delusional_psyche_tot;

    public int rational_spelt_tot;
    public int delusional_spelt_tot;

    public List<effect> passive_effects = new List<effect>();

    void Awake(){
        if(isturn){check_effect(effect_trigger.on_turn_start);}
    }

    public void endturn(){
        check_effect(effect_trigger.on_turn_end); // ends turn
        isturn = !isturn; // passes turn
        balance_stats();
        antagonist.isturn = !isturn;
        antagonist.balance_stats();
        antagonist.check_effect(effect_trigger.on_turn_start); // starts turn for enemy
    }

    public void check_action(List<effect> effects, effect_cost cost, bool self){
        bool check = true;

        if(cost.Cost_strain > fluid_strain_amount){check = false;}
        if(cost.Cost_blood > fluid_blood_amount){check = false;}
        if(cost.Cost_psyche > fluid_psyche_amount){check = false;}

        if(cost.Cost_blood_beast > beast_blood_tot){check = false;}
        if(cost.Cost_blood_human > human_blood_tot){check = false;}

        if(cost.Cost_psyche_delusional > delusional_psyche_tot){check = false;}
        if(cost.Cost_psyche_rational > rational_psyche_tot){check = false;}

        if(cost.Cost_strain_adrenaline > adrenaline_strain_tot){check = false;}
        if(cost.Cost_strain_endorphin > endorphins_strain_tot){check = false;}
        // checks we have valid fluid amounts for the cost

        if(check){
            fluid_blood_amount -= cost.Cost_blood;
            fluid_psyche_amount -= cost.Cost_psyche;
            fluid_strain_amount -= cost.Cost_strain;

            beast_blood_tot -= cost.Cost_blood_beast;
            human_spelt_tot += cost.Cost_blood_beast;
            human_blood_tot -= cost.Cost_blood_human;
            beast_spelt_tot += cost.Cost_blood_human;

            delusional_psyche_tot -= cost.Cost_psyche_delusional;
            delusional_spelt_tot += cost.Cost_psyche_delusional;
            rational_psyche_tot -= cost.Cost_psyche_rational;
            rational_spelt_tot += cost.Cost_psyche_rational;

            adrenaline_strain_tot -= cost.Cost_strain_adrenaline;
            adrenaline_spelt_tot += cost.Cost_strain_adrenaline;
            endorphins_strain_tot -= cost.Cost_strain_endorphin;
            endorphin_spelt_tot += cost.Cost_strain_endorphin;

            foreach(effect f in effects){
                apply_effect(f,self); // applies effect
            }
        }
    }

    public void apply_effect(effect template, bool self){ // used to apply action effects
        AVATAR body = antagonist;
        AVATAR enemy = gameObject.GetComponent<AVATAR>();
        if(self){ // applies effect to self
            body = gameObject.GetComponent<AVATAR>();
            enemy = antagonist;
        }

        effect eff = Instantiate(template).GetComponent<effect>();

        eff.b = body; // assigns avatar references
        eff.e = enemy;

        eff.b.passive_effects.Add(eff);

        body.check_effect(effect_trigger.on_hit); // triggers any on hit effects (I.e, extra damage or blocking attack)
        body.check_effect(effect_trigger.on_attack); // triggers the final damage after checks
    }

    public void apply_effect_change(effect_stat f, string change_type){
        int total = 0;
        List<effect> list = new List<effect>();
        foreach(effect e in passive_effects){ // goes through all applied effects
            foreach(effect_IDS i in f.effects_of_type){ // goes through all valid tags
                if(e.eff_id.Contains(i)){
                    total ++;
                    list.Add(e); // adds valid items to list
                }
            }
        }
        while(total >= f.potency){
            total -= (int)f.potency;
            if(change_type == "add"){ // doubles potency each time, USE WITH CAUTION
                for(int i = 0; i < list.Count; i++){
                    // loops through total valid effects
                    if(i < f.limit || f.limit <= 0){
                        effect fe = Instantiate(f.add_effect).GetComponent<effect>();
                        passive_effects.Add(fe);
                        fe.b = gameObject.GetComponent<AVATAR>();
                        fe.e = antagonist;
                    }
                }
            }
            if(change_type == "remove"){ // works
                for(int i = 0; i < list.Count; i++){
                    // loops through total valid effects
                    if(i < f.limit || f.limit <= 0){
                        effect fe = list[i]; // effect that needs to be removed
                        passive_effects.Remove(fe);
                        Destroy(fe.gameObject);
                    }
                }
            }
            if(change_type == "change"){ // works
                for(int i = 0; i < list.Count; i++){
                    // loops through total valid effects
                    if(i < f.potency || f.potency <= 0){ 
                        effect fe = list[i]; // effect that needs to be removed
                        passive_effects.Remove(fe);
                        Destroy(fe.gameObject);
                    }
                    if(i < f.limit || f.limit <= 0){
                        effect fe = Instantiate(f.add_effect).GetComponent<effect>();
                        passive_effects.Add(fe);
                        fe.b = gameObject.GetComponent<AVATAR>();
                        fe.e = antagonist;
                    }
                }
            }
        }
    }

    public void check_effect(effect_trigger ch_id){
        for(int i = 0; i < passive_effects.Count; i++){
            effect f = passive_effects[i];
            if(f.tri_id.Contains(ch_id) || f.tri_id.Contains(effect_trigger.ALL)){ // ALL means activates on any check
                f.activate();
            }
        }
    }

    public void check_stats(){
        fluid_strain_amount = Mathf.Clamp(fluid_strain_amount,0,STRAIN_CAP*3);
        fluid_blood_amount = Mathf.Clamp(fluid_blood_amount,0,BLOOD_CAP*3);
        fluid_psyche_amount = Mathf.Clamp(fluid_psyche_amount,0,PSYCHE_CAP*3);

        adrenaline_strain_tot = Mathf.Clamp(adrenaline_strain_tot,0,fluid_strain_amount);
        endorphins_strain_tot = Mathf.Clamp(endorphins_strain_tot,0,fluid_strain_amount);

        human_blood_tot = Mathf.Clamp(human_blood_tot,0,fluid_blood_amount);
        beast_blood_tot = Mathf.Clamp(beast_blood_tot,0,fluid_blood_amount);

        rational_psyche_tot = Mathf.Clamp(rational_psyche_tot,0,fluid_psyche_amount);
        delusional_psyche_tot = Mathf.Clamp(delusional_psyche_tot,0,fluid_psyche_amount);
    }

    public void balance_stats(){
        fluid_strain_amount = Mathf.Clamp(fluid_strain_amount,0,STRAIN_CAP*3);
        fluid_blood_amount = Mathf.Clamp(fluid_blood_amount,0,BLOOD_CAP*3);
        fluid_psyche_amount = Mathf.Clamp(fluid_psyche_amount,0,PSYCHE_CAP*3);
        // ensures fluids cant exceed 0-limit

        // needs to account for tot being greater rather than the individual value
        adrenaline_strain_tot += adrenaline_spelt_tot;
        endorphins_strain_tot += endorphin_spelt_tot;
        float tot = adrenaline_strain_tot+endorphins_strain_tot; 
        if(tot != fluid_strain_amount){
            float p1 = fluid_strain_amount*(adrenaline_strain_tot/tot);
            float p2 = fluid_strain_amount*(endorphins_strain_tot/tot);

            adrenaline_strain_tot = (int)(p1-(p1%1));
            endorphins_strain_tot = (int)(p2-(p2%1));
        }
        tot = adrenaline_strain_tot+endorphins_strain_tot; 
        if(tot != fluid_strain_amount){
            adrenaline_strain_tot += fluid_strain_amount-(int)tot;
        }
        adrenaline_strain_tot = Mathf.Clamp(adrenaline_strain_tot,0,fluid_strain_amount);
        endorphins_strain_tot = Mathf.Clamp(endorphins_strain_tot,0,fluid_strain_amount);
        // balances strain

        human_blood_tot += human_spelt_tot;
        beast_blood_tot += beast_spelt_tot;
        tot = human_blood_tot+beast_blood_tot;
        if(tot != fluid_blood_amount){
            float p1 = fluid_blood_amount*(human_blood_tot/tot);
            float p2 = fluid_blood_amount*(beast_blood_tot/tot);

            human_blood_tot = (int)(p1-(p1%1));
            beast_blood_tot = (int)(p2-(p2%1));
        }
        tot = human_blood_tot+beast_blood_tot; 
        if(tot != fluid_blood_amount){
            human_blood_tot += fluid_blood_amount-(int)tot;
        }
        human_blood_tot = Mathf.Clamp(human_blood_tot,0,fluid_blood_amount);
        beast_blood_tot = Mathf.Clamp(beast_blood_tot,0,fluid_blood_amount);
        // balances blood

        rational_psyche_tot += rational_spelt_tot;
        delusional_psyche_tot += delusional_spelt_tot;
        tot = rational_psyche_tot+delusional_psyche_tot;
        if(tot != fluid_psyche_amount){
            float p1 = fluid_psyche_amount*(rational_psyche_tot/tot);
            float p2 = fluid_psyche_amount*(delusional_psyche_tot/tot);

            rational_psyche_tot = (int)(p1-(p1%1));
            delusional_psyche_tot = (int)(p2-(p2%1));
        }
        tot = rational_psyche_tot+delusional_psyche_tot; 
        if(tot != fluid_psyche_amount){
            rational_psyche_tot += fluid_psyche_amount-(int)tot;
        }
        rational_psyche_tot = Mathf.Clamp(rational_psyche_tot,0,fluid_psyche_amount);
        delusional_psyche_tot = Mathf.Clamp(delusional_psyche_tot,0,fluid_psyche_amount);
        // balances psyche

        beast_spelt_tot = 0;
        human_spelt_tot = 0;
        rational_spelt_tot = 0;
        endorphin_spelt_tot = 0;
        adrenaline_spelt_tot = 0;
        delusional_spelt_tot = 0;
    }
}
