using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{

    public AVATAR av;

    public int eq_slot = 2;

    public weapon w1;

    public weapon[] weapons = new weapon[2];
    public Transform[] weapon_slots;

    public seal selected;
    public seal_act seal_obj;

    public Camera cam;

    public bool iscombat;

    // for now just does debug
    void Update()
    { // weapon collider needs to inactive outside of combat, make codified combat exiter so that highlights and such dont confuse
        if(av.isturn){
            // does control only on turn
            if(av.antagonist == null){
                // out of combat
                iscombat = false;

                // seal stuff
                if(selected != null){
                    // seal controls
                    follow_mouse();
                    if(Input.GetMouseButtonDown(1)){
                        selected.return_seal();
                        selected = null;
                        seal_obj = null;
                    }
                    if(Input.GetMouseButtonUp(0)){
                        // check to apply
                        if(seal_obj.check()){
                            selected = null; 
                            seal_obj = null;
                        }
                        else{
                            // return seal
                            selected.return_seal();
                            selected = null;
                            seal_obj = null;
                        }
                    }
                }

                if(Input.GetKeyDown(KeyCode.Q)){
                    equip_weapon(w1);
                }
            }
            else{
                // inside combat
                iscombat = true;
                if(Input.GetKeyDown(KeyCode.Space)){
                    // end turn
                    av.endturn();
                }
                
            }
            

            
        }
    }

    void follow_mouse(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(mousePosition.x,mousePosition.y,0);
        seal_obj.transform.position = mousePosition;
    }

    public void pickup(seal sl){
        if(selected != null){
            // replaces selected
            selected.return_seal();
            selected = null;
            seal_obj = null;
        }
        selected = sl;
        selected.doesfade(true);
        if(sl.obj == null){
            // make new seal
            seal_obj = Instantiate(selected.act).GetComponent<seal_act>();
            seal_obj.s = selected;
            selected.obj = seal_obj;
        }
        else{
            // seal already exits
            seal_obj = sl.obj;
        }
    }

    public void equip_weapon(weapon wep){
        // first dictates what hand we are equiping to
        if(wep != null && eq_slot >= wep.size){
            eq_slot -= wep.size;
            weapon w = Instantiate(wep.gameObject).GetComponent<weapon>();
            if(weapons[0] != null){
                weapons[1] = w;
                w.transform.parent = weapon_slots[1];
            }
            else{
                weapons[0] = w;
                w.transform.parent = weapon_slots[0];
            }
            w.transform.localPosition = Vector2.zero;
            w.body = av;
            w.head.body = av;
            w.haft.body = av;
            w.knocker.body = av;
        }
    }

    public void dequip_weapon(int pos){
        weapon w = weapons[pos];
        if(w != null){
            eq_slot += w.size;
            // delete weapon
            Destroy(w.gameObject);
            // refund actions attachements, etc
        } 
    }
}