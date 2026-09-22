using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using EngineGDI.DataFiles;

namespace EngineGDI
{
    public static class PathGenerator
    {
        public static int MaxX = 39;
        public static int MaxY = 21;
        public static bool[,] Grid;
        public static int X;
        public static int Y;
        public static int I;
        public static int StartEmpty;
        public static int CurrentLength;
        public static string LevelJson;
        public static string OutputJson;
        private static float startPosX;
        private static float startPosY;
        private static bool isInsideSafeZone;
        public static int n;

        public static List<EnemyRoute> ValidRoutes;
        public static List<EnemyRoute> FilteredRoutes;
        public static List<Position> TempPath;
        public static EnemyRoute TempRoute;
        public static JavaScriptSerializer Serializer;
        public static Level LevelData;
        public static PathData FinalData;
        public static EnemyRoute Value;
        public static Random Rng = new Random();

        public static void GeneratePaths(string levelFilePath, string outputFilePath)
        {
            Grid = new bool[MaxX, MaxY];
            ValidRoutes = new List<EnemyRoute>();
            Serializer = new JavaScriptSerializer();

            LevelJson = File.ReadAllText(levelFilePath);
            LevelData = Serializer.Deserialize<Level>(LevelJson);

            for (I = 0; I < LevelData.WallsInFile.Count; I++)
            {
                Grid[(int)LevelData.WallsInFile[I].X, (int)LevelData.WallsInFile[I].Y] = true;
            }

            for (I = 0; I < LevelData.BricksInFile.Count; I++)
            {
                Grid[(int)LevelData.BricksInFile[I].X, (int)LevelData.BricksInFile[I].Y] = true;
            }

            for (Y = 0; Y < MaxY; Y++)
            {
                StartEmpty = -1;
                CurrentLength = 0;
                for (X = 0; X < MaxX; X++)
                {
                    if (!Grid[X, Y])
                    {
                        if (CurrentLength == 0) StartEmpty = X;
                        CurrentLength++;
                    }
                    else
                    {
                        if (CurrentLength >= 3)
                        {
                            TempPath = new List<Position>();
                            TempPath.Add(new Position { X = StartEmpty, Y = Y });
                            TempPath.Add(new Position { X = X - 1, Y = Y });

                            TempRoute = new EnemyRoute();
                            TempRoute.Path = TempPath;
                            ValidRoutes.Add(TempRoute);
                        }
                        CurrentLength = 0;
                    }
                }
                if (CurrentLength >= 3)
                {
                    TempPath = new List<Position>();
                    TempPath.Add(new Position { X = StartEmpty, Y = Y });
                    TempPath.Add(new Position { X = MaxX - 1, Y = Y });

                    TempRoute = new EnemyRoute();
                    TempRoute.Path = TempPath;
                    ValidRoutes.Add(TempRoute);
                }
            }

            for (X = 0; X < MaxX; X++)
            {
                StartEmpty = -1;
                CurrentLength = 0;
                for (Y = 0; Y < MaxY; Y++)
                {
                    if (!Grid[X, Y])
                    {
                        if (CurrentLength == 0) StartEmpty = Y;
                        CurrentLength++;
                    }
                    else
                    {
                        if (CurrentLength >= 3)
                        {
                            TempPath = new List<Position>();
                            TempPath.Add(new Position { X = X, Y = StartEmpty });
                            TempPath.Add(new Position { X = X, Y = Y - 1 });

                            TempRoute = new EnemyRoute();
                            TempRoute.Path = TempPath;
                            ValidRoutes.Add(TempRoute);
                        }
                        CurrentLength = 0;
                    }
                }
                if (CurrentLength >= 3)
                {
                    TempPath = new List<Position>();
                    TempPath.Add(new Position { X = X, Y = StartEmpty });
                    TempPath.Add(new Position { X = X, Y = MaxY - 1 });

                    TempRoute = new EnemyRoute();
                    TempRoute.Path = TempPath;
                    ValidRoutes.Add(TempRoute);
                }
            }

            n = ValidRoutes.Count;
            while (n > 1)
            {
                n--;
                I = Rng.Next(n + 1);
                Value = ValidRoutes[I];
                ValidRoutes[I] = ValidRoutes[n];
                ValidRoutes[n] = Value;
            }

            FilteredRoutes = new List<EnemyRoute>();

            for (I = 0; I < ValidRoutes.Count; I++)
            {
                startPosX = ValidRoutes[I].Path[0].X;
                startPosY = ValidRoutes[I].Path[0].Y;

                isInsideSafeZone = (startPosX >= 0 && startPosX <= 5 &&
                                    startPosY >= 0 && startPosY <= 5);

                if (!isInsideSafeZone && FilteredRoutes.Count < 20)
                {
                    FilteredRoutes.Add(ValidRoutes[I]);
                }
            }

            FinalData = new PathData();
            FinalData.EnemyRoutes = FilteredRoutes;
            OutputJson = Serializer.Serialize(FinalData);
            File.WriteAllText(outputFilePath, OutputJson);
        }
    }
}


