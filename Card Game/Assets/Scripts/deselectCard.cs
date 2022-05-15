using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deselectCard : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    Card card;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("deselect card called");
        gm.updateHandPosition();
    }
}
