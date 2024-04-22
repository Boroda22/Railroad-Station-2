using Railroad_Station_2.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad_Station_2.Services
{
    /// <summary>
    /// Генератор данных жд. станции
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Наименования для стрелок (вершин)
        /// </summary>
        private static char[] namesSwithes = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };
        /// <summary>
        /// Список жд.парков
        /// </summary>
        private static List<RailwayPark> railwayParks;
        /// <summary>
        /// Генератор данных
        /// </summary>
        /// <param name="parksCount"></param>
        /// <param name="maxSwitches"></param>
        /// <param name="maxWays"></param>
        /// <param name="maxSections"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static List<RailwayPark> GenerateData(int parksCount, int maxSwitches, int maxWays, int maxSections, Direction direction)
        {
            if (parksCount == 0)
            {
                return null;
            }
            //обнуляем список парков
            railwayParks = new List<RailwayPark>();

            var startInd = (int)direction; //чтобы в наименовании парка участвовал тип направления (чет/нечет)

            for (int ind = 0; ind < parksCount; ind++)
            {
                var railwayPark = new RailwayPark($"Парк-{startInd}", direction);
                generateSwhitches(maxSwitches, maxWays, maxSections, railwayPark);

                railwayParks.Add(railwayPark);
            }

            return railwayParks;
        }
        /// <summary>
        /// Поиск кратчайшего пути
        /// </summary>
        /// <param name="startName"></param>
        /// <param name="finishName"></param>
        /// <param name="railwayPark"></param>
        /// <returns></returns>
        public static string FindShortestPath(string startName, string finishName, RailwayPark railwayPark)
        {            
            var dijkstra = new Dijkstra(railwayPark);
            var path = dijkstra.FindShortestPath(startName, finishName);

            return path;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxSwitches"></param>
        /// <param name="maxWays"></param>
        /// <param name="maxSections"></param>
        /// <param name="railwayPark"></param>
        private static void generateSwhitches(int maxSwitches, int maxWays, int maxSections, RailwayPark railwayPark)
        {
            //добавление вершин
            Random rnd = new Random();
            int value = rnd.Next(1, maxSwitches+1);
            for (int x = 0; x < value; x++)
            {
                railwayPark.AddSwitch(namesSwithes[x].ToString());
            }

            generateRailways(maxWays, maxSections, railwayPark);
        }

        /// <summary>
        /// добавление ребер
        /// </summary>
        /// <param name="maxWays"></param>
        /// <param name="maxSections"></param>
        /// <param name="railwayPark"></param>
        private static void generateRailways(int maxWays, int maxSections, RailwayPark railwayPark)
        {
            //Random rnd = new Random();
            //int value = rnd.Next(1, maxWays+1);            
            //for(int x = 1; x <= value; x++)
            //{
            //    Random rndInd = new Random();
            //    int startInd = rndInd.Next(namesSwithes.Length);
            //    int endInd = rnd.Next(namesSwithes.Length);
            //    railwayPark.AddEdge(namesSwithes[startInd].ToString(), namesSwithes[endInd].ToString(), maxSections);

            //}

            railwayPark.AddRailway("A", "B", maxSections);
            railwayPark.AddRailway("A", "C", maxSections);
            railwayPark.AddRailway("A", "D", maxSections);
            railwayPark.AddRailway("B", "C", maxSections);
            railwayPark.AddRailway("B", "E", maxSections);
            railwayPark.AddRailway("C", "D", maxSections);
            railwayPark.AddRailway("C", "E", maxSections);
            railwayPark.AddRailway("C", "F", maxSections);
            railwayPark.AddRailway("D", "F", maxSections);
            railwayPark.AddRailway("E", "F", maxSections);
            railwayPark.AddRailway("E", "G", maxSections);
            railwayPark.AddRailway("F", "G", maxSections);
        }
    }
}
