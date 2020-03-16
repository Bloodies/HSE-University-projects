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
        MOV ECX, 0 //���稪
        MOV EAX, 0 //��� ���᫥��� ᬥ饭��
        MOV EBX, 0 //�࠭����� ᬥ饭�� �� ��ப��
        FINIT
        FLD QWORD PTR answers[0] //����� � FPU x                                 //4
     @1:
        FLD QWORD PTR stepX      //����� � ��� 蠣 �                             //3
        FADDP                    // �ਡ���塞 � � 蠣 � � ���⠥� �������
        MOV AL, 32               //���᫥��� ᬥ饭�� �� ��મ��
        MUL CL
        MOV EBX, EAX
        FST QWORD PTR answers[EBX][0]   //������ � ��ப� ���祭�� �

        FLDZ  //������ n                                                         //2
        FLDZ  //�㬬� �鸞                                                       //1
        FLD1  //����� �鸞                                                     //0
     @2:           //���᫥��� �� ��������� n
        FLD ST(2)       //��७�ᨬ n
        FMUL ST, ST     //������ n
        FADD ST, ST     // 2 * (n^2)
        FLD ST(3)       //��७�ᨬ n
        FADD ST, ST(4)  //��室�� 3 * n
        FADD ST, ST(4)
        FADDP           // (2*(n^2)) + (3*n)
       // FLD1            //����㦠�� 1
       // FADDP           // (2*(n^2)) + (3*n) + 1
        FLD ST(4)       //��७�ᨬ �
        FMUL ST, ST     //x^2

        FLD1            //������ -2
        FADD ST, ST
        FCHS

        FMULP           //-2*(x^2)
        FDIVRP          //(-2*(x^2)) / (2*(n^2)) + (3*n) + 1
        FMULP           //�।��騩 ����� 㬭����� �� (-2*(x^2)) / (2*(n^2)) + (3*n) + 1
        FADD ST(1), ST  //�ਡ���塞 � �㬬�
        FLD1            //���६����㥬 n
        FADDP ST(3), ST
        INC CH          //���६����㥬 ���稪
        CMP CH, 19
        JL @2

        MOV CH, 0
        FFREEP ST    //�᢮�������� ॣ���� ���
        FSTP QWORD PTR answers[EBX][8] //������ ���᫥���� �㬬� � ���ᨢ � �⢥⠬�
        FFREEP ST

        FLDZ     // n
        FLDZ    //�㬬�
        FLD1    //�����
     @3:
        FLD ST(2)       //�� ⮦� ᠬ�� �� � ᢥ���
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
        FLD ST           //��� ��६ ���᫥��� ����� ��� �㬬�
        FABS             //��६ �� ��� ����� ��� �஢�ન �� �筮��� � �ᥫ���

        FCOM QWORD PTR e   //�஢��塞 � �ᥫ���    if> rep
        FSTSW AX           //��७�ᨬ 䫠�� �� ��� � ��� (AX)
        SAHF               //����㦠�� 䫠�� �� AX � ॣ���� 䫠��� ���
        JB @4              //(�᫨ �ᥫ��� �����, � ���� ��室�� �� 横��)
        FFREEP ST
        FADD ST(1), ST
        JMP @3

     @4:
        FFREEP ST
        FFREEP ST
        FSTP QWORD PTR answers[EBX][16]
        FFREEP ST

        FLD ST    //�����㥬 x          //���᫥��� ᠬ�� �㭪樨
        FCOS        //��ᨭ�� �� x
        FMUL ST, ST    //��ᨭ�� � ������
        FLD1
        FSUBP ST(1)    //���⠥� 1
        FADD ST, ST    //㬭����� �� 2
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
