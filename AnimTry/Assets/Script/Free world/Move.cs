using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    float speed = 0.54f;
    Animator animator;
    public GameObject inventory;
    bool isInventoryActive = false;
    InventoryController inventoryController;
    public static bool canMove = true;
    [SerializeField]
    private GameObject MenuPanel;
    static bool isMenuActive = false;

    public static void ActiveMenu(bool inform)
    {
        isMenuActive = inform;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventoryController = new InventoryController();
        Move.canMove = true;
    }
    private void Update()
    {

        if (!SceneManager.GetActiveScene().name.Equals("FightScene"))
        {
            if (canMove)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                    speed = 1f;
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                    speed = 0.54f;

                if (Input.GetAxis("Vertical") >= 0.5)
                {
                    Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                    this.transform.position += Movement * speed * Time.deltaTime;
                    this.transform.rotation = Quaternion.Euler(0.0f, 0f, 0.0f);
                    animator.SetFloat("Speed", Mathf.Abs(speed));
                }
                else if (Input.GetAxis("Horizontal") > 0.5)
                {
                    Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                    this.transform.position += Movement * speed * Time.deltaTime;
                    this.transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
                    animator.SetFloat("Speed", Mathf.Abs(speed));
                }
                else if (Input.GetAxis("Horizontal") < -0.5)
                {
                    Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                    this.transform.position += Movement * speed * Time.deltaTime;
                    this.transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
                    animator.SetFloat("Speed", Mathf.Abs(speed));
                }
                else if (Input.GetAxis("Vertical") < -0.5)
                {
                    Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                    this.transform.position += Movement * speed * Time.deltaTime;
                    this.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
                    animator.SetFloat("Speed", Mathf.Abs(speed));
                }
                else
                {
                    animator.SetFloat("Speed", Mathf.Abs(0.1f));
                }
            }

        }

        //Open-Close Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Move.isMenuActive)
            {
                Move.isMenuActive = true;
            }
            else
            {
                Move.isMenuActive = false;
            }
        }

        if (Move.isMenuActive)
        {
            MenuPanel.SetActive(true);

            Menu.ActiveMenuButton(MenuPanel);

            Time.timeScale = 0f;
        }
        else
        {
            MenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }

        //действия при открытии и закрытии инвентаря
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryActive)
            {
                isInventoryActive = true;
                inventory.SetActive(true);

                //первая вкладка стает "выбранной"
                inventory.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<BookmarksInventory>().isChoice = true;
                inventory.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                Inventory.isCreate = true;

                //делает закрытими все вкладки кроме первой
                for (int i = 0; i < ButtonsList.buttons.Count; i++)
                {
                    if (ButtonsList.buttons[i].GetIndex() != 0)
                    {
                        if (ButtonsList.buttons[i].animator != null)
                            ButtonsList.buttons[i].animator.Play("Button_Always", 0, 0f);

                    }
                }

            }
            else
            {
                isInventoryActive = false;
                inventory.SetActive(false);

                //делает все вкладки "не выбранными"
                foreach (var bookButton in ButtonsList.buttons)
                    bookButton.isChoice = false;

                //первая вкладка преобретает анимацию выбранной. Это делается для того, чтобы при следующей активации инвентаря первая вкладка отработала
                inventoryController.ChoosePage(0, inventory);

            }
        }


    }
}


