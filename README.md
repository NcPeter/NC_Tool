NC-Tool ist ein G-Code Generator.

Mit Hilfe von intuitiv zu bedienenden Zyklen
können viele kleinere Fräsaufgaben auf der heimischen
Fräsmaschine ausgeführt werden.

Die Eingaben werden für den Bediener in Bild und Text unterstützt.

![image](https://github.com/NcPeter/NC_Tool/assets/156658983/4d76bc65-a869-4999-996c-1f8d1f3c1885)

Verfügbare Zyklen:
1. Flächenbearbeitung
    - Planfräsen
    - schräge Flächen fräsen
2. Standardbohren / Gewinde fräsen
    - Tiefloch bohren
    - Gewinde fräsen
3. Taschen
    - Rechtecktasche fräsen
    - Kreistasche fräsen
4. Zapfen
    - Rechteckzapfen fräsen
    - Kreiszapfen fräsen
5. Nuten
    - einfache Nut fräsen
    - Ringnut fräsen
    - Dichtungsnut (rechteckig) fräsen
    - runde Nut (gebogene Nut) fräsen
6. Bohrbilder
    - Bohrtabelle
    - Bohrbild Lochkreis
    - Bohrbild auf Linien (Lochmuster)
7. Sonderzyklen
    - Buchstaben und Schriftzüge gravieren
    - 2D DXF-Daten gravieren

Die Zyklen sind denen von Heidenhain iTNC530 nachempfunden, wurden aber komplett
neu programmiert und optimiert. So wird immer im Gleichlauf gefräst und bei
Taschen entfällt das Vorbohren in der Mitte, da die Werkzeuge auf einer Helixbahn in die
Zustelltiefe fahren.

Die Ausgabe  erfolgt als Standard G-Code und ist im Ausgabefenster noch editierbar.
Der G-Code wird zudem grafisch als Fräsbahn dargestellt.



Bisher gibt es einstellbar die Sprachen: deutsch und englisch

Dies ist mein erstes Projekt in C#, Teile wurden aus VB übersetzt.
Ich bin noch nicht so vertraut mit C# und ganz sicher nicht perfekt.

;-)

Liebe Grüße, Peter.
