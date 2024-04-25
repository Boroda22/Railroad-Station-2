using Railroad_Station_2.Models;
using Railroad_Station_2.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Railroad_Station_2
{
    class Program
    {
        /// <summary>
        /// Станция, коллекция парков
        /// </summary>
        private static List<RailwayPark> railwayParks = new List<RailwayPark>();
        /// <summary>
        /// Вывод меню
        /// </summary>
        private static void showMenu()
        {
            Console.WriteLine("0\tГенерация данных");
            Console.WriteLine("1\tВывести все вершины (стрелки)");
            Console.WriteLine("2\tВывести кратчайший путь из А в B");
            Console.WriteLine("9\tВыход");
        }

        private static void generateData()
        {
            railwayParks.Clear();
            railwayParks = Generator.GenerateData(parksCount: 5,   // количество парков
                                                  maxSwitches: 9,  // количество стрелок (вершин)
                                                  maxWays: 3,      // количество путей, прилегающих к стрелке (сомнительно, что может быть не равно 2-м)
                                                  maxSections: 9,  // количество секций/стыков (для СЦБ)
                                                  Direction.Odd    // направление, четный/нечетный
                                                  );
            Console.WriteLine("Данные обновлены");
        }

        private static void showShortestPath()
        {
            if (railwayParks.Any())
            {
                Console.WriteLine("Укажите путь между A и B:");
                Console.Write("A=");
                var startPoint = Console.ReadLine().ToUpper();
                Console.Write("B=");
                var endPoint = Console.ReadLine().ToUpper();
                foreach (var park in railwayParks)
                {
                    var result = Generator.FindShortestPath(startPoint, endPoint, park);
                    Console.WriteLine($"Кратчайший маршрут {park.ParkName}:\n {result}"); 
                }                
            }
            else
            {
                Console.WriteLine("Нет длянных для вывода");
            }
        }

        private static void showAllSwitches()
        {
            if (railwayParks.Any())
            {
                foreach(var park in railwayParks)
                {
                    Console.WriteLine($"Данные для {park.ParkName}");
                    if (park.Switches.Any())
                    {
                        foreach (var swi in park.Switches)
                        {
                            Console.WriteLine($"Стрелка {swi.SwitchName}");
                            if (swi.Railways.Any())
                            {
                                Console.WriteLine($"\tПрилегающие пути:");
                                foreach(var item in swi.Railways)
                                {
                                    Console.WriteLine($"\t\t{item.RailwayName}/[Стрелка {item.ConnectedSwitch.SwitchName}]/длина={item.TotalLenght}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет длянных для вывода");
                    }
                }
            }
            else
            {
                Console.WriteLine("Нет длянных для вывода");
            }
        }

        static void Main(string[] args)
        {
            showMenu();
            bool exit = false;
            while (!exit)
            {
                var ddd = Console.ReadKey();
                Console.WriteLine();
                switch (ddd.KeyChar)
                {
                    case '0': //генерация данных
                    {
                        generateData();
                        break;
                    }
                    case '1': //вывод всех вершин (стрелок)
                    {
                        showAllSwitches();
                        break;
                    }
                    case '2': //поиск кратчайшего пути
                    {                        
                        showShortestPath();
                        break;
                    }
                    case '9': //Выход
                    {
                        exit = true;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Укажите выбор"); 
                        break;
                    }
                }
            }
        }
    }
}
