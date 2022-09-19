using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class HighScoreHandler : MonoBehaviour
{
    List<PlayerAchivments> scoreList = new List<PlayerAchivments>();
    [SerializeField] int maxCount = 10;
   [SerializeField] string filename;




    private void LoadHighScores()
    {
        scoreList = JsonHelper.ReadListFromJSON<PlayerAchivments>(filename);

        
        while(scoreList.Count > maxCount)
        {
            scoreList.RemoveAt(maxCount);
        }
    }

    private void SaveHighScores()
    {
        JsonHelper.SaveToJSON<PlayerAchivments>(scoreList, filename);
    }

    public void AddHighScoreIfPossible(PlayerAchivments playerAchivments)
    {
        for(int i=0; i < maxCount; i++)
        {
            if(i>= scoreList.Count || playerAchivments.Score> scoreList[i].Score)
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
