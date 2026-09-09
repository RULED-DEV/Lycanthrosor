using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class border : MonoBehaviour
{
    GameObject HL;
    SpriteRenderer spr;
    public Material highlight_mat;

    void Start()
    {
        HL = Instantiate(new GameObject());
        HL.transform.parent = transform;
        spr = HL.AddComponent<SpriteRenderer>();
        spr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        // copies sprite to highlight
        spr.material = highlight_mat;
        
        HL.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        HL.transform.localPosition = Vector3.zero;
        HL.transform.localEulerAngles = Vector3.zero;
        spr.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder-1;
        spr.sortingLayerID = gameObject.GetComponent<SpriteRenderer>().sortingLayerID;

        HL.SetActive(false);
    }

    void OnMouseEnter(){
        HL.SetActive(true);
    }

    void OnMouseExit(){
        HL.SetActive(false);
    }
}
