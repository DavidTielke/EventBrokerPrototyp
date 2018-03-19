![Titel](/Images/Titel.png)
Im Folgenden findet Ihr das Visual Studio 2017 Projekt zum Let's Code "EventBroker mit TDD". 
Ich versuche nach jeder Episode einen Push zu machen, wenn Ihr also erst ab Episode x mitmachen
wollt, könnt Ihr euch einfach den Checkin x-1 clonen.

## Motivation
### Let's Code?
Normalerweise werden Vorträge und Workshops sorgfältig ausgearbeitet und Themen und Beispiele
vorbereitet. In einem Let's Code ist das ein wenig anders: Nur das Thema ist mir bekannt, der 
Rest wird wie bei einer normalen Entwicklung Stück für Stück entwickelt. Quasi wie beim Pair-Programming,
nur das Ihr als stiller Beobachter dabei seid :)

### Warum das Thema?
Eine Möglichkeit der nachrichtenbasierten Kommunikation wird in nahezu jedem Projekt benötigt,
und sorgt auf lange Sicht hin, für ein Projekt, welches einfacher gewartet und erweitert werden 
kann und wesentlich flexibler ist. Während meinen Beratungsprojekten gibt es immer wieder die
Notwendigkeit eines EventBrokers und so entstand die Idee zu diesem Let's Code: Einen EventBroker
mit TDD entwickeln, einfach, verständlich und mit einem hohen Lerneffekt. In erster Linie ist diese
Serie dazu gedacht, die entsprechenden Videos an meine Kunden weiterzugeben um diese bei Ihren Projekten
zu unterstützen. Da die Videos auf YouTube gehostet werden, kann aber natürlich auch jeder andere
darauf zugreifen und diese und auch die Quellcodes hier, in den eigenen Projekten benutzen.

## Anforderungen
- [x] Anf1: Die Registrierung für eine Nachricht sollte mit einem Lambda-Ausdruck erfolgen. **(erfüllt in Episode 1)**
- [ ] Anf2: Beim eintreffen einer Nachricht, sollte idese an alle Subscriber weitergeleitet werden.
- [ ] Anf3: Durch angabe eines Filters soll es möglich sein, eine Nachricht nur unter bestimmten Bedingungen zu erhalten.
- [ ] Anf4: Die Registrierung für einen noch nicht erstellten EventHandler muss möglich sein.
- [ ] Anf5: Falls ein Subscriber beim behandeln einer Nachricht eine Exception auslöst, darf dies nicht die restlichen Subscriber beeinflussen
- [ ] Anf6: Optionale Möglichkeit von WeakReferences zu Subscribern mit Weak<T> umsetzen.

## Erscheinungstermine
Jeweils Montag bis Freitag gibt es jeden Morgen um 8:30 Uhr eine neue Episode.

## Episoden

[Hier](https://www.youtube.com/playlist?list=PLl90zba6gg1-S9d0A9CMCC2DBRNGi90JK) findet Ihr die gesamte Playlist auf YouTube

### Episode 1 - Theorie und Anforderungen
[![E1 - Test](https://img.youtube.com/vi/zKwGwdlBxgs/0.jpg)](https://www.youtube.com/watch?v=zKwGwdlBxgs&list=PLl90zba6gg1-S9d0A9CMCC2DBRNGi90JK&index=1&t=0s)  
* Commit: Keiner...

### Episode 2 - Das Projekt aufsetzen
[![E1 - Test](https://img.youtube.com/vi/wczgFW10XBI/0.jpg)](https://www.youtube.com/watch?v=wczgFW10XBI)  
* Commit: [hier](https://github.com/DavidTielke/EventBrokerPrototyp/commit/462dc7b6bf4822b3027a340d9b4fee3b544b9d0f) 

### Episode 3 - Die erste Subscription
[![E1 - Test](https://img.youtube.com/vi/wNkC1RaeUFU/0.jpg)](https://www.youtube.com/watch?v=wNkC1RaeUFU&list=PLl90zba6gg1-S9d0A9CMCC2DBRNGi90JK&index=1&t=0s)  
* Commit: [hier](https://github.com/DavidTielke/EventBrokerPrototyp/commit/160f9e9e8ce22b84c06c6f4183621541b2a7cf1a)

### Episode 4 - Doppelte Handler
[![E1 - Test](https://img.youtube.com/vi/TAju_j6wS3s/0.jpg)](https://www.youtube.com/watch?v=TAju_j6wS3s&list=PLl90zba6gg1-S9d0A9CMCC2DBRNGi90JK&index=1&t=0s)  
* Commit: [hier](https://github.com/DavidTielke/EventBrokerPrototyp/commit/2fc4664d2014cf85135c5073ec8f273a0fc49194)
