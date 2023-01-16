using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MessageEffectManager : MonoBehaviour
{

    [SerializeField] GameManager GameManager;
    [SerializeField] GameObject Good;
    [SerializeField] GameObject Failure;

    [SerializeField] Transform BeatPoint;
    public GameObject effectPrefab;
    GameObject effect;
    void OnEnable()
    {

        effect = Instantiate(effectPrefab, BeatPoint.position, Quaternion.identity);
        effect.SetActive(false);
        GameManager
          .OnMessageEffect
          .Where(result => result == "good")
          .Subscribe(result => goodShow());

        GameManager
          .OnMessageEffect
          .Where(result => result == "failure")
          .Subscribe(result => failureShow());
    }

    void goodShow()
    {
        Good.SetActive(false);
        Good.SetActive(true);
        effect.SetActive(false);
        effect.SetActive(true); 

        Observable.Timer(TimeSpan.FromMilliseconds(200))
          .Subscribe(_ => Good.SetActive(false));

        Observable.Timer(TimeSpan.FromMilliseconds(500))
          .Subscribe(_ => effect.SetActive(false));
    }

    void failureShow()
    {
        Failure.SetActive(false);
        Failure.SetActive(true);

        Observable.Timer(TimeSpan.FromMilliseconds(200))
          .Subscribe(_ => Failure.SetActive(false));
    }
}