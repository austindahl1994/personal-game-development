using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    public GameManager gm;
    public cardPlayer play;
    //public Card card;

    private Color hideImage;
    private Color showImage;
    private GameObject player;
    private Camera cam;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        play = FindObjectOfType<cardPlayer>();
        this.transform.position = transform.parent.transform.position;
        //set the enemy as the listed element under the enemyUI portion, listed 2 at the moment
        updateplayer();
        image = GetComponent<Image>();
        hideImage = image.color;
        showImage = image.color;
        hideImage.a = 0.0f;
        showImage.a = 1.0f;
        image.color = hideImage;
        cam = Camera.main;
    }

    public void updateplayer()
    {
        player = this.gameObject.transform.GetChild(2).gameObject; //enemy gameobject
        //Debug.Log("The current enemy is: " + enemy);
        player.gameObject.SetActive(true);
        player.gameObject.GetComponent<player>().posSetup();
        player.gameObject.GetComponent<player>().updateBlock();
        player.gameObject.transform.SetSiblingIndex(2);
        player.gameObject.GetComponent<player>().updatePlayerHealth(0);

        //Debug.Log("Current enemy is: " + enemy);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = showImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = hideImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("this position is canvas positioning is: " + this.transform.position);
        //Debug.Log(getCurrentUIPosition());
        //Debug.Log("clicked on enemy UI");
        //Debug.Log("calling gm.getcardobject");
        //Debug.Log("Hand count from enemyUI: " + gm.getHand().Count);
        Card card1 = gm.getCardObject();
        //Debug.Log(card1);
        if (card1 != null)
        {
            card1.transform.localScale = new Vector3(1, 1, 1);
            //Debug.Log("Sending both: " + card1 + " " + enemy + "from ui script");
            play.playCard(card1, player);
        }
        else
        {
            //show a UI screen with montster information?
            //Debug.Log(card1);
            Debug.Log("player selected: " + player);
        }
        /*
        if (card != null) {
            Debug.Log("Which has an attack value of: " + card.attack);
        }*/
    }
    public Vector3 getCurrentUIPosition()
    {
        Vector3 uipos = new Vector3();
        uipos = cam.ScreenToWorldPoint(new Vector3(this.transform.position.x, this.transform.position.y, cam.nearClipPlane));
        Debug.Log(uipos);
        return uipos;
    }
}
