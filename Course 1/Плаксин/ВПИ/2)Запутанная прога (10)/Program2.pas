program p;
var b, a, tmp: real;   {!!!}
    c, i1: integer;
    d: integer;
begin
    readln(a);
    a:= a*0.10;  {ghbrjk!}
    b:= 1;
    c:= 1;
    d:= -1;
    repeat tmp:= b;  {временно}
       c:=c + 2;
       b:= tmp+d/c;
       d:= -d;
    until abs(b-tmp) < a;
    writeln(a*10{маразм});
    writeln(b*4);
    writeln(c);
end.