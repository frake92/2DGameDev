using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI appleTexts;
    void Start()
    {
        appleTexts.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        appleTexts.text = player.applesCount.ToString();
    }
}
