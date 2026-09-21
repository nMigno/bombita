using EngineGDI.DataFiles;
using System;
using System.Drawing;
using System.Media;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static EngineGDI.Player;

namespace EngineGDI
{
    public static class Program
    {
        // mostrar debug
        public static bool showDebug = true;
        public static string currentMsg = "";

        public static int SCREEN_WIDTH = 1560;
        public static int SCREEN_HEIGHT = 880;

        public static AudioManager audioManager = new AudioManager();
        public static Player pacman;
        public static Enemy enemy;
        public static Collider collider;
        public static Background background;
        public static Background backgroundMenu;
        public static Maze maze;
        public static LevelExit exit;
        public static GameOver gameOverScreen;
        public static UIManager uiManager;

        public enum GameState
        {
            start,
            playing,
            victory,
            defeat
        }

        public static GameState CurrentState = GameState.playing;

        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        static bool isColliding = false;
        static bool wasColliding = false;

        static string title = "Assets/Text/Title.png";
        static string btnStart = "Assets/Text/BtnStart.png";
        static string btnStartSelect = "Assets/Text/BtnStart_Selected.png";

        static string btnExit = "Assets/Text/BtnExit.png";
        static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 16.0f);
            enemy = new Enemy(200.0f, 44.4f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            backgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            maze = new Maze("DataFiles/level0.json");
            gameOverScreen = new GameOver();
            uiManager = new UIManager();

            //suscripcios CLASEDELEGADO
            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;
            collider.OnDestroyBrickWall += maze.RemoveBrickWall;

            while (Engine.IsWindowOpen)
            {
                #region Engine Window Control
                Engine.UpdateWindow();
                #endregion

                calcDeltatime();


                Input();
                Update();
                Render();

                #region Engine Window Control
                Engine.Clear(Color.Black);
                // mensajes de debug
                if (showDebug)
                {
                    Engine.ClearDebug();
                    Engine.DebugLog(currentMsg);

                }
                Engine.Window.Invalidate();
                #endregion
            }
            //desuscriptions CLASEDELEGADO
            pacman.OnLifeChanged -= pacman.Die;
            pacman.OnLifeChanged -= audioManager.PlayPlayerDie;
        }
        static void calcDeltatime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }
        static void Input()
        {
            if (CurrentState == GameState.playing) pacman.Inputs();
        }
        static void Update()
        {
            if (CurrentState == GameState.playing)
            {
                pacman.Update(deltaTime);

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
                        for (int j = 0; j < maze.BrickWallsInMaze.Count; j++)
                        {
                            collider.IsTransformColliding(maze.BrickWallsInMaze[j].transform, pacman.ActiveBomb.explosions[i].transform);
                        }
                    }
                }

                collider.IsTransformColliding(pacman.transform, exit.Transform);
                collider.IsTransformColliding(pacman.transform, enemy.transform);

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

        }       

        static void Render()
        {
            if (CurrentState == GameState.playing)
            {
                background.Render();
                exit.Render();
                pacman.Render();
                enemy.Render();
                maze.Render();
                uiManager.Render();
            }
            else if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                gameOverScreen.Render();
            }
        }

        public static void RestartGame()
        {
            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 16.0f);
            enemy = new Enemy(200.0f, 44.4f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            backgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            maze = new Maze("DataFiles/level0.json");

            //suscripcios CLASEDELEGADO
            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;
            collider.OnDestroyBrickWall += maze.RemoveBrickWall;

            CurrentState = GameState.playing;
        }
    }
}