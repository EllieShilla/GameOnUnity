using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFoodForInventory : MonoBehaviour
{
    public List<GameObject> DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;
    public string[] abc = new string[4] { "W", "A", "D", "S" };
    bool isPass = true;
    public static int CurrentCountofButton;


    private void Start()
    {
    }

    private void Update()
    {
        if (StartCreateItem.isStartCreate)
        {
            //Result();
            if (CurrentCountofButton > 0 && isPass)
            {
                if (WaitingForKey == 0)
                {
                    QTEGen = Random.Range(0, 4);
                    CountingDown = 1;
                    StartCoroutine(CountDown());

                    WaitingForKey = 1;
                    OutLetter();
                }

                PressAnalyisis(QTEGen);
            }

            Result();

        }

    }


    void OutLetter()
    {
        switch (QTEGen)
        {
            case 0:
                DisplayBox[0].GetComponent<Text>().text = "[" + abc[QTEGen] + "]";
                break;
            case 1:
                DisplayBox[1].GetComponent<Text>().text = "[" + abc[QTEGen] + "]";
                break;
            case 2:
                DisplayBox[2].GetComponent<Text>().text = "[" + abc[QTEGen] + "]";
                break;
            case 3:
                DisplayBox[3].GetComponent<Text>().text = "[" + abc[QTEGen] + "]";
                break;
        }
    }
    static bool isPositiveResult = false;
    static bool verification = false;
    public static bool ReturnResult()
    {
        return isPositiveResult;
    }

    public static void ReZero()
    {
        verification = false;
        isPositiveResult = false;
    }


    public static bool verificationOfResults()
    {
        return verification;
    }

    void Result()
    {

        if (CurrentCountofButton == 0)
        {
            if (LocalizationManager.Language.Equals("English"))
                PassBox.GetComponent<Text>().text = "You cooked a great meal!";
            else
                PassBox.GetComponent<Text>().text = "Вы приготовили отличное блюдо!";

            isPositiveResult = true;
        }
        if (!isPass)
        {
            if (LocalizationManager.Language.Equals("English"))
                PassBox.GetComponent<Text>().text = "Dish failed!";
            else 
                PassBox.GetComponent<Text>().text = "Блюдо не удалось!";

            isPositiveResult = false;
        }
        

        if (CurrentCountofButton == 0 || !isPass)
        {
            StartCoroutine(CloseQTEPanel());
        }
    }

    IEnumerator CloseQTEPanel()
    {
        yield return new WaitForSeconds(2f);
        PassBox.GetComponent<Text>().text = "";
        isPass = true;
        verification = true;
        Move.canMove = true;
        StartCreateItem.isStartCreate = false;
    }


    void PressAnalyisis(int numLetter)
    {
        switch (numLetter)
        {
            case 0:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                        CorrectKey = 1;
                    else
                        CorrectKey = 2;

                    StartCoroutine(KeyPressing());
                }
                break;
            case 1:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                        CorrectKey = 1;
                    else
                        CorrectKey = 2;

                    StartCoroutine(KeyPressing());
                }
                break;
            case 2:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                        CorrectKey = 1;
                    else
                        CorrectKey = 2;

                    StartCoroutine(KeyPressing());
                }
                break;
            case 3:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                        CorrectKey = 1;
                    else
                        CorrectKey = 2;

                    StartCoroutine(KeyPressing());
                }
                break;
        }
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;

        switch (CorrectKey)
        {
            case 1:
                CountingDown = 2;
                if (LocalizationManager.Language.Equals("English"))
                    PassBox.GetComponent<Text>().text = "EXCELLENT";
                else
                    PassBox.GetComponent<Text>().text = "ОТЛИЧНО";

                CurrentCountofButton -= 1;
                yield return new WaitForSeconds(0.1f);
                CorrectKey = 0;
                PassBox.GetComponent<Text>().text = "";

                DisplayNull();

                yield return new WaitForSeconds(1f);
                WaitingForKey = 0;
                CountingDown = 1;
                break;
            case 2:
                CountingDown = 2;
                if (LocalizationManager.Language.Equals("English"))
                    PassBox.GetComponent<Text>().text = "FAILURE";
                else 
                    PassBox.GetComponent<Text>().text = "НЕУДАЧА";

                isPass = false;
                yield return new WaitForSeconds(0.1f);
                CorrectKey = 0;
                PassBox.GetComponent<Text>().text = "";

                DisplayNull();

                yield return new WaitForSeconds(0.5f);
                WaitingForKey = 0;
                CountingDown = 1;
                break;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "FAIL!";
            isPass = false;
            yield return new WaitForSeconds(0.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";

            DisplayNull();

            yield return new WaitForSeconds(0.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    void DisplayNull()
    {
        foreach (var disp in DisplayBox)
            disp.GetComponent<Text>().text = "";
    }
}
