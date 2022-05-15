using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public List<relic> allRelics = new List<relic>();
    public List<relic> allActiveRelics = new List<relic>();
    public relic relic;

    private void Start()
    {
        //relic = FindObjectOfType<relic>();
        foreach (relic relic in allRelics) {
            if (relic.isRelicActive) {
                allActiveRelics.Add(relic);
            }
        }

        foreach (relic relic in allActiveRelics) {
            
        }
    }

    public void setRelicActive(string relicName) {
        
    }
}
