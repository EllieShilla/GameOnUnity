using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private void Start()
    {
        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isSitting", true);
    }
}
