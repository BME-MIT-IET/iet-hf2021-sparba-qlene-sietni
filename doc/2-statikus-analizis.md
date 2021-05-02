# 2. feladat: Statikus analízis

Ehhez a feladathoz az 5. gyakorlaton látott SonarCloud-ot használtuk. Ehhez tanári segítséget kellett kérni, mert tulajdonosi jogosultság szükséges a SonarCloud beüzemeléséhez. Ezek után minden ugyanúgy történt, mint a gyakorlaton, annyi kivétellel, hogy a build-nél maven helyett a .NET-es verziót választottuk. Itt a <your clean build command> helyére bekerült + 3 sor, amiket ezen az oldalon ajánlottak: https://www.codewrecks.com/post/github/github-sonar-cloud/ . Mi a .sln fájlokat nem írtuk bele, mert automatikusan felismerte azokat. Ennek a buildnek a lefutása után kaptunk 8 bugot és 898 code smell-t.
  
## Bugok:

A 8 bug össszesen 2 típúsból állt:

- NullReferenceException: Ennél a bugnál azért jelzett, mert olyan objektumokon voltak meghívva függvények, hivatkozások amikre esély volt, hogy azok nullok is lehetnek, így NullReferenceException-t dobott volna. A megoldása az, ha ennek az objetumnak a használata előtt leellenőrizzük, hogy az nem null-e és csak akkor használjuk.

- Újra deklarálás: Ez valójában nem tekinthető bugnak, de valóban átláthatóbb a kód ennek a hibának az eltűntetésével. Itt a SonarCloud azért jelzett bugot, mert azt érzékelte, hogy egy változónak egymást követő sorokban értéket adunk és úgy vélte, hogy ez felesleges, mert úgyis csak az utolsó értéket fogja megkapni. Azt azonban nem vette figyelembe, hogy úgy adunk értéket egymás után egy változónak, hogy magát a változót mindig felhasználjuk hozzá és sorban alakítgatjuk, lépésről lépésre. A megoldás az, hogy nem saját magát alakítgatja a változó, hanem minden egyes új változáshoz új objektumot hozunk létre, hogy az utolsó változás után a legutóbb létrejött változónak az értékét átadhassuk annak, aminek eredetileg át akartuk írni az értékét.

## Code Smellek:

Rengeteg code smell-t jelzett a SonarCloud, ezek közt szerepltek többségében:

- Kikommentezett kódok
- Mergel-ni kívánt `if` állítások
- Pontos `Exception` típus meghatározása
- Class-ok `seal`-elése
- Class-ok átnevezése, refaktorálása, hogy megfeleljen a szabályoknak
- Függvények komplexitásának csökkentése
- Class-ok leszármaztatásának megváltozatása / új class-ok létrehozása a megfelelő célra

Nem az összeset néztük meg, hiszen egy ekkora méretű projektnél az lehetetlen lett volna. Olyan hibába, ami valós problémát jelentene, nem ütköztünk.