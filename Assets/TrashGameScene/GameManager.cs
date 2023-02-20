using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Diagnostics;
using Random = System.Random;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] string FilePath;
    [SerializeField] string ClipPath_BGM; //　追加
    [SerializeField] string ClipPath_SE; //　追加

    [SerializeField] EffectManager effectManager;
    [SerializeField] SlideManager slideManager;

    [SerializeField] GameObject _1;
    [SerializeField] GameObject _2;
    [SerializeField] GameObject _3;
    [SerializeField] GameObject _4;


    [SerializeField] GameObject bagBW;



    [SerializeField] Sprite BW_1;
    [SerializeField] Sprite BW_2;
    [SerializeField] Sprite BW_3;

    [SerializeField] Sprite NBW_1;
    [SerializeField] Sprite NBW_2;
    [SerializeField] Sprite NBW_3;

    [SerializeField] Sprite GCP_1;
    [SerializeField] Sprite GCP_2;
    [SerializeField] Sprite GCP_3;
    [SerializeField] Sprite GCP_4;

    [SerializeField] Sprite OW_1;
    [SerializeField] Sprite OW_2;

    List<Sprite> BW;
    List<Sprite> NBW;
    List<Sprite> GCP;
    List<Sprite> OW;



    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform BeatPoint;

    [SerializeField] Button Play;
    [SerializeField] Button SetChart;


    AudioSource Music;
    AudioSource SE;


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

    public bool touch_BW;
    public bool touch_NBW;
    public bool touch_GCP;
    public bool touch_OW;

    private float end_time;
    // イベントを通知するサブジェクトを追加
    Subject<string> MessageEffectSubject = new Subject<string>();

    // イベントを検知するオブザーバーを追加
    public IObservable<string> OnMessageEffect
    {
        get { return MessageEffectSubject; }
    }

    bool one = true;

    void OnEnable()
    {

        Music = this.GetComponent<AudioSource>(); // 追加
        //SE = this.GetComponent<AudioSource>(); // 追加

        Distance = Math.Abs(BeatPoint.position.x - SpawnPoint.position.x);
        During = 2 * 1000;
        isPlaying = false;
        GoIndex = 0;

        CheckRange = 220;
        BeatRange = 120;

        BW = new List<Sprite>();
        BW.Add(BW_1);
        BW.Add(BW_2);
        BW.Add(BW_3);

        NBW = new List<Sprite>();
        NBW.Add(NBW_1);
        NBW.Add(NBW_2);
        NBW.Add(NBW_3);

        GCP = new List<Sprite>();
        GCP.Add(GCP_1);
        GCP.Add(GCP_2);
        GCP.Add(GCP_3);
        GCP.Add(GCP_4);

        OW = new List<Sprite>();
        OW.Add(OW_1);
        OW.Add(OW_2);


        if (one = true)
        {
            loadChart();
            one = false;
            touch_BW = false;
            touch_NBW = false;
            touch_GCP = false;
            touch_OW = false;
        }





        touch_BW = false;


        this.UpdateAsObservable()
          .Where(_ => isPlaying)
          .Where(_ => Notes.Count > GoIndex)
          .Where(_ => Notes[GoIndex].GetComponent<NoteController>().getTiming() <= ((Time.time * 1000 - PlayTime) + During))
          .Subscribe(_ => {
              Notes[GoIndex].GetComponent<NoteController>().go(Distance, During);
              GoIndex++;
          });



        this.UpdateAsObservable()
            .Where(_ => isPlaying)
            .Where(_ => Input.GetKeyDown(KeyCode.V))
            .Subscribe(_ => {
                beat("1", Time.time * 1000 - PlayTime);
                effectManager.setboolBW();
            });


        this.UpdateAsObservable()
          .Where(_ => isPlaying)
          .Where(_ => Input.GetKeyDown(KeyCode.B))
          .Subscribe(_ => {
              beat("2", Time.time * 1000 - PlayTime);
              effectManager.setboolNBW();
          });

        this.UpdateAsObservable()
            .Where(_ => isPlaying)
            .Where(_ => Input.GetKeyDown(KeyCode.N))
            .Subscribe(_ => {
                beat("3", Time.time * 1000 - PlayTime);
                effectManager.setboolGCP();
            });

        this.UpdateAsObservable()
           .Where(_ => isPlaying)
           .Where(_ => Input.GetKeyDown(KeyCode.M))
           .Subscribe(_ => {
               beat("4", Time.time * 1000 - PlayTime);
               effectManager.setboolOW();
           });

        this.UpdateAsObservable()
                  .Where(_ => touch_BW)
                  // .Where(_ => Input.GetKeyDown(KeyCode.V))
                  .Subscribe(_ => {
                      beat("1", Time.time * 1000 - PlayTime);
                      effectManager.setboolBW();
                      touch_BW = false;
                  });

        this.UpdateAsObservable()
                  .Where(_ => touch_NBW)
                  // .Where(_ => Input.GetKeyDown(KeyCode.V))
                  .Subscribe(_ => {
                      beat("2", Time.time * 1000 - PlayTime);
                      effectManager.setboolNBW();
                      touch_NBW = false;
                  });
        this.UpdateAsObservable()
                  .Where(_ => touch_GCP)
                  // .Where(_ => Input.GetKeyDown(KeyCode.V))
                  .Subscribe(_ => {
                      beat("3", Time.time * 1000 - PlayTime);
                      effectManager.setboolGCP();
                      touch_GCP = false;

                  });

        this.UpdateAsObservable()
                  .Where(_ => touch_OW)
                  // .Where(_ => Input.GetKeyDown(KeyCode.V))
                  .Subscribe(_ => {
                      beat("4", Time.time * 1000 - PlayTime);
                      effectManager.setboolOW();
                      touch_OW = false;
                  });

        this.UpdateAsObservable()
                  .Where(_ => isPlaying)
                  .Subscribe(_ => {
                      end_time += Time.deltaTime;
                      Debug.Log(end_time);
                      if (end_time > 45.0f)
                      {
                          //  Debug.Log("ddddddddddddd");
                          //Scene
                          SceneManager.LoadScene("Mor_2_Story");
                      }
                  });





    }

    public Image image;

    void loadChart()
    {
        Music.clip = (AudioClip)Resources.Load(ClipPath_BGM); // 追加
                                                              // SE.clip = (AudioClip)Resources.Load(ClipPath_SE); // 追加

        Notes = new List<GameObject>();
        NoteTimings = new List<float>(); // 追加

        string jsonText = Resources.Load<TextAsset>(FilePath).ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        Title = json["title"].Get<string>();
        BPM = int.Parse(json["bpm"].Get<string>());

        foreach (var note in json["notes"])
        {
            string type = note["type"].Get<string>();
            float timing = float.Parse(note["timing"].Get<string>());

            Random myObject = new Random();
            int ranNum = myObject.Next(1, 100);

            GameObject Note;
            if (type == "1")
            {
                Note = Instantiate(_1, SpawnPoint.position, Quaternion.identity);
                Note.GetComponent<SpriteRenderer>().sprite = BW[ranNum % BW.Count];

            }
            else if (type == "2")
            {
                Note = Instantiate(_2, SpawnPoint.position, Quaternion.identity);
                Note.GetComponent<SpriteRenderer>().sprite = NBW[ranNum % NBW.Count];
            }
            else if (type == "3")
            {
                Note = Instantiate(_3, SpawnPoint.position, Quaternion.identity);
                Note.GetComponent<SpriteRenderer>().sprite = GCP[ranNum % GCP.Count];
            }
            else if (type == "4")
            {
                Note = Instantiate(_4, SpawnPoint.position, Quaternion.identity);
                Note.GetComponent<SpriteRenderer>().sprite = OW[ranNum % OW.Count];
            }
            else
            {
                Note = Instantiate(_1, BeatPoint.position, Quaternion.identity);
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
                                                     // SE.Play();
                                                     //スコア加算
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

    public void play()
    {
        Music.Stop(); // 追加
        Music.Play(); // 追加
        PlayTime = Time.time * 1000;
        isPlaying = true;
        end_time = 0.0f;
    }
}