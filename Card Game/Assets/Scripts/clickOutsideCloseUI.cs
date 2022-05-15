using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickOutsideCloseUI : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    //if click outside of this invisible element essentially go back to the game
    public void OnPointerClick(PointerEventData eventData)
    {
        gm.clearUIPile();
        gm.isInUI = false;
        gm.updateHandPosition();
        gm.showHand();
        gm.pileUI.gameObject.SetActive(false);
    }
}
