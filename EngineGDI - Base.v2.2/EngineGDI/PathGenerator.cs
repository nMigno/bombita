using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using EngineGDI.DataFiles;

namespace EngineGDI
{
    public static class PathGenerator
    {
        public static int maxX = 39;
        public static int maxY = 21;
        public static bool[,] grid;
        public static int x;
        public static int y;
        public static int i;
        public static int startEmpty;
        public static int currentLength;
        public static string levelJson;
        public static string outputJson;
        static float startPosX;
        static float startPosY;
        static bool isInsideSafeZone;
        public static int n;

        public static List<EnemyRoute> validRoutes;
        public static List<EnemyRoute> filteredRoutes;
        public static List<Position> tempPath;
        public static EnemyRoute tempRoute;
        public static JavaScriptSerializer serializer;
        public static Level levelData;
        public static PathData finalData;
        public static EnemyRoute value;
        public static Random rng = new Random();

        public static void GeneratePaths(string levelFilePath, string outputFilePath)
        {
            grid = new bool[maxX, maxY];
            validRoutes = new List<EnemyRoute>();
            serializer = new JavaScriptSerializer();

            levelJson = File.ReadAllText(levelFilePath);
            levelData = serializer.Deserialize<Level>(levelJson);

            for (i = 0; i < levelData.WallsInFile.Count; i++)
            {
                grid[(int)levelData.WallsInFile[i].X, (int)levelData.WallsInFile[i].Y] = true;
            }

            for (i = 0; i < levelData.BricksInFile.Count; i++)
            {
                grid[(int)levelData.BricksInFile[i].X, (int)levelData.BricksInFile[i].Y] = true;
            }

            for (y = 0; y < maxY; y++)
            {
                startEmpty = -1;
                currentLength = 0;
                for (x = 0; x < maxX; x++)
                {
                    if (!grid[x, y])
                    {
                        if (currentLength == 0) startEmpty = x;
                        currentLength++;
                    }
                    else
                    {
                        if (currentLength >= 3)
                        {
                            tempPath = new List<Position>();
                            tempPath.Add(new Position { X = startEmpty, Y = y });
                            tempPath.Add(new Position { X = x - 1, Y = y });

                            tempRoute = new EnemyRoute();
                            tempRoute.Path = tempPath;
                            validRoutes.Add(tempRoute);
                        }
                        currentLength = 0;
                    }
                }
                if (currentLength >= 3)
                {
                    tempPath = new List<Position>();
                    tempPath.Add(new Position { X = startEmpty, Y = y });
                    tempPath.Add(new Position { X = maxX - 1, Y = y });

                    tempRoute = new EnemyRoute();
                    tempRoute.Path = tempPath;
                    validRoutes.Add(tempRoute);
                }
            }

            for (x = 0; x < maxX; x++)
            {
                startEmpty = -1;
                currentLength = 0;
                for (y = 0; y < maxY; y++)
                {
                    if (!grid[x, y])
                    {
                        if (currentLength == 0) startEmpty = y;
                        currentLength++;
                    }
                    else
                    {
                        if (currentLength >= 3)
                        {
                            tempPath = new List<Position>();
                            tempPath.Add(new Position { X = x, Y = startEmpty });
                            tempPath.Add(new Position { X = x, Y = y - 1 });

                            tempRoute = new EnemyRoute();
                            tempRoute.Path = tempPath;
                            validRoutes.Add(tempRoute);
                        }
                        currentLength = 0;
                    }
                }
                if (currentLength >= 3)
                {
                    tempPath = new List<Position>();
                    tempPath.Add(new Position { X = x, Y = startEmpty });
                    tempPath.Add(new Position { X = x, Y = maxY - 1 });

                    tempRoute = new EnemyRoute();
                    tempRoute.Path = tempPath;
                    validRoutes.Add(tempRoute);
                }
            }

            n = validRoutes.Count;
            while (n > 1)
            {
                n--;
                i = rng.Next(n + 1);
                value = validRoutes[i];
                validRoutes[i] = validRoutes[n];
                validRoutes[n] = value;
            }

            filteredRoutes = new List<EnemyRoute>();

            for (i = 0; i < validRoutes.Count; i++)
            {
                startPosX = validRoutes[i].Path[0].X;
                startPosY = validRoutes[i].Path[0].Y;

                isInsideSafeZone = (startPosX >= 0 && startPosX <= 5 &&
                                    startPosY >= 0 && startPosY <= 5);

                if (!isInsideSafeZone && filteredRoutes.Count < 20)
                {
                    filteredRoutes.Add(validRoutes[i]);
                }
            }

            finalData = new PathData();
            finalData.EnemyRoutes = filteredRoutes;
            outputJson = serializer.Serialize(finalData);
            File.WriteAllText(outputFilePath, outputJson);
        }
    }
}


