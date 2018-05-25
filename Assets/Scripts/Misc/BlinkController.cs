using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace TeamF
{
    // TODO : da rivedere
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
                ResetBrightness();
            }

            corutine = StartCoroutine(DamageBlinkCorutine());
        }

        public void PoisonedBlink(float _durationTime)
        {
            if (activeTweeners.Count > 0)
            {
                CompleteTweens();
                ResetColor();
            }

            EffectBlink(PoisonedBlinkTime, _durationTime, PosonedColor);
        }

        public void SlowedBlink(float _durationTime)
        {
            if (activeTweeners.Count > 0)
            {
                CompleteTweens();
                ResetColor();
            }

            EffectBlink(SlowedBlinkTime, _durationTime, SlowedColor);
        }

        public void ResetEffects()
        {
            ResetBrightness();
            ResetColor();
        }
        #endregion

        IEnumerator DamageBlinkCorutine()
        {
            float initialValue = renderers[0].material.GetFloat("_Brightness");

            for (int i = 0; i < renderers.Count; i++)
                renderers[i].material.SetFloat("_Brightness", DamageBrightness);

            yield return new WaitForSeconds(DamageBlinkTime);

            for (int i = 0; i < renderers.Count; i++)
                renderers[i].material.SetFloat("_Brightness", initialValue);
        }

        void EffectBlink(float _time, float _duration, Color _colorToReach)
        {
            if (renderers == null)
                return;

            int loops = (int)(_duration / _time);

            for (int i = 0; i < renderers.Count; i++)
            {
                Tweener tween = renderers[i].material.DOColor(_colorToReach, "_Color", _time);
                tween.SetLoops(loops, LoopType.Yoyo);
                tween.OnComplete(() => { ResetColor(); activeTweeners.Remove(tween); });
                activeTweeners.Add(tween);
            }
        }

        void ResetBrightness()
        {
            for (int i = 0; i < renderers.Count; i++)
                renderers[i].material.SetFloat("_Brightness", initalBrightness);
        }

        void CompleteTweens()
        {
            for (int i = 0; i < activeTweeners.Count; i++)
                activeTweeners[i].Complete();
        }

        void ResetColor()
        {
            for (int i = 0; i < renderers.Count; i++)
                renderers[i].material.SetColor("_Color", Color.white);
        }
    }
}
