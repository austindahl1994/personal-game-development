using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifacts : MonoBehaviour
{
    //creating a list of GameObjects to be used as choices for when the character levels up
    public List<GameObject> list = new List<GameObject>();
    public GameObject artifact1;
    public GameObject artifact2;
    public GameObject artifact3;
    //creating an array 

    private void Start()
    {
        list.Add(artifact1);
        list.Add(artifact2);
        list.Add(artifact3);
    }

    public GameObject getRandomListItem() {
        int rando = Random.Range(0, list.Count-1);
        Debug.Log(list.Count);
        return list[rando];
    }

    public void removeObjectList(GameObject listItem) {
        for (int i = 0; i < list.Count-1; i++) {
            if (GameObject.ReferenceEquals(list[i], listItem)) {
                list.RemoveAt(i);
            }
        }
    }

    public void addObjectToList(GameObject itemToBeAdded) {
        list.Add(itemToBeAdded);
    }
}
