using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{

    [SerializeField] string FilePath;


    [SerializeField] GameObject _1;
    [SerializeField] GameObject _2;
    [SerializeField] GameObject _3;
    [SerializeField] GameObject _4;

    [SerializeField] Button button_1;
    [SerializeField] Button button_2;
    [SerializeField] Button button_3;
    [SerializeField] Button button_4;


    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform BeatPoint;

    [SerializeField] Button Play;
    [SerializeField] Button SetChart;

    string Title;
    int BPM;
    List<GameObject> Notes;

    float PlayTime;
    float Distance;
    float During;
    bool isPlaying;
    int GoIndex;

    float CheckRange;
    float BeatRange;
    List<float> NoteTimings; // 追加

    // イベントを通知するサブジェクトを追加
    Subject<string> MessageEffectSubject = new Subject<string>();

    // イベントを検知するオブザーバーを追加
    public IObservable<string> OnMessageEffect
    {
        get { return MessageEffectSubject; }
    }



    void OnEnable()
    {
        // 追加した変数に値をセット
        Distance = Math.Abs(BeatPoint.position.x - SpawnPoint.position.x);
        During = 2 * 1000;
        isPlaying = false;
        GoIndex = 0;

        CheckRange = 220; // 追加
        BeatRange = 120; // 追加


        Play.onClick
          .AsObservable()
          .Subscribe(_ => play());

        SetChart.onClick
          .AsObservable()
          .Subscribe(_ => loadChart());

        // ノーツを発射するタイミングかチェックし、go関数を発火
        this.UpdateAsObservable()
          .Where(_ => isPlaying)
          .Where(_ => Notes.Count > GoIndex)
          .Where(_ => Notes[GoIndex].GetComponent<NoteController>().getTiming() <= ((Time.time * 1000 - PlayTime) + During))
          .Subscribe(_ => {
              Notes[GoIndex].GetComponent<NoteController>().go(Distance, During);
              GoIndex++;
          });

      
        // 追加
           button_1.onClick
          .AsObservable()
          .Where(_ => isPlaying)
          .Subscribe(_ => {
              beat("1", Time.time * 1000 - PlayTime);
          });
        // 追加
           button_2.onClick
           .AsObservable()
           .Where(_ => isPlaying)
           .Subscribe(_ => {
           beat("2", Time.time * 1000 - PlayTime);
       });
        // 追加
           button_3.onClick
           .AsObservable()
           .Where(_ => isPlaying)
           .Subscribe(_ => {
              beat("3", Time.time * 1000 - PlayTime);
           });
        // 追加
            button_4.onClick
           .AsObservable()
           .Where(_ => isPlaying)
           .Subscribe(_ => {
           beat("4", Time.time * 1000 - PlayTime);
       });
    }


    void loadChart()
    {
      
        Notes = new List<GameObject>();
        NoteTimings = new List<float>(); // 追加

        string jsonText = Resources.Load<TextAsset>(FilePath).ToString();
    
        JsonNode json    =    JsonNode.Parse(jsonText);

        Title = json["title"].Get<string>();
        BPM = int.Parse(json["bpm"].Get<string>());

        foreach (var note in json["notes"])
        {
            string type = note["type"].Get<string>();
            float timing = float.Parse(note["timing"].Get<string>());

            GameObject Note;
            if (type == "1")
            {
                Note = Instantiate(_1, SpawnPoint.position, Quaternion.identity);
            }
            else if (type == "2")
            {
                Note = Instantiate(_2, SpawnPoint.position, Quaternion.identity);
            }
            else if (type == "3")
            {
                Note = Instantiate(_3, SpawnPoint.position, Quaternion.identity); 
            }
            else if (type == "4")
            {
                Note = Instantiate(_4, SpawnPoint.position, Quaternion.identity);
            }
            else
            {
                Note = Instantiate(_1, SpawnPoint.position, Quaternion.identity);
            }

            // setParameter関数を発火
            Note.GetComponent<NoteController>().setParameter(type, timing);

            Notes.Add(Note);
            NoteTimings.Add(timing); // 追加
        }

       
    }

    // 追加
    void beat(string type, float timing)
    {
        float minDiff = -1;
        int minDiffIndex = -1;

        for (int i = 0; i < NoteTimings.Count; i++)
        {
            if (NoteTimings[i] > 0)
            {
                float diff = Math.Abs(NoteTimings[i] - timing);
                if (minDiff == -1 || minDiff > diff)
                {
                    minDiff = diff;
                    minDiffIndex = i;
                }
            }
        }

        if (minDiff != -1 & minDiff < CheckRange)
        {
            if (minDiff < BeatRange & Notes[minDiffIndex].GetComponent<NoteController>().getType() == type)
            {
                NoteTimings[minDiffIndex] = -1;
                Notes[minDiffIndex].SetActive(false);
                MessageEffectSubject.OnNext("good"); // イベントを通知
            }
            else
            {
                NoteTimings[minDiffIndex] = -1;
                Notes[minDiffIndex].SetActive(false);
                MessageEffectSubject.OnNext("failure"); // イベントを通知
            }
        }
        else
        {
            //Debug.Log("through");
        }
    }

    void play()
    {
       
        PlayTime = Time.time * 1000;
        isPlaying = true;
    }
}