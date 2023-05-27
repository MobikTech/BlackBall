using System;
using System.Collections;
using DG.Tweening;
using Mobik.Common.Core;
using TMPro;
using UnityEngine;

namespace BlackBall
{
    public class StartTimer : MonoBehaviourCached
    {
        [SerializeField] private int _seconds;
        [SerializeField] private TMP_Text _timerText = null!;
        
        private void Awake()
        {
            StartCoroutine(StartTimerCoroutine(() =>
            {
                _timerText.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }));
        }

        private IEnumerator StartTimerCoroutine(Action? callback)
        {
            int secondsLeft = _seconds;
            WaitForSeconds waiter = new WaitForSeconds(1);
            
            while (secondsLeft >= 0)
            {
                _timerText.transform.DOScale(1, 0f);
                _timerText.DOFade(1f, 0f);
                _timerText.text = secondsLeft.ToString();
                _timerText.transform.DOScale(0, 1f).SetEase(Ease.InCubic);
                _timerText.DOFade(0f, 1f).SetEase(Ease.InCubic);

                secondsLeft--;
                yield return waiter;
            }
            
            callback?.Invoke();
        }
    }
}