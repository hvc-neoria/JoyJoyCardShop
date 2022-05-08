using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public static class EtcUtility
{
    // public CameraAngle(Vector2 value)
    // {
    //     Vector2 result = value;
    //     if (value.x < minX)
    //     {
    //         float fromZeroToMinus360 = value.x % maxX;
    //         float fromZeroTo360 = fromZeroToMinus360 + maxX;
    //         result.x = fromZeroTo360;
    //     }
    //     if (value.x >= maxX) result.x = value.x % maxX;
    //     if (value.y < minY) result.y = minY;
    //     if (value.y > maxY) result.y = maxY;
    //     this.value = result;
    // }
    /// 移動ベクトルをカメラの水平な正面の向きに回転させる
    // public MoveVector2 RotateTo(Vector3 cameraForward)
    // {
    //     Vector3 horizontalMoveVector3 = new Vector3(this.value.x, 0, this.value.y);
    //     Vector3 horizontalCameraForward = new Vector3(cameraForward.x, 0, cameraForward.z);
    //     var result = Quaternion.LookRotation(horizontalCameraForward, Vector3.up) * horizontalMoveVector3;
    //     return new MoveVector2(new Vector2(result.x, result.z));
    // }

    /// <summary>
    /// 返り値のベクトルの始点を、衝突元のコライダーの中心とすると、
    /// 衝突元のコライダーの軌道予測線となる。
    /// </summary>
    /// <param name="otherCollision"></param>
    /// <returns></returns>
    public static Vector3 ReflectionVector(this Collision otherCollision)
    {
        Vector3 inVector = otherCollision.relativeVelocity;
        Vector3 normalizedNormal = otherCollision.impulse.normalized;
        Vector3 outVector = Vector3.Reflect(-inVector, normalizedNormal);

        // デバッグ用
        // Vector3 collisionPoint = otherCollision.contacts[0].point;
        // Debug.DrawLine(collisionPoint, collisionPoint + inVector, Color.red, 1f);
        // Debug.DrawLine(collisionPoint, collisionPoint + normalizedNormal, Color.green, 1f);
        // Debug.DrawLine(collisionPoint, collisionPoint + outVector, Color.blue, 1f);
        // Vector3 position = transform.position;
        // Debug.DrawLine(position, position + inVector, Color.cyan, 1f);
        // Debug.DrawLine(position, position + normalizedNormal, Color.magenta, 1f);
        // Debug.DrawLine(position, position + outVector, Color.yellow, 1f); //軌道予測線
        return outVector;
    }

    public static Vector3[] GetPositionsSimply(this LineRenderer lineRenderer)
    {
        int count = lineRenderer.positionCount;
        Vector3[] results = new Vector3[count];
        // resultに変数が返されるのだが、outの記載が不要で紛らわしい
        lineRenderer.GetPositions(results);
        return results;
    }


    public static string[][] ToStrings(this TextAsset csvFile)
    {
        StringReader reader = new StringReader(csvFile.text);

        List<string[]> csvDatas = new List<string[]>();
        while (reader.Peek() != -1) // reader.Peekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
        string[][] result = csvDatas.ToArray();
        return result;
    }

    /// <summary>
    /// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
    /// </summary>
    public static T FindObjectOfInterface<T>() where T : class
    {
        foreach (var n in GameObject.FindObjectsOfType<Component>())
        {
            var component = n as T;
            if (component != null)
            {
                return component;
            }
        }
        return null;
    }

    public static Vector3 MousePositionToHitPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return ToHitPosition(mousePosition).point;
    }

    public static RaycastHit MousePositionToHitInfo()
    {
        return ToHitPosition(Input.mousePosition);
    }

    static RaycastHit ToHitPosition(Vector3 screenPoint)
    {
        Ray mousePositionToRay = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hitInfo = new RaycastHit();
        Physics.Raycast(mousePositionToRay, out hitInfo);
        return hitInfo;
    }

    /// SetPassを増やさず色を変更する
    public static void SetColor(this Renderer renderer, Color color)
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_Color", color);
        renderer.SetPropertyBlock(block);

        // 複数回色を変更する場合は、下記ID算出を先に行うことで処理を減らすことができる
        // int id = Shader.PropertyToID("_Color");
        // block.SetColor(id, Color.red);
        // https://qiita.com/OKsaiyowa/items/465caccc9e0b9d94ba35
    }

    public static void SetTexture(this Renderer renderer, Texture texture)
    {
        var block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", texture);
        renderer.SetPropertyBlock(block);
    }

    // https://baba-s.hatenablog.com/entry/2014/06/05/220224
    public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self) where T : Component
    {
        return self.GetComponentsInChildren<T>().Where(c => self != c.gameObject).ToArray();
    }

    public static float NormalDistribution()
    {
        float x = UnityEngine.Random.value;
        float y = UnityEngine.Random.value;
        float v = Mathf.Sqrt(-2f * Mathf.Log(x)) * Mathf.Cos(2f * Mathf.PI * y);
        return v;
    }
}