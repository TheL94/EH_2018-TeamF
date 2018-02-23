using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalyzingCloud : ElementalComboBase
    {
        public float TimeOfEffect = 4f;
        List<IParalyzable> paralizedElement = new List<IParalyzable>();

        protected override void OnUpdate()
        {
            
        }

        protected override void OnEnteringCollider(Collider other)
        {
            IParalyzable paralized = other.GetComponent<IParalyzable>();
            if(paralized != null)
            {
                paralized.Paralize(TimeOfEffect);
                paralizedElement.Add(paralized);
            }
        }
    }
}