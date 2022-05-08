using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using static Rarity;

/// <summary>
/// カードの入手数
/// </summary>
public static class GotCounts
{
    static SortedDictionary<Rarity, int[]> Value = new SortedDictionary<Rarity, int[]>()
    {
        {Common, new int[55] },
        {Rare, new int[15] },
        {SR, new int[5] },
        {UR, new int[3] }
    };
    public static int Length(Rarity rarity) => Value[rarity].Length;
    public static int Total(Rarity rarity) => Value[rarity].Sum();
    public static int Total() => Value.Sum(x => x.Value.Sum());

    // https://pokecaminv.tokyo/archives/217
    public static Rarity GetRarityRandom()
    {
        int n = Random.Range(0, 1800);
        if (n <= 0)
        {
            return UR;
        }
        else if (n <= 11)
        {
            return SR;
        }
        else if (n <= 95)
        {
            return Rare;
        }
        else
        {
            return Common;
        }
    }

    public static int GetIndexRandom(Rarity rarity) => Random.Range(0, Value[rarity].Length);
    public static void Increment(Rarity rarity, int index) => Value[rarity][index]++;

    public static string ToRarityString(Rarity rarity)
    {
        string rarityString = "";
        switch (rarity)
        {
            case Common: rarityString = "C"; break;
            case Rare: rarityString = "R"; break;
            case SR: rarityString = "SR"; break;
            case UR: rarityString = "UR"; break;
        }
        return rarityString;
    }
    public static int Get(Rarity rarity, int index) => Value[rarity][index];
}
