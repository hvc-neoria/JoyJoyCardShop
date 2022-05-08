using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// カード一枚あたりの給料
/// </summary>
public class UnitSalary : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        Disp();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Disp();
        }
    }

    void Disp()
    {
        text.text = $"給料：{Card.UnitSalary.value.ToString("N0")}円/枚";
    }
}
