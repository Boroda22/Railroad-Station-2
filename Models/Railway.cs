using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railroad_Station_2.Models
{
    /// <summary>
    /// Железнодорожный путь (ребро графа)
    /// </summary>
    public class Railway
    {
        /// <summary>
        /// Секции ребра (для определения свободного пути
        /// </summary>
        private List<Section> sections { get; }
        /// <summary>
        /// Общий счетчик путей
        /// </summary>
        private static int totalIndex = 0;
        private List<Section> createSections(int maxSections)
        {
            var result = new List<Section>();
            Random rnd = new Random();
            int value = rnd.Next(1, maxSections + 1);
            for (int x = 0; x < value; x++)
            {
                // по умолчанию участок свободын, длинной 100м.
                var section = new Section()
                {
                    IsFree = true,
                    Name = x.ToString(),
                    Lenght = 100
                };

                result.Add(section);
            }

            return result;
        }

        /// <summary>
        /// Связанная стрелка (вершина)
        /// </summary>
        public RailwaySwitch ConnectedSwitch { get; }
        /// <summary>
        /// Вес ребра (зависит от суммы секций (стыков от светофора до светофора)
        /// </summary>
        public int EdgeWeight
        {
            get
            {
                return sections.Sum(x=>x.Lenght);
            }
        }
        /// <summary>
        /// Признак свободного пути
        /// </summary>
        public bool IsFree
        {
            get
            {
                return sections.All(x => x.IsFree);
            }
        }
        /// <summary>
        /// Общая длина пути
        /// </summary>
        public int TotalLenght
        {
            get
            {
                return sections.Sum(x => x.Lenght);
            }
        }
        /// <summary>
        /// Наименование пути
        /// </summary>
        public string RailwayName
        {
            get
            {
                return $"Путь №{RailwayIndex}";
            }
        }
        /// <summary>
        /// Текущий номер пути
        /// </summary>
        public int RailwayIndex { get; }
        /// <summary>
        /// Сброс счетчика путей при генерации новых данных
        /// </summary>
        public static void ResetIndex()
        {
            totalIndex = 0;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connectedVertex">Связанная вершина</param>        
        public Railway(RailwaySwitch connectedVertex, int maxSections)
        {
            ConnectedSwitch = connectedVertex;
            sections = createSections(maxSections);
            totalIndex++;
            RailwayIndex = totalIndex;
        }


        public override string ToString()
        {
            var res = IsFree ? "Свободен" : "Занят";
            return $"{RailwayName}/Вес={EdgeWeight}/{res}";
        }

        
    }

    /// <summary>
    /// Участок пути
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Наименование участка путей
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Свободный или занятый участок
        /// </summary>
        public bool IsFree { get; set; } 
        /// <summary>
        /// Длина секции для вычисления общей длины пути
        /// </summary>
        public int Lenght { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
