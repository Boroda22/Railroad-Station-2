using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad_Station_2.Models
{
    /// <summary>
    /// Граф
    /// </summary>
    /// <summary>
    /// Граф
    /// </summary>
    public class RailwayPark
    {
        /// <summary>
        /// Наименование парка
        /// </summary>
        public string ParkName { get; }
        /// <summary>
        /// Направление движения (чет/нечет)
        /// </summary>
        public Direction Direction { get; set; }
        /// <summary>
        /// Список стрелок (вершин графа)
        /// </summary>
        public List<RailwaySwitch> Switches { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public RailwayPark()
        {
            Switches = new List<RailwaySwitch>();
        }

        public RailwayPark(string name, Direction direction)
        {
            ParkName = name;
            Direction = direction;
            Switches = new List<RailwaySwitch>();
        }

        /// <summary>
        /// Добавление стрелки (вершины)
        /// </summary>
        /// <param name="switchName">Имя вершины</param>
        public void AddSwitch(string switchName)
        {
            Switches.Add(new RailwaySwitch(switchName));
        }

        /// <summary>
        /// Поиск вершины
        /// </summary>
        /// <param name="switchName">Название вершины</param>
        /// <returns>Найденная вершина</returns>
        public RailwaySwitch FindVertex(string switchName)
        {
            foreach (var v in Switches)
            {
                if (v.SwitchName.Equals(switchName))
                {
                    return v;
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление пути (ребра)
        /// </summary>
        /// <param name="firstName">Имя первой вершины</param>
        /// <param name="secondName">Имя второй вершины</param>
        public void AddRailway(string firstName, string secondName, int maxSections)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, maxSections);
                v2.AddEdge(v1, maxSections);
            }
        }
    }
}
