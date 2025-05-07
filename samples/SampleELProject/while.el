namespace samples;

class sampleWhile
{
    void functionWhile()
    {
        var x = 10;
        var y = 20;
        
        while ((x == 10) ? true : ((2 - 1 * (3 + 3)) == 14))
        {
            while ((x = x + 1) > 0)
            {
                if (y == 9)
                {
                    break;
                }
                
                continue;
            }
        }
    }
}