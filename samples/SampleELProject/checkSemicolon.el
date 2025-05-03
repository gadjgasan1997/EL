namespace samples;

class type
{
    void check()
    {
        // Объявление переменной
        int x1;
        var x2 = 2;
        var x3 = x2 + 1;
        var x4 = x3++;
        var x5 = (1 == 2) || ((3 == 4) && (9 < 8));
        
        // Присваивание переменной значения
        x1 = 1;
        x2 = x1;
        x3 = x2--;
        x4 = x2 = x1;
        
        // Вызовы функциий
        function1();
        functionWithArgs1(functionWithArgs2(functionWithArgs3(function1())));
        
        var result1 = function1()++;
        var result2 = boolFunction1() || boolFunction2();
        var result3 = functionWithArgs1(function1())++;
        var result4 = functionWithArgs1(boolFunction1() || boolFunction2()) + 10;
        
        int anotherResult1;
        bool anotherResult2;
        int anotherResult3;
        int anotherResult4;
        
        anotherResult1 = function1();
        anotherResult2 = boolFunction1() || boolFunction2();
        anotherResult3 = functionWithArgs1(function1())++;
        anotherResult4 = functionWithArgs1(boolFunction1() || boolFunction2()) + 10;
        
        // Условие
        if (x1 == 3)
        {
        }
        
        if (x2 == function1() || (boolFunction1() != boolFunction2()))
        {
        }
        
        // Цикл while
        while (boolFunction1())
        {
            
        }
        
        while ((x1 = y) > 0)
        {
            y--;
        }
    }
}