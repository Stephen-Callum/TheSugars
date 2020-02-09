using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LeaderBoard
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }

    public LeaderBoard(int _id, string _name, int _score, DateTime _date)
    {
        ID = _id;
        Name = _name;
        Score = _score;
        Date = _date;
    }
}
