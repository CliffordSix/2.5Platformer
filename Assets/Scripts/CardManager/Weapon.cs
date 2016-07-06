using System;

public class Weapon : Item {

    public int Attack = 10;

    public Weapon(string n, string d, UInt32 ID)
    {
        Name = n;
        Description = d;
        uniqueID = ID;

    }

    public int getAttack()
    {
        return Attack;
    }

}
