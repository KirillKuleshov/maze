using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int width;
    public int height;
    public float coinSpawnTime = 5f;
    public float lastCoinSpawnTime = 0f;
    public int numEnemyes = 0;
    public GameObject coinPrefab;
    public GameObject ratPrefab;
    public GameObject zombiePrefab;

    private GameObject player;
    private int[,] cMap;
    private Transform enemyAnchor, coinAnchor;

    void Start()
    {
        width = Generator.width;
        height = Generator.height;
        player = GameObject.Find("Player");
        cMap = Generator.GetMap();
        enemyAnchor = (new GameObject("Enemy Anchor")).transform;
        coinAnchor = (new GameObject("Coin Anchor")).transform;
        numCoins = 0;
        numCoinsOnMap = 0;

        InstantiateEnemy("Zombie");
    }

    void Update()
    {
        if (Time.time > lastCoinSpawnTime && numCoinsOnMap < 10)
        {
            InstantiateCoin();
        }

        if (numCoins % 20 == 0 && numCoins / 10 > numEnemyes)
        {
            InstantiateEnemy("Rat");
        }

        if (numCoins / 10 > numEnemyes)
        {
            InstantiateEnemy("Zombie");            
        }
    }

    private void InstantiateCoin()
    {
        lastCoinSpawnTime = Time.time + coinSpawnTime;
        GameObject coin = Instantiate<GameObject>(coinPrefab);

        int x = Random.Range(1, width);
        int y = Random.Range(1, height);

        coin.transform.SetParent(coinAnchor);
        coin.transform.position = new Vector3(x, y, -1);

        numCoinsOnMap++;
        if (cMap[x, y] == 1)
        {
            Destroy(coin);
            numCoinsOnMap--;
            InstantiateCoin();
        }
    }

    private void InstantiateEnemy(string enemy)
    {
        int x = Random.Range(1, width);
        int y = Random.Range(1, height);

        if (enemy == "Zombie") 
        {
            GameObject go = Instantiate<GameObject>(zombiePrefab);
            go.transform.SetParent(enemyAnchor);
            go.transform.position = new Vector3(x, y, -1);
            numEnemyes++;
            if (cMap[x, y] == 1)
            {
                Destroy(go);
                numEnemyes--;
                InstantiateEnemy(enemy);
            }
        }
        else if (enemy == "Rat")
        {
            GameObject go = Instantiate<GameObject>(ratPrefab);
            go.transform.SetParent(enemyAnchor);
            go.transform.position = new Vector3(x, y, -1);
            numEnemyes++;
            if (cMap[x, y] == 1)
            {
                Destroy(go);
                numEnemyes--;
                InstantiateEnemy(enemy);
            }
        }     
    }

    public static int numCoins { get; set; }
    public static int numCoinsOnMap { get; set; }
    public static string playerName { get; set; }
}
