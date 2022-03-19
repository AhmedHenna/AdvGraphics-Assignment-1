using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

public class TextStart : MonoBehaviour
{
    public static int playerIndex;
    public Text nameText;
    private System.Random random = new System.Random();


    void Start()
    {
        playerIndex = random.Next(0, 11);

        RestClient.GetArray<string>("https://advassignment1-default-rtdb.firebaseio.com/users.json").Then(response =>
        {
            nameText.text = response[playerIndex];
        });
    }

   
}
