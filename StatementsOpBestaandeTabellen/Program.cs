//1. Welke brouwers hebben een omzet van minstens 140.000?

USE javadevt_DotNetVlaanderen4;

SELECT*
FROM Brewers
WHERE Turnover >= 140000;

//2. Geef de Id en naam van alle bieren waarbij de naam “fontein” bevat
USE javadevt_DotNetVlaanderen4;

SELECT Id, Name 
FROM Beers
WHERE Name LIKE "%fontein%" OR "%Fontein%";

//3. Toon de 5 duurste bieren van de brouwer met de meeste omzet
USE javadevt_DotNetVlaanderen4;

SELECT BrewerID, Brewers.Name AS BrewerName, Turnover, Beers.Name AS BeerName, Price
FROM Brewers 
    INNER JOIN Beers 
    ON Brewers.ID = Beers.BrewerID
ORDER BY Turnover DESC, Price DESC
LIMIT 5;

//3a. Toon ook de 5 duurste bieren van de brouwer met de minste omzet
USE javadevt_DotNetVlaanderen4;

SELECT BrewerID, Brewers.Name AS BrewerName, Turnover, Beers.Name AS BeerName, Price
FROM Brewers 
    INNER JOIN Beers 
    ON Brewers.ID = Beers.BrewerID
ORDER BY Turnover ASC, Price DESC
LIMIT 5;

//4. Geef de namen van alle brouwers die in Sint-Jans-Molenbeek, Leuven of Antwerpen wonen
//4a. Toon ook de naam en het alcoholpercentage van hun bieren
//4b. Sorteer op omzet
USE javadevt_DotNetVlaanderen4;

SELECT Brewers.Name AS BrewerName, City, Beers.Name as BeerName, Alcohol
FROM Brewers 
    INNER JOIN Beers 
    ON Brewers.Id = Beers.BrewerID
WHERE City IN ( "Sint-Jans-Molenbeek", "Leuven", "Antwerpen")
ORDER BY Turnover;

//5. Geef het aantal bieren per brouwer
//5a. Toon ook de naam van de brouwerij
//5b. Toon enkel de brouwerijen die meer dan 5 bieren brouwen
//5c. Sorteer van hoogste aantal bieren per brouwerij naar het minste
USE javadevt_DotNetVlaanderen4;

SELECT Brewers.Name AS BrewerName, Count(Beers.Name) AS TotalBeers
FROM Brewers 
    INNER JOIN Beers 
    ON Brewers.Id = Beers.BrewerId
GROUP BY Brewers.Name
    HAVING TotalBeers >= 5
ORDER BY TotalBeers DESC;

//6. Toon alle informatie van de bieren met een percentage van minstens 7%
//6a.Toon ook alle gerelateerde data.
//6a1.Vermijd duplicate data.
//6a2. Geef bij kolommen met dezelfde namen een duidelijke beschrijving
//6b. Sorteer aflopend op het alcoholpercentage.
USE javadevt_DotNetVlaanderen4;

SELECT Beers.Id AS BeerId, Beers.Name AS BeerName, Brewers.Name AS BrewerName, Categories.Category AS CategoryName, Price, Stock, Alcohol, Version
FROM Beers
LEFT JOIN Categories ON Beers.CategoryId = Categories.Id
LEFT JOIN Brewers ON Beers.BrewerId = Brewers.Id
WHERE Alcohol >= 7
ORDER BY Alcohol DESC;

//7. Geef de bieren met een alcoholpercentage van meer dan 7 procent van de brouwers die meer dan 65.000 omzet verdienen
//7a. Sorteer op omzet, daarna op alcoholpercentage
//7b. Toon ook de categorie van deze bieren
USE javadevt_DotNetVlaanderen4;

SELECT Beers.Id AS BeerId, Beers.Name AS BeerName, Beers.Alcohol, Brewers.Name, Brewers.Turnover, Categories.Category
FROM Beers
LEFT JOIN Categories ON Beers.CategoryId = Categories.Id
LEFT JOIN Brewers ON Beers.BrewerId = Brewers.Id
WHERE Alcohol >= 7 AND Turnover >= 65000
ORDER BY Turnover DESC, Alcohol DESC;

//8. Geef het aantal bieren per categorie in een menselijk leesbare vorm.
//8a. Sorteer op naam van de categorie
//8b. Toon enkel de volgende bieren: Lambik,  AlcolholVrij, Pils, Edelbier, Amber, Light
//8b1. Hou er rekening mee dat bovenstaande selectie makkelijk kan veranderen in de toekomst.
//8b2. Hoe zorgen we ervoor dat deze lijst makkelijk leesbaar en aanpasbaar is?
USE javadevt_DotNetVlaanderen4;

SELECT Categories.Category, Beers.Name AS BeerName, Beers.Price, Beers.Stock, Beers.Alcohol
FROM Beers
LEFT JOIN Categories ON Beers.CategoryId = Categories.Id
WHERE Category IN ("Lambik", "Alcoholvrij", "Pils", "Edelbier", "Amber", "Light")
ORDER BY Category;

//9. Toon de bieren die door meer dan 1 brouwerij gebrouwen worden.
//9a. Toon alle relevante gerelateerde data.
//9b. Sla dit resultaat op als een View
//9b1. Toon aan dat je view dynamisch is door data te wijzigen.
//9b2. Neem screenshots van voor en na.
USE javadevt_DotNetVlaanderen4;

SELECT Id, Name, CategoryId, Price, Stock, Alcohol, Count(BrewerId) AS AmountBrewers
FROM Beers
GROUP BY Name
HAVING AmountBrewers >1;

//Maak een Stored Procedure met 2 input parameters. Laat deze query alle bieren ophalen die aan de volgende parameters voldoen:
//Een keyword dat voorkomt in de naam van het bier
//De maximum omzet van de brouwer
//Het is niet voldoende om enkel de Stored Procedure in je DB te hebben. Toon ook de SQL code die je nodig had voor dit statement te maken.
CREATE DEFINER =`javadevt_StudVla`@`%` PROCEDURE `BeersKeywordMaxTurnover`(keyword varchar(30), maxTurnover int(10))

SELECT Beers.*, Brewers.Name, Brewers.Turnover
FROM Beers
INNER JOIN Brewers ON Beers.BrewerId = Brewers.Id
WHERE Beers.Name LIKE Concat('%', keyword,'%')
AND Turnover<=maxTurnover




    QUERIES OP NIEUWE TABELLEN

//Toon de Brouwers die aan geen enkel cafe leveren.


//Toon alle cafes die geopend zijn in de voorbije 3 jaar.
//Toon ook de bieren en hun brouwers die hier getapt worden.
//Sla deze informatie op als View
USE javadevt_DotNetVlaanderen4;

CREATE VIEW ViewRecentBarsxBeers AS
SELECT Bars.BarId, OpeningYear, Beers.Name AS BeerName, CategoryId, Alcohol, Brewers.Name
FROM Bars
INNER JOIN BarsxBeers ON Bars.BarId = BarsxBeers.BarId
INNER JOIN Beers ON BarsxBeers.BeerId = Beers.Id
INNER JOIN Brewers ON Beers.BrewerId = Brewers.Id
WHERE OpeningYear >= 2019;


//Toon het aantal eigenaars per cafe.
USE javadevt_DotNetVlaanderen4;
SELECT Bars.Name, Count(OwnerId)
FROM Bars
INNER JOIN Owners ON Bars.Owner = Owners.OwnerId
GROUP BY Bars.Name;

//Geef de namen van alle brouwers die in een stad wonen waar meer dan 2 cafe’s zijn.


//Toon alle cafés met mannelijke eigenaars die binnen 5 jaar op pensioen zullen gaan
//Hiervoor mag je uitgaan dat de pensioenleeftijd 65 jaar is en ze verjaren op 1 januari
USE javadevt_DotNetVlaanderen4;

SELECT *
FROM Bars
INNER JOIN Owners ON Bars.Owner = Owners.OwnerId
WHERE Owners.BirthYear BETWEEN  1957 AND 1962
AND Owners.Gender = 'M';