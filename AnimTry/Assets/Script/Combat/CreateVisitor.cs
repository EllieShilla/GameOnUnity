using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CreateVisitor : MonoBehaviour
{
    List<Vector3> VisitorPosition = new List<Vector3>() {
        new Vector3((float)-4.8, (float)0.2, (float)-2.899),
        new Vector3((float)-4.8, (float)0.2, (float)-3.865), 
        //new Vector3((float)-4.8, (float)0.1, (float)-4.794) 
    };
    int count = 0;

    public void NewVisitors()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.Find("VisitorList").GetComponent<VisitorList>().visitors);
        count = 0;
        GameObject[] enemiesArr = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var item in enemies)
        {
            Vector3 vector3 = new Vector3(VisitorPosition[count].x, VisitorPosition[count].y, VisitorPosition[count].z);

            if (enemiesArr.FirstOrDefault(i => i.transform.position == vector3)==null)
            {
                GameObject character = Instantiate(item, vector3, Quaternion.identity) as GameObject;
                character.transform.Rotate(0, 270, 0);
                character.SetActive(true);
                character.name = item.name;
                character.transform.Find("Selector").gameObject.SetActive(false);
                character.GetComponent<EnemyStateMachine>().Selector.SetActive(true);
            }

            count++;
        }
    }
}

