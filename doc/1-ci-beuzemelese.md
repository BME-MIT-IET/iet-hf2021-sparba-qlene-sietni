# 1. feladat: CI beüzemelése

A feladat elvégzéséhez **GitHub Actions**-t, azon belül a felajánlott .NET-es workflow-t használtuk. Ez a projekt a .NET Standard 2-t használja, mivel a GitHub Actions a legfrissebb .NET-tel (5.0.202) dolgozik, ami tartalmazza azt, így nem ütköztünk konfigurációs hibába a beüzemeléskor.

Mivel a build sikeres volt, ez azt jelenti, hogy a program hibamentes futásra képes, tehát csak különböző **bug**-okat, illetve **code smell**-eket kell javítanunk.