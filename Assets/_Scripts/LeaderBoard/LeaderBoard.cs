using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LeaderBoard : IComparable<LeaderBoard>
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

    public int CompareTo(LeaderBoard other)
    {
        // 1st > 2nd    return -1
        if (other.Score > this.Score)
        {
            return 1;
        }
        // 1st < 2nd    return 1
        else if (other.Score < this.Score)
        {
            return -1;
        }
        // compare dates
        else if (other.Date < this.Date)
        {
            return -1;
        }
        else if (other.Date > this.Date)
        {
            return 1;
        }
        // 1st == 2nd  and date is same return 0
        return 0;
    }
}
