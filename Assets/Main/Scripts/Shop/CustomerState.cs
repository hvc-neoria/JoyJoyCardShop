using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public partial class Customer : MonoBehaviour
{
    public class GoingGoods : CustomerStateBase
    {
        int remainingGoingGoodsNum;

        public override void OnEnter(Customer owner, CustomerStateBase prevState)
        {
            if (prevState is null)
            {
                remainingGoingGoodsNum = Random.Range(1, 5);
                owner.Loop(100f, 3f, () => owner.nav.avoidancePriority--);
            }
            owner.nav.avoidancePriority--;
            owner.SetDestination(GetRandomGoodsPosition());
        }

        public override void OnUpdate(Customer owner)
        {
            if (owner.nav.remainingDistance == 0)
            {
                if (remainingGoingGoodsNum > 1)
                {
                    remainingGoingGoodsNum--;
                    owner.currentState = goingGoods;
                }
                else
                {
                    owner.currentState = goingRegister;
                }
                // owner.myTrans.DOLookAt(owner.trueDestination, 0.5f, AxisConstraint.Y)
                // .OnComplete(() => owner.currentState = goingEnd);
            }
        }

        Vector3 GetRandomGoodsPosition()
        {
            int rackFaceNum = Random.Range(0, 10);
            float x = 0;
            switch (rackFaceNum)
            {
                case 0: x = -6f; break;
                case 1: x = -4f; break;
                case 2: x = -3.5f; break;
                case 3: x = -1.5f; break;
                case 4: x = -1f; break;
                case 5: x = 1f; break;
                case 6: x = 1.5f; break;
                case 7: x = 3.5f; break;
                case 8: x = 4f; break;
                case 9: x = 6f; break;
            }
            float z = Random.Range(2f, 7f);
            return new Vector3(x, 1f, z);
        }
    }

    public class GoingRegister : CustomerStateBase
    {
        public override void OnEnter(Customer owner, CustomerStateBase prevState)
        {
            owner.SetDestination(RegisterPos);
            owner.nav.avoidancePriority = 30;
            owner.nav.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        }

        public override void OnUpdate(Customer owner)
        {
            if (owner.readPacks == null)
            {
                if (owner.nav.remainingDistance < 1.3f)
                {
                    CreateReadPacks(owner);
                    LookAtRegister(owner);
                }
            }
            else
            {
                if (IsReadAll(owner.readPacks))
                {
                    owner.currentState = goingOutside;
                }
            }
        }

        private void CreateReadPacks(Customer owner)
        {
            int packCount = PackCount();
            owner.readPacks = new ReadPack[packCount];

            float x = EtcUtility.NormalDistribution() / 3f * 0.2f;
            for (int i = 0; i < packCount; i++)
            {
                GameObject packGameObject = owner.packInstantiater.Instantiate(x, i);
                owner.readPacks[i] = packGameObject.GetComponent<ReadPack>();
            }
        }

        int PackCount()
        {
            var m = Master.I;
            int packCountMax = Math.Min(m.packCountAtFirst + (int)((float)GotCounts.Total(Rarity.Rare)
                * m.packCountIncrement), m.packCountAtLast);
            int packCount = Random.Range(1, packCountMax + 1);
            Debug.Log("packCount" + packCount);
            return packCount;
        }

        private static void LookAtRegister(Customer owner)
        {
            owner.myTrans.DOLookAt(RegisterPos + Vector3.forward * -2f, 0.2f, AxisConstraint.Y).SetDelay(0.2f);
            owner.nav.updateRotation = false;
        }

        bool IsReadAll(ReadPack[] readPacks)
        {
            foreach (var item in readPacks)
            {
                if (!item.IsRead) return false;
            }
            return true;
        }
    }

    public class GoingOutside : CustomerStateBase
    {
        static readonly Vector3 OutsidePos = new Vector3(-1f, 1f, 10f);
        static readonly Vector3 OutsidePos2 = new Vector3(-7f, 1f, 10f);

        public override void OnEnter(Customer owner, CustomerStateBase prevState)
        {
            owner.nav.updateRotation = true;
            owner.nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

            Vector3 pos = Random.Range(0, 2) == 0 ? OutsidePos : OutsidePos2;
            owner.SetDestination(pos);
        }

        public override void OnUpdate(Customer owner)
        {
            if (owner.nav.remainingDistance == 0)
            {
                Destroy(owner.gameObject);
            }
        }
    }
}