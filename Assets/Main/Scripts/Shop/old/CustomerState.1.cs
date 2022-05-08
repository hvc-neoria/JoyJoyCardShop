// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public partial class Customer : MonoBehaviour
// {
//     public class GoingGoods : CustomerStateBase
//     {
//         public override void OnEnter(Customer owner, CustomerStateBase prevState)
//         {
//             owner.SetDestination(GetRandomGoodsPosition());
//         }

//         public override void OnArrive(Customer owner)
//         {
//             // owner.myTrans.DOLookAt(owner.trueDestination, 0.5f, AxisConstraint.Y)
//             // .OnComplete(() => owner.currentState = goingEnd);
//             owner.currentState = goingRegister;
//         }

//         Vector3 GetRandomGoodsPosition()
//         {
//             int rackFaceNum = UnityEngine.Random.Range(0, 10);
//             float x = 0;
//             switch (rackFaceNum)
//             {
//                 case 0: x = -6f; break;
//                 case 1: x = -4f; break;
//                 case 2: x = -3.5f; break;
//                 case 3: x = -1.5f; break;
//                 case 4: x = -1f; break;
//                 case 5: x = 1f; break;
//                 case 6: x = 1.5f; break;
//                 case 7: x = 3.5f; break;
//                 case 8: x = 4f; break;
//                 case 9: x = 6f; break;
//             }
//             // x = 1.125f;
//             float z = UnityEngine.Random.Range(2f, 7f);
//             return new Vector3(x, 1f, z);
//         }
//     }

//     public class GoingRegister : CustomerStateBase
//     {
//         public static Customer currentCustomer { get; set; }

//         ReadPack[] readPacks;

//         public override void OnEnter(Customer owner, CustomerStateBase prevState)
//         {
//             owner.SetDestination(RegisterPos);
//         }

//         public override void OnArrive(Customer owner)
//         {
//             if (currentCustomer == null)
//             {
//                 currentCustomer = owner;
//                 owner.myTrans.DOLookAt(RegisterPos, 0.2f, AxisConstraint.Y).SetDelay(0.2f);
//                 owner.nav.updateRotation = false;
//                 // DOVirtual.DelayedCall(2f, () => owner.currentState = goingOutside);

//                 int packCount = UnityEngine.Random.Range(1, 6);
//                 readPacks = new ReadPack[packCount];
//                 for (int i = 0; i < packCount; i++)
//                 {
//                     GameObject packGameObject = owner.packInstantiater.Instantiate();
//                     readPacks[i] = packGameObject.GetComponent<ReadPack>();
//                 }
//             }
//         }

//         public override void OnUpdate(Customer owner)
//         {
//             if (currentCustomer == owner)
//             {
//                 foreach (var item in readPacks)
//                 {
//                     if (!item.isRead) return;
//                 }
//                 owner.currentState = goingOutside;
//             }
//         }
//     }

//     public class GoingOutside : CustomerStateBase
//     {
//         static readonly Vector3 OutsidePos = new Vector3(-1f, 1f, 10f);

//         public override void OnEnter(Customer owner, CustomerStateBase prevState)
//         {
//             owner.nav.updateRotation = true;
//             GoingRegister.currentCustomer = null;
//             owner.nav.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance;
//             owner.SetDestination(OutsidePos);
//         }

//         public override void OnArrive(Customer owner)
//         {
//             Destroy(owner.gameObject);
//         }
//     }
// }