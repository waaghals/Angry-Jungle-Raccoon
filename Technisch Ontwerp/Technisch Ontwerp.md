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
4. Commando laag; Error checking, validatie en eventueel uitvoeren van de service laag.
5. Controllers Laag: Aansturing van commando's en service laag.
6. View Data laag; ViewModels, data voor gebruik in views
7. Presentatie laag; Views

Niet elke laag hoeft er altijd te zijn, zo is bijvoorbeeld niet altijd de commando of de view data laag nodig.
Tevens is de union architecture nogal gefocused op interfaces. Door het gebruik van interfaces onstaat er een losse koppeling tussen de lagen en zijn de onderdelen makkelijke te veranderen of te vervangen zonder een groot gedeelte van de code te hoeven veranderen.


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

#Verwijzingen
[1] http://joncairns.com/2013/04/fat-model-skinny-controller-is-a-load-of-rubbish/
[2] http://stackoverflow.com/a/8828946
[3] http://rules.ssw.com.au/SoftwareDevelopment/RulesToBetterMVC/Pages/The-layers-of-the-onion-architecture.aspx