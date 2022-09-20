using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class HighScoreUI : MonoBehaviour
{
    List<PlayerAchivments> scoreList = new List<PlayerAchivments>();
    List<GameObject> uiElements = new List<GameObject>();

    [SerializeField] int maxCount = 3;
    [SerializeField] string filename;

    [SerializeField] GameObject uiTextPrefab;
    [SerializeField] Transform wrapperElement;

    bool itExists = false;
    // public TMP_Text text;// text2,text3;
    private void Awake()
    {
       wrapperElement= GameObject.FindGameObjectWithTag("ScoreList").transform;
    }
    private void Start()
    {
      //  scoreList.Add(new PlayerAchivments("dick Harper", 100, 100, 100));
      //  scoreList.Add(new PlayerAchivments("josh", 2, 3, 4));
      //  SaveHighScores();
  
        if (File.Exists(JsonHelper.GetPath(filename)))
        {
            // Debug.LogError("File Exists: " + JsonHelper.GetPath(filename));
          
            LoadHighScores();
            for(int i=0; i< scoreList.Count; i++)
            {
                if (scoreList[i].Name == SaveSystem.getUserName())
                {
                    itExists = true;
                }
            }

            if (!itExists)
            {
                PlayerAchivments thisPlayer = new PlayerAchivments(SaveSystem.getUserName(), 0, 0, 0);

                scoreList.Add(thisPlayer);
                SaveHighScores();
                LoadHighScores();
            }
            //Delete Duplicate names from ScoreList
            /*
            List<PlayerAchivments> duplicateScoreList = new List<PlayerAchivments>();
            int i, a;
            for ( i = 0; i == scoreList.Count; i++)
            {
                
                for ( a=0; a== scoreList.Count -1 ;a++)
                {
                    if (scoreList[i].Name == scoreList[a + 1].Name)
                    {
                        scoreList.RemoveAt(i);
                    }
                }
                

            }
              SaveHighScores();
            LoadHighScores();
            */

            //Sort ScoreList
          
           
                      //                scoreList   = scoreList.OrderByDescending(o => o.Score).ToList();
          
          //  SaveHighScores();
          //  LoadHighScores();
            /*   
               for (int i =0; i< scoreList.Count; i++)
               {
                   AddHighScoreIfPossible(scoreList[i]);

               }

               */

          //  updateUI(scoreList);
        }
        if (!File.Exists(JsonHelper.GetPath(filename)))
        {
            Debug.LogError("File path does not exists: " + JsonHelper.GetPath(filename));
           

        }

        SaveSystem.buttonHolder = GameObject.FindGameObjectWithTag("ContinueButton");
        if (SaveSystem.justCreatedNewAccount)
        {
           SaveSystem.buttonHolder= GameObject.FindGameObjectWithTag("ContinueButton");
            SaveSystem.buttonHolder.active = false;
        }

   
        LoadHighScores();

         scoreList= scoreList.OrderByDescending(o => o.Score).ToList();
        
        //  SaveHighScores();
        //  LoadHighScores();
           
         /*  for (int i =0; i< scoreList.Count; i++)
           {
               AddHighScoreIfPossible(scoreList[i]);

           }
         */
           

        updateUI(scoreList);
    }
  

    private void LoadHighScores()
    {
        scoreList = JsonHelper.ReadListFromJSON<PlayerAchivments>(filename);


        while (scoreList.Count > maxCount)
        {
            scoreList.RemoveAt(maxCount);
        }
    }

    private void updateUI(List<PlayerAchivments> list)
    {
        for (int i = 0; i<list.Count; i++)
        {
            PlayerAchivments pA = list[i];

            if (pA.Score > -1)
            {
                if (i >= uiElements.Count)
                {
                    //Instaniate new entry UI 
                    var inst = Instantiate(uiTextPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(wrapperElement,false);

                    uiElements.Add(inst);
                }
                var scoreTexts = uiElements[i].GetComponentsInChildren<TMP_Text>();
                Debug.Log("uiELEMENTS@@@@@@@ : " + uiElements[i].name);
               
                           
                scoreTexts[0].text = scoreList[i].Name;


                scoreTexts[1].text = "Score:" + scoreList[i].Score;

                /*
                scoreTexts[0].text = " Player " + i + " : " + 
                                 scoreList[i].Name + " | Time: " + 
                                 scoreList[i].Time.ToString() + "| Score : "+
                                 scoreList[i].Score.ToString();

                -------------------------------------------------------------------------------

                   uiElements[i].GetComponentInChildren<TMP_Text>().text= " Player " + i + " : " +
                                  scoreList[i].Name + " | Time: " +
                                  scoreList[i].Time.ToString() + "| Score : " +
                                  scoreList[i].Score.ToString();


                */


            }
        }
    }

    private void SaveHighScores()
    {
        JsonHelper.SaveToJSON<PlayerAchivments>(scoreList, filename);
    }

    public void AddHighScoreIfPossible(PlayerAchivments playerAchivments)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= scoreList.Count || playerAchivments.Score > scoreList[i].Score)
            {
                //add new high score
                scoreList.Insert(i, playerAchivments);

                while (scoreList.Count > maxCount)
                {
                    scoreList.RemoveAt(maxCount);
                }

                SaveHighScores();

                break; // Break as no point to go further as the scores will be lower
            }
        }
    }
}
