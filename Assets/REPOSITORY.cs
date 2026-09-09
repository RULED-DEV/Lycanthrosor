using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REPOSITORY : MonoBehaviour
{
    // used to store components and actions

    public List<wep_comp> knife_comps;
    public List<wep_comp> gun_comps;
    public List<wep_comp> hafted_comps;
    public List<wep_comp> club_comps;
    public List<wep_comp> shovel_comps;
    public List<wep_comp> sword_comps;

    public List<wep_comp> getlist(weapon_type w){
        List<wep_comp> l = null;
        if(w == weapon_type.knife){l = knife_comps;}
        else if(w == weapon_type.gun){l = gun_comps;}
        else if(w == weapon_type.haft){l = hafted_comps;}
        else if(w == weapon_type.club){l = club_comps;}
        else if(w == weapon_type.shovel){l = shovel_comps;}
        else if(w == weapon_type.sword){l = sword_comps;}
        return new List<wep_comp>(l);
    }
}