using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning_Task3
{
    static class FileWorker
    {
        public static Dictionary<int, List<int>> ReadFriendshipFile(string FileName)
        {
            using (var reader = new StreamReader(FileName))
            {
                var FriendsGraph = new Dictionary<int, List<int>>();

                while (!reader.EndOfStream)
                {
                    int [] pair = reader.ReadLine().Split('\t').Select(x => Int32.Parse(x)).ToArray();

                    if (!FriendsGraph.ContainsKey(pair[0]))
                        FriendsGraph.Add(pair[0], new List<int>());

                    FriendsGraph[pair[0]].Add(pair[1]);
                }

                return FriendsGraph;
            }
        }
        public static Dictionary<int, List<int>> ReadCheckinsFile(string FileName)
        {
            using (var reader = new StreamReader(FileName))
            {
                var Places = new Dictionary<int, List<int>>();

                while (!reader.EndOfStream)
                {
                    string[] pair = reader.ReadLine().Split('\t');

                    if (!Places.ContainsKey(Int32.Parse(pair[0])))
                        Places.Add(Int32.Parse(pair[0]), new List<int>());

                    Places[Int32.Parse(pair[0])].Add(Int32.Parse(pair[pair.Length - 1]));

                }

                return Places;
            }
        }

        // Create friendship fold with user ids in interval [begin; end)
        public static void CreateCheckinsDataFold(Dictionary<int, List<int>> Checkins, int begin, int end, string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                foreach (int i in Checkins.Keys)
                {
                    if (i >= begin && i < end)
                        foreach (int j in Checkins[i])
                            writer.WriteLine(String.Format("{0}\t{1}", i, j));
                }
            }
        }

        // Create friendship fold with user ids in interval [begin; end)
        public static void CreateFriendshipDataFold(Dictionary<int, List<int>> FriendshipGraph, int begin, int end, string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                foreach (int i in FriendshipGraph.Keys)
                {
                    if (i >= begin && i < end)
                        foreach (int j in FriendshipGraph[i])
                        {
                            if (j >= begin && j < end)
                                writer.WriteLine(String.Format("{0}\t{1}", i, j));
                        }
                }
            }
        }

        public static Dictionary<int, int> ReadClustersFile(string FileName)
        {
            var Clusters = new Dictionary<int, int>();

            using (var reader = new StreamReader(FileName))
            {
                while (!reader.EndOfStream)
                {
                    int[] pair = reader.ReadLine().Split('\t').Select(x => Int32.Parse(x)).ToArray();
                    Clusters.Add(pair[0], pair[1]);
                }

            }

            return Clusters;
        }

        public static void WriteResult(int UserId, int ClusterId, List<int> UserPlaces, List<int> TopPlaces, double prediction,  string FileName)
        {
            using (var writer = new StreamWriter(FileName))
            {
                writer.WriteLine(String.Format("User: {0}, Cluster: {1}", UserId, ClusterId));

                writer.Write("User Places:");
                foreach (int up in UserPlaces)
                    writer.Write(" " + up);

                writer.Write("\n");
                writer.Write("Top Places:");
                foreach (int tp in TopPlaces)
                    writer.Write(" " + tp);

                writer.Write("\n");
                writer.Write("Accuracy: " + prediction);
            }
        }
    }
}
