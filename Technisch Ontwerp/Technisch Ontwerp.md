Technisch Ontwerp
===============

# Architectuur
## MVC
Aangezien er al een constraint is welke afdwingt dat Asp.net MVC 4 gebruikt wordt is de globale architectuur die gebruikt wordt het Model-View-Controller pattern.
Asp.net MVC bied al een basis voor het MVC pattern waarop wordt uitgebreidt.

Om code zo overzichtelijk mogelijk te houden dient er niet veel van het MVC pattern te worden afgeweken.
In veel MVC uitwerkingen ligt veel business logic in de models. Om de data opslag, data manipulatie en de data voor de views allemaal in de models te houden maakt de models onnodig dik.[1] Vaak gebeurt het dan dat de data manipulatie en de view data in de controller worden gestopt. Maar ook dit is sub optimaal.

De meeste MVC uitwerkingen:

* Models: Dik, veel logica
* Controllers: Dun, weinig logica
* Views: Dom, weinig tot geen logica, alleen presentatie

> I make sure my controller is merely the coach, in that it is neither the play (services) or the player (entity model or view model), but simply decides who plays what position and what play to make. [2] 

Indien een applicatie veel logica kent wil dat nog wel eens uit lekken naar de controllers.

Met als gevolg:

* Models: Dik, veel logica
* Controllers: Dik, veel logica
* Views: Dom, weinig (tot geen logica), alleen presentatie

Dit is een ongewenste situatie.

##Union Architecture
Daarom is er gekozen voor de volgende architectuur ook wel bekent als Union Architecture[3]

1. Data laag; Models met database gegevens
2. Data access laag; Repositories met Models (Data logic)
3. Service laag; Model manipulatie (Business logic)
4. Controllers Laag: Aansturing van service laag.
5. View Data laag; ViewModels, data voor gebruik in views
6. Presentatie laag; Views

De union architecture nogal gefocused op interfaces. Door het gebruik van interfaces onstaat er een losse koppeling tussen de lagen en zijn de onderdelen makkelijke te veranderen of te vervangen zonder een groot gedeelte van de code te hoeven veranderen.[4] De classes in de lagen hebben dus geen of weinig dependecies op de andere lagen. Een uitzondering hierop is de View data laag en de Presentatie laag aangezien een ViewModel geschreven wordt voor een specifice view en dus een harde koppeling heeft.

## Design Patterns
Om de data laag en de service laag te koppelen wordt er gebruik gemaakt het Repository Pattern.

Unity voor dependencie injection

# Framework
##Object Relation Mapper
Er zijn een aantal constraints waarmee de ORM over weg moet kunnen. Zo moet de ORM MSSQL spreken aangezien het gebruik van MSSQL is verplicht.

Eisen:

* Relatief simpel aangezien veel groepsleden onbekent zijn met C# en ORM's
* Veel informatie op het internet voor
* Relaties afdwingen in code
* MSSQL spreken

De twee meest voor de hand liggende ORM's zijn dan N-Hibernate en Entity Framework.
Uiteindelijk is er gekozen voor het Entity Framework omdat:

* Het een editor in Visual Studio heeft.
* Je kan zowel eerst code schrijven en daaruit automatisch een database genereren als anders om
* Entity framework is ondersteund door Microsoft dus veel ondersteuning en informatie
* Het Entity framework implenteerd al gedeeltelijk het Unit of Work en het Repository Pattern.

Hier onder staat het overzicht:

#Verwijzingen
1. http://joncairns.com/2013/04/fat-model-skinny-controller-is-a-load-of-rubbish/ 19-11-13
2. http://stackoverflow.com/a/8828946 19-11-13
3. http://rules.ssw.com.au/SoftwareDevelopment/RulesToBetterMVC/Pages/The-layers-of-the-onion-architecture.aspx 20-11-13
4. http://www.asp.net/mvc/tutorials/older-versions/models-(data)/validating-with-a-service-layer-cs 20-11-13

# Traceability

Al onze requirements afgedekt door de door ons 11 vastgelegde PUC’s. De mensen die de prototypes maken, deze lopen evenredig met de PUC’s, zijn verantwoordelijk voor de bijbehorende requirements. 

Hier onder staat het overzicht:

| PUC nr.       | PUC naam      | Requirements  |
| -------------:|:------------- | :-----|
| 1      | Medestudent beoordelen | 1.1.1  1.2.1  1.2.2  1.3.1  1.3.2  1.4.1  1.5.1  1.5.2  1.6.1  1.6.2  1.1.2  1.1.3  1.2.3  1.2.4 1.2.5  1.2.6  1.2.7  1.2.8  1.3.3  1.4.2  1.4.3  1.5.3  1.5.4  1.5.5 |
| 2      | Groeps indeling aanmaken      |   2.1.1  2.2.1  2.3.1  2.3.2  2.5.1  2.6.1  2.6.2   2.6.3  2.7.1  2.7.2  2.1.2  2.2.2  2.2.3  2.3.3  2.4.1  2.5.2  2.6.4 |
| 4 | Student eigen resultaat inzien      |    6.1.1  6.1.2  6.1.3  6.1.4  6.2.1  6.2.2  6.3.1  6.3.2  6.4.1  |
| 5 | Tutor groepen beoordeling inzien | 5.1.1  5.1.2  5.1.3  5.2.1  5.3.1  5.4.1  5.5.1  5.5.2  5.6.1  5.5.4  |
| 6 | Student cijfer toekennen | 6.1.1  6.1.2  6.1.3  6.1.4  6.2.1  6.2.2  6.3.1  6.3.2  6.4.1 |
| 7 | Student non-actief zetten | 7.1.1  7.2.1  7.2.2  7.3.1  7.3.2  7.4.1  7.5.1  7.6.1  7.6.2  7.6.3  7.7.1  7.7.2  7.7.3  7.1.2  7.1.3  7.2.3  7.5.2  7.6.4 |
| 8 | Een project aanmaken | 8.0.1  8.0.2  8.0.3  8.0.4  8.1.1  8.1.2  8.1.3  8.1.4  8.1.5  8.1.6  8.2.1  8.2.2  8.2.3  8.2.4  8.2.5  8.2.6  8.2.7 |
| 10 | Onderbouwing aanvragen | 10.1.1  10.2.1  10.3.1  10.4.1 |
| 11 | Ter beschikking stellen tutor toewijzing | 11.1.1  11.1.2  11.2.1  11.3.1  11.3.2  11.3.3  11.4.1  11.5.1  11.5.2  11.6.1  11.7.1  11.7.2 |
| 12 | Groepen toewijzen aan project | 12.1.1  12.1.2  12.1.3  12.2.1  12.3.1  12.3.2  12.4.1  12.4.2  12.4.3  12.5.1  12.5.2  12.1.4  12.1.5  12.1.6  12.2.2  12.3.3  12.4.4  12.4.5 |
| 13 | Mentor vooruitgang student inzien | 13.1.1  13.2.1  13.2.2  13.3.1  13.4.1  13.4.2  13.5.1  13.1.2  13.2.3  13.4.3 |

