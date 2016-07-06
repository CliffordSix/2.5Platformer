using System;

public class Monster : Card {

    int Health = 100;
    int Armor = 0;
    int Attack = 10;
    //element Element;
    //List<Scrap> scrapCost = new List<Scrap>();

    public Monster(string n, string d, UInt32 ID, UInt16 dif, int h, int atk, int arm) :  base(n, d, ID, dif)
    {
        Name = n;
        Description = d;
        uniqueID = ID;
        Difficulty = dif;
        Armor = arm;
        Attack = atk;
        Health = h;

    }

    public int getHealth()
    {
        return Health;
    }

    public int getAttack()
    {
        return Attack;
    }

    public int getArmor()
    {
        return Armor;
    }
}
