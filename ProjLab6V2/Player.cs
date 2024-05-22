using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjLab6V2
{
    internal class Player
    {
        public string name
        {
            get;
            set;
        }
        public int money_amount
        {
            get;
            set;
        }
        public int the_biggest_win
        {
            get;
            set;
        }
        public Player(string name, int money_amount=1000, int the_biggest_win=0)
        {
            this.name = name;
            this.money_amount = money_amount;
            this.the_biggest_win = the_biggest_win;
        }
        /*public void updateDataPlayer(int money_amount, int the_biggest_win)
        {
            this.money_amount = money_amount;
            if(this.the_biggest_win<the_biggest_win) this.the_biggest_win = the_biggest_win;
        }*/

        
        
        /*public int checkWin(List<Round> rounds)
        {
            int number = 0;
            foreach (Round round in rounds)
            {
                number += round.win_amount;
            }
            return number;
        }*/
        public Match makeMatch(int bet, int round, int[] list_spot)
        {
            var new_match = new Match(bet, round, list_spot);
            return new_match;
        }
    }
}
