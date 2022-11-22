using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonManager : MonoBehaviour
{
    public int level_count = 1;
    private GameObject grid;
    public GameObject button_prefab;

    void Start()
    {
        grid = GameObject.Find("ButtonGrid");

        for (int i = 1; i < level_count+1; i++){
            GameObject newButton = Instantiate(button_prefab);
            newButton.transform.SetParent(grid.transform, false);

            newButton.GetComponentInChildren<TMP_Text>().text = i.ToString();

            newButton.GetComponent<LevelButton>().level = i;
        }
    }
}
