using System;

public class Armor : Item {

    public int Armor_ = 0;


    public Armor(string n, string d, UInt32 ID)
    {
        Name = n;
        Description = d;
        uniqueID = ID;

    }

    public int getArmor()
    {
        return Armor_;
    }


}
