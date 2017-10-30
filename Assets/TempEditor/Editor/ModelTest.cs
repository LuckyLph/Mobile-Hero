using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class ModelTest
{

    [Test]
    public void TestBD()
    {
        DatabaseMock db = new DatabaseMock();
        Assert.IsTrue(db.GetSong("hi").SongName == "hi");
    }

    [Test]
    public void TestScoreIncrement()
    {
        Player player = new Player();
        player.IncreaseScore(Constants.NOTE_WORTH, /*Distance fictive*/0.5f);
        Assert.IsTrue(player.Score > 0);
    }

    [Test]
    public void TestMaximumScore()
    {
        Player player = new Player();
        for (int i = 0; i < 30; i++)
        {

            player.IncreaseScore(Constants.NOTE_WORTH,/*Distance nulle (frappe parfaite)*/0.0f);
            player.AddStreak();

        }
        Debug.Log(player.GetMaximumScore());
        Assert.AreEqual(player.GetMaximumScore(), player.Score);
    }

    [Test]
    public void TestThing()
    {
        List<LogicalNote> liste = new List<LogicalNote>();
        liste.Add(new LogicalNote(0, 3.2f));
        liste.Add(new LogicalNote(3, 1.2f));
        liste.Add(new LogicalNote(2, 0.45f));
        Song testSong = new Song("Test", "chanson de test", SongDifficultyType.VERY_EASY, liste, 0);
        Assert.AreEqual(testSong.SongName, "Test");
        Assert.AreEqual(testSong.Description, "chanson de test");
        Assert.AreEqual(testSong.Difficulty, SongDifficultyType.VERY_EASY);
        Assert.IsTrue(testSong.Notes.Count == 3);
    }

    [Test]
    public void TestMockDB()
    {

    }

    [Test]
    public void TestStreakIncrement()
    {
        Player player = new Player();
        for (int i = 0; i < 3; i++)
        {
            player.AddStreak();
        }
        Assert.IsTrue(player.Streak == 3);
    }

    [Test]
    public void TestStreakScoreIncrement()
    {
        float currentScore = 0;
        Player player = new Player();
        player.IncreaseScore(Constants.NOTE_WORTH, 0f);
        player.AddStreak();
        Assert.IsTrue(player.Score == Constants.NOTE_WORTH);
        currentScore = player.Score;

        player.IncreaseScore(Constants.NOTE_WORTH, 0f);
        Assert.IsTrue(player.Score == currentScore + 5 + Mathf.Sqrt(player.Streak));
    }

    [Test]
    public void TestStreakReset()
    {
        Player player = new Player();
        player.IncreaseScore(Constants.NOTE_WORTH, 0.5f);
        player.IncreaseScore(Constants.NOTE_WORTH, 0.5f);
        player.ResetStreak();
        Assert.IsTrue(player.Streak == 0);
    }

    [Test]
    public void TestLowestValueNoteIncrement()
    {
        Player player = new Player();
        player.IncreaseScore(Constants.NOTE_WORTH, 1.189f);
        Assert.IsTrue(player.Score == 3);
    }

    [Test]
    public void TestHighestValueNoteIncrement()
    {
        Player player = new Player();
        player.IncreaseScore(Constants.NOTE_WORTH, 0);
        Assert.IsTrue(player.Score == Constants.NOTE_WORTH);
    }
}
