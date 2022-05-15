using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public cardScriptable card;

    private GameManager gm;
    private cardPlayer play;
    public int handIndex;
    public bool isInHand;

    public TMP_Text nameText;
    public TMP_Text description;
    public TMP_Text manaText;

    public Image artworkImage;

    public bool typeTarget; 
    public int manaCost;
    public int attack;
    public int defense;
    public int poison;
    public int bleed;
    public int modAdd;
    public int modMultiply;

    public bool isSelectedCard;
    public bool firstTime = true;

    private void Start()
    {
        play = FindObjectOfType<cardPlayer>();
        isSelectedCard = false;
        typeTarget = card.typeTarget;
        manaCost = card.manaCost;
        nameText.text = card.name;
        description.text = card.description;
        artworkImage.sprite = card.artwork;
        manaText.text = card.manaCost.ToString();
        attack = card.attack;
        defense = card.defense;
        poison = card.poison;
        bleed = card.bleed;
        description.text = updateText(card.description);
        //for some reason exitHover was not starting as true, so first time a card was hovered over it would not act properly?
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (this.manaCost > card.manaCost)
        {
            manaText.color = Color.red;
        }
        else if (this.manaCost < card.manaCost)
        {
            manaText.color = Color.green;
        }
        else {
            manaText.color = Color.white;
        }
    }

    //Modifies text input from scriptable to show correct values 
    public string updateText(string unmodifiedText) {
        string modifiedText = null;
        //Debug.Log(unmodifiedText);

        // Change value for attack on card
        if (unmodifiedText.Contains("att")) {
            //Debug.Log("it contains att");
            modifiedText = unmodifiedText.Replace("att", attack.ToString());
        }

        // Change value for defense on card
        if (unmodifiedText.Contains("def"))
        {
            //Debug.Log("it contains (defense)");
            modifiedText = unmodifiedText.Replace("def", defense.ToString());
        }
        
        // If no changes were made then use original text
        if (modifiedText == null) {
            return unmodifiedText;
        } else {
            return modifiedText;
        }
    }

    //Moves card up and makes larger for easier viewing, uses pointerEnter and pointerExit
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("The cursor entered the selectable UI element.");
        //Debug.Log("Is card in hand: " + isInHand);
        //Debug.Log("are we in UI: " + gm.isInUI);
        if (this.isInHand && !gm.isInUI) //stops UI cards from being able to move
        {
            //Debug.Log("function called");
            //Debug.Log("hovering over card at " + handIndex);
            this.gameObject.transform.position = new Vector2(this.transform.position.x, 140);
            transform.SetAsLastSibling();
            this.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("The cursor exited the selectable UI element.");
        if (this.isInHand) {
            if (!this.isSelectedCard) {
                this.gameObject.transform.position = new Vector2(this.transform.position.x, 40);
                this.transform.SetSiblingIndex(handIndex);
                transform.GetSiblingIndex();
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("card clicked");
        if (this.manaCost > gm.playerMana) {
            Debug.Log("Not enough mana!");
            return;
        }
        if (typeTarget)
        {
            if (isSelectedCard)
            {
                isSelectedCard = false;
                gm.cardIsSelected = false;
            }
            else if (!gm.cardIsSelected)
            {
                isSelectedCard = true;
                gm.cardIsSelected = true;
            }
            else
            {
                gm.updateHandPosition();
                this.gameObject.transform.position = new Vector2(this.transform.position.x, 140);
                transform.SetAsLastSibling();
                this.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1);
                isSelectedCard = true;
                gm.cardIsSelected = true;
            }
        }
        else {
            this.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("sending over: " + this);
            play.playCard(this);
        }
    }
}
