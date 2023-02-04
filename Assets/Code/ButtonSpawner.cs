using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] int resourceCost = 1;

    [SerializeField] private int spawnNodeNum;
    [SerializeField] private int endNodeNum;

    [SerializeField] private BaseSprite enemyType;

    [SerializeField] int spawnHealth = 1;
    [SerializeField] int spawnDamage = 1;

    [SerializeField] float movementSpeed = 0.4f;

    PlayerBase playerBase;

    private void Start()
    {
        playerBase = GameObject.FindAnyObjectByType<PlayerBase>();
    }
    private void OnMouseDown()
    {
        if (playerBase.reasource >= resourceCost)
        {
            playerBase.reasource -= resourceCost;
            GameObject firstNode = GameObject.Find("PathNode (" + spawnNodeNum + ")");
            BaseSprite instance = Instantiate(enemyType, firstNode.transform.position, Quaternion.identity);

            instance.setVariables(spawnHealth, spawnDamage, movementSpeed, spawnNodeNum, firstNode, endNodeNum);
        }
    }
}