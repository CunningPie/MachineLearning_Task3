using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning_Task3
{
    static class Prediction
    {
        public static List<int> GetTopCheckinPlaces(List<int> Users, Dictionary<int, List<int>> Places)
        {
            var RatingDictionary = new Dictionary<int, int>();

            foreach (int user in Users)
            {
                if (Places.ContainsKey(user))
                {
                    foreach (int place in Places[user])
                    {
                        if (!RatingDictionary.ContainsKey(place))
                            RatingDictionary.Add(place, 1);
                        else
                            RatingDictionary[place]++;
                    }
                }
            }

            var top = RatingDictionary.Select(x => x.Value).ToList().OrderByDescending(x => x).Distinct().ToList();
            int topThreshold;

            if (top.Count >= 10)
                topThreshold = top[9];
            else
                topThreshold = top[top.Count - 1];

            //var Res = RatingDictionary.Where(x => x.Value >= topThreshold).OrderBy(x => x.Value).Select(x => x.Key).ToList();
            var Res = (from entry in RatingDictionary orderby entry.Value descending select entry.Key).ToList() ;
            return Res.Count > 10 ? Res.Take(10).ToList() : Res;
        }

        public static double Predict(List<int> Users, Dictionary<int, List<int>> Places, int hideUserID)
        {
            var topPlaces = GetTopCheckinPlaces(Users.Where(x => x != hideUserID).ToList(), Places);

            var hideUserList = new List<int>();
            hideUserList.Add(hideUserID);

            var userPlaces = GetTopCheckinPlaces(hideUserList, Places);

            double acc = 0;

            foreach (int checkin in topPlaces)
                if (userPlaces.Contains(checkin))
                    acc++;

            return acc / 10;
        }

    }
}
