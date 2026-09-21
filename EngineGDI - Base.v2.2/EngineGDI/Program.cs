using EngineGDI.DataFiles;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
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
        public static List<Enemy> Enemies = new List<Enemy>();
        public static Collider collider;
        public static Background background;
        public static Background backgroundMenu;
        public static Maze maze;
        public static LevelExit exit;
        public static GameOver gameOverScreen;
        public static UIManager uiManager;
        public static MainMenu mainMenuScreen;

        public enum GameState
        {
            start,
            playing,
            victory,
            defeat
        }

        public static GameState CurrentState = GameState.start;

        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        public static PathData Pathing;
        public static string EnemyPath;
        public static float EnemySpeed = 100.0f;
        public static List<Vector2> Routes;
        public static Enemy Enemy;

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

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            levelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "DataFiles", "level0.json");
            EnemyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "DataFiles", "EnemyPathing.json");

            // Line below generates random positioning of enemies. We
            // deactivate ir for avoiding constant random enemy positioning
            // every time we open the game
            //PathGenerator.GeneratePaths(levelPath, EnemyPath);

            Pathing = PositionData.ReadPathFromJson(EnemyPath);
            LoadEnemies();

            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 40.0f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            backgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            maze = new Maze("DataFiles/level0.json");
            gameOverScreen = new GameOver();
            uiManager = new UIManager();
            mainMenuScreen = new MainMenu();


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
                
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].Update(deltaTime);
                    collider.IsTransformColliding(pacman.transform, Enemies[i].transform);
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

        static void Render()
        {
            if (CurrentState == GameState.playing)
            {
                background.Render();
                exit.Render();
                pacman.Render();
                maze.Render();
                uiManager.Render();

                for (int i = 0;i < Enemies.Count; i++)
                {
                    Enemies[i].Render();
                }
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

        public static void RestartGame()
        {
            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 40.0f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            backgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            maze = new Maze("DataFiles/level0.json");

            //suscripcios CLASEDELEGADO
            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;
            collider.OnDestroyBrickWall += maze.RemoveBrickWall;

            LoadEnemies();

            CurrentState = GameState.playing;
        }

        public static void LoadEnemies()
        {
            Enemies.Clear();

            foreach (var route in Pathing.EnemyRoutes)
            {
                Routes = new List<Vector2>();

                foreach (var pathing in route.Path)
                {
                    Routes.Add(new Vector2 { x = pathing.X, y = pathing.Y });
                }

                Enemy = new Enemy(EnemySpeed, Routes);
                Enemies.Add(Enemy);
            }
        }
    }
}