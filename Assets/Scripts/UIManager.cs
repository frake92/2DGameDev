using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject image;
    public GameObject text;

   void Start()
    {
        image = GameObject.FindGameObjectWithTag("Image");
        text.AddComponent<TextMeshProUGUI>().text = "Hallo";
        image.GetComponent<Image>().color = new Color32(255, 188, 164, 100);
    }

     void Update()
    {
        Debug.Log(image.name);
    }
}
