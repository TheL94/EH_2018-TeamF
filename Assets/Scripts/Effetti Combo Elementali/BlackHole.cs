using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace TeamF {
    public class BlackHole : ElementalComboBase
    {
        List<NavMeshAgent> enemyCaught = new List<NavMeshAgent>();

        protected override void ComboEffect(Collider other)
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
            foreach (NavMeshAgent nav in enemyCaught)
            {
                nav.isStopped = false;
            }
        }
    }
}