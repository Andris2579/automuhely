-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Sze 17. 10:55
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
-- Adatbázis: `automuhely`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `alkatreszek`
--

CREATE TABLE IF NOT EXISTS `alkatreszek` (
  `alkatresz_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `keszlet_mennyiseg` int(11) DEFAULT NULL,
  `utanrendelesi_szint` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `alkatresz_rendelesek`
--

CREATE TABLE IF NOT EXISTS `alkatresz_rendelesek` (
  `rendelés_id` int(11) NOT NULL,
  `alkatresz_id` int(11) DEFAULT NULL,
  `beszallito_id` int(11) DEFAULT NULL,
  `rendeles_datum` datetime DEFAULT NULL,
  `rendelt_mennyiseg` int(11) DEFAULT NULL,
  `allapot` enum('Rendelés alatt','Szállítva','Teljesítve') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `beszallitok`
--

CREATE TABLE IF NOT EXISTS `beszallitok` (
  `beszallito_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `elerhetoseg` varchar(150) DEFAULT NULL,
  `cim` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE IF NOT EXISTS `felhasznalok` (
  `felhasznalo_id` int(11) NOT NULL,
  `felhasznalonev` varchar(50) DEFAULT NULL,
  `jelszo_hash` varchar(255) DEFAULT NULL,
  `szerep` enum('Adminisztrátor','Szerelő','Ügyfél') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok_ugyfelek`
--

CREATE TABLE IF NOT EXISTS `felhasznalok_ugyfelek` (
  `felhasznalo_id` int(11) NOT NULL,
  `ugyfel_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `hibakodok`
--

CREATE TABLE IF NOT EXISTS `hibakodok` (
  `kod_id` int(11) NOT NULL,
  `kod` varchar(10) DEFAULT NULL,
  `leiras` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `idopontfoglalasok`
--

CREATE TABLE IF NOT EXISTS `idopontfoglalasok` (
  `idopont_id` int(11) NOT NULL,
  `jarmu_id` int(11) DEFAULT NULL,
  `csomag_id` int(11) DEFAULT NULL,
  `idopont` datetime DEFAULT NULL,
  `allapot` enum('Foglalt','Folyamatban','Befejezett','Lemondva') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jarmuvek`
--

CREATE TABLE IF NOT EXISTS `jarmuvek` (
  `jarmu_id` int(11) NOT NULL,
  `rendszam` varchar(10) DEFAULT NULL,
  `tipus_id` int(11) DEFAULT NULL,
  `kod_id` int(11) DEFAULT NULL,
  `sablon_id` int(11) DEFAULT NULL,
  `gyartas_eve` year(4) DEFAULT NULL,
  `motor_adatok` varchar(100) DEFAULT NULL,
  `alvaz_adatok` varchar(100) DEFAULT NULL,
  `elozo_javitasok` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `munkafolyamat_sablonok`
--

CREATE TABLE IF NOT EXISTS `munkafolyamat_sablonok` (
  `sablon_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `lepesek` text DEFAULT NULL,
  `becsult_ido` time DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szerelesi_utmutatok`
--

CREATE TABLE IF NOT EXISTS `szerelesi_utmutatok` (
  `utmutato_id` int(11) NOT NULL,
  `cim` varchar(100) DEFAULT NULL,
  `tartalom` text DEFAULT NULL,
  `jarmu_tipus` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szervizcsomagok`
--

CREATE TABLE IF NOT EXISTS `szervizcsomagok` (
  `csomag_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `ar` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tipus`
--

CREATE TABLE IF NOT EXISTS `tipus` (
  `tipus_id` int(11) NOT NULL,
  `tipus` varchar(50) DEFAULT NULL,
  `utmutato_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfelek`
--

CREATE TABLE IF NOT EXISTS `ugyfelek` (
  `ugyfel_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `elerhetoseg` varchar(150) DEFAULT NULL,
  `cim` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfel_jarmuvek`
--

CREATE TABLE IF NOT EXISTS `ugyfel_jarmuvek` (
  `ugyfel_id` int(11) NOT NULL,
  `jarmu_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `alkatreszek`
--
ALTER TABLE `alkatreszek`
  ADD PRIMARY KEY (`alkatresz_id`);

--
-- A tábla indexei `alkatresz_rendelesek`
--
ALTER TABLE `alkatresz_rendelesek`
  ADD PRIMARY KEY (`rendelés_id`),
  ADD KEY `alkatresz_id` (`alkatresz_id`),
  ADD KEY `beszallito_id` (`beszallito_id`);

--
-- A tábla indexei `beszallitok`
--
ALTER TABLE `beszallitok`
  ADD PRIMARY KEY (`beszallito_id`);

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`felhasznalo_id`),
  ADD UNIQUE KEY `felhasznalonev` (`felhasznalonev`);

--
-- A tábla indexei `felhasznalok_ugyfelek`
--
ALTER TABLE `felhasznalok_ugyfelek`
  ADD PRIMARY KEY (`felhasznalo_id`,`ugyfel_id`),
  ADD KEY `ugyfel_id` (`ugyfel_id`);

--
-- A tábla indexei `hibakodok`
--
ALTER TABLE `hibakodok`
  ADD PRIMARY KEY (`kod_id`);

--
-- A tábla indexei `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD PRIMARY KEY (`idopont_id`),
  ADD KEY `jarmu_id` (`jarmu_id`),
  ADD KEY `csomag_id` (`csomag_id`);

--
-- A tábla indexei `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD PRIMARY KEY (`jarmu_id`),
  ADD UNIQUE KEY `rendszam` (`rendszam`),
  ADD KEY `tipus_id` (`tipus_id`),
  ADD KEY `kod_id` (`kod_id`),
  ADD KEY `sablon_id` (`sablon_id`);

--
-- A tábla indexei `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  ADD PRIMARY KEY (`sablon_id`);

--
-- A tábla indexei `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD PRIMARY KEY (`utmutato_id`);

--
-- A tábla indexei `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  ADD PRIMARY KEY (`csomag_id`);

--
-- A tábla indexei `tipus`
--
ALTER TABLE `tipus`
  ADD PRIMARY KEY (`tipus_id`),
  ADD KEY `utmutato_id` (`utmutato_id`);

--
-- A tábla indexei `ugyfelek`
--
ALTER TABLE `ugyfelek`
  ADD PRIMARY KEY (`ugyfel_id`);

--
-- A tábla indexei `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD PRIMARY KEY (`ugyfel_id`,`jarmu_id`),
  ADD KEY `jarmu_id` (`jarmu_id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `alkatreszek`
--
ALTER TABLE `alkatreszek`
  MODIFY `alkatresz_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `alkatresz_rendelesek`
--
ALTER TABLE `alkatresz_rendelesek`
  MODIFY `rendelés_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `beszallitok`
--
ALTER TABLE `beszallitok`
  MODIFY `beszallito_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `hibakodok`
--
ALTER TABLE `hibakodok`
  MODIFY `kod_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `idopont_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `jarmuvek`
--
ALTER TABLE `jarmuvek`
  MODIFY `jarmu_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  MODIFY `sablon_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  MODIFY `utmutato_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  MODIFY `csomag_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `ugyfel_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `alkatresz_rendelesek`
--
ALTER TABLE `alkatresz_rendelesek`
  ADD CONSTRAINT `alkatresz_rendelesek_ibfk_1` FOREIGN KEY (`alkatresz_id`) REFERENCES `alkatreszek` (`alkatresz_id`),
  ADD CONSTRAINT `alkatresz_rendelesek_ibfk_2` FOREIGN KEY (`beszallito_id`) REFERENCES `beszallitok` (`beszallito_id`);

--
-- Megkötések a táblához `felhasznalok_ugyfelek`
--
ALTER TABLE `felhasznalok_ugyfelek`
  ADD CONSTRAINT `felhasznalok_ugyfelek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `felhasznalok_ugyfelek_ibfk_2` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD CONSTRAINT `idopontfoglalasok_ibfk_1` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`),
  ADD CONSTRAINT `idopontfoglalasok_ibfk_2` FOREIGN KEY (`csomag_id`) REFERENCES `szervizcsomagok` (`csomag_id`);

--
-- Megkötések a táblához `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD CONSTRAINT `jarmuvek_ibfk_1` FOREIGN KEY (`tipus_id`) REFERENCES `tipus` (`tipus_id`),
  ADD CONSTRAINT `jarmuvek_ibfk_2` FOREIGN KEY (`kod_id`) REFERENCES `hibakodok` (`kod_id`),
  ADD CONSTRAINT `jarmuvek_ibfk_3` FOREIGN KEY (`sablon_id`) REFERENCES `munkafolyamat_sablonok` (`sablon_id`);

--
-- Megkötések a táblához `tipus`
--
ALTER TABLE `tipus`
  ADD CONSTRAINT `tipus_ibfk_1` FOREIGN KEY (`utmutato_id`) REFERENCES `szerelesi_utmutatok` (`utmutato_id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_1` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_2` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`) ON DELETE CASCADE;
COMMIT;