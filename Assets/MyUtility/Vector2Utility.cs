using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utility
{
    public static Vector3 ToXOZ(this Vector2 vector2) => new Vector3(vector2.x, 0, vector2.y);

    /// <summary>
    /// ベクトルを円形に沿って縮小する。
    /// 斜め入力でルート2になる問題を解決する。
    /// 長さが1に限らない正規化。
    /// スティックの浅い斜め入力も円形に沿って縮小する。
    /// </summary>
    public static Vector2 GetShorterAlongCircle(this Vector2 vector2)
    {
        float xLength = Mathf.Abs(vector2.x);
        float yLength = Mathf.Abs(vector2.y);
        float longestLength = Mathf.Max(xLength, yLength);

        return vector2.normalized * longestLength;
    }

    /// <summary>
    /// 角度を球座標に変換する。
    /// 緯度・経度のように角度を与える。
    /// Transform.RotateAroundでは困難な角度制限が容易であることが特徴。
    /// </summary>
    /// <returns></returns>
    public static Vector3 ToSphericalCoordinate(this Vector2 angle, float radius)
    {
        // 緯度・経度のように角度を与えられるようにするオフセットに変換する
        float xOffset = -angle.x - 180f;
        float yOffset = -angle.y + 90f;
        Vector2 offset = new Vector2(xOffset, yOffset);

        Vector3 result = ToSphericalCoordinateOriginally(offset, radius);
        return result;
    }

    /// <summary>
    /// 角度を球座標に変換する。
    /// Transform.RotateAroundでは困難な角度制限が容易であることが特徴。
    /// <para>x=0, y=0のときx=0, y=1, z=0</para>
    /// <para>x=0, y=90のときx=0, y=0, z=1</para>
    /// <para>x=90, y=90のときx=1, y=0, z=0</para>
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    static Vector3 ToSphericalCoordinateOriginally(this Vector2 angle, float radius)
    {
        float xRad = angle.x * Mathf.Deg2Rad;
        float yRad = angle.y * Mathf.Deg2Rad;
        Vector3 result = new Vector3(
            radius * Mathf.Sin(yRad) * Mathf.Sin(xRad),
            radius * Mathf.Cos(yRad),
            radius * Mathf.Sin(yRad) * Mathf.Cos(xRad)
        );
        return result;
    }
}