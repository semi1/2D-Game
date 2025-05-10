using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{

    AudioManager audioManager;

    public Text coinAmount;
    public int currentCoin = 0;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        coinAmount.text = currentCoin.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coins")
        {
            audioManager.playCoins();
            currentCoin++;
            other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Trigger");
            Destroy(other.gameObject, 1f);
        }
    }
}
