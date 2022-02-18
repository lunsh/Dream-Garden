using System.Collections;
using System.Collections.Generic;

public class Data
{
    public int hearts;
    public int[] plantIDs;
    public List<Plant> plants;

    public Data()
    {
        hearts = 0;
        plantIDs = new int[] { -1, -1, -1 };
        plants = Methods.CreateList<Plant>(3);
    }
}
