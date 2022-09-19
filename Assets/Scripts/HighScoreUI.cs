using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    List<PlayerAchivments> scoreList = new List<PlayerAchivments>();
    List<GameObject> uiElements = new List<GameObject>();

    [SerializeField] int maxCount = 3;
    [SerializeField] string filename;

    [SerializeField] GameObject uiTextPrefab;
    [SerializeField] Transform wrapperElement;

    public TMP_Text text;// text2,text3;
    private void Start()
    {
        LoadHighScores();
        updateUI(scoreList);
       /* for (int i = 0; i <= scoreList.Count; i++)
        {
            if (i == 0)
            {
                text.text = " Player " + i + " : " + scoreList[i].Name + " | Time: " + scoreList[i].Time.ToString() + "  ";
            }
            if (i == 1)
            {
                text2.text = " Player " + i + " : " + scoreList[i].Name + " | Time: " + scoreList[i].Time.ToString() + "  ";
            }
            if (i == 2)
            {
                text3.text = " Player " + i + " : " + scoreList[i].Name + " | Time: " + scoreList[i].Time.ToString() + "  ";
            }
        }*/
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

            if (pA.Score > 0)
            {
                if (i >= uiElements.Count)
                {
                    //Instaniate new entry UI 
                    var inst = Instantiate(uiTextPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(wrapperElement);

                    uiElements.Add(inst);
                }
                var scoreTexts = uiElements[i].GetComponentsInChildren<TMP_Text>();
                scoreTexts[i].text = " Player " + i + " : " + 
                    scoreList[i].Name + " | Time: " + 
                    scoreList[i].Time.ToString() + "| Score : "+
                    scoreList[i].Score.ToString();
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
