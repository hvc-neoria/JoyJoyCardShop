using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    void Awake()
    {
        Card.OnDrawnUR += Disp;
    }

    void OnDestroy()
    {
        Card.OnDrawnUR -= Disp;
    }

    void Disp()
    {
        var text = GetComponent<Text>();
        text.text =
$@"ウルトラレア：{GotCounts.Total(Rarity.UR).ToString("N0")}枚
スーパーレア：{GotCounts.Total(Rarity.SR).ToString("N0")}枚
レア：{GotCounts.Total(Rarity.Rare).ToString("N0")}枚
コモン：{GotCounts.Total(Rarity.Common).ToString("N0")}枚

総入手枚数：{GotCounts.Total().ToString("N0")}枚

収入：{PlayersMoney.Income.value.ToString("N0")}円
出費：{PlayersMoney.Expense.value.ToString("N0")}円";
    }
}
