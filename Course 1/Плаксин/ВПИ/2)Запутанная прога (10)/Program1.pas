program p;
var ol, o1, tmp: real;   {!!!}
    ll, i1: integer;
    l1: integer;
begin
    readln(o1);
    o1:= o1*0.10;  {ghbrjk!}
    ol:= 1;
    ll:= 1;
    l1:= -1;
    repeat tmp:= Ol;  {временно}
       ll:=ll + 2;
       ol:= tmp+l1/ll;
       l1:= -l1;
    until abs(ol-tmp) < O1;
    writeln(o1*10{маразм},ol*4,ll);
end.