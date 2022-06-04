using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private player player;
    public cardPlayer play;
    //list of all the cards
    public List<Card> allAvailableCards = new List<Card>(); //where cards are added from
    public List<Card> allHiddenCards = new List<Card>(); //this in another list at beginning of run
    public List<Card> deck = new List<Card>();
    public List<Card> roundCards = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> discard = new List<Card>();

    //These are for the pileUI to show and scroll with text to describe which card list looked at
    public GameObject pileUI;
    public Image scrollArea;
    public TMP_Text pileText;

    //these 4 references are used for card positioning for hand/UI
    public Transform[] cardSlotsOnBoard;
    public Transform cardSlotOnUI;
    public Transform cardHolderArea;

    //reference for the clickable monster UI
    public Transform[] enemyCoveringUI;
    public List<Transform> skipEnemiesList = new List<Transform>();
    public TMP_Text mana;
    public TMP_Text deckAmountText;
    public TMP_Text discardAmountText;
    private int interval;
    private int drawAmount; //draw amount could be 3
    private int maxHandSize; //relic to increase handsize and card draw at start? not always 10
    private int currentHandSize;
    public bool isInUI;
    private int maxMana;
    public int playerMana;
    public bool cardIsSelected;
    public int startNumberEnemies;
    public int enemyStartingIndex;
    //modded ints from relics
    public int modManaAdd;
    public int modManaMultiply;
    private bool waitingForNextTurn;
    public bool isPlayerTurn;
    private void Start()
    {
        enemyStartingIndex = 0;
        player = FindObjectOfType<player>();
        play = FindObjectOfType<cardPlayer>();
        modManaAdd = 0;
        modManaMultiply = 1;
        isInUI = false; //allows cards to be shown in hand
        cardIsSelected = false;
        maxMana = (3 + modManaAdd) * modManaMultiply; //get from player scriptable
        playerMana = maxMana; //set mana at start for player
        //this adds from the deck to roundCards, which are used in gameplay as to not affect deck
        foreach (Card card in deck) {
            roundCards.Add(card);
        }
        startNumberEnemies = getAllEnemies().Count;
        drawAmount = 5; //this is for tests, use value that can be changed for actual game
        maxHandSize = 10; //to ensure cards can't go off screen, could be lower as a detriment?
        drawHand();
        //Debug.Log("All enemies are: " + getAllEnemies());
    }

    private void Update()
    {
        deckAmountText.text = roundCards.Count.ToString(); //text for draw pile
        discardAmountText.text = discard.Count.ToString(); //text for discard pile
        mana.text = playerMana.ToString(); //text for mana
        currentHandSize = hand.Count;
    }

    public void setEnemyPos() { //have this similar to 
        int enemyCount = 0;
        enemyCount = enemyCoveringUI.Length; //does same this as below
        foreach (Transform slot in enemyCoveringUI) {
            if (slot.childCount > 0) {
                enemyCount++;
            }
        }
    }

    //if no cards, shuffle from discard, else draw a random cards from roundCards
    public void DrawCard()
    {
        if (isInUI)
        {
            return;
        }
        if (roundCards.Count == 0)
        {
            shuffleIntoDeck();
        }
        if (roundCards.Count >= 1 & currentHandSize < maxHandSize)
        {
            Card randCard = roundCards[Random.Range(0, roundCards.Count)];
            //Debug.Log("drew a card");
            randCard.gameObject.SetActive(true); //set card active
            randCard.handIndex = currentHandSize;
            //Debug.Log("deck size before: " + deck.Count);
            roundCards.Remove(randCard);
            //Debug.Log("deck size after: " + deck.Count);
            //Debug.Log("hand size before: " + hand.Count);
            hand.Add(randCard);
            randCard.isInHand = true;
            //Debug.Log("hand size after: " + hand.Count);
            updateHandPosition();
        }

        if (roundCards.Count == 0 && discard.Count == 0) //not even needed?
        {
            return;
        }
    }

    //get the card that is currently selected (send to enemy UI script)
    public Card getCardObject() {
        //Debug.Log("getcardObject in gm was called");
        //Debug.Log("Hand count is: " + hand.Count);
        foreach (Card card in hand) {
            //Debug.Log("Checking: " + card + "with it being in hand: " + card.isInHand);
            //Debug.Log("Is the card selected? " + card.isSelectedCard);
            if (card.isSelectedCard)
            {
                return card;
            }
        }
        return null;
    }

    public List<Card> getHand() {
        return hand;
    }

    public void addToHand(GameObject card) {
        hand.Add(card.GetComponent<Card>());
    }

    //after card effects are done resets the card and moves to discard pile
    public void moveToDiscardPile(Card cardPlayed)
    {
        hand.Remove(cardPlayed);
        //Debug.Log("card removed from hand");
        cardPlayed.isInHand = false;
        //Debug.Log("Card inInhand set false");
        discard.Add(cardPlayed);
        //Debug.Log("added card to discard pile");
        updateHandPosition();
        //Debug.Log("hand position updated");
        cardPlayed.gameObject.SetActive(false);
        //Debug.Log("game object no longer true");
    }

    public void destroyFragileCard(Card cardPlayed) {
        hand.Remove(cardPlayed);
        cardPlayed.isInHand = false;
        cardPlayed.gameObject.SetActive(false);
        updateHandPosition();
        Destroy(cardPlayed);
    }

    public void rewardScreen() { 
        
    }
    //shuffles cards from discard pile into hand
    public void shuffleIntoDeck() {

        foreach (Card card in discard) { //type gameObject in array/list
            roundCards.Add(card);//need to set random functionality??
        }
        discard.Clear();
    }

    public List<GameObject> getAllEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach (Transform enemySlot in enemyCoveringUI)
        {
            if (enemySlot.childCount > 0)
            {
                //Debug.Log("sent over: " + enemySlot.GetChild(0).GetChild(2).gameObject);
                enemies.Add(enemySlot.GetChild(0).GetChild(2).gameObject);
            }
        }
        return enemies;
    }

    public Transform getAvailableEnemySlots() {
        foreach (Transform slot in enemyCoveringUI) {
            if (slot.gameObject.name == "enemyBossSlot") {
                continue;
            }
            if (slot.childCount == 0 || slot.GetChild(0) == null)
            {
                return slot;
            }
            if (slot.gameObject.transform.GetChild(0) != null && !slot.GetChild(0).gameObject.activeInHierarchy)
            {
                return slot;
            }
        }
        //Debug.Log("No available slots");
        return null;
    }

    //clears the hand, putting all into discard list and setting active
    public void endTurn() {
        isPlayerTurn = false;
        if (waitingForNextTurn) {
            return;
        }
        waitingForNextTurn = true;
        foreach (Card card in hand) {
            card.isInHand = false;
            card.gameObject.SetActive(false);
            discard.Add(card);
        }
        hand.Clear();
        //start enemy turn??
        foreach (GameObject enemy in getAllEnemies())
        {
            enemy.GetComponent<enemy>().setBlock(0);
            enemy.GetComponent<enemy>().hasTakenTurn = false;
        }
        nextEnemyTurn(enemyStartingIndex);
    }

    public void nextEnemyTurn(int index) {
        if (isPlayerTurn) {
            return;
        }
        if (index >= enemyCoveringUI.Length) {
            enemyStartingIndex = 0;
            startPlayerTurn();
        } else if (enemyCoveringUI[index].childCount == 0 ||
                    !enemyCoveringUI[index].GetChild(0).gameObject.activeInHierarchy ||
                    enemyCoveringUI[index].GetChild(0).gameObject == null) {
            index++;
            setCurrentIndex(index);
            nextEnemyTurn(index);
        } else {
            setCurrentIndex(index);
            enemyCoveringUI[index].GetChild(0).GetChild(2).gameObject.GetComponent<enemy>().doAllActions(index);
        }
    }
    private void setCurrentIndex(int index)
    {
        enemyStartingIndex = index;
    }

    public int getCurrentIndex()
    {
        return enemyStartingIndex;
    }

    private void drawHand() {
        while (hand.Count < drawAmount)
        {
            currentHandSize = hand.Count;
            DrawCard();
        }
    }

    private void startPlayerTurn() {
        skipEnemiesList.Clear();
        isPlayerTurn = true;
        foreach (GameObject enemy in getAllEnemies()) {
            enemy.GetComponent<enemy>().setIntent();
            enemy.GetComponent<enemy>().hasTakenTurn = false;
        }
        player.GetComponent<player>().setBlock(0);
        waitingForNextTurn = false;
        player.doAllActions();
        drawHand();
        playerMana = 0;
        playerMana += maxMana;
    }

    //three options for buttons to show cards on a UI screen, activate with UI button
    public void showDiscardPile() {
        pileText.text = "Discard";
        showPile(discard);
    }

    public void showRoundPile() {
        pileText.text = "Draw Pile";
        showPile(roundCards);
    }

    public void showDeckPile() {
        pileText.text = "Deck";
        showPile(deck);
    }

    //creates a UI to show user cards currently in the pile requested
    private void showPile (List<Card> pile) {
        isInUI = true;
        hideHand();
        clearUIPile();
        pileUI.gameObject.SetActive(true);
        int pilex = 0;
        int piley = 0;
        int modify = (Mathf.FloorToInt(pile.Count / 5));
        //Debug.Log(modify);
        if (pile.Count > 15)
        {
            scrollArea.rectTransform.sizeDelta = new Vector2(1110.0f, 1000 + (modify * 220));
        }
        else {
            scrollArea.rectTransform.sizeDelta = new Vector2(1110.0f, 1000);
        }
        int startingPosition = (int)((scrollArea.rectTransform.sizeDelta.y / 4.0f) * -1);
        int heightToStartFrom = (int)((scrollArea.rectTransform.sizeDelta.y / 2.0f) - 200);
        scrollArea.rectTransform.position = new Vector2 (960, startingPosition + 200);
        //Debug.Log("Size is: " + scrollArea.rectTransform.sizeDelta.x);
        //Debug.Log("Size is: " + scrollArea.rectTransform.sizeDelta.y);
        cardSlotOnUI.transform.position = Vector2.zero;
        pileText.transform.localPosition = new Vector2(0, heightToStartFrom + 150);
        //Debug.Log(pileText.transform.position);
        //Debug.Log(heightToStartFrom);
        foreach (Card card in pile) {
            cardSlotOnUI.localPosition = new Vector2(((pilex % 5)* 200) + -400, (piley * -300) + heightToStartFrom);
            card.transform.SetParent(scrollArea.transform);
            card.gameObject.SetActive(true);
            card.transform.position = cardSlotOnUI.transform.position;
            pilex++;
            if ((pilex % 5) == 0) {
                piley++;
            }
        }
    }

    //makes all cards in hand active to show them, then calls updateHandPos to put in correct place
    public void showHand() {
        foreach (Card card in hand)
        {
            //Debug.Log("is in UI is: " + isInUI);
            //Debug.Log("Show hand was called");
            card.gameObject.SetActive(true);
        }
        updateHandPosition();
    }

    //sets all cards in hand to not active to not allow clicks while UI open etc.
    public void hideHand() {
        foreach (Card card in hand)
        {
            card.gameObject.SetActive(false);
        }
    }

    //removes all the cards from UI to place them into single spot
    public void clearUIPile() {
        foreach (Card card in discard) {
            card.transform.position = cardHolderArea.transform.position;
            card.gameObject.SetActive(false);
            card.transform.SetParent(cardHolderArea.transform);
        }
        foreach (Card card in roundCards) {
            card.transform.position = cardHolderArea.transform.position;
            card.gameObject.SetActive(false);
            card.transform.SetParent(cardHolderArea.transform);
        }
        foreach (Card card in deck) {
            card.transform.position = cardHolderArea.transform.position;
            card.gameObject.SetActive(false);
            card.transform.SetParent(cardHolderArea.transform);
        }
    }

    //updates all the cards position in hands, can also be used to display cards correctly
    public void updateHandPosition() {
        cardIsSelected = false;
        float median;
        float x;
        float pyramid;
        int i;
        float y;
        foreach (Transform slots in cardSlotsOnBoard) {
            slots.transform.position = new Vector2(960, 590);
        }
        currentHandSize = hand.Count;
        pyramid = 0;
        //Debug.Log("current hand size is: " + currentHandSize);
        //Debug.Log("current hand size is: " + hand.Count);
        median = currentHandSize / 2.0f;
        //Debug.Log(median);
        y = Mathf.Ceil(median);
        if (hand.Count % 2 != 0) //if it is odd
        { //middle number will be 80
            for (i = 0; i < currentHandSize; i++) {
                if (i < y)
                {
                    x = -1;
                }
                else
                {
                    x = 1;
                }
                pyramid = currentHandSize - y - i;
                //Debug.Log("median value is " + median);
                //Debug.Log("current hand size is: " + currentHandSize);
                //Debug.Log("y value is " + y);
                //Debug.Log("i value is " + i);
                //Debug.Log("pyramid value is " + Mathf.Abs(pyramid));
                //Debug.Log("x value is " + x);
                int temp =  (int)(140 * (Mathf.Abs(pyramid)) * x + 960);
                cardSlotsOnBoard[i].transform.position = new Vector2(temp, 40);
                hand[i].isInHand = true;
                hand[i].transform.localScale = new Vector3(1, 1, 1);
                hand[i].isSelectedCard = false;
                //Debug.Log("cardslot: " + i + " is located: " + cardSlots[i].transform.position);
                hand[i].handIndex = i;
                hand[i].transform.position = cardSlotsOnBoard[i].position;
                hand[i].transform.SetSiblingIndex(i);
                //hand[i].gameObject.SetActive(true);
                //Debug.Log("Sibling Index : " + hand[i].transform.GetSiblingIndex());
                //Debug.Log("-----------------------------------");
            }
        }
        else { //left number is 910 right is 1050
            y = Mathf.Ceil(median) + 1;
            for (i = 0; i < currentHandSize; i++)
            {
                if (i < y - 1)
                {
                    x = -1;
                }
                else
                {
                    x = 1;
                }
                pyramid = currentHandSize - y - i;
                //Debug.Log("median value is " + median);
                //Debug.Log("current hand size is: " + currentHandSize);
                //Debug.Log("y value is " + y);
                //Debug.Log("i value is " + i);
                //Debug.Log("pyramid value is " + Mathf.Abs(pyramid));
                //Debug.Log("x value is " + x);
                int temp = (int)(140 * (Mathf.Abs(pyramid)) * x + 910);
                cardSlotsOnBoard[i].transform.position = new Vector2(temp, 40);
                hand[i].transform.localScale = new Vector3(1, 1, 1);
                hand[i].isInHand = true;
                hand[i].isSelectedCard = false;
                //Debug.Log("cardslot: " + i + " is located: " + cardSlots[i].transform.position);
                hand[i].handIndex = i;
                hand[i].transform.position = cardSlotsOnBoard[i].position;
                hand[i].transform.SetSiblingIndex(i);
                //hand[i].gameObject.SetActive(true);
                //Debug.Log("Sibling Index : " + hand[i].transform.GetSiblingIndex());
                //Debug.Log("-----------------------------------");
            }
        }
    }
}
