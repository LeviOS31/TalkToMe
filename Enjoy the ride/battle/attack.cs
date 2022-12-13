using Godot;
using System;

//Levi en Nick

public class Attack
{
    private int high;
    private int low;
    private int critdamage;
    private int damage;
    private int crit;
    private int miss;


    public Attack(int high, int low, int critdamage, int crit, int miss)
    {
        this.high = high;
        this.low = low;
        this.critdamage = critdamage;
        this.crit = crit;
        this.miss = miss;
    }

    public int calc()
    {
        Random random = new Random();
        int num = random.Next(1, 100);
        if (num > 0 && num < miss)
        {
            return 0;
        }
        else
        {
            if (critchance())
            {
                damage = random.Next(low, high) * critdamage;
                return damage;
            }
            else
            {
                damage = random.Next(low, high);
                return damage;
            }
        }
    }

    private bool critchance() 
    {
        Random random = new Random();
        int num = random.Next(1, 100);
        if (num > 0 && num < crit)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
