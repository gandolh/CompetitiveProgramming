using AoC.Data;
using AoC.Quesint0;

namespace AoC.Quest10
{
    internal class Quest10 : BaseQuest
    {
        private readonly MatrixCoordinate UP = new MatrixCoordinate(0, -1);
        private readonly MatrixCoordinate DOWN = new MatrixCoordinate(0, 1);
        private readonly MatrixCoordinate LEFT = new MatrixCoordinate(-1, 0);
        private readonly MatrixCoordinate RIGHT = new MatrixCoordinate(1, 0);
        private readonly MatrixCoordinate WALL = new MatrixCoordinate(-99, -99);
        private readonly MatrixCoordinate NONE = new MatrixCoordinate(-1, -1);

        public override Task Solve()
        {
            string inPath = GetPathTo("quest10_6.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            int n = lines.Length;
            int m = lines[0].Length;
            MatrixCoordinate posS = new MatrixCoordinate(-1, -1);
            Vector2<MatrixCoordinate>[,] vectorField = new Vector2<MatrixCoordinate>[m, n];

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] != 'S')
                        vectorField[j, i] = GetDirections(lines[i][j]);
                    else
                        posS = new MatrixCoordinate(j, i);
                }

            (MatrixCoordinate x, MatrixCoordinate y) = getDirectionsForS(posS, vectorField, n, m);
            vectorField[posS.X, posS.Y] = new(x, y);
            int[,] walkingMatrix = BfsRoute(posS, vectorField, n, m);
            //PrintMatrix(walkingMatrix, n,m);
            MarkUnusedPipesAsWalls(walkingMatrix, vectorField, n, m);
            SearchPoints(vectorField, n, m);

            return Task.CompletedTask;
        }

        private void MarkUnusedPipesAsWalls(int[,] walkingMatrix, Vector2<MatrixCoordinate>[,] vectorField, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (walkingMatrix[j,i] == 0)
                    {
                        vectorField[j, i].first = WALL;
                        vectorField[j, i].second = WALL;
                    }
        }

        private int[,] BfsRoute(MatrixCoordinate posS, Vector2<MatrixCoordinate>[,] vectorField, int n, int m)
        {
            int[,] walkingMatrix = new int[m, n];
            Queue<MatrixCoordinate> q = new Queue<MatrixCoordinate>();
            q.Enqueue(posS);
            walkingMatrix[posS.X, posS.Y] = 1;
            int maxIndex = -1;
            while (q.Count != 0)
            {
                var current = q.Dequeue();
                int prevIndex = walkingMatrix[current.X, current.Y];
                if (prevIndex > maxIndex)
                    maxIndex = prevIndex;
                

                var directions = vectorField[current.X, current.Y];
                var firstNext = new MatrixCoordinate(current.X + directions.first.X, current.Y + directions.first.Y);
                var secondNext = new MatrixCoordinate(current.X + directions.second.X, current.Y + directions.second.Y);

                if (InBounds(firstNext.X, firstNext.Y, n, m) && walkingMatrix[firstNext.X, firstNext.Y] == 0)
                {
                    q.Enqueue(firstNext);
                    walkingMatrix[firstNext.X, firstNext.Y] = prevIndex + 1;
                }
                else if (InBounds(secondNext.X, secondNext.Y, n, m) && walkingMatrix[secondNext.X, secondNext.Y] == 0)
                {
                    q.Enqueue(secondNext);
                    walkingMatrix[secondNext.X, secondNext.Y] = prevIndex + 1;
                }
            }
            //Console.WriteLine(maxIndex / 2);
            return walkingMatrix;
        }

        private void SearchPoints(Vector2<MatrixCoordinate>[,] vectorField, int n, int m)
        {
            int countInsidePoints = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (vectorField[j,i].first == WALL && vectorField[j, i].second == WALL)
                    {
                        bool isInside = IsInsideRayTrace(i, j, vectorField, m);
                        if (isInside) countInsidePoints++;
                        //Console.Write(isInside ? 'I' : '0');
                    }
                    else
                    {
                        //Console.Write(VectorToChar(vectorField[j,i]));
                    }
                }
                //Console.WriteLine();
            }

            Console.WriteLine(countInsidePoints);
        }

        private bool IsInsideRayTrace(int i, int j, Vector2<MatrixCoordinate>[,] vectorField, int m)
        {
            int encounters = 0;
            for (int k = j; k >= 0; k--)
            {
                char letter = VectorToChar(vectorField[k,i]);
                if (letter == '7')
                {
                    encounters++;
                    do
                    {
                        k--;
                        letter = VectorToChar(vectorField[k, i]);
                    } while (k >= 0 && (letter == '-'));
                    if(letter != 'L') k++;
                }
                else if (letter == 'J')
                {
                    encounters++;
                    do
                    {   
                        k--;
                        letter = VectorToChar(vectorField[k, i]);
                    } while (k >= 0 && (letter == '-'));
                    if (letter != 'F') k++;
                }
                else if (letter != '.') encounters++;
            }
            return encounters % 2 == 1;
        }



        private (MatrixCoordinate x, MatrixCoordinate y) getDirectionsForS(MatrixCoordinate posS, Vector2<MatrixCoordinate>[,] vectorField, int n, int m)
        {
            int posX = posS.X;
            int posY = posS.Y;
            List<MatrixCoordinate> v = new List<MatrixCoordinate>();
            Vector2<MatrixCoordinate>? upperElt = null;
            Vector2<MatrixCoordinate>? downElt = null;
            Vector2<MatrixCoordinate>? rightElt = null;
            Vector2<MatrixCoordinate>? leftElt = null;

            if (posY - 1 >= 0)
                upperElt = vectorField[posX, posY - 1];
            if (posY < n)
                downElt = vectorField[posX, posY + 1];
            if (posX < m)
                rightElt = vectorField[posX + 1, posY];
            if (posX - 1 >= 0)
                leftElt = vectorField[posX - 1, posY];


            if (upperElt != null && (upperElt.first.Y == 1 || upperElt.second.Y == 1)) v.Add(UP);
            if (downElt != null && (downElt.first.Y == -1 || downElt.second.Y == -1)) v.Add(DOWN);
            if (rightElt != null && (rightElt.first.X == -1 || rightElt.second.X == -1)) v.Add(RIGHT);
            if (leftElt != null && (leftElt.first.X == 1 || leftElt.second.X == 1)) v.Add(LEFT);
            return (v[0], v[1]);
        }

        Vector2<MatrixCoordinate> GetDirections(char c)
        {
            if (c == '|')
                return new(UP, DOWN);
            if (c == '-')
                return new(LEFT, RIGHT);
            if (c == 'L')
                return new(UP, RIGHT);
            if (c == 'J')
                return new(UP, LEFT);
            if (c == '7')
                return new(DOWN, LEFT);
            if (c == 'F')
                return new(DOWN, RIGHT);
            //if (c == '.')
            return new(WALL, WALL);
        }

        private char VectorToChar(Vector2<MatrixCoordinate> vector2)
        {
            if (vector2.first == UP && vector2.second == DOWN)
                return '|';
            if (vector2.first == LEFT && vector2.second == RIGHT)
                return '-';
            if (vector2.first == UP && vector2.second == RIGHT)
                return 'L';
            if (vector2.first == UP && vector2.second == LEFT)
                return 'J';
            if (vector2.first == DOWN && vector2.second == LEFT)
                return '7';
            if (vector2.first == DOWN && vector2.second == RIGHT)
                return 'F';
            return '.';
        }

        void PrintMatrix(int[,] walkingMatrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write((walkingMatrix[j, i] == 0 ? "." : walkingMatrix[j, i] - 1) + " ");
                }
                Console.WriteLine();
            }
        }

        bool InBounds(int x, int y, int n, int m)
        {
            if (x < 0 || y < 0)
                return false;
            if (x > m || y > n) return false;
            return true;
        }
    }
}


