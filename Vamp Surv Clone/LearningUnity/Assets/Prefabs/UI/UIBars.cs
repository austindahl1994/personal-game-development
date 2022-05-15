using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBars : MonoBehaviour
{
    private Animator anim;
    //public GameObject underPlayerHP; doesn't move directly with player
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (anim.GetBool("hidden"))
            {
                anim.SetBool("hidden", false);
            }
            else {
                anim.SetBool("hidden", true);
            }

            /*if (underPlayerHP.activeSelf)
            {
                underPlayerHP.SetActive(false);
            }
            else {
                underPlayerHP.SetActive(true);
            }*/
        } 
    }
}
