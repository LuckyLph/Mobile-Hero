public class Constants
{
    private Constants()
    {

    }

    #region Strings


    public const string MOBILE_HERO_DATABASE_NAME = "MobileHeroDb.db";
    public const string DATABASE_CONNECTION_PATH_PREFIX = "URI=file:";

    public const string DATABASE_CONNECTION_SUCCESS_DEBUG_TEXT = "Connection Bd réussie";
    public const string DATABASE_CONNECTION_FAILURE_DEBUG_TEXT = "Connection Bd échouée";
    public const string SONG_INSERT_SUCCESS_DEBUG_TEXT = "Chanson insérée dans la BD";
    public const string SONG_INSERT_FAILURE_DEBUG_TEXT = "Échec de l'insertion de la chanson dans la BD";
    public const string SONG_SELECT_SUCCESS_DEBUG_TEXT = "Chanson obtenue de la BD";
    public const string SONG_SELECT_FAILURE_DEBUG_TEXT = "Échec de l'obtention de la chanson de la BD";
    public const string SONG_UPDATE_FAILURE_DEBUG_TEXT = "Échec de la mise à jour de la chanson dans la BD";
    public const string SONG_UPDATE_SUCCESS_DEBUG_TEXT = "Chanson de la bd mise à jour";
    public const string NOTE_INSERT_SUCCESS_DEBUG_TEXT = "note insérée dans la BD";
    public const string NOTE_INSERT_FAILURE_DEBUG_TEXT = "Échec de l'insertion de la note dans la BD";
    public const string NOTE_SELECT_SUCCESS_DEBUG_TEXT = "Note obtenue de la BD";
    public const string NOTE_SELECT_FAILURE_DEBUG_TEXT = "Échec de l'obtention de la note de la BD";
    public const string NOTE_UPDATE_FAILURE_DEBUG_TEXT = "Échec de la mise à jour de la note dans la BD";
    public const string NOTE_UPDATE_SUCCESS_DEBUG_TEXT = "Note de la bd mise à jour";

    public const string NO_SONG_SELECTED = "La chanson à charger n'est pas définie";
    public const string LINE_NAME = "Line";
    public const string SPAWN_NAME = "Spawn";
    public const string FIREWORK_SPAWN_NAME = "FireworkSpawn";
    public const string TRANSITION_GUITAR_TEXT = "TransitionGuitar";
    public const string FLAMES_SPAWN_NAME = "FlamesSpawn";
    public const string CANVAS_NAME = "Canvas";
    public const string TEXT_SCORE_NAME = "TextScore";
    public const string TEXT_SCORE_TEXT = "Score: ";
    public const string TEXT_STREAK_NAME = "TextStreak";
    public const string TEXT_TOP_STREAK_NAME = "TextTopStreak";
    public const string TEXT_PERCENT_NAME = "TextPercent";
    public const string TEXT_TOP_STREAK_TEXT = "Highest streak: ";
    public const string TEXT_PERCENT_TEXT = " % Success";
    public const string TEXT_STREAK_TEXT = "x";
    public const string SPLASHSCREEN_TEXT_NAME = "splashscreenText";
    public const string CAMERA_NAME = "Main Camera";
    public const string SPLASHSCREEN_SCENE_NAME = "Splashscreen";
    public const string MENU_SCENE_NAME = "Menu";
    public const string ENDGAME_SCENE_NAME = "EndGame";
    public const string GAME_SCENE_NAME = "MobileHero";
    public const string OBJECT_TAG_NOTE = "Note";
    public const string TOUCH_BEGIN_TEXT = "OnTouchBegin";
    public const string TOUCH_END_TEXT = "OnTouchEnd";
    public const string TOUCH_HOLD_TEXT = "OnTouchHold";

    public const string MENU_SOUNDTRACK_PATH = "Metallica-Master Of Puppets";
    public const string END_GAME_MENU_SOUNDTRACK_PATH = "EndGameSoundtrack";
    public const string PREFAB_PATH = "Prefabs";
    public const string GUITAR_MISS_PATH = "Guitar_miss_";
    public const string GUITAR_MISS1_PATH = "Guitar_miss_1";
    public const string GUITAR_MISS2_PATH = "Guitar_miss_2";
    public const string GUITAR_MISS3_PATH = "Guitar_miss_3";
    public const string GUITAR_MISS4_PATH = "Guitar_miss_4";
    public const string GUITAR_MISS5_PATH = "Guitar_miss_5";
    public const string GUITAR_MISS6_PATH = "Guitar_miss_6";
    public const string GUITAR_MISS7_PATH = "Guitar_miss_7";
    public const string GUITAR_MISS8_PATH = "Guitar_miss_8";
    public const string GUITAR_MISS9_PATH = "Guitar_miss_9";
    public const string GUITAR_TRANSITION_PATH = "GuitarTransition";
    public const string MUSIC_OBJECT_NAME = "Music";
    public const string FIREWORK_OBJECT_NAME = "Fireworks";
    public const string FLAMES_OBJECT_NAME = "BigFlames";
    public const string BACKGROUND_NAME = "Background";
    public const string FIREBALL_NAME = "Fireball";
    public const string NOTE_NAME = "Note";
    public const string SONGNAME_OBJECT_NAME = "SongName";
    public const string GRAPHIC_BUTTON_NAME = "GraphicButton";
    public const string ENDSCREEN_SCORE = "Score: ";
    public const string ENDSCREEN_STREAK = "Top Streak: ";
    public const string ENDSCREEN_PERCENT = "Success: ";

    #endregion

    #region Amounts

    public const int OUT_OF_BOUND_Y = -6;
    public const float IN_RANGE_TOP_Y = -2.5f;
    public const float IN_RANGE_BOTTOM_Y = -4.35f;
    public const float GROWTH_BEFORE_DEATH = 1.5f;
    public const float TIME_TO_GROW = 0.2f;
    public const float SPLASH_SCREEN_ZOOM_TOTAL_GROWTH = 1.35f;
    public const float SPLASHSCREEN_ZOOM_GROWTH_TIME = 2f;
    public const float NOTE_SPEED = 3.5f;
    public const float NOTE_SPEED_PER_DIFFICULTY = 1.5f;
    public const int NOTE_WORTH = 5;
    public const float ADDITIONAL_DELAY_PER_NOTE = 0f;
    public const int SPAWN_FIREWORK_EVERY = 25;
    public const int SPAWN_FLAMES_EVERY = 50;
    public const float VOLUME_MUSIC = 0.7f;
    public const float VOLUME_MUSIC_MISSING = 0.3f;
    public const float OVERALL_DELAY_BEFORE_PLAY = 3.25f;
    public const float WAIT_BEFORE_BACKTOMENU = 3.5f;
    public const int FLAMES_X_ROTATION = -90;
    public const float HIDE_BUTTONS_HIGHER_THAN = 395.0f;

    public const float TIMESCALE = 1f;

    public const int DISTANCE_BETWEEN_BUTTONS = 220;

    public const float MIN_WORLD_X = -2.87f;
    public const float MAX_WORLD_X = 2.87f;
    public const float MIN_WORLD_Y = -5.0f;
    public const float MAX_WORLD_Y = 5f;

    public const int OFFSET_IF_HIGHSCORE = 300;

    public const float ENDSCREEN_SCORE_FOR_E = 0.25f;
    public const float ENDSCREEN_SCORE_FOR_D = 0.4f;
    public const float ENDSCREEN_SCORE_FOR_C = 0.55f;
    public const float ENDSCREEN_SCORE_FOR_B = 0.7f;
    public const float ENDSCREEN_SCORE_FOR_A = 0.85f;

    #endregion

    #region Paths

    public const string SOUNDTRACK_PATH = "Soundtracks";
    public const string SOUND_EFFECTS_PATH = "Sound effects";
    public const string BACKGROUND_PATH = "Backgrounds";
    //public const string PATH_TO_SOUNDTRACKS = "";

    #endregion
}
