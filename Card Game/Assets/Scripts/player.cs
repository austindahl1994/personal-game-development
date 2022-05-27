using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    public Sprite poisonSprite;
    public Sprite defenseSprite;
    public Sprite armorBreak;
    public Sprite sunderedSprite;
    public Sprite buffSprite;
    public Sprite retainSprite;
    public Sprite countdownSprite;
    public Sprite regenSprite;
    public Sprite burnSprite;

    public playerStats stats;
    public Transform statusArea;
    private Animator anim;
    private Camera cam;

    private healthBarSlider hpBar;
    private GameObject blockBar;
    private TMP_Text defenseText;

    private float playerMaxHealth;
    private float playerHealth = 0f;
    private int poison;
    private int block;
    private string playerName;
    public Dictionary<Sprite, int> status = new Dictionary<Sprite, int>();

    private void Start()
    {
        statusArea = transform.parent.transform.GetChild(0).transform.GetChild(2).transform;
        this.anim = GetComponent<Animator>();
        setupBars();
        cam = Camera.main;
        posSetup();
        block = stats.startingBlock;
        playerMaxHealth = stats.health;
        playerHealth = playerMaxHealth;
        playerName = stats.name;
        updateBlock(); //displays original armor
        setupStatus();
        hpBar.setHealth(playerHealth, playerMaxHealth); //sets the initial values for the enemy
    }

    private void setupStatus()
    {
        status.Add(poisonSprite, 0);
        status.Add(defenseSprite, 0);
        status.Add(armorBreak, 0);
        status.Add(sunderedSprite, 0);
        status.Add(buffSprite, 0);
        status.Add(retainSprite, 0);
        status.Add(countdownSprite, 0);
        status.Add(regenSprite, 0);
        status.Add(burnSprite, 0);
        //Debug.Log(status);
        updateStatusBar();
    }
    public void decrementAllStatuses()
    {
        if (status[poisonSprite] > 0)
        {
            status[poisonSprite]--;
        }
        if (status[armorBreak] > 0)
        {
            status[armorBreak]--;
        }
        if (status[defenseSprite] > 0)
        {
            status[defenseSprite]--;
        }
        if (status[sunderedSprite] > 0)
        {
            status[sunderedSprite]--;
        }
        if (status[buffSprite] > 0)
        {
            status[buffSprite]--;
        }
        if (status[retainSprite] > 0)
        {
            status[retainSprite]--;
        }
        if (status[countdownSprite] > 0)
        {
            status[countdownSprite]--;
        }
        if (status[regenSprite] > 0)
        {
            status[regenSprite]--;
        }
        if (status[burnSprite] > 0)
        {
            status[burnSprite]--;
        }

        updateStatusBar();
    }
    public void updateStatusBar()
    {
        //Debug.Log("poison for player is: " + status[poisonSprite]);
        int i = 0;
        foreach (Transform child in statusArea) {
            child.gameObject.SetActive(false);
        }
        foreach (Sprite key in status.Keys)
        {
            //statusArea.transform.GetChild(i).gameObject.SetActive(false);
            //Debug.Log("The value of: " + key + " is: " + status[key]);
            if (status[key] != 0)
            {
                statusArea.transform.GetChild(i).gameObject.SetActive(true);
                //Debug.Log(statusArea.transform.GetChild(i).gameObject.GetComponent<Image>().sprite);
                statusArea.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = key;
                statusArea.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = status[key].ToString();
                i++;
            }
        }
    }

    public void doAllActions()
    {
        directHealth(status[poisonSprite]);
        decrementAllStatuses();
        setBlock(0);
    }

    public void updatePlayerHealth(float incomingDamage) //damage inc as a negative to lower defense/hp
    {
        if (playerHealth == stats.health && incomingDamage == 0)
        {
            hpBar.setHealth(playerHealth, playerMaxHealth);
        }
        //Debug.Log("Update enemy health was called");
        if (block >= incomingDamage) //defense is greater so no need to do anything else
        {
            block -= (int)incomingDamage;
            updateBlock();
            return;
        }
        else if (block != 0)
        {
            int reducedAmount = (int)incomingDamage - block;
            block = 0;
            updateBlock();
            playerHealth -= reducedAmount;
        }
        else
        {
            playerHealth -= (incomingDamage);
        }
        //Debug.Log("Enemy health after modification: " + enemyHealth + " and max health: " + enemyMaxHealth);
        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            hpBar.setHealth(playerHealth, playerMaxHealth);
            //Debug.Log("The player now has: " + playerHealth + " hp left.");
        }
        else if (playerHealth <= 0)
        {
            playerHealth = 0f;
            hpBar.setHealth(0, playerMaxHealth);
            Die();
        }
        else
        {
            //Debug.Log("The enemy took: " + -(mod) + " damage");
            hpBar.setHealth(playerHealth, playerMaxHealth);
            //Debug.Log("The enemy now has: " + enemyHealth + " hp left.");
        }
    }
    public void setBlock(int input)
    {
        this.block = input;
        updateBlock();
    }

    public void addBlock(int block)
    {
        this.block += block;
        Debug.Log(this.name + " has: " + this.block);
        updateBlock();
    }

    public void directBlock(int amount)
    {
        block -= amount;
        updateBlock();
    }

    public void directHealth(int amount)
    {
        playerHealth -= amount;
        hpBar.setHealth(playerHealth, playerMaxHealth);
        if (playerHealth <= 0) {
            Die();
        }
    }

    public void posSetup()
    {
        //Debug.Log("The slimes current position is: " + this.gameObject.transform.position);
        GameObject parent = this.transform.parent.gameObject;
        //Debug.Log("The parents current position is: " + parent.transform.position);
        //Debug.Log("The current parent of: " + this.gameObject + " is: " + parent.gameObject);
        //Debug.Log("This current position: " + this.gameObject.transform.position);
        Vector3 temp = new Vector3();
        temp = cam.ScreenToWorldPoint(parent.gameObject.transform.position);
        this.transform.position = new Vector3(temp.x, temp.y, 0);
        //Debug.Log("This current position: " + this.gameObject.transform.position);
    }

    public void setupBars()
    {
        //sets the UI element as parent to modify specific bars/values inc. defense
        hpBar = transform.parent.gameObject.transform.GetChild(0).gameObject.transform.GetComponentInChildren<healthBarSlider>();
        blockBar = transform.parent.gameObject.transform.GetChild(1).gameObject;
        defenseText = blockBar.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        blockBar.SetActive(false); //unless enemy starts with armor, will not show
    }

    private void Die()
    {
        gameObject.tag = "Untagged";
        //Debug.Log("Die was called");
        this.anim.SetTrigger("death");
    }

    public void updateBlock()
    {
        if (block > 0)
        {
            blockBar.SetActive(true);
            blockBar.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            defenseText.text = block.ToString();
        }
        if (block <= 0)
        {
            blockBar.SetActive(false);
        }
    }
}
