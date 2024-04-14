-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 13. 19:18
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `kolcsonzesek`
--
CREATE DATABASE IF NOT EXISTS `kolcsonzesek` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `kolcsonzesek`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kolcsonzesek`
--

CREATE TABLE `kolcsonzesek` (
  `id` int(255) NOT NULL COMMENT 'Kölcsönzések azonositója',
  `kolcsonzokId` int(255) NOT NULL COMMENT 'Kölcsönzők azonositója',
  `iro` mediumtext CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `mufaj` mediumtext CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `cim` mediumtext CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kolcsonzok`
--

CREATE TABLE `kolcsonzok` (
  `id` int(255) NOT NULL COMMENT 'Kölcsönző azonositó',
  `nev` mediumtext CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT 'Kölcsönző neve',
  `szulIdo` datetime(6) NOT NULL COMMENT 'Kölcsönző születési ideje',
  `kolcsonzesek` int(255) NOT NULL COMMENT 'Kölcsönzései index'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `kolcsonzesek`
--
ALTER TABLE `kolcsonzesek`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kolcsonzokId` (`kolcsonzokId`);

--
-- A tábla indexei `kolcsonzok`
--
ALTER TABLE `kolcsonzok`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Kolcsonzesek` (`kolcsonzesek`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `kolcsonzesek`
--
ALTER TABLE `kolcsonzesek`
  ADD CONSTRAINT `kolcsonzesek_ibfk_1` FOREIGN KEY (`kolcsonzokId`) REFERENCES `kolcsonzok` (`id`);

--
-- Megkötések a táblához `kolcsonzok`
--
ALTER TABLE `kolcsonzok`
  ADD CONSTRAINT `kolcsonzok_ibfk_1` FOREIGN KEY (`kolcsonzesek`) REFERENCES `kolcsonzesek` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
