using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this file holds every enum type the game will use

public enum effect_IDS
{        
    Body, // stress
    blood, // corruption
    soul, // mind
    lycanthropy,
    human,
    physical,
    balance,
    occult,
    orthadox,
    bodily_wound,
    weapons,
    // broad abstract types

    bleed,
    clot,
    regeneration,
    Tumour
    // specific ID names
}
// holds all the valid effect ID types

public enum effect_trigger
{        
    ALL,
    on_turn_start,
    on_turn_end,
    on_hit,
    on_attack,
    on_action,
    on_apply
}
// holds all valid trigger types

public enum effect_type
{        
    Exchange_adrenaline_endorphin,
    Exchange_human_beast,
    Exchange_rational_delusional,
    // changes given humour

    Diminish_strain,
    Diminish_blood,
    Diminish_psyche,
    // changes fluid total

    Cost_strain_adrenaline,
    Cost_strain_endorphin,
    Cost_blood_human,
    Cost_blood_beast,
    Cost_psyche_rational,
    Cost_psyche_delusional,
    // cost for actions

    Effect_add,
    Effect_remove,
    Effect_change
    // applies/changes bodies effects
}
// holds all the effect types that exist

public enum weapon_type
{        
    knife,
    gun,
    club,
    haft,
    sword,
    shovel
}
// holds all the weapon types

public enum component_type
{        
    head,
    haft,
    knocker
}
// holds all the component types

[System.Serializable]
public struct effect_cost
{
    public int Cost_strain;
    public int Cost_blood;
    public int Cost_psyche;

    public int Cost_strain_adrenaline;
    public int Cost_strain_endorphin;

    public int Cost_blood_beast;
    public int Cost_blood_human;

    public int Cost_psyche_delusional;
    public int Cost_psyche_rational;

    public void ADD(effect_cost c){
        // used to aggregate multiple costs into one cost
        Cost_strain += c.Cost_strain;
        Cost_blood += c.Cost_blood;
        Cost_psyche += c.Cost_psyche;

        Cost_strain_adrenaline += c.Cost_strain_adrenaline;
        Cost_strain_endorphin += c.Cost_strain_endorphin;

        Cost_blood_beast += c.Cost_blood_beast;
        Cost_blood_human += c.Cost_blood_human;

        Cost_psyche_delusional += c.Cost_psyche_delusional;
        Cost_psyche_rational += c.Cost_psyche_rational;
    }
}

[System.Serializable]
public struct effect_stat
{
    // holds an action type (balance humour, diminish liquid etc)
    // holds value for action type

    public effect_type effect_ID;

    public float potency;

    public int limit;

    public List<effect_IDS> effects_of_type; // only relevant for change,remove

    public effect add_effect; // only relevant for add,change
}