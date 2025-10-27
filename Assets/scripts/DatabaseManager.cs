using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection connection;
    public static DatabaseManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string dbPath = System.IO.Path.Combine(Application.persistentDataPath, "GameScores.db");
            connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            connection.CreateTable<ScoreEntry>();

            Debug.Log("Database created at: " + dbPath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(string playerName, int score)
    {
        var entry = new ScoreEntry
        {
            PlayerName = playerName,
            Score = score
        };
        connection.Insert(entry);
        Debug.Log($"Saved score: {playerName} - {score}");
        Debug.Log("Total entries: " + connection.Table<ScoreEntry>().Count());
    }

    public List<ScoreEntry> GetTopScores(int limit = 10)
    {
        return connection.Table<ScoreEntry>().OrderByDescending(x => x.Score).Take(limit).ToList();
    }
}

public class ScoreEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }
}
