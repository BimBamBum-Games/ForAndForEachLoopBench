# ForAndForEachLoopBench
Unity For Loop and ForEach Bench (Haluk OZGEN)
Kucuk miktarlarda yapilan GC olcumlerinde ForEach loop garbage uretmedigi gozlenmektedir. Liste uzunlugu artirildiginda loop dongusu tamamlanmasi icin gecen sure artacagindan garbage olusumu profiler uzerinde gozlemlenememekle birlikte sayisal olarak bir farklilik yakalandigi gercektir. Deep Profile ile bakildiginda BeginForEach metodu icersinde garbage olusumu yoktur.
Ayrica ForEach loop index gezinme hizi For loop hizina gore muazzam derecede yuksek seviyelerdedir.
