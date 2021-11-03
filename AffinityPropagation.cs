using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning_Task3
{
    static class AP
    {
        /*
        public static int GetUsersSimilarity(Dictionary<int, List<int>> Users, int userId1, int userId2)
        {
            int similarity = 0;

            if (Users[userId1].Contains(userId2))
                similarity++;

            foreach (int x in Users[userId1])
            {
                if (!Users[userId2].Contains(x))
                    similarity--;
            }

            foreach (int x in Users[userId2])
            {
                if (!Users[userId1].Contains(x))
                    similarity--;
            }

            return similarity;
        }*/

        public static double GetUsersSimilarity(Dictionary<int, List<int>> Users, int userId1, int userId2)
        {
            double similarity = -1;

            if (Users[userId1].Contains(userId2))
                similarity = 1 + 1E-16d;

            return similarity;
        }

        public static double[,] CreateSimilarityTable(Dictionary<int, List<int>> Users)
        {
            var SimTable = new double[Users.Count, Users.Count];
            List<double> Values = new List<double>();

            Parallel.For(0, Users.Count - 1, (i) =>
            {
                for (int j = i + 1; j < Users.Count; j++)
                {
                    double simIJ = GetUsersSimilarity(Users, i, j);
                    SimTable[i, j] = simIJ;
                    SimTable[j, i] = simIJ;

                    if (!Values.Contains(simIJ))
                        Values.Add(simIJ);
                }
            });

            Values.Sort();

            for (int i = 0; i < Users.Count; i++)
                SimTable[i, i] = Values[Values.Count / 2];


            return SimTable;
        }

       
        public static int[] AffinityPropagation(double [,] SimilarityTable)
        {
            int N = (int)Math.Sqrt(SimilarityTable.Length);
            double [,] R = new double[N, N]; // Responsibility
            double [,] A = new double[N, N]; // Availability

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    R[i, j] = 0;
                    A[i, j] = 0;
                }
            int iter = 0;

            while (iter < 10)
            {
                for (int i = 0; i < N; i++)
                    for (int k = 0; k < N; k++)
                    {
                        List<double> MaxValueList = new List<double>();

                        for (int p = 0; p < N; p++)
                        {
                            if (p != k)
                                MaxValueList.Add(A[i, p] + SimilarityTable[i, p]);
                        }

                        R[i, k] = SimilarityTable[i, k] - MaxValueList.Max();
                    }

                for (int i = 0; i < N; i++)
                    for (int k = 0; k < N; k++)
                    {
                        double sum = 0;

                        for (int t = 0; t < N; t++)
                        {
                            if (t != i && t != k)
                                sum += Math.Max(0, R[t, k]);
                        }

                        if (i != k)
                        {
                            A[i, k] = Math.Min(0, R[k, k] + sum);
                        }
                        else
                        {
                            A[i, k] = sum + Math.Max(0, R[i, k]);
                        }
                    }

                iter++;
            }

            int[] Marks = new int[N];
            double[] MarkValues = new double[N];

            for (int i = 0; i < N; i++)
            {
                MarkValues[i] = R[i, 0] += A[i, 0];
                Marks[i] = 0;

                for (int k = 0; k < N; k++)
                {
                    double kvalue = R[i, k] += A[i, k];
                    if (MarkValues[i] < kvalue)
                    {
                        MarkValues[i] = kvalue;
                        Marks[i] = k;
                    }

                }
            }

            return Marks;
        }
    }
}
