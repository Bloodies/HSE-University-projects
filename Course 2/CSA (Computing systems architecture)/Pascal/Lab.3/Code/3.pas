{$asmmode intel}

CONST
    e: double =  0.0001;
    stepX: double = 0.09;
VAR
    i: integer;
    answers: array [0..9, 0..3] of double;
BEGIN
    answers[0,0] := 0.1;

    ASM
        MOV ECX, 0 //счетчик
        MOV EAX, 0 //для вычисления смещения
        MOV EBX, 0 //храниться смещения по строкам
        FINIT
        FLD QWORD PTR answers[0] //ложим в FPU x                                 //4
     @1:
        FLD QWORD PTR stepX      //ложим в ФПУ шаг х                             //3
        FADDP                    // прибавляем к х шаг х и достаем верхушку
        MOV AL, 32               //вычисления смещения по стркоам
        MUL CL
        MOV EBX, EAX
        FST QWORD PTR answers[EBX][0]   //кладем в строку значение х

        FLDZ  //кладем n                                                         //2
        FLDZ  //сумма ряда                                                       //1
        FLD1  //элемент ряда                                                     //0
     @2:           //вычисление по заданному n
        FLD ST(2)       //переносим n
        FMUL ST, ST     //квадрат n
        FADD ST, ST     // 2 * (n^2)
        FLD ST(3)       //переносим n
        FADD ST, ST(4)  //находим 3 * n
        FADD ST, ST(4)
        FADDP           // (2*(n^2)) + (3*n)
       // FLD1            //загружаем 1
       // FADDP           // (2*(n^2)) + (3*n) + 1
        FLD ST(4)       //переносим х
        FMUL ST, ST     //x^2

        FLD1            //делаем -2
        FADD ST, ST
        FCHS

        FMULP           //-2*(x^2)
        FDIVRP          //(-2*(x^2)) / (2*(n^2)) + (3*n) + 1
        FMULP           //предыдущий элемент умножаем на (-2*(x^2)) / (2*(n^2)) + (3*n) + 1
        FADD ST(1), ST  //прибавляем к сумме
        FLD1            //инкрементируем n
        FADDP ST(3), ST
        INC CH          //инкрементируем счетчик
        CMP CH, 19
        JL @2

        MOV CH, 0
        FFREEP ST    //освобождение регистра ФПУ
        FSTP QWORD PTR answers[EBX][8] //кладем вычисленную сумму в массив с ответами
        FFREEP ST

        FLDZ     // n
        FLDZ    //Сумма
        FLD1    //элемент
     @3:
        FLD ST(2)       //все тоже самое что и сверху
        FMUL ST, ST
        FADD ST, ST
        FLD ST(3)
        FADD ST, ST(4)
        FADD ST, ST(4)
        FADDP
        FLD1
        FADDP
        FLD ST(4)
        FMUL ST, ST
        FLD1
        FADD ST, ST
        FCHS
        FMULP
        FDIVRP
        FMULP
        FLD1
        FADDP ST(3), ST
        FLD ST           //тут берем вычисленный элемент для суммы
        FABS             //берем от его модуль для проверки на точность с эпселонт

        FCOM QWORD PTR e   //проверяем с эпселонт    if> rep
        FSTSW AX           //переносим флаги из ФПУ в ЦПУ (AX)
        SAHF               //загружаем флаги из AX в регистр флагов ЦПУ
        JB @4              //(если эпселонт больше, то просто выходим из цикла)
        FFREEP ST
        FADD ST(1), ST
        JMP @3

     @4:
        FFREEP ST
        FFREEP ST
        FSTP QWORD PTR answers[EBX][16]
        FFREEP ST

        FLD ST    //копируем x          //вычисление самой функции
        FCOS        //косинус от x
        FMUL ST, ST    //косинус в квадрате
        FLD1
        FSUBP ST(1)    //вычитаем 1
        FADD ST, ST    //умножаем на 2
        FSTP QWORD PTR answers[EBX][24]

        INC CL
        CMP CL, 10
        JL @1
    END;

    For i := 0 to 9 do
    begin
        Write('X=', answers[i,0]:1:2);
        Write('  SN=', answers[i,1]:1:4);
        Write('  SE=', answers[i,2]:1:4);
        Write('  Y=', answers[i,3]:1:4);
        Writeln();
    end;
    Readln();
END.
