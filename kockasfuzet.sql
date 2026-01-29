-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Jan 29. 09:39
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `kockasfuzet`
--
CREATE DATABASE IF NOT EXISTS `kockasfuzet` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `kockasfuzet`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szolgaltatas`
--

DROP TABLE IF EXISTS `szolgaltatas`;
CREATE TABLE `szolgaltatas` (
  `Azon` int(11) NOT NULL,
  `Nev` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szolgaltato`
--

DROP TABLE IF EXISTS `szolgaltato`;
CREATE TABLE `szolgaltato` (
  `RovidNev` varchar(8) NOT NULL,
  `Nev` varchar(256) NOT NULL,
  `UgyfelSzolgalat` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `szolgaltato`
--

INSERT INTO `szolgaltato` (`RovidNev`, `Nev`, `UgyfelSzolgalat`) VALUES
('ÉRV', 'Északmagyarországi Regionális Vízművek Zrt.', '3530 Miskolc, Corvin u. 2.'),
('MiVíz', 'MIVÍZ Kft.', '3530 Miskolc, Corvin u. 2.'),
('MVM Next', 'MVM Next Energiakereskedelmi Zrt', '3530 Miskolc, Arany János utca 6-8.'),
('Telecom', 'Magyar Telekom Nyrt.', '3525 Miskolc, Szentpáli utca 2 - 6.');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `számla`
--

DROP TABLE IF EXISTS `számla`;
CREATE TABLE `számla` (
  `ID` int(11) NOT NULL,
  `SzolgaltatasAzon` int(11) NOT NULL,
  `SzolgaltatoRovid` varchar(8) NOT NULL,
  `Tol` date NOT NULL,
  `Ig` date NOT NULL,
  `Osszeg` int(11) NOT NULL,
  `Hatarido` date NOT NULL,
  `Befizetve` date DEFAULT NULL,
  `Megjegyzes` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `szolgaltatas`
--
ALTER TABLE `szolgaltatas`
  ADD PRIMARY KEY (`Azon`);

--
-- A tábla indexei `szolgaltato`
--
ALTER TABLE `szolgaltato`
  ADD PRIMARY KEY (`RovidNev`);

--
-- A tábla indexei `számla`
--
ALTER TABLE `számla`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `SzolgaltatasAzon` (`SzolgaltatasAzon`),
  ADD KEY `SzolgaltatoRovid` (`SzolgaltatoRovid`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `szolgaltatas`
--
ALTER TABLE `szolgaltatas`
  MODIFY `Azon` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `számla`
--
ALTER TABLE `számla`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `számla`
--
ALTER TABLE `számla`
  ADD CONSTRAINT `számla_ibfk_1` FOREIGN KEY (`SzolgaltatasAzon`) REFERENCES `szolgaltatas` (`Azon`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `számla_ibfk_2` FOREIGN KEY (`SzolgaltatoRovid`) REFERENCES `szolgaltato` (`RovidNev`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
