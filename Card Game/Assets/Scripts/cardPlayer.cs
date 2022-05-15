using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPlayer : MonoBehaviour
{
    //public Card card;
    public GameManager gm;
    private List<GameObject> allEnemies = new List<GameObject>();
    private Card card;
    private enemy enemy;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void playTargetCard(Card cardPlayed, GameObject enemy)
    {
        gm.playerMana -= cardPlayed.manaCost;
        //Debug.Log("Sending both: " + cardPlayed + " " + enemy);
        enemy.gameObject.GetComponent<enemy>().UpdateEnemyHealth(cardPlayed.attack);
        gm.moveToDiscardPile(cardPlayed);
    }

    //if the card requires no target play it immediately
    public void playCard(Card cardPlayed)
    {
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
