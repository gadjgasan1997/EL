namespace samples;

class type
{
    void functionFor()
    {
        for (var x = 0; x < 10; x++)
        {
            for (var i = 0; i < 5; i++)
            {
            }
        }
        
        for (var x = 0; x < 10;)
        {
            x++;
            
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
        
        int x = 0;
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