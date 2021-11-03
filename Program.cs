using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning_Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            //var FriendsDictionary = FileWorker.ReadFriendshipFile("../../dataset/Gowalla_edges.txt");
            //FileWorker.CreateFriendshipDataFold(FriendsDictionary, 0, 1000, "First1000UsersFold.txt");

            //var PlacesDictionary = FileWorker.ReadCheckinsFile("../../dataset/Gowalla_totalCheckins.txt");
            //FileWorker.CreateCheckinsDataFold(PlacesDictionary, 0, 1000, "First1000UsersCheckinsFold.txt");

            var Friends = FileWorker.ReadFriendshipFile("../../dataset/First1000UsersFold.txt");
            var Places = FileWorker.ReadCheckinsFile("../../dataset/First1000UsersCheckinsFold.txt");
            var Clusters = FileWorker.ReadClustersFile("../../dataset/Clusters.txt");

            int[] hideUsers = { 0, 1, 615, 616 };

            foreach (int i in hideUsers)
            {
                int hideUser = i;
                var clusterId = Clusters[hideUser];
                var clusterUsers = Clusters.Where(x => x.Value == clusterId && x.Key != hideUser).Select(x => x.Key).ToList();

                var hideUserList = new List<int>();
                hideUserList.Add(hideUser);

                var userPlaces = Prediction.GetTopCheckinPlaces(hideUserList, Places);
                var topPlaces = Prediction.GetTopCheckinPlaces(clusterUsers, Places);
                var predict = Prediction.Predict(clusterUsers, Places, hideUser);
                FileWorker.WriteResult(hideUser, clusterId, userPlaces, topPlaces, predict, "../../dataset/User" + hideUser + "Result.txt");
            }
            //var TableS = AP.CreateSimilarityTable(Friends);
            //var Res = AP.AffinityPropagation(TableS);
        }
    }
}
