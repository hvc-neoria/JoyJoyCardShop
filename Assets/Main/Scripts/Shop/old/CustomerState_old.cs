

// public class GoingGoods : CustomerStateBase
// {
//     public override void OnEnter(Customer owner, CustomerStateBase prevState)
//     {
//         owner.SetDestination(GetRandomGoodsPosition());
//     }

//     public override void OnArrive(Customer owner)
//     {
//         // owner.myTrans.DOLookAt(owner.trueDestination, 0.5f, AxisConstraint.Y)
//         // .OnComplete(() => owner.currentState = goingEnd);
//         owner.currentState = goingEnd;
//     }

//     Vector3 GetRandomGoodsPosition()
//     {
//         int rackFaceNum = UnityEngine.Random.Range(0, 10);
//         float x = 0;
//         switch (rackFaceNum)
//         {
//             case 0: x = -6f; break;
//             case 1: x = -4f; break;
//             case 2: x = -3.5f; break;
//             case 3: x = -1.5f; break;
//             case 4: x = -1f; break;
//             case 5: x = 1f; break;
//             case 6: x = 1.5f; break;
//             case 7: x = 3.5f; break;
//             case 8: x = 4f; break;
//             case 9: x = 6f; break;
//         }
//         // x = 1.125f;
//         float z = UnityEngine.Random.Range(2f, 7f);
//         return new Vector3(x, 1f, z);
//     }
// }

// public class GoingEnd : CustomerStateBase
// {
//     static Vector3 endPos => endCustomer == null ? RegisterPos : endCustomer.myTrans.position + endCustomer.myTrans.forward * -1f;
//     static Customer endCustomer;

//     public override void OnEnter(Customer owner, CustomerStateBase prevState)
//     {
//         owner.SetDestination(endPos);
//     }

//     public override void OnArrive(Customer owner)
//     {
//         owner.nextCustomer = endCustomer;
//         if (endCustomer == null)
//         {
//             owner.currentState = waitingCheck;
//         }
//         else
//         {
//             owner.currentState = goingForward;
//         }
//         // owner.currentState = endCustomer == null ? waitingCheck : goingForward;
//         endCustomer = owner;
//         OnUpdateEnd.Invoke();
//         // owner.myTrans.DOLookAt(owner.trueDestination, 0.5f, AxisConstraint.Y)
//         // .OnComplete()
//     }
// }

// public class GoingForward : CustomerStateBase
// {
//     public override void OnEnter(Customer owner, CustomerStateBase prevState)
//     {
//         if (owner.nextCustomer.myTrans == null || owner.nextCustomer.currentState == goingOutside)
//         {
//             owner.SetDestination(RegisterPos);
//         }
//         else
//         {
//             owner.SetDestination(owner.nextCustomer.myTrans.position + owner.nextCustomer.myTrans.forward * -1f);
//         }
//     }

//     public override void OnUpdate(Customer owner)
//     {
//         // owner.nextCustomer
//     }

//     public override void OnArrive(Customer owner)
//     {
//         float hoge = (owner.myTrans.position - RegisterPos).sqrMagnitude;
//         if (hoge < 0.1f)
//         {
//             owner.currentState = waitingCheck;
//             return;
//         }
//         if (owner.nextCustomer.myTrans == null)
//         {
//             owner.currentState = goingForward;
//             return;
//         }
//         float sqrDistance = (owner.myTrans.position - owner.nextCustomer.myTrans.position).sqrMagnitude;
//         if (sqrDistance > 1f)
//         {
//             owner.currentState = goingForward;
//         }
//     }
// }

// public class WaitingCheck : CustomerStateBase
// {
//     public override void OnEnter(Customer owner, CustomerStateBase prevState)
//     {
//         DOVirtual.DelayedCall(2f, () => owner.currentState = goingOutside);
//     }
// }

// public class GoingOutside : CustomerStateBase
// {
//     static readonly Vector3 OutsidePos = new Vector3(-1f, 1f, 10f);

//     public override void OnEnter(Customer owner, CustomerStateBase prevState)
//     {
//         owner.SetDestination(OutsidePos);
//     }

//     public override void OnArrive(Customer owner)
//     {
//         Destroy(owner.gameObject);
//     }
// }
