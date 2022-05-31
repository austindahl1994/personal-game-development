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
    private GameManager gm;

    //all status sprites
    public Sprite poisonSprite;
    public Sprite defenseSprite;
    public Sprite armorBreak;
    public Sprite sunderedSprite;
    public Sprite buffSprite;
    public Sprite retainSprite;
    public Sprite countdownSprite;
    public Sprite regenSprite;
    public Sprite burnSprite;

    public Sprite attackIntentSprite;
    public Sprite defendIntentSprite;
    public Sprite summonIntentSprite;
    public Sprite corrosionIntent;
    public Sprite unknownIntentSprite;
    public Sprite healSelfIntentSprite;
    public Sprite healOthersIntentSprite;
    public Sprite buffOthersIntentSprite;
    public Sprite giveCardIntentSprite;
    public Sprite curseIntentSprite;


    private Animator anim;
    private Camera cam;

    private healthBarSlider hpBar;
    private GameObject blockBar;
    private TMP_Text defenseText;

    private Sprite spriteImage;
    public Transform statusArea;
    public Transform intentArea;

    private float enemyMaxHealth;
    private float enemyHealth = 0f;
    private int damage;
    private int defense;
    private int block;
    private int buffAmount;
    private string enemyName;
    public Dictionary<Sprite, int> status = new Dictionary<Sprite, int>();
    public List<string> actions = new List<string>();
    public List<string> nextAction = new List<string>();
    private int countDownTimer;
    private int currentIndex;
    public bool hasTakenTurn;
    public bool intentSet;

    //need to add coroutines that go in order for each enemy, so it shows blocking damage and such

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        this.anim = GetComponent<Animator>();
        player = FindObjectOfType<player>();
        setupBars();
        cam = Camera.main;
        addActions();
        //Debug.Log("The enemy: " + this.gameObject + " has: " + actions.Count + " actions.");
        posSetup(); //sets the original position of the gameObject enemy to the UI
        setStartValues();
        setupStatus();
        updateStatusBar();
        setIntent();
        StartCoroutine(temp());
    }

    IEnumerator temp()
    {
        yield return new WaitForSeconds(0.01f);
        this.GetComponentInParent<enemyUI>().updateEnemy();
        yield return null;
    }

    private void setupStatus() {
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
    }

    public void setIntent() {
        //Debug.Log("Set intent was called for: " + this.gameObject);
        //if out of index check and make sure the enemy has actions/can do actions
        if (this.intentSet || this.gameObject.tag == "newSummon")
        {
            return;
        }
        else {
            //Debug.Log("Set intent called for: " + this.gameObject + "intentSet is:" + intentSet);
            intentSet = true;
            //Debug.Log("intentSet is now:" + intentSet);
            int i;
            //Debug.Log(this.gameObject + "has amount of actions:" + stats.actionsPerTurn);
            nextAction.Clear();
            for (i = 0; i < stats.actionsPerTurn; i++)
            {
                nextAction.Add(actions[Random.Range(0, actions.Count)]);
            }
            clearIntent();

            showIntent(nextAction[0], 0);
        }
        
        //modify some value attached to object based on each action in nextAction

        // Debug.Log(this.gameObject);
        // Debug.Log(nextAction.Count);
    }

    public void refreshIntent() {
        if (this.gameObject.tag != "newSummon") {
            showIntent(nextAction[0], 0);
        }
    }

    public void clearIntent() {
        for (int i = 0; i < intentArea.transform.childCount; i++)
        {
            intentArea.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void decrementAllStatuses() {
        if (status[poisonSprite] > 0) {
            status[poisonSprite]--;
        }
        if (status[armorBreak] > 0) {
            status[armorBreak]--;
        }
        if (status[sunderedSprite] > 0) {
            status[sunderedSprite]--;
        }
        if (status[countdownSprite] > 0){
            status[countdownSprite]--;
        }
        if (status[regenSprite] > 0){
            status[regenSprite]--;
        }

        updateStatusBar();
    }

    public void increaseAllStatuses() {
        if (status[poisonSprite] > 0){
            status[poisonSprite]++;
        }
        if (status[armorBreak] > 0){
            status[armorBreak]++;
        }
        if (status[defenseSprite] > 0){
            status[defenseSprite]++;
        }
        if (status[sunderedSprite] > 0){
            status[sunderedSprite]++;
        }
        if (status[buffSprite] > 0){
            status[buffSprite]++;
        }
        if (status[retainSprite] > 0){
            status[retainSprite]++;
        }
        if (status[countdownSprite] > 0){
            status[countdownSprite]++;
        }
        if (status[regenSprite] > 0){
            status[regenSprite]++;
        }
        if (status[burnSprite] > 0){
            status[burnSprite]++;
        }
        updateStatusBar();
    }

    public void setStartValues() {
        intentSet = false;
        hasTakenTurn = false;
        currentIndex = 0;
        damage = stats.damage;
        defense = stats.defense;
        buffAmount = stats.buffAmount;
        block = stats.startingBlock;
        enemyMaxHealth = stats.health;
        enemyHealth = enemyMaxHealth;
        enemyName = stats.name;
        countDownTimer = 3;
        updateBlock(); //displays original armor
        hpBar.setHealth(enemyHealth, enemyMaxHealth); //sets the initial values for the enemy 
        statusArea = transform.parent.transform.GetChild(0).transform.GetChild(2).transform;
        intentArea = transform.parent.transform.GetChild(0).transform.GetChild(3).transform;
    }

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
        gm.nextEnemyTurn(gm.getCurrentIndex()+1);
        gameObject.tag = "Untagged";
        //Debug.Log("Die was called");
        this.anim.SetTrigger("death");
    }

    public void nextPhase() { //called after death animation is done
        Debug.Log("Next phase called");
        this.transform.SetAsLastSibling();
        if (!gm.isPlayerTurn) {
            gm.nextEnemyTurn(gm.getCurrentIndex() + 1);
        }
        this.GetComponentInParent<enemyUI>().updateEnemy();
        this.GetComponentInParent<enemyUI>().newEnemyReset();
        this.gameObject.transform.position = new Vector3(0, 0, -20);
    }

    public void setParentFalse() {
        //gm.nextEnemyTurn(gm.getCurrentIndex());
        this.transform.parent.gameObject.SetActive(false);
        Destroy(this.transform.parent.gameObject);
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

    public void posSetup()
    {
        if (this.gameObject.transform.parent != null) {
            //Debug.Log("Parent is: " + this.gameObject.transform.parent);
            //Debug.Log("The slimes current position is: " + this.gameObject.transform.position);
            //Debug.Log("The parents current position is: " + this.transform.parent.gameObject.transform.position);
            //Debug.Log("The current parent of: " + this.gameObject + " is: " + this.transform.parent.gameObject);
            //Debug.Log("This current position: " + this.gameObject.transform.position);
            Vector3 temp = new Vector3();
            temp = Camera.main.ScreenToWorldPoint(this.transform.parent.gameObject.transform.position);
            this.transform.position = new Vector3(temp.x, temp.y, 0);
            //Debug.Log("This current position: " + this.gameObject.transform.position);
        }
    }

    public void doAllActions(int index)
    {
        if (hasTakenTurn) {
            return;
        }
        hasTakenTurn = true;
        setCurrentIndex(index);
        directHealth(status[poisonSprite]);
        decrementAllStatuses();
        if (this.enemyHealth <= 0)
        {
            //gm.nextEnemyTurn(++index);
            return;
        }
        else
        {
            StartCoroutine(takeTurn(index));
        }
    }

    public IEnumerator takeTurn(int index) {
        if (this.gameObject.transform.parent.tag == "newSummon") {
            this.gameObject.transform.parent.tag = "summoned";
            nextAction.Clear();
            intentSet = false;
            yield return new WaitForSeconds(0.4f);
            gm.nextEnemyTurn(++index);
            yield return null;
        }
        //Debug.Log("Take turn now is: " + this);
        //Debug.Log(this.gameObject + " has: " + nextAction.Count + " actions");
        if (nextAction[0] != "countDown") {
            clearIntent();
        }
        foreach (string action in nextAction) {
            StartCoroutine(action);
        }
        nextAction.Clear();
        yield return new WaitForSeconds(0.9f);
        //Debug.Log(this.gameObject + " has: " + nextAction.Count + " actions");
        //setIntent();
        intentSet = false;
        gm.nextEnemyTurn(++index);
        yield return null;
    }
    private void setCurrentIndex(int index) {
        currentIndex = index;
    }

    private int getCurrentIndex() {
        return currentIndex;
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

    //I hate all the code beyond this point
    public void showIntent(string intent, int index) {
        int num = 0;
        //Debug.Log("Index and string sent: " + index + " " + intent);
        if (intent == "attack") { spriteImage = attackIntentSprite;  num = damage + status[buffSprite]; }
        if (intent == "defend") { spriteImage = defendIntentSprite; num = defense; }
        if (intent == "countDown") { spriteImage = countdownSprite; num = countDownTimer; }
        if (intent == "poisonPlayer") { spriteImage = poisonSprite; num = stats.poisonDamage; }
        if (intent == "curse") { spriteImage = curseIntentSprite; num = 0; }
        if (intent == "summon") { spriteImage = summonIntentSprite; num = 0; }
        if (intent == "buffSelf") { spriteImage = buffSprite; num = buffAmount; }
        if (intent == "buffOthers") { spriteImage = buffOthersIntentSprite; num = 0; }
        if (intent == "healSelf") { spriteImage = healSelfIntentSprite; num = stats.healSelfAmount; }
        if (intent == "regenSelf") { spriteImage = regenSprite; num = stats.regenSelfAmount; }
        if (intent == "healOthers") { spriteImage = healOthersIntentSprite; num = stats.healOthersAmount; }
        if (intent == "corrosion") { spriteImage = corrosionIntent; num = 0; }
        if (intent == "canGiveCard") { spriteImage = giveCardIntentSprite; num = 0; }
        //Debug.Log("Action at index " + index + " is: " + nextAction[index]);
        //nextAction.RemoveAt(index);
        intentArea.transform.GetChild(index).gameObject.SetActive(true);
        //Debug.Log(statusArea.transform.GetChild(i).gameObject.GetComponent<Image>().sprite);
        intentArea.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = spriteImage;
        if (num == 0)
        {
            intentArea.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text = " ";
        }
        else {
            intentArea.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text = num.ToString();
        }

        index++;

        if (index >= stats.actionsPerTurn)
        {
            return;
        }
        else {
            showIntent(nextAction[index], index);
        }
    }

    public void addActions() {
        if (stats.damage > 0 && !stats.countdownAction) { actions.Add("attack"); }
        if (stats.defense > 0) { actions.Add("defend"); }
        if (stats.countdownAction) { actions.Add("countDown"); }
        if (stats.poisonDamage > 0) { actions.Add("poisonPlayer");}
        if (stats.regenSelfAmount > 0) {actions.Add("regenSelf");}
        if (stats.curseAction){actions.Add("curse");}
        if (stats.summon != null){actions.Add("summon");}
        if (stats.buffSelfAction){actions.Add("buffSelf");}
        if (stats.buffOthersAction){actions.Add("buffOthers");}
        if (stats.weakenAction){actions.Add("weaken");}
        if (stats.sunderAction){actions.Add("sunder");}
        if (stats.healSelfAmount > 0){actions.Add("healSelf");}
        if (stats.healOthersAmount > 0){actions.Add("healOtherEnemies");}
        if (stats.corrosionAmount > 0){actions.Add("corrosion");}
        if (stats.canGiveCard != null){actions.Add("canGiveCard");}
        if (stats.burnDamage > 0) {actions.Add("burn");}
    }

    IEnumerator attack() {
        //Debug.Log(this.gameObject + " is attacking for: " + damage);
        player.GetComponent<player>().updatePlayerHealth(damage + status[buffSprite]);
        yield return null;
    }
    IEnumerator defend()
    {
        //Debug.Log(this.gameObject + " is defending!");
        addBlock(defense);
        yield return null;
    }
    IEnumerator countDown()
    {
        //Debug.Log(this.gameObject + " is counting down!");
        //Debug.Log("countdown before: " + countDownTimer);
        countDownTimer--;
        //Debug.Log("countdown after: " + countDownTimer);
        showIntent("countDown", 0);
        if (countDownTimer <= 0) {
            anim.SetTrigger("explode");
            player.gameObject.GetComponent<player>().updatePlayerHealth(damage);
            //anim.SetTrigger("death");
            UpdateEnemyHealth(-999);
        }
        yield return null;
    }
    IEnumerator poisonPlayer(){
        Debug.Log(this.gameObject + " is poisoning player!");
        player.status[poisonSprite] += stats.poisonDamage;
        player.updateStatusBar();
        yield return null;
    }
    IEnumerator curse(){
        Debug.Log(this.gameObject + " is cursing player!");
        yield return null;
    } //needs work
    IEnumerator summon(){
        Debug.Log(this.gameObject + " is summoning minion!");
        if (gm.getAvailableEnemySlots() != null) {
            GameObject newEnemy = Instantiate(stats.summon, gm.getAvailableEnemySlots(), true);
            gm.skipEnemyTurn(newEnemy.transform);
            Debug.Log("Enemy to skip is: " + newEnemy);
        }
        yield return null;
    } //needs work
    IEnumerator buffSelf(){
        Debug.Log(this.gameObject + " is buffing self!");
        yield return null;
    } //needs work
    IEnumerator buffOthers(){
        Debug.Log(this.gameObject + " is buffing others!");
        yield return null;
    } //needs work
    IEnumerator debuff(){
        Debug.Log(this.gameObject + " is debuffing player!");
        yield return null;
    } //needs work
    IEnumerator healself(){
        Debug.Log(this.gameObject + " is healing self!");
        yield return null;
    } //needs work
    IEnumerator healOtherEnemies(){
        Debug.Log(this.gameObject + " is healing others!");
        yield return null;
    } //needs work
    IEnumerator corrosion(){
        Debug.Log(this.gameObject + " is corroding armor!");
        yield return null;
    } //needs work
    IEnumerator canGiveCard(){
        Debug.Log(this.gameObject + " is giving card!");
        yield return null;
    } //needs work
}
