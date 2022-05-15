using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropItem : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public GameObject droppedCrystalPrefab;

    public EnemyStats enemyStats;
    public playerStats playerStats;

    private int startAmount;
    private int addedCrystals;
    public void dropItem()
    {
        addedCrystals = (Random.Range(1, 101) % playerStats.crystalLuck);
        for (startAmount = 0; startAmount < (enemyStats.level + addedCrystals); startAmount++) {
            GameObject crystal = Instantiate(droppedCrystalPrefab, transform.position, Quaternion.identity);
            new Vector3(crystal.transform.position.x, crystal.transform.position.y, crystal.transform.position.z + 1);
        }

        //for specific drops
        if (Random.Range(0, 100) <= playerStats.itemLuck * enemyStats.level) {
            GameObject item = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
            new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 1);
        }
    }
}
