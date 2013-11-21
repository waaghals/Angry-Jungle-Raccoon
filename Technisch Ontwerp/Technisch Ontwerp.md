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

#Source control

##Git 

Om met elkaar samen aan het systeem te kunnen werken hebben wij voor Git gekozen als source control oplossing. We hebben hiervoor gekozen omdat iedereen al een keer heeft gewerkt met Git in eerdere projecten en iedereen dus al de nodige ervaring heeft met Git om meteen aan de slag te kunnen met het project.

##Git flow

Om te voorkomen dat we conflicten krijgen als we tegelijkertijd aan het werk zijn gebruiken we een speciale manier van het omgaan met de bestanden in de repository. Deze oplossing genaamd 'Git flow' houdt in dat we gebruik maken van verschillende branches. Elk onderdeel van het systeem heeft een apparte branch. Deze onderdelen noemen we features. Als je aan een nieuwe feature begint maak je vanuit de developer branch een nieuwe sub-tak genaamd feature/myfeature, met als myfeature het onderdeel waar je aan gaat werken. Is de feature klaar dan wordt deze toegevoegd aan de developers branch. Hier wordt alles een geheel en dat geheel wordt aan de release branch toegevoed waar eventueel nog bugfixes uitgevoerd kunnen worden. De inlever versie van het systeem komt in de master branch. Als we voor de oplevering nog ergens een fout tegen komen wordt deze gefixt in de hotfix branch en de hotfix wordt daarna aan de master en develop branches toegevoegd.

##SourceTree

Om iedereen een gelijke en makkelijk te gebruiken omgeving te geven voor het gebruik van Git flow gebruiken we het programma SourceTree. Dit is een Git gui die git flow ondersteund. We hebben hier ook voor gekozen omdat SourceTree meerdere platformen ondersteund.

#Coding guidelines 

Om de code overzichtelijk en eenduidig te houden heeft Patrick een stijlgids gemaakt waarin de stijl en naamgeving wordt uitgelegd die we aan gaan houden. Ook staan hierin regels opgesteld voor het aanmaken van bestanden en mappen om de folderstructuur overzichtelijk te houden. We hebben dan ook allemaal afgesproken afgesproken onszelf aan de guidelines te houden zodat we het niet af hoeven te dwingen met een programma. Tijdens de code reviews wordt er dan ook op gelet dat de code aan de guidelines voldoet.

#Verwijzingen
1. http://joncairns.com/2013/04/fat-model-skinny-controller-is-a-load-of-rubbish/ 19-11-13
2. http://stackoverflow.com/a/8828946 19-11-13
3. http://rules.ssw.com.au/SoftwareDevelopment/RulesToBetterMVC/Pages/The-layers-of-the-onion-architecture.aspx 20-11-13
4. http://www.asp.net/mvc/tutorials/older-versions/models-(data)/validating-with-a-service-layer-cs 20-11-13