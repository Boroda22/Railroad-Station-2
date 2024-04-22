using Railroad_Station_2.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad_Station_2.Services
{
    /// <summary>
    /// Алгоритм Дейкстры
    /// </summary>
    public class Dijkstra
    {
        RailwayPark graph;

        List<GraphVertexInfo> infos;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>
        public Dijkstra(RailwayPark graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Switches)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Информация о вершине</returns>
        GraphVertexInfo GetVertexInfo(RailwaySwitch v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        /// <summary>
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startName">Название стартовой вершины</param>
        /// <param name="finishName">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
        public string FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }

        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public string FindShortestPath(RailwaySwitch startVertex, RailwaySwitch finishVertex)
        {
            if(startVertex == null || finishVertex == null)
            {
                return ($"Не найдена одна из вершин указанного пути");
            }
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }
                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
        private void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            //проверяем свободность пути, определяем вес смежных ребер и выбираем наименьшее ребро с вершиной
            foreach (var railway in info.Vertex.Railways)
            {
                //проверяем занятость всего пути
                if (railway.IsFree)
                {
                    var nextInfo = GetVertexInfo(railway.ConnectedSwitch);
                    var sum = info.EdgesWeightSum + railway.EdgeWeight;
                    if (sum < nextInfo.EdgesWeightSum)
                    {
                        nextInfo.EdgesWeightSum = sum;
                        nextInfo.PreviousVertex = info.Vertex;
                    }
                }                
            }
        }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        private string GetPath(RailwaySwitch startVertex, RailwaySwitch endVertex)
        {
            var path = string.Empty;
            if(startVertex == null || endVertex == null)
            {
                return "Нет пути";
            }

            path = endVertex.ToString();
            while (startVertex != endVertex)
            {                
                var vertInfo = GetVertexInfo(endVertex);
                if(vertInfo != null)
                {
                    endVertex = vertInfo.PreviousVertex;
                    //не имеет вершины, значит тупик
                    if (endVertex != null)
                    {
                        path = $"{endVertex.ToString()}=>{path}";
                    }
                    else
                    {
                        path = $"Тупик на {vertInfo.Vertex.SwitchName}";
                    }
                }
                else
                {
                    return $"Указанный маршрут не найден";
                }
            }

            return path;
        }
    }
}
