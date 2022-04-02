using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    public Character character;
    public List<Character> group;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        //if (gameObject.transform.position == new Vector3(-22.75f, 0.01f, -4.18f))
        //    animator.SetBool("isFight", true);

    }
    
}
