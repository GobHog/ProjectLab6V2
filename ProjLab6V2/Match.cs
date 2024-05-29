using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjLab6V2
{
    internal class Match
    {
        public int bet
        {
            get;
            set;
        }
        public int number_round
        {
            get;
            set;
        }
        public int[] list_spot
        {
            get;
            set;
        }
        public Match(int bet, int number_round, int[] list_spot)
        {
            this.bet = bet;
            this.number_round = number_round;
            this.list_spot = list_spot;
        }
        public List<Round> makeRound(int bet, int number_round, int[] list_spot)
        {
            int[][] numbers = {
                new int[1] { 3 },
                new int[2] { 1, 9 },
                new int[3] { 0, 1, 15 },
                new int[4] { 0, 1, 3, 25 },
                new int[5] { 0, 1, 4, 22, 100 },
                new int[6] { 0, 0, 3, 4, 70, 250 },
                new int[7] { 0, 0, 1, 2, 21, 100, 500 },
                new int[8] { 0, 0, 0, 2, 12, 100, 300, 750 },
                new int[9] { 0, 0, 0, 0, 2, 13, 110, 400, 1000 },
                new int[10] { 0, 0, 0, 0, 0, 2, 14, 120, 500, 2000 },
                new int[11] { 0, 0, 0, 0, 0, 0, 2, 15, 130, 600, 3000 },
                new int[12] { 0, 0, 0, 0, 0, 0, 0, 2, 16, 140, 700, 4000 },
                new int[13] { 0, 0, 0, 0, 0, 0, 0, 0, 2, 17, 150, 800, 5000 },
                new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 18, 160, 900, 7500 },
                new int[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 19, 170, 1000, 10000 }
            };
            var list_round = new List<Round>();
            Random random = new Random();
            for (int i = 0; i < number_round; i++)
            {
                //получение 20 рандомных чисел
                
                List<int> numbers1 = new List<int>();
                //добавление чисел от 1 до 80
                for (int i1 = 1; i1 <= 80; i1++)
                {
                    numbers1.Add(i1);
                }
                int[] uniqueRandomNumbers = new int[20];
                for (int i1 = 0; i1 < 20; i1++)
                {
                    int randomIndex = random.Next(0, numbers1.Count()-1);
                    uniqueRandomNumbers[i1] = numbers1[randomIndex];
                    numbers1.Remove(uniqueRandomNumbers[i1]);
                }
                //получение списка
                List<int> list = list_spot.Where(x => x != 0).ToList();
                //получение кол-ва совпавших элементов
                var commonElements = uniqueRandomNumbers.Intersect(list);
                var win = 0;
                if (commonElements.Count() != 0) win = bet * numbers[list_spot.Length - 1][commonElements.Count()-1];
                    else win = 0;
                var new_round = new Round(uniqueRandomNumbers, win);
                list_round.Add(new_round);
            }
            return list_round;
        }
    }
}
