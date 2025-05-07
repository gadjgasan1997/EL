namespace samples;

class sampleFunctions
{
    void function1()
    {
        return;
    }
    
    void function2(int x)
    {
        return;
    }
    
    void function3(string x, bool y)
    {
        return;
    }
    
    bool function4()
    {
        return false;
    }
    
    string function5(int x)
    {
        return "test";
    }
    
    string function5(int x, int y, int l, string value)
    {
        if (x == 0)
        {
            if (y < 10)
            {
                while (l < 5)
                {
                    l++;
                    if (value == "my_value")
                    {
                        return "test";
                    }
                }
                
                return "test";
            }
            
            else
            {
                return "test";
            }
        }
        else
        {
            return "test";
        }
    }
    
    string function6(string x, bool y)
    {
        return x;
    }
}