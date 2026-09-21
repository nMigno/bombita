using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        public AudioManager audioManager;
        public Player pacman;
        public Collider collider;
        public Background background;
        public Background backgroundMenu;
        public Maze maze;
        public LevelExit exit;
        public EnemyManager enemies;
        public GameOver gameOverScreen;
        public UIManager uiManager;
        public MainMenu mainMenuScreen;

        public enum GameState
        {
            start,
            playing,
            victory,
            defeat
        }

        public static GameState CurrentState = GameState.start;

        public static float xf = 100;
        public static float yf = 100;
        public static int ActualCorner = 0;

        public static string levelPath;

        static bool isColliding = false;
        static bool wasColliding = false;

        static string title = "Assets/Text/Title.png";
        static string btnStart = "Assets/Text/BtnStart.png";
        static string btnStartSelect = "Assets/Text/BtnStart_Selected.png";

        static string btnExit = "Assets/Text/BtnExit.png";
        static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";

        int SCREEN_HEIGHT;
        int SCREEN_WIDTH;
        private GameManager(int Width, int Height)
        {
            SCREEN_HEIGHT = Height;
            SCREEN_WIDTH = Width;
            audioManager = new AudioManager();
            mainMenuScreen = new MainMenu(SCREEN_WIDTH);
            gameOverScreen = new GameOver(SCREEN_WIDTH);

            RestartGame();
        }
        public static void Initialize(int width, int height)
        {
            instance = new GameManager(width, height);
        }
        public void Input()
        {
            if (CurrentState == GameState.playing) pacman.Inputs();
        }
        public void Update(float deltaTime)
        {
            if (CurrentState == GameState.playing)
            {
               pacman.Update(deltaTime);
               enemies.Update(deltaTime);

                for (int i = 0; i < maze.WallsInMaze.Count; i++)
                {
                    collider.IsTransformColliding(pacman.transform, maze.WallsInMaze[i].transform);
                }
                for (int i = 0; i < maze.BrickWallsInMaze.Count; i++)
                {
                    collider.IsTransformColliding(pacman.transform, maze.BrickWallsInMaze[i].transform);
                }
                if (pacman.ActiveBomb != null)
                {
                    for (int i = 0; i < pacman.ActiveBomb.explosions.Count; i++)
                    {
                        collider.IsTransformColliding(pacman.transform, pacman.ActiveBomb.explosions[i].transform);
                        for (int j = 0; j < maze.BrickWallsInMaze.Count; j++)
                        {
                            collider.IsTransformColliding(maze.BrickWallsInMaze[j].transform, pacman.ActiveBomb.explosions[i].transform);
                        }
                        for (int j = 0; j < enemies.Enemies.Count; j++)
                        {
                            collider.IsTransformColliding(enemies.Enemies[j].transform, pacman.ActiveBomb.explosions[i].transform);
                        }
                    }

                }


                collider.IsTransformColliding(pacman.transform, exit.Transform);

                for (int i = 0; i < enemies.Enemies.Count; i++)
                {
                    collider.IsTransformColliding(pacman.transform, enemies.Enemies[i].transform);
                }


                if (pacman.Dead)
                {
                    pacman.Lives--;

                    if (pacman.Lives >= 0)
                    {
                        pacman.Respawn();
                    }
                    else
                    {
                        CurrentState = GameState.defeat;
                    }
                }
            }
            else if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                gameOverScreen.Update();
            }
            else if (CurrentState == GameState.start)
            {
                mainMenuScreen.Update();
            }

        }
        public void Render()
        {
            if (CurrentState == GameState.playing)
            {
                background.Render();
                exit.Render();
                pacman.Render();
                maze.Render();
                uiManager.Render();
                enemies.Render();
            }
            else if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                gameOverScreen.Render();
            }
            else if (CurrentState == GameState.start)
            {
                mainMenuScreen.Render();
            }
        }        
        public void RestartGame()
        {
            levelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles", "level0.json");


            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 40.0f);
            enemies = new EnemyManager();
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            backgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            maze = new Maze("DataFiles/level0.json");
            uiManager = new UIManager();


            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;
            collider.OnPlayerExitColision += PlayerExitedLevel;
            collider.OnPlayerColisionWithSomethingThatKillsIt += pacman.Die;
            collider.OnDestroyBrickWall += maze.RemoveBrickWall;
            collider.OnDestroyEnemy += enemies.RemoveEnemy;
            gameOverScreen.OnRestartGame += RestartGame;

            if (CurrentState == GameState.victory ||
                CurrentState == GameState.defeat)
            {
                CurrentState = GameState.playing;
            }
            else
            {
                CurrentState = GameState.start;
            }

        }
        public static void PlayerExitedLevel()
        {
           //if (exit.Opened)
            {
                CurrentState = GameState.victory;
            }
        }
    }
}
