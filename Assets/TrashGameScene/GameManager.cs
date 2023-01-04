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


  

    void OnEnable()
    {
        // 追加した変数に値をセット
        Distance = Math.Abs(BeatPoint.position.x - SpawnPoint.position.x);
        During = 2 * 1000;
        isPlaying = false;
        GoIndex = 0;

   

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
    }


    void loadChart()
    {
      
        Notes = new List<GameObject>();
        
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
                Note = Instantiate(_3, SpawnPoint.position, Quaternion.identity); // default don
            }
            else if (type == "4")
            {
                Note = Instantiate(_4, SpawnPoint.position, Quaternion.identity); // default don
            }
            else
            {
                Note = Instantiate(_1, SpawnPoint.position, Quaternion.identity);
            }

            // setParameter関数を発火
            Note.GetComponent<NoteController>().setParameter(type, timing);

            Notes.Add(Note);
        }

       
    }

    void play()
    {
       
        PlayTime = Time.time * 1000;
        isPlaying = true;
    }
}