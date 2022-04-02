using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DialogBox").SetActive(false);
        GameObject.Find("FightBox").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
