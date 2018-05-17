using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace TeamF
{
    public class BlinkController : MonoBehaviour
    {
        public float DamageBlinkTime;
        public float DamageBrightness;
        float initalBrightness;

        public float PoisonedBlinkTime;
        public Color PosonedColor;

        public float SlowedBlinkTime;
        public Color SlowedColor;

        List<SkinnedMeshRenderer> renderers;

        List<Tweener> activeTweeners = new List<Tweener>();
        Coroutine corutine;

        private void Start()
        {
            renderers = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
            initalBrightness = renderers[0].material.GetFloat("_Brightness");
        }

        #region API
        public void DamageBlink()
        {
            if (renderers == null)
                return;

            if (corutine != null)
            {
                StopCoroutine(corutine);
                foreach (SkinnedMeshRenderer renderer in renderers)
                    renderer.material.SetFloat("_Brightness", initalBrightness);
            }

            corutine = StartCoroutine(DamageBlinkCorutine());
        }

        public void PoisonedBlink(float _durationTime)
        {
            if (activeTweeners.Count > 0)
            {
                for (int i = 0; i < activeTweeners.Count; i++)
                {
                    activeTweeners[i].Kill();
                    renderers[i].material.SetColor("_Color", Color.white);
                }
            }

            EffectBlink(PoisonedBlinkTime, _durationTime, PosonedColor);
        }

        public void SlowedBlink(float _durationTime)
        {
            if (activeTweeners.Count > 0)
                foreach (Tweener tween in activeTweeners)
                    tween.Kill();

            EffectBlink(SlowedBlinkTime, _durationTime, SlowedColor);
        }
        #endregion

        IEnumerator DamageBlinkCorutine()
        {
            float initialValue = renderers[0].material.GetFloat("_Brightness");

            foreach (SkinnedMeshRenderer renderer in renderers)
                renderer.material.SetFloat("_Brightness", DamageBrightness);

            yield return new WaitForSeconds(DamageBlinkTime);

            foreach (SkinnedMeshRenderer renderer in renderers)
                renderer.material.SetFloat("_Brightness", initialValue);
        }

        void EffectBlink(float _time, float _duration, Color _colorToReach)
        {
            if (renderers == null)
                return;

            int loops = (int)(_duration / _time);

            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                Tweener tween = renderer.material.DOColor(_colorToReach, "_Color", _time);
                tween.SetLoops(loops, LoopType.Yoyo);
                activeTweeners.Add(tween);
            }
        }
    }
}
