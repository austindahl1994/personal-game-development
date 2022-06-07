using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPlayer : MonoBehaviour
{
    //public Card card;
    public GameManager gm;
    private List<GameObject> allEnemies = new List<GameObject>();
    private player player;
    private Card card;
    private enemy enemy;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<player>();
    }

    //if the card requires no target play it immediately
    public void playCard(Card cardPlayed)
    {
        if (cardPlayed.defense > 0)
        {
            player.GetComponent<player>().addBlock(cardPlayed.defense);
        }
        dealAOE(cardPlayed.aoeAttack);
        gm.playerMana -= cardPlayed.manaCost;
        if (cardPlayed.fragile)
        {
            //do destroy animation, possibly go to 
            //Debug.Log("Destroying card: " + cardPlayed);
            gm.destroyFragileCard(cardPlayed);
        }
        else
        {
            gm.moveToDiscardPile(cardPlayed);
        }
    }

    public void playCard(Card cardPlayed, GameObject target)
    {
        //Debug.Log("Target is: " + target);
        player.GetComponent<player>().addBlock(cardPlayed.defense);
        //Debug.Log("Card received was: " + cardPlayed);
        //Debug.Log("Card has attack: " + cardPlayed.attack);
        target.gameObject.GetComponent<enemy>().addPoison(cardPlayed.poison);
        target.gameObject.GetComponent<enemy>().UpdateEnemyHealth(cardPlayed.attack);
        dealAOE(cardPlayed.aoeAttack);
        gm.playerMana -= cardPlayed.manaCost;
        if (cardPlayed.fragile)
        {
            //do destroy animation, possibly go to 
            //Debug.Log("Destroying card: " + cardPlayed);
            gm.destroyFragileCard(cardPlayed);
        }
        else {
            gm.moveToDiscardPile(cardPlayed);
        }
    }
    public void dealAOE(float damage)
    {
        Debug.Log("dealAOE called for damage: " + damage);
        allEnemies.AddRange(gm.getAllEnemies());
        //Debug.Log("List of all enemies from cardPlayer: " + allEnemies);
        foreach (GameObject enemy in allEnemies)
        {
            enemy.gameObject.GetComponent<enemy>().UpdateEnemyHealth(damage);
        }
        allEnemies.Clear();
    }

    public void playStartOfTurnEffect(Card cardPlayed) {
        StartCoroutine(cardPlayed.startOfTurnEffectName, cardPlayed.startOfTurnValue);
    }
    public void playEndOfTurnEffect(Card cardPlayed) {
        StartCoroutine(cardPlayed.endOfTurnEffectName, cardPlayed.endOfTurnValue);
    }

    IEnumerator heal(int value) {
        player.GetComponent<player>().updatePlayerHealth(value);
        yield return null;
    }

    IEnumerator damage(int value)
    {
        player.GetComponent<player>().updatePlayerHealth(value);
        yield return null;
    }

    IEnumerator draw(int value)
    {
        for (int i = 0; i < value; i++) {
            gm.DrawCard();
        }
        yield return null;
    }


}
