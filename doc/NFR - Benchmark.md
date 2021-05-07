## Nem-funkcionális jellemzők vizsgálata: teljesítmény

### Felhasznált technológiák
A Visual Studio-ban telepítettük a BenchmarkRunner extensiont, amellyel egyszerűen elindíthatók a benchmark tesztek, amelyek lekódolásához és a benchmark projekt buildeléséhez a BenchmarkDotNet NuGet csomagot kellett letölteni, a projekthez hozzáadni (a solution-ön belül készítettünk egy új projektet a benchmark tesztek számára, ehhez adtuk hozzá).

### Elvégzett munka
Az RDFSharp könyvtár néhány funkciójának benchmark tesztjét implementáltuk. A BenchmarkDotNet könyvtár több funkcióját is használtuk: az elengedhetetlen, vagyis a benchmark-oláshoz szükséges `[Benchmark]` attribútumon kívül használtuk még a benchmark függvények parametrizálásánál használható `[Arguments]` és `[ArgumentsSource]` attribútumokat, amelyek segítségével a benchmark környezet a tesztfüggvényeket különböző argumentumokkal futtatja le; valamint használtuk a `[Setup]` és `[Cleanup]`-t is, amelyekkel a tesztfüggvény előtti inicializációt-, valamint azutáni cleanup-ot lehet elvégezni (például törölt dolgokat visszaállítani, mint ahogy tettük).

### Eredmények és tanulságok
A BenchmarkDotNet lehetőségeit kihasználva egyszerű teszteket írni a kódunk teljesítményének tesztelésére, emellet számos lehetőséget biztosít a program futása közbeni mindenféle statisztika, mint például ram használat, garbage collector futások száma, vagy a függvény futásidő kiíratására; a függvények összehasonlítására a futás után a konzolra kiírt táblázatban, sőt: ezeket az adatokat az artifacts mappába egy szöveges fájlba is kiírja későbbi használatra.
