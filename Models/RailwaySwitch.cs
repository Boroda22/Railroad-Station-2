using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad_Station_2.Models
{
    /// <summary>
    /// Направление движения (чет/нечет)
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Нечетное
        /// </summary>
        Odd = 1,
        /// <summary>
        /// Четное
        /// </summary>
        Even = 2
    }

    /// <summary>
    /// Вершина графа
    /// </summary>
    public class RailwaySwitch
    {
        /// <summary>
        /// Название стрелки (вершины)
        /// </summary>
        public string SwitchName { get; }

        /// <summary>
        /// Список путей (ребер)
        /// </summary>
        public List<Railway> Railways { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertexName">Название вершины</param>
        public RailwaySwitch(string vertexName)
        {
            SwitchName = vertexName;
            Railways = new List<Railway>();
        }

        /// <summary>
        /// Добавить путь (ребро)
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(Railway newEdge)
        {
            Railways.Add(newEdge);
        }

        /// <summary>
        /// Добавить путь (ребро)
        /// </summary>
        /// <param name="vertex">Вершина</param>        
        public void AddEdge(RailwaySwitch vertex, int maxSections)
        {
            AddEdge(new Railway(vertex, maxSections));
        }

        /// <summary>
        /// Преобразование в строку
        /// </summary>
        /// <returns>Имя вершины</returns>
        public override string ToString()
        {
            return SwitchName;
        }
    }
}
