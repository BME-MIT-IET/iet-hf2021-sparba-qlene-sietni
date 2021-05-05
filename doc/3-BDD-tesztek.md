## 3. feladat: BDD teszt - dokumentáció

### Felhasznált technológiák
A Visual Studio-ban telepítettük a SpecFlow extensiont, amelynek segítségével a Features és a Steps mappák hozzáadása után a mappákban létrehoztuk a különböző tesztfájlokat. A tesztekben a beépített könyvtárakon kívül főképp az RDFSharp projekt osztályait, valamint a SpecFlow és az NUnit osztályait használtuk.

### Elvégzett munka
BDD teszteket készítettünk a filterekhez, aggregátorokhoz, patternekhez és modifierekhez.
Ehhez először elkészítettük a scenario-kat, a SpecFlow segítségével létrehoztuk a lépéseket megvalósító osztályokat, majd a generált osztályokban és függvényekben implementáltuk a tesztek lépéseit.

### Eredmények és tanulságok
A Cucumbert használva felhasználóbarát, átlag ember számára olvasható scenario-kat lehet írni, amelyeket azután a SpecFlow segítségével egyszerűen lehet implementálni, így ha valakit érdekel egy lépés pontosabb leírása, a rendszer segítségével könnyen utánanézhet a lépések megvalósításának.
