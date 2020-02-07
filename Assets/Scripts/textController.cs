using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour
{

    public void ChangeText(string txt)
    {
        gameObject.GetComponent<Text>().text = txt;
    }

}
