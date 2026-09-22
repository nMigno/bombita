using System;
using System.IO;

namespace EngineGDI
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        public AudioManager audioManager;
        public Player BombitaMan;
        public Collider Collider;
        public Background Background;
        public Background BackgroundMenu;
        public Maze Maze;
        public LevelExit Exit;
        public EnemyManager Enemies;
        public GameOver GameOverScreen;
        public UIManager UiManager;
        public MainMenu MainMenuScreen;

        public enum GameState
        {
            start,
            playing,
            victory,
            defeat
        }

        public static GameState CurrentState = GameState.start;

        public static string LevelPath;

        private static bool isColliding = false;

        private int SCREEN_WIDTH;
        private GameManager(int Width)
        {
            SCREEN_WIDTH = Width;
            audioManager = new AudioManager();
            MainMenuScreen = new MainMenu(SCREEN_WIDTH);
            GameOverScreen = new GameOver(SCREEN_WIDTH);

            RestartGame();
        }
        public static void Initialize(int width, int height)
        {
            instance = new GameManager(width);
        }
        public void Input()
        {
            if (CurrentState == GameState.playing) BombitaMan.Inputs();
        }
        public void Update(float deltaTime)
        {
            if (CurrentState == GameState.playing)
            {
                BombitaMan.Update(deltaTime);
                Enemies.Update(deltaTime);

                for (int i = 0; i < Maze.WallsInMaze.Count; i++)
                {
                    Collider.IsTransformColliding(BombitaMan.transform, Maze.WallsInMaze[i].transform);
                }
                for (int i = 0; i < Maze.BrickWallsInMaze.Count; i++)
                {
                    Collider.IsTransformColliding(BombitaMan.transform, Maze.BrickWallsInMaze[i].transform);
                }
                if (BombitaMan.ActiveBomb != null)
                {
                    if (BombitaMan.ActiveBomb.CurrentState == Bomb.BombState.colliding)
                    {
                        isColliding = Collider.IsBoxColliding(BombitaMan.transform.Position, BombitaMan.transform.RealSize,
                            BombitaMan.ActiveBomb.Transform.Position, BombitaMan.ActiveBomb.Transform.RealSize);
                        if (!isColliding)
                        {
                            BombitaMan.ActiveBomb.CurrentState = Bomb.BombState.free;
                        }
                    }
                    else if (BombitaMan.ActiveBomb.CurrentState == Bomb.BombState.free)
                    {
                        Collider.IsTransformColliding(BombitaMan.transform, BombitaMan.ActiveBomb.Transform);
                    }

                    for (int i = 0; i < BombitaMan.ActiveBomb.explosions.Count; i++)
                    {
                        Collider.IsTransformColliding(BombitaMan.transform, BombitaMan.ActiveBomb.explosions[i].Transform);
                        for (int j = 0; j < Maze.BrickWallsInMaze.Count; j++)
                        {
                            Collider.IsTransformColliding(Maze.BrickWallsInMaze[j].transform, BombitaMan.ActiveBomb.explosions[i].Transform);
                        }
                        for (int j = 0; j < Enemies.Enemies.Count; j++)
                        {
                            Collider.IsTransformColliding(Enemies.Enemies[j].transform, BombitaMan.ActiveBomb.explosions[i].Transform);
                        }
                    }

                }


                Collider.IsTransformColliding(BombitaMan.transform, Exit.Transform);

                for (int i = 0; i < Enemies.Enemies.Count; i++)
                {
                    Collider.IsTransformColliding(BombitaMan.transform, Enemies.Enemies[i].transform);
                }


                if (BombitaMan.Dead)
                {
                    BombitaMan.Lives--;

                    if (BombitaMan.Lives >= 0)
                    {
                        BombitaMan.Respawn();
                    }
                    else
                    {
                        CurrentState = GameState.defeat;
                    }
                }
            }
            else if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                GameOverScreen.Update();
            }
            else if (CurrentState == GameState.start)
            {
                MainMenuScreen.Update();
            }

        }
        public void Render()
        {
            if (CurrentState == GameState.playing)
            {
                Background.Render();
                Exit.Render();
                BombitaMan.Render();
                Maze.Render();
                UiManager.Render();
                Enemies.Render();
            }
            else if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                GameOverScreen.Render();
            }
            else if (CurrentState == GameState.start)
            {
                MainMenuScreen.Render();
            }
        }        
        public void RestartGame()
        {
            LevelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles", "level0.json");


            Exit = new LevelExit(40.0f, 720.0f);
            BombitaMan = new Player(41.0f, 40.0f);
            Enemies = new EnemyManager();
            Collider = new Collider();
            Background = new Background(0, 0, "Textures/bg-lv0.png");
            BackgroundMenu = new Background(0, 0, "Textures/bg-black.png");
            Maze = new Maze("DataFiles/level0.json");
            UiManager = new UIManager();


            BombitaMan.OnLifeChanged += BombitaMan.Die;
            BombitaMan.OnLifeChanged += audioManager.PlayPlayerDie;
            Collider.OnPlayerExitColision += PlayerExitedLevel;
            Collider.OnPlayerColisionWithSomethingThatKillsIt += BombitaMan.Die;
            Collider.OnDestroyBrickWall += Maze.RemoveBrickWall;
            Collider.OnDestroyEnemy += Enemies.RemoveEnemy;
            GameOverScreen.OnRestartGame += RestartGame;

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