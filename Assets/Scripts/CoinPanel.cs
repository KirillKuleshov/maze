using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    Text coinCountText;
    // Start is called before the first frame update
    void Start()
    {
        Transform trans = transform.Find("Coin Count");
        coinCountText = trans.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = Controller.numCoins.ToString();
    }
}
