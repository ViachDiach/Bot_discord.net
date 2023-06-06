
CREATE table BotuserData(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    author VARCHAR(30),
    authorid VARCHAR(30),
    procent int,
    curse VARCHAR(30)
    );

INSERT INTO BotuserData (author, authorid, procent, curse) VALUES ('Author.Text','980377311801147414', '88', 'КРУТОЙ ЧЕЛ');

UPDATE BotuserData SET procent = 14 WHERE  author = 'graf_donbasskiy';

SELECT * FROM BotuserData;

DROP TABLE BotuserData;

////////////////////////////////////////////////////////////

CREATE table moneyRates(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    currency VARCHAR(5),
    value REAL
    );

INSERT INTO moneyRates (currency, value) VALUES ('kzt', 0.10);
UPDATE moneyRates SET currency = 0,17  WHERE  value = 'kzt';

SELECT * FROM moneyRates;

DROP TABLE moneyRates;

////////////////////////////////////////////////////////////

CREATE table Allusers(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    author VARCHAR(30),
    balanse int,
    inventory VARCHAR(30)
    );

CREATE table CoffeeMachine(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    author VARCHAR(30),
    price int,
    url VARCHAR(100)
    );



INSERT INTO CoffeeMachine (goods, price, url) VALUES ('Мохито', 2, 'https://img.freepik.com/premium-photo/vodka-or-gin-in-shot-glass-with-ice-decorated-with-strawberry-slice-and-thyme-on-travertine-podium_92695-2563.jpg');

UPDATE CoffeeMachine SET balanse = balanse + 3 WHERE  author = 'graf_donbasskiy'

SELECT * FROM CoffeeMachine;

DROP TABLE CoffeeMachine;

Пиво 
Вино
Мартини 
Виски

INSERT INTO Allusers (author, balanse, inventory) VALUES ('graf_donbasskiy', 2, 'Xni-Xny');
SELECT * FROM Allusers;
UPDATE Allusers SET balanse = balanse + 3 WHERE  author = 'graf_donbasskiy';
UPDATE Allusers SET balanse = balanse + 0.1 WHERE  author = 'graf_donbasskiy';

DROP TABLE Allusers;


 