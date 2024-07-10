using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using Object = System.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

public class databaseManager : MonoBehaviour
{
    string createURL = "http://165.232.114.94/api/create";
    string createUserUrl = "http://165.232.114.94/api/createUser";
    string getTopicsUrl = "http://165.232.114.94/api/getTopics";
    string getDailyTopicsUrl = "http://165.232.114.94/api/getDailyTopics";
    string getSentencePackageUrl = "http://165.232.114.94/api/getSentencePackage";
    string createWithColumsURL = "http://165.232.114.94/api/createWithColumns";
    string updateOrInsertURL = "http://165.232.114.94/api/updateOrInsert";
    string updateURL = "http://165.232.114.94/api/updateValue";
    string singleSelectURL = "http://165.232.114.94/api/singleSelect";
    string randomQuestionURL = "http://165.232.114.94/api/randomQuestions";
    string calculateBiasURL = "http://165.232.114.94/api/calculateBias";
    string incrementURL = "http://165.232.114.94/api/increment";
    string getDailyTopicsURL = "http://165.232.114.94/api/dailyTopics";
    string getWordsURL = "http://165.232.114.94/api/getWords";
    string submitSentenceAnswerURL = "http://165.232.114.94/api/submitSentenceAnswer";
    string submitWordAnswerURL = "http://165.232.114.94/api/submitWordAnswer";
    string submitTutorialWordAnswerURL = "http://165.232.114.94/api/submitTutorialWordAnswer";
    string submitSentenceProgressURL = "http://165.232.114.94/api/submitSentenceProgress";
    string submitTopicDailyProgressURL = "http://165.232.114.94/api/submitTopicDailyProgress";
    string insertWrongAnswerURL = "http://165.232.114.94/api/insertWrongAnswer";
    string getWordSentencesURL = "http://165.232.114.94/api/getWordSentences";
    string submitSurveyURL = "http://165.232.114.94/api/submitSurvey";
    string getXPValueURL = "http://165.232.114.94/api/getXPValue";
    string getSkillValueURL = "http://165.232.114.94/api/getSkillValue";
    string updateXPValueURL = "http://165.232.114.94/api/updateXPValue";
    string calculateGlobalSkillURL = "http://165.232.114.94/api/calculateGlobalSkill";

    string submitTutorialURL = "http://165.232.114.94/api/submitTutorial";

    // public string[] wordsAnswer;

    // public string[] wordsIds;
    public List<string> wordsAnswer = new List<string>();
    public List<string> wordsIds = new List<string>();
    public static databaseManager instance;
    private string conn;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    private IDataReader reader;
    public string result = "";

    public string[] questionOnly;
    public string[] sentencesOnly;
    public string[] indicesOnly;

    public string[] resultsOnly;
    public string[] Topics;
    public string[] dailyTopics;
    public string[] refillTopic;
    public int refillTopicNo;
    public string TopicNo;

    public List<string> scorePercentage = new List<string>();
    public List<int> XP = new List<int>();
    public List<double> skill = new List<double>();
    public int gamePlay;
    public int globalXP;
    public double globalSkill;
    void Start()
    {
        gamePlay = PlayerPrefs.GetInt("gamePlayed", gamePlay);

        if (PlayerPrefs.GetInt("gamePlayed") == 0)
        {
            StartCoroutine(createUser("", 0, 0, 0));

            PlayerPrefs.SetInt("gamePlayed", 1);
        }

        StartCoroutine(getXPValue());
        StartCoroutine(getSkillValue());
        StartCoroutine(getTopics());
        StartCoroutine(getDailyTopics());
        // compareWords(new List<string> { "item1", "item2", "item3" }, new List<string> { "item1@     ", "item2", "item3" });
        // StartCoroutine(getWordSentences());
        //PlayerPrefs.SetString("saveTopicRefill",refillTopic[2]="yes");

    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator submitTutorial(string DBQuestionNo, int answer)
    {
        // Debug.Log(DBQuestionNo);
        // Debug.Log(answer);
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("DBQuestionNo", DBQuestionNo);
        form.AddField("answer", answer);
        UnityWebRequest www = UnityWebRequest.Post(submitTutorialURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator submitSurvey(
        string gender,
        string age,
        string levelOfEducation,
        string englishProficiency,
        string thinkingBehaviour,
        string averageNewsCheck,
        string selectedOutlets
    )
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("gender", gender);
        form.AddField("age", age);
        form.AddField("education", levelOfEducation);
        form.AddField("proficiency", englishProficiency);
        form.AddField("behaviour", thinkingBehaviour);
        form.AddField("averageNewsCheck", averageNewsCheck);
        form.AddField("selectedOutlets", selectedOutlets);
        UnityWebRequest www = UnityWebRequest.Post(submitSurveyURL, form);
        yield return www.SendWebRequest();
    }

    public IEnumerator getDailyTopics()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post(getDailyTopicsUrl, form);
        yield return www.SendWebRequest();
        string dailytopicsString = www.downloadHandler.text;
        dailyTopics = dailytopicsString.Split('|');
        dailyTopics = dailyTopics.Take(dailyTopics.Count() - 1).ToArray();
    }
    public IEnumerator getSentencePackage(string topic)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("topic", topic);
        UnityWebRequest www = UnityWebRequest.Post(getSentencePackageUrl, form);
        yield return www.SendWebRequest();
        string sentences = www.downloadHandler.text;
        string[] sentencesArray = sentences.Split('|');
        sentencesArray = sentencesArray.Take(sentencesArray.Count() - 1).ToArray();
        indicesOnly = sentencesArray.Where((x, i) => i % 3 == 0).ToArray();
        sentencesOnly = sentencesArray.Where((x, i) => i % 3 == 1).ToArray();
        resultsOnly = sentencesArray.Where((x, i) => i % 3 == 2).ToArray();
    }
    public IEnumerator getWordSentences()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(getWordSentencesURL, form);
        yield return www.SendWebRequest();
        string sentences = www.downloadHandler.text;
        string[] sentencesArray = sentences.Split('|');
        sentencesArray = sentencesArray.Take(sentencesArray.Count() - 1).ToArray();
        indicesOnly = sentencesArray.Where((x, i) => i % 2 == 0).ToArray();
        sentencesOnly = sentencesArray.Where((x, i) => i % 2 == 1).ToArray();
    }
    public IEnumerator submitSentenceAnswer(
        string sentence_id,
        int annotation,
        int answer,
        double skill
    )
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("sentence_id", sentence_id);
        form.AddField("annotaion", annotation);
        form.AddField("answer", answer);
        form.AddField("skill", skill.ToString());
        UnityWebRequest www = UnityWebRequest.Post(submitSentenceAnswerURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator submitWordAnswer(string word_id, int annotation, int answer)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("word_id", word_id);
        form.AddField("annotaion", annotation);
        form.AddField("answer", answer);
        UnityWebRequest www = UnityWebRequest.Post(submitWordAnswerURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator submitTutorialWordAnswer(string word, int annotation, int answer)
    {

        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("word", word);
        form.AddField("annotation", annotation);
        form.AddField("answer", answer);
        UnityWebRequest www = UnityWebRequest.Post(submitTutorialWordAnswerURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator insertWrongAnswer(string word)
    {
        WWWForm form = new WWWForm();
        form.AddField("word", word);
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post(insertWrongAnswerURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator submitSentenceProgress(string sentence_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("sentence_id", sentence_id);
        UnityWebRequest www = UnityWebRequest.Post(submitSentenceProgressURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator submitTopicDailyProgress(string topic_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("topic_id", topic_id);
        UnityWebRequest www = UnityWebRequest.Post(submitTopicDailyProgressURL, form);
        yield return www.SendWebRequest();
    }

    public IEnumerator create(string tableName, Object[] values)
    {
        // Debug.Log(id);
        // WWWForm form = new WWWForm();
        // form.AddField("id", id);

        var data = new object[] { new { tablename = tableName }, new { values = values }, };
        var jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = UnityWebRequest.Put(createURL, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // UnityWebRequest www = UnityWebRequest.Post(URL, form);
        // yield return www.SendWebRequest();
    }
    public IEnumerator createWithColumns(string tableName, string[] cols, Object[] values)
    {
        // Debug.Log(id);
        // WWWForm form = new WWWForm();
        // form.AddField("id", id);

        var data = new object[]
        {
            new { tablename = tableName },
            new { cols = cols },
            new { values = values },
        };
        var jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = UnityWebRequest.Put(createWithColumsURL, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // UnityWebRequest www = UnityWebRequest.Post(URL, form);
        // yield return www.SendWebRequest();
    }
    public IEnumerator updateOrInsert(
        string tableName,
        string[] uniqueCols,
        string changedValue,
        Object[] values
    )
    {
        // Debug.Log(id);
        // WWWForm form = new WWWForm();
        // form.AddField("id", id);

        var data = new object[]
        {
            new { tablename = tableName },
            new { uniqueCols = uniqueCols },
            new { changedValue = changedValue },
            new { values = values },
        };
        var jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = UnityWebRequest.Put(updateOrInsertURL, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // UnityWebRequest www = UnityWebRequest.Post(URL, form);
        // yield return www.SendWebRequest();
    }

    //insert into table
    // public void InsertInto(string tableName, Object[] values)
    // { // basic Insert with just values
    //     openConnection();
    //     string query;

    //     query = "INSERT INTO " + tableName + " VALUES (";

    //     for (var i = 0; i < values.Length; i++)
    //     {
    //         if (i == 0 && values[i].GetType().Equals(typeof(Int32)))
    //         {
    //             query += "" + values[i];
    //         }
    //         else if (i == 0 && values[i].GetType().Equals(typeof(String)))
    //         {
    //             query += "'" + values[i] + "'";
    //         }
    //         else if (i != 0 && values[i].GetType().Equals(typeof(Int32)))
    //         {
    //             query += "," + values[i];
    //         }
    //         else
    //         {
    //             query += ", '" + values[i] + "'";
    //         }
    //     }
    //     query += ")";
    //     dbcmd = dbconn.CreateCommand();
    //     dbcmd.CommandText = query;
    //     reader = dbcmd.ExecuteReader();
    //     closeConnection();
    // }

    public IEnumerator updateValue(
        string tableName,
        string col,
        Object[] values,
        string condition,
        string conditionValue
    )
    { // Specific insert with col and values
        var data = new object[]
        {
            new { tablename = tableName },
            new { col = col },
            new { values = values },
            new { condition = condition },
            new { conditionValue = conditionValue },
        };
        var jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = UnityWebRequest.Put(updateURL, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // openConnection();
        // string query;
        // query = "UPDATE  " + tableName + " SET ";
        // for (var i = 0; i < values.Length; i++)
        // {
        //     if (i == 0 && values[i].GetType().Equals(typeof(Int32)))
        //     {
        //         query += col[0] + " = " + values[i];
        //     }
        //     else if (i == 0 && values[i].GetType().Equals(typeof(String)))
        //     {
        //         query += col[0] + " = " + "'" + values[i] + "'";
        //     }
        //     else if (i != 0 && values[i].GetType().Equals(typeof(Int32)))
        //     {
        //         query += " , " + col[0] + " = " + values[i];
        //     }
        //     else
        //     {
        //         query += " , " + col[0] + " = " + ", '" + values[i] + "'";
        //     }
        // }
        // query += " WHERE " + condition + " = '" + conditionValue + "'";
        // dbcmd = dbconn.CreateCommand();
        // dbcmd.CommandText = query;
        // reader = dbcmd.ExecuteReader();
        // closeConnection();
    }

    public IEnumerator singleselect(
        string tableName,
        String col,
        string condition,
        string conditionValue
    )
    {
        // Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("tableName", tableName);
        form.AddField("col", col);
        form.AddField("condition", condition);
        form.AddField("conditionValue", conditionValue);

        UnityWebRequest www = UnityWebRequest.Post(singleSelectURL, form);
        yield return www.SendWebRequest();
        result = www.downloadHandler.text;
        // Selects a single Item
        // openConnection();
        // string query;
        // query = "SELECT " + col + " FROM " + tableName + " WHERE " + condition + " = '" + conditionValue + "'";
        // dbcmd = dbconn.CreateCommand();
        // dbcmd.CommandText = query;
        // reader = dbcmd.ExecuteReader();
        // string result = "";

        // while (reader.Read())
        // {
        //     result = reader.GetInt32(0).ToString(); // Fill array with all matches


        // }
        // closeConnection();
        // return result;
    }
    public IEnumerator createUser(
        string user_id,
        double global_skill,
        int global_XP,
        int game_finished
    )
    {
        WWWForm form = new WWWForm();
        form.AddField("id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("global_skill", global_skill.ToString());
        form.AddField("global_XP", global_XP);
        form.AddField("game_finished", game_finished);

        UnityWebRequest www = UnityWebRequest.Post(createUserUrl, form);
        yield return www.SendWebRequest();
        result = www.downloadHandler.text;
        // Selects a single Item
        // openConnection();
        // string query;
        // query = "SELECT " + col + " FROM " + tableName + " WHERE " + condition + " = '" + conditionValue + "'";
        // dbcmd = dbconn.CreateCommand();
        // dbcmd.CommandText = query;
        // reader = dbcmd.ExecuteReader();
        // string result = "";

        // while (reader.Read())
        // {
        //     result = reader.GetInt32(0).ToString(); // Fill array with all matches


        // }
        // closeConnection();
        // return result;
    }

    public IEnumerator getXPValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post(getXPValueURL, form);
        yield return www.SendWebRequest();
        globalXP = Convert.ToInt32(www.downloadHandler.text);
    }
    public IEnumerator getSkillValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post(getSkillValueURL, form);
        yield return www.SendWebRequest();
        globalSkill = Convert.ToDouble(www.downloadHandler.text);
        if (guiManager.Instance.gameType == guiManager.GameType.gameStart)
        {
            inAppEarning.Instance.homeGlobalSkillsSlider.value = (float)databaseManager.instance.globalSkill;
        }
        else
        {
            inAppEarning.Instance.homeGlobalSkillsSlider.value = 0;
        }
    }
    public IEnumerator updateXPValue(int xp)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("xp", xp);
        UnityWebRequest www = UnityWebRequest.Post(updateXPValueURL, form);
        yield return www.SendWebRequest();
    }
    public IEnumerator calculateGlobalSkill()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post(calculateGlobalSkillURL, form);
        yield return www.SendWebRequest();
    }

    // public Dictionary<int, object[]> generateQuestionsArray()
    // {
    //     openConnection();
    //     string query;
    //     query = "SELECT id , Sentence FROM Sentences ORDER BY RANDOM() LIMIT 10";
    //     dbcmd = dbconn.CreateCommand();
    //     dbcmd.CommandText = query;
    //     reader = dbcmd.ExecuteReader();
    //     Dictionary<int, object[]> questions = new Dictionary<int, object[]>();
    //     int i = 0;
    //     while (reader.Read())
    //     {
    //         questions.Add(i, new object[] { reader.GetInt32(0), reader.GetString(1) });
    //         i++;


    //     }
    //     closeConnection();
    //     return questions;
    // }

    public IEnumerator generateQuestions()
    {
        // Debug.Log(id);

        UnityWebRequest www = UnityWebRequest.Get(randomQuestionURL);
        yield return www.SendWebRequest();
        string questions = www.downloadHandler.text;
        string[] questionsArray = questions.Split('|');
        questionsArray = questionsArray.Take(questionsArray.Count() - 1).ToArray();
        questionOnly = questionsArray.Where((x, i) => i % 2 == 0).ToArray();
        indicesOnly = questionsArray.Where((x, i) => i % 2 != 0).ToArray();
        // foreach (string q in questionOnly)
        // {
        //     Debug.Log(q);
        // }

        // foreach (string i in indicesOnly)
        // {
        //     Debug.Log(i);
        // }
    }

    public IEnumerator increment(
        string tableName,
        String col,
        string condition,
        string conditionValue
    )
    {
        // Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("tableName", tableName);
        form.AddField("col", col);
        form.AddField("condition", condition);
        form.AddField("conditionValue", conditionValue);
        UnityWebRequest www = UnityWebRequest.Post(incrementURL, form);
        yield return www.SendWebRequest();
        // Selects a single Item
        // openConnection();
        // string query;
        // query = "SELECT " + col + " FROM " + tableName + " WHERE " + condition + " = '" + conditionValue + "'";
        // dbcmd = dbconn.CreateCommand();
        // dbcmd.CommandText = query;
        // reader = dbcmd.ExecuteReader();
        // string result = "";

        // while (reader.Read())
        // {
        //     result = reader.GetInt32(0).ToString(); // Fill array with all matches


        // }
        // closeConnection();
        // return result;
    }

    public IEnumerator calculateBias(string sentenceId)
    {
        // Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("sentenceId", sentenceId);
        UnityWebRequest www = UnityWebRequest.Post(calculateBiasURL, form);
        yield return www.SendWebRequest();
        // Selects a single Item
        // openConnection();
        // string query;
        // query = "SELECT " + col + " FROM " + tableName + " WHERE " + condition + " = '" + conditionValue + "'";
        // dbcmd = dbconn.CreateCommand();
        // dbcmd.CommandText = query;
        // reader = dbcmd.ExecuteReader();
        // string result = "";

        // while (reader.Read())
        // {
        //     result = reader.GetInt32(0).ToString(); // Fill array with all matches


        // }
        // closeConnection();
        // return result;
    }

    // void openConnection()
    // {
    //     conn = "URI=file:" + Application.dataPath + "/game.db"; //Path to database.
    //     dbconn = (IDbConnection)new SqliteConnection(conn);
    //     dbconn.Open(); //Open connection to the database.
    // }

    // void closeConnection()
    // {
    //     reader.Close();
    //     reader = null;
    //     dbcmd.Dispose();
    //     dbcmd = null;
    //     dbconn.Close();
    //     dbconn = null;
    // }
    public IEnumerator getTopics()
    {
        UnityWebRequest www = UnityWebRequest.Get(getTopicsUrl);
        yield return www.SendWebRequest();
        string topics = www.downloadHandler.text;
        string[] topicsArray = topics.Split('|');
        Topics = topicsArray.Take(topicsArray.Count() - 1).ToArray();
    }
    // public IEnumerator getDailyTopics()
    // {

    //     UnityWebRequest www = UnityWebRequest.Get(getDailyTopicsURL);
    //     yield return www.SendWebRequest();
    //     string questions = www.downloadHandler.text;
    //     string[] questionsArray = questions.Split('|');
    //     Topics = questionsArray.Take(questionsArray.Count() - 1).ToArray();

    // }
    public IEnumerator getWords(string sentenceId)
    {
        WWWForm form = new WWWForm();
        form.AddField("sentenceId", sentenceId);
        UnityWebRequest www = UnityWebRequest.Post(getWordsURL, form);
        yield return www.SendWebRequest();
        string Answers = www.downloadHandler.text;
        List<string> wordsList = Answers.Split('|').ToList();
        wordsList = wordsList.Take(wordsList.Count() - 1).ToList();
        wordsAnswer = wordsList.Where((x, i) => i % 2 == 0).ToList();
        wordsIds = wordsList.Where((x, i) => i % 2 == 1).ToList();
    }

    public double calculateWordScore(List<string> words, List<string> answers)
    {
        answers = answers.Select(x => wordCleaner(x)).ToList();

        //if no answers submitted
        if (answers.Count == 0 && words.Count != 0)
        {
            return 0;
            //if there is no biased words
        }
        else if (words.Count == 0)
        {
            return 0.5;
        }
        else
        {
            var wrongAnswers = answers.Except(words).ToList();
            // wrongAnswers.ForEach((value) => Debug.Log(value));
            var rightAnswers = answers.Intersect(words).ToList();
            // rightAnswers.ForEach((value) => Debug.Log(value));
            double factor = rightAnswers.Count * 2 + wrongAnswers.Count;
            double percentage = (rightAnswers.Count * 2) / factor;

            return percentage * 0.5;
        }
    }
    public int star(double sentenceScore)
    {
        if (
            guiManager.Instance.gameMode == guiManager.GameMode.breakingnewsMode
            || guiManager.Instance.gameMode == guiManager.GameMode.sentenceMode
        )
        {
            if (sentenceScore == 0.5)
            {
                XP.Add(20);
                skill.Add(1);
                scorePercentage.Add("100%");
                uiUpdated.Instance.rightAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    true
                );
                uiUpdated.Instance.wrongAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    false
                );
                uiUpdated.Instance.showPercentageInResult[
                    QuestionsManager.Instance.questionNo
                ].text = scorePercentage[QuestionsManager.Instance.questionNo];
                return 1;
            }
            else if (sentenceScore == 0)
            {
                XP.Add(0);
                skill.Add(0);
                scorePercentage.Add("0%");
                uiUpdated.Instance.rightAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    false
                );
                uiUpdated.Instance.wrongAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    true
                );
                uiUpdated.Instance.showPercentageInResult[
                    QuestionsManager.Instance.questionNo
                ].text = scorePercentage[QuestionsManager.Instance.questionNo];
                return 0;
            }
        }
        // else if (guiManager.Instance.gameMode == guiManager.GameMode.breakingnewsMode || guiManager.Instance.gameMode == guiManager.GameMode.sentenceMode && sentenceScore == 0)
        // {
        //     scorePercentage.Add("0%");
        //     uiUpdated.Instance.rightAnsSign[QuestionsManager.Instance.questionNo].SetActive(false);
        //     uiUpdated.Instance.wrongAnsSign[QuestionsManager.Instance.questionNo].SetActive(true);
        //     uiUpdated.Instance.showPercentageInResult[QuestionsManager.Instance.questionNo].text = scorePercentage[QuestionsManager.Instance.questionNo];
        //     return 0;
        // }
        else if (guiManager.Instance.gameMode == guiManager.GameMode.publishSentenceMode)
        {
            double wordScore = calculateWordScore(
                wordsAnswer,
                oneWordManager.Instance.selectedWords
            );
            // Debug.Log(wordScore);
            double totalScore = wordScore + sentenceScore;

            skill.Add(totalScore);
            XP.Add(Convert.ToInt32(totalScore * 20));
            scorePercentage.Add(Math.Round(totalScore * 100) + "%");
            if (totalScore >= 0.7)
            {
                uiUpdated.Instance.rightAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    true
                );
                uiUpdated.Instance.wrongAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    false
                );
            }
            else
            {
                uiUpdated.Instance.rightAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    false
                );
                uiUpdated.Instance.wrongAnsSign[QuestionsManager.Instance.questionNo].SetActive(
                    true
                );
            }
            uiUpdated.Instance.showPercentageInResult[QuestionsManager.Instance.questionNo].text =
                scorePercentage[QuestionsManager.Instance.questionNo];
            return totalScore >= 0.7 ? 1 : 0;
        }
        // }else if

        // if (sentenceScore + wordScore > 0.7)
        // {
        //     soundManager.Instance.starCollectSound();
        //     return 1;
        // }
        return 0;
    }

    public string wordCleaner(string word)
    {
        string pattern = @"([\w\-]+)";
        var match = Regex.Match(word, pattern);
        return match.Groups[1].Value;
    }

    public void compareWords(List<string> words, List<string> answers)
    {
        answers = answers.Select(x => wordCleaner(x)).ToList();

        // answers.ForEach((value) => Debug.Log(value));
        // words.ForEach((value) => Debug.Log(value));

        for (int i = 0; i < words.Count; i++)
        {
            if (answers.Contains(words[i]))
            {
                StartCoroutine(submitWordAnswer(wordsIds[i], 1, 1));
            }
            else
            {
                StartCoroutine(submitWordAnswer(wordsIds[i], 0, 0));
            }
        }

        var wrongAnswers = answers.Except(words).ToList();
        wrongAnswers.ForEach((word) => StartCoroutine(insertWrongAnswer(word)));
        // if (correctAnswers.Length != 0)
        // {
        //     for (int i = 0; i < correctAnswers.Length; i++)
        //     {
        //         Debug.Log();
        //         startCoroutine(submitWordAnswer(wordsIds[Array.IndexOf(words, wordCleaner(answer))], 1, 1));
        //     }
        // }
        // if (AnsweredWrong.Length != 0)
        // {

        // }
        // if (notAnswered.Length != 0)
        // {

        // }

    }
    public void compareWordsTutorial(List<string> words, List<string> answers)
    {
        answers = answers.Select(x => wordCleaner(x)).ToList();

        // answers.ForEach((value) => Debug.Log(value));
        // words.ForEach((value) => Debug.Log(value));

        for (int i = 0; i < words.Count; i++)
        {
            if (answers.Contains(words[i]))
            {
                StartCoroutine(submitTutorialWordAnswer(words[i], 1, 1));
            }
            else
            {
                StartCoroutine(submitTutorialWordAnswer(words[i], 0, 0));
            }
        }

        var wrongAnswers = answers.Except(words).ToList();
        wrongAnswers.ForEach((word) => StartCoroutine(submitTutorialWordAnswer(word, 1, 0)));
        // if (correctAnswers.Length != 0)
        // {
        //     for (int i = 0; i < correctAnswers.Length; i++)
        //     {
        //         Debug.Log();
        //         startCoroutine(submitWordAnswer(wordsIds[Array.IndexOf(words, wordCleaner(answer))], 1, 1));
        //     }
        // }
        // if (AnsweredWrong.Length != 0)
        // {

        // }
        // if (notAnswered.Length != 0)
        // {

        // }

    }

    public void insertNonSelectedWords()
    {
        var answers = oneWordManager.Instance.selectedWords.Select(x => wordCleaner(x)).ToList();

        // nonSelectedWords.ForEach((word) => Debug.Log(word));
        // nonSelectedWords.ForEach((word) => Debug.Log(word));
        // answers.ForEach((value) => Debug.Log(value));
        // words.ForEach((value) => Debug.Log(value));

        for (int i = 0; i < wordsAnswer.Count; i++)
        {
            if (!answers.Contains(wordsAnswer[i]))
            {
                if (guiManager.Instance.gameType == guiManager.GameType.tutorial)
                {
                    StartCoroutine(databaseManager.instance.submitTutorialWordAnswer(wordsAnswer[i], 0, 0));
                }
                else
                {
                    StartCoroutine(submitWordAnswer(wordsIds[i], 0, 0));
                }

            }
        }
    }
}
