namespace samples;

class sampleFor
{
    void functionFor()
    {
        for (var x1 = 0; x1 < 10; x1++)
        {
            for (var i = 0; i < 5; i++)
            {
            }
        }
        
        for (var x2 = 0; x2 < 10;)
        {
            x2++;
            
            for (var i = 0; i < 5;)
            {
                i++;
            }
        }
        
        int x = 0;
        for (; x < 10; x++)
        {
            var i = 0;
            for (; i < 5; i++)
            {
                
            }
        }
        
        for (; x < 10;)
        {
            x++;
            
            var i = 0;
            for (; i < 5;)
            {
                i++;
            }
        }
    }
}