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
    public void playTargetCard(Card cardPlayed, GameObject target)
    {
        if (target.name == "player")
        {
            Debug.Log("Player card played on");
            if (cardPlayed.defense > 0)
            {
                target.GetComponent<player>().addBlock(cardPlayed.defense);
            }
        }
        else {
            Debug.Log(target.name);
            gm.playerMana -= cardPlayed.manaCost;
            //Debug.Log("Sending both: " + cardPlayed + " " + enemy);
            target.gameObject.GetComponent<enemy>().addPoison(cardPlayed.poison);
            target.gameObject.GetComponent<enemy>().UpdateEnemyHealth(cardPlayed.attack);
            gm.moveToDiscardPile(cardPlayed);
        }
    }

    //if the card requires no target play it immediately
    public void playCard(Card cardPlayed)
    {
        if (cardPlayed.defense > 0)
        {
            player.GetComponent<player>().addBlock(cardPlayed.defense);
        }
        //Debug.Log("Card received was: " + cardPlayed);
        //Debug.Log("Card has attack: " + cardPlayed.attack);
        dealAOE(cardPlayed.attack);
        gm.playerMana -= cardPlayed.manaCost;
        gm.moveToDiscardPile(cardPlayed);
    }
    public void dealAOE(float damage)
    {
        //Debug.Log("dealAOE called");
        allEnemies.AddRange(gm.getAllEnemies());
        //Debug.Log("List of all enemies from cardPlayer: " + allEnemies);
        foreach (GameObject enemy in allEnemies)
        {
            enemy.gameObject.GetComponent<enemy>().UpdateEnemyHealth(damage);
        }
        allEnemies.Clear();
    }
}
