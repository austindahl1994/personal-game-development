using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemy : MonoBehaviour
{
    //scriptable object
    public enemyStats stats;
    private player player;

    //all status sprites
    public Sprite poisonSprite;
    public Sprite strengthSprite;
    public Sprite defenseSprite;
    public Sprite vulnerableSprite;


    private Animator anim;
    private Camera cam;

    private healthBarSlider hpBar;
    private GameObject blockBar;
    private TMP_Text defenseText;

    public Image spriteImage;
    public Transform statusArea;
    public TMP_Text[] statusText;

    private float enemyMaxHealth;
    private float enemyHealth = 0f;
    private int damage;
    private int block;
    private int poison;
    private string enemyName;
    public Dictionary<Sprite, int> status = new Dictionary<Sprite, int>();
    public List<string> actions = new List<string>();
    public List<string> nextAction = new List<string>();
    private int totalActions;
    //need to add coroutines that go in order for each enemy, so it shows blocking damage and such

    private void Start()
    {
        damage = stats.damage; //should have a function that updates the damage based on add/multiply factors
        this.anim = GetComponent<Animator>();
        player = FindObjectOfType<player>();
        setupBars();
        cam = Camera.main;
        addActions();
        totalActions = actions.Count;
        //Debug.Log("The enemy: " + this.gameObject + " has: " + actions.Count + " actions.");
        posSetup(); //sets the original position of the gameObject enemy to the UI
        setStartValues();
        setupStatus();
        updateStatusBar();
        setIntent();
    }

    private void setupStatus() {
        status.Add(poisonSprite, 0);
        status.Add(strengthSprite, 0);
        status.Add(defenseSprite, 0);
        status.Add(vulnerableSprite, 0);
        //Debug.Log(status);
    }

    public void setIntent() {
        //Debug.Log(this.gameObject + "has amount of actions:" + stats.actionsPerTurn);
        for (int i = 0; i < this.stats.actionsPerTurn; i++) {
            if (totalActions >= stats.actionsPerTurn) {
                string temp = actions[Random.Range(0, actions.Count - 1)];
                nextAction.Add(temp);
            }
        }
        //modify some value attached to object based on each action in nextAction

       // Debug.Log(this.gameObject);
       // Debug.Log(nextAction.Count);
    }

    public void doAllActions() {
        if (enemyHealth <= 0) {
            return;
        }
        directHealth(status[poisonSprite]);
        decrementAllStatuses();
        updateStatusBar();
        setBlock(0);
        takeTurn();
    }

    public void decrementAllStatuses() {
        if (status[poisonSprite] > 0) {
            status[poisonSprite]--;
        }
        if (status[vulnerableSprite] > 0)
        {
            status[vulnerableSprite]--;
        }
        if (status[defenseSprite] > 0)
        {
            status[defenseSprite]--;
        }
        if (status[strengthSprite] > 0)
        {
            status[strengthSprite]--;
        }
    }

    public void increaseAllStatuses() { 
        
    }

    public void setStartValues() {
        block = stats.startingBlock;
        enemyMaxHealth = stats.health;
        enemyHealth = enemyMaxHealth;
        enemyName = stats.name;
        updateBlock(); //displays original armor
        hpBar.setHealth(enemyHealth, enemyMaxHealth); //sets the initial values for the enemy
        statusArea = transform.parent.transform.GetChild(0).transform.GetChild(2).transform;
    }

    //go through each child, if sprite is not null make it's position the next one?
    //other option, have each child start with sprite already? if amount for that sprite is 
    //greater than 0 then turn it on? if active set at box location, move box right, if box more than
    //certain x, set back to original x and change y to lower position?
    //for each child of statusSpot (is statusSpot statusspotHolder? if so change name,
    //check when more awake)
    public void updateStatusBar() {
        int i = 0;

        foreach (Sprite key in status.Keys)
        {
            statusArea.transform.GetChild(i).gameObject.SetActive(false);
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

    public void addStatus(Sprite sprite, int amount) {
        
    }
    //will check each child for sprite, if matches clear, then update position?
    public void clearStatus(Sprite sprite) { 
        
    }

    public float getEnemyHealth() {
        return this.enemyHealth;
    }

    public void updateAnimator()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.ResetTrigger("death");
    }

    private void Die()
    {
        gameObject.tag = "Untagged";
        //Debug.Log("Die was called");
        this.anim.SetTrigger("death");
    }

    public void nextPhase() { //called after death animation is done
        Debug.Log("Next phase called");
        this.transform.SetAsLastSibling();
        this.GetComponentInParent<enemyUI>().updateEnemy();
        this.gameObject.transform.position = new Vector3(0, 0, -20);
    }


    public void setParentFalse() {
        this.transform.parent.gameObject.SetActive(false);
    }

    public void updateBlock() {
        if (block > 0) {
            blockBar.SetActive(true);
            blockBar.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            defenseText.text = block.ToString();
        }
        if (block <= 0) {
            blockBar.SetActive(false);
        }
    }

    public void addPoison(int amount) {
        status[poisonSprite] += amount;
        updateStatusBar();
    }

    public void setBlock(int input) {
        if (this.stats.retainsBlock) {
            return;
        }
        this.block = input;
        updateBlock();
    }

    public void addBlock(int block) {
        this.block += block;
        updateBlock();
    }

    public void directBlock(int amount) {
        block -= amount;
        updateBlock();
    }

    public void directHealth(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            enemyHealth = 0f;
            hpBar.setHealth(0, enemyMaxHealth);
            Die();
        }
        else {
            hpBar.setHealth(enemyHealth, enemyMaxHealth);
        }
    }

    public void setupBars() {
        //sets the UI element as parent to modify specific bars/values inc. defense
        hpBar = transform.parent.gameObject.transform.GetChild(0).gameObject.transform.GetComponentInChildren<healthBarSlider>();
        blockBar = transform.parent.gameObject.transform.GetChild(1).gameObject;
        defenseText = blockBar.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        blockBar.SetActive(false); //unless enemy starts with armor, will not show
    }

    //if defense > 0, defenseBar.setActice = true;

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

    public void takeTurn() {
        Debug.Log("Take turn now is: " + this);
        //invoke(foo(), 3.0f);
        foreach (string action in nextAction) {
            StartCoroutine(action);
        }
        nextAction.Clear();
        setIntent();
    }

    public void showBuffIcons() { 
        
    }

    public void UpdateEnemyHealth(float incomingDamage) //damage inc as a negative to lower defense/hp
    {
        if (enemyHealth == stats.health && incomingDamage == 0)
        {
            hpBar.setHealth(enemyHealth, enemyMaxHealth);
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
            enemyHealth -= reducedAmount;
        }
        else
        {
            enemyHealth -= (incomingDamage);
        }
        //Debug.Log("Enemy health after modification: " + enemyHealth + " and max health: " + enemyMaxHealth);
        if (enemyHealth >= enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
            hpBar.setHealth(enemyHealth, enemyMaxHealth);
            //Debug.Log("The enemy now has: " + enemyHealth + " hp left.");
        }
        else if (enemyHealth <= 0)
        {
            enemyHealth = 0f;
            hpBar.setHealth(0, enemyMaxHealth);
            Die();
        }
        else
        {
            //Debug.Log("The enemy took: " + -(mod) + " damage");
            hpBar.setHealth(enemyHealth, enemyMaxHealth);
            //Debug.Log("The enemy now has: " + enemyHealth + " hp left.");
        }
    }

    public void addActions() {
        if (stats.attackAction) {
            actions.Add("attack");
        }
        if (stats.defendAction)
        {
            actions.Add("defend");
        }
        if (stats.hybrid)
        {
            actions.Add("hybrid");
        }
        if (stats.countdownAction)
        {
            actions.Add("countDown");
        }
        if (stats.poisonAction)
        {
            actions.Add("poisonPlayer");
        }
        if (stats.curseAction)
        {
            actions.Add("curse");
        }
        if (stats.summonAction)
        {
            actions.Add("summon");
        }
        if (stats.buffSelfAction)
        {
            actions.Add("buffSelf");
        }
        if (stats.buffOthersAction)
        {
            actions.Add("buffOthers");
        }
        if (stats.debuffAction)
        {
            actions.Add("debuff");
        }
        if (stats.healSelf)
        {
            actions.Add("healSelf");
        }
        if (stats.healOthers)
        {
            actions.Add("healOthers");
        }
        if (stats.corrosion)
        {
            actions.Add("corrosion");
        }
        if (stats.canGiveCard)
        {
            actions.Add("canGiveCard");
        }
    }

    IEnumerator attack() {
        Debug.Log(this.gameObject + " is attacking!");
        player.GetComponent<player>().updatePlayerHealth(damage);
        yield return null;
    }
    IEnumerator defend()
    {
        Debug.Log(this.gameObject + " is defending!");
        yield return null;
    }
    IEnumerator hybrid()
    {
        Debug.Log(this.gameObject + " is attacking and defending!");
        yield return null;
    }
    IEnumerator countDown()
    {
        Debug.Log(this.gameObject + " is counting down!");
        yield return null;
    }
    IEnumerator poisonPlayer()
    {
        Debug.Log(this.gameObject + " is poisoning player!");
        yield return null;
    }
    IEnumerator curse()
    {
        Debug.Log(this.gameObject + " is cursing player!");
        yield return null;
    }
    IEnumerator summon()
    {
        Debug.Log(this.gameObject + " is summoning minion!");
        yield return null;
    }
    IEnumerator buffSelf()
    {
        Debug.Log(this.gameObject + " is buffing self!");
        yield return null;
    }
    IEnumerator buffOthers()
    {
        Debug.Log(this.gameObject + " is buffing others!");
        yield return null;
    }
    IEnumerator debuff()
    {
        Debug.Log(this.gameObject + " is debuffing player!");
        yield return null;
    }
    IEnumerator healself()
    {
        Debug.Log(this.gameObject + " is healing self!");
        yield return null;
    }
    IEnumerator healOthers()
    {
        Debug.Log(this.gameObject + " is healing others!");
        yield return null;
    }
    IEnumerator corrosion()
    {
        Debug.Log(this.gameObject + " is corroding armor!");
        yield return null;
    }
    IEnumerator canGiveCard()
    {
        Debug.Log(this.gameObject + " is giving card!");
        yield return null;
    }
}
