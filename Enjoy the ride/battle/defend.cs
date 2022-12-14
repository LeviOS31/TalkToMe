using Godot;
using System;

//Levi

public class Defend
{
    private int block;
    private int blockchance;

    public Defend(int block, int blockchance)
    {
        this.block = block;
        this.blockchance = blockchance;
    }

    public int calc()
    {
        Random random = new Random();
        int num = random.Next(1, 100);
        if (num > 0 && num < blockchance)
        {
            int blocked = random.Next(40, block);
            GD.Print("blocked: " + blocked);
            return blocked;
        }
        else
        {
            return 0;
        }
    }
}