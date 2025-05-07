namespace samples;

class sampleIf
{
    bool functionIf()
    {
        int x = 10;
        int y = 30;
        if (1 < x++ || y > 3)
        {
            var result = 3;
            if (result == 3 && (11 < 12 * 3))
            {
                return false;
            }
            
            return ((10 * 0) == 0);
        }
        else
        {
            var result = true;
            return result;
        }
    }
}