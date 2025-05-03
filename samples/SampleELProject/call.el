namespace samples;

class type
{
    void call()
    {
        function1();
        function2(1);
        function3("some", x == 3 || (y / 10 == 8));
        
        var result1 = function4();
        var result2 = function5(x);
        var result3 = function6("10", y++ == 11);
        var result4 = function6(function5(9), function4());
    }
}