using System.Collections.Generic;

/// <summary>
/// Classe qui représente un chanson de jeu.
/// </summary>
public class Song
{
    public string SongName { get; private set; }
    public string Description { get; private set; }
    public SongDifficultyType Difficulty { get; private set; }
    public List<LogicalNote> Notes { get; private set; }
    public int Highscore { get; private set; }

    public Song(string name, string description, SongDifficultyType difficulty, List<LogicalNote> notes, int highscore)
    {
        SongName = name;
        Description = description;
        Difficulty = difficulty;
        Notes = notes;
        Highscore = highscore;
    }
}

