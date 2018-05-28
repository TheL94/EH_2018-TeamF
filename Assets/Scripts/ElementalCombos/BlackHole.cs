using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace TeamF {
    public class BlackHole : ElementalComboBase
    {
        List<NavMeshAgent> enemyCaught = new List<NavMeshAgent>();

        protected override void OnInit()
        {
            GameManager.I.AudioMng.PlaySound(Clips.ComboBlackHole);
        }

        protected override void OnEnterCollider(Collider other)
        {
            NavMeshAgent navMesh = other.GetComponent<NavMeshAgent>();
            if (navMesh != null)
            {
                navMesh.transform.DOMove(transform.position, 2f);
                if (navMesh.isActiveAndEnabled)
                {
                    navMesh.isStopped = true;
                    enemyCaught.Add(navMesh);
                }
            }
        }

        protected override void OnEndEffect()
        {
            for (int i = 0; i < enemyCaught.Count; i++)
            {
                if (enemyCaught[i] != null)
                {
                    if (enemyCaught[i].isActiveAndEnabled)
                        enemyCaught[i].isStopped = false;  
                }
            }
            enemyCaught.Clear();
            base.OnEndEffect();
        }
    }
}