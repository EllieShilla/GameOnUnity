using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    float speed = 0.54f;
    Animator animator;
    public GameObject inventory;
    bool isInventoryActive = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!SceneManager.GetActiveScene().name.Equals("FightScene"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed = 1f;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                speed = 0.54f;


            if (Input.GetAxis("Vertical") >= 0.5)
            {
                Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                this.transform.position += Movement * speed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(transform.position.x, 0f, transform.position.z);
                animator.SetFloat("Speed", Mathf.Abs(speed));
            }
            else if (Input.GetAxis("Horizontal") > 0.5)
            {
                Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                this.transform.position += Movement * speed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(transform.position.x, 90f, transform.position.z);
                animator.SetFloat("Speed", Mathf.Abs(speed));
            }
            else if (Input.GetAxis("Horizontal") < -0.5)
            {
                Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                this.transform.position += Movement * speed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(transform.position.x, -90f, transform.position.z);
                animator.SetFloat("Speed", Mathf.Abs(speed));
            }
            else if (Input.GetAxis("Vertical") < -0.5)
            {
                Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                this.transform.position += Movement * speed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(transform.position.x, 180f, transform.position.z);
                animator.SetFloat("Speed", Mathf.Abs(speed));
            }
            else
            {
                animator.SetFloat("Speed", Mathf.Abs(0.1f));
            }

          
        }




        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryActive)
            {
                isInventoryActive = true;
                inventory.SetActive(true);
                inventory.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<BookmarksInventory>().isChoice = true;
                inventory.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Inventory.isCreate = true;
            }
            else
            {
                isInventoryActive = false;
                inventory.SetActive(false);
            }
        }


    }
}
