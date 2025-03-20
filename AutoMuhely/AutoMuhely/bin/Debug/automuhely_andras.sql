-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Már 20. 08:28
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


--
-- Adatbázis: `automuhely`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `alkatreszek`
--

CREATE TABLE `alkatreszek` (
  `alkatresz_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `keszlet_mennyiseg` int(11) DEFAULT NULL,
  `utanrendelesi_szint` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `felhasznalo_id` int(11) NOT NULL,
  `felhasznalonev` varchar(50) DEFAULT NULL,
  `jelszo_hash` varchar(255) DEFAULT NULL,
  `szerep` enum('Adminisztrátor','Szerelő','Ügyfél') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`felhasznalo_id`, `felhasznalonev`, `jelszo_hash`, `szerep`) VALUES
(1, 'Andris2579', '1e4fe302c5ebe8ad151dde5bdc33e21d5a3a76b4c50a26c366d1ba2dc892a32d', 'Ügyfél');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok_ugyfelek`
--

CREATE TABLE `felhasznalok_ugyfelek` (
  `felhasznalo_id` int(11) NOT NULL,
  `ugyfel_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `felhasznalok_ugyfelek`
--

INSERT INTO `felhasznalok_ugyfelek` (`felhasznalo_id`, `ugyfel_id`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `hibakodok`
--

CREATE TABLE `hibakodok` (
  `kod_id` int(11) NOT NULL,
  `kod` varchar(10) DEFAULT NULL,
  `leiras` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `idopontfoglalasok`
--

CREATE TABLE `idopontfoglalasok` (
  `idopont_id` int(11) NOT NULL,
  `jarmu_id` int(11) DEFAULT NULL,
  `csomag_id` int(11) DEFAULT NULL,
  `idopont` datetime DEFAULT NULL,
  `allapot` enum('Foglalt','Folyamatban','Befejezett','Lemondva') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `idopontfoglalasok`
--

INSERT INTO `idopontfoglalasok` (`idopont_id`, `jarmu_id`, `csomag_id`, `idopont`, `allapot`) VALUES
(1, 1, 1, NULL, 'Foglalt');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jarmuvek`
--

CREATE TABLE `jarmuvek` (
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

--
-- A tábla adatainak kiíratása `jarmuvek`
--

INSERT INTO `jarmuvek` (`jarmu_id`, `rendszam`, `tipus_id`, `kod_id`, `sablon_id`, `gyartas_eve`, `motor_adatok`, `alvaz_adatok`, `elozo_javitasok`) VALUES
(1, 'ABC-123', 1, NULL, NULL, NULL, NULL, NULL, '');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `marka`
--

CREATE TABLE `marka` (
  `marka_id` int(11) NOT NULL,
  `marka_neve` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `marka`
--

INSERT INTO `marka` (`marka_id`, `marka_neve`) VALUES
(1, 'Toyota'),
(2, 'BMW'),
(3, 'Ford');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `munkafolyamat_sablonok`
--

CREATE TABLE `munkafolyamat_sablonok` (
  `sablon_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `lepesek` text DEFAULT NULL,
  `becsult_ido` time DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendelesek`
--

CREATE TABLE `rendelesek` (
  `rendeles_id` int(11) NOT NULL,
  `felhasznalo_id` int(11) NOT NULL,
  `alkatresz_id` int(11) NOT NULL,
  `mennyiseg` int(11) NOT NULL,
  `statusz` enum('Leadva','Kérvényezve','Elutasítva') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szerelesi_utmutatok`
--

CREATE TABLE `szerelesi_utmutatok` (
  `utmutato_id` int(11) NOT NULL,
  `cim` varchar(100) DEFAULT NULL,
  `tartalom` text DEFAULT NULL,
  `jarmu_tipus` int(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szervizcsomagok`
--

CREATE TABLE `szervizcsomagok` (
  `csomag_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `ar` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `szervizcsomagok`
--

INSERT INTO `szervizcsomagok` (`csomag_id`, `nev`, `leiras`, `ar`) VALUES
(1, 'Olajcsere', 'A motorolaj időszakos cseréje elengedhetetlen a motor élettartamának növelése érdekében. Szolgáltatásunk során prémium minőségű olajjal és új olajszűrővel látjuk el járművét.', 20000.00),
(2, 'Fékrendszer ellenőrzés', 'A fékek megfelelő működése létfontosságú a biztonságos közlekedéshez. Ellenőrizzük a fékbetéteket, tárcsákat és a fékfolyadék szintjét, és szükség esetén cserét is végzünk.', 15000.00),
(3, 'Gumicsere', 'Nyári vagy téli gumik cseréje, centírozása és állapotfelmérése modern gépekkel. Biztosítjuk a megfelelő tapadást és futómű-geometriát a biztonságos vezetés érdekében.', 10000.00),
(4, 'Klímarendszer karbantartás', 'A klímaberendezés tisztítása és a hűtőközeg újratöltése biztosítja a megfelelő hűtési teljesítményt és a baktériummentes levegőt. Kellemetlen szagok és hatékonyságvesztés esetén ajánlott.', 25000.00),
(5, 'Futómű beállítás', 'A helytelen futómű-beállítás egyenetlen gumikopást és instabil vezetési élményt eredményezhet. Precíz mérőműszereinkkel pontos beállítást végzünk a hosszabb élettartam és jobb tapadás érdekében.', 18000.00),
(6, 'Motor diagnosztika', 'Modern számítógépes diagnosztikával kiolvassuk és elemezzük a motorvezérlő hibakódjait. Segítünk az ismeretlen motorhibák gyors feltárásában és a javítási lehetőségek meghatározásában.', 12000.00),
(7, 'Akkumulátor ellenőrzés', 'Akkumulátora állapotának felmérése kulcsfontosságú a megbízható indításhoz. Feszültség- és kapacitásméréssel meghatározzuk, hogy szükség van-e töltésre vagy cserére.', 8000.00),
(8, 'Vezérműszíj csere', 'A vezérműszíj az egyik legfontosabb alkatrész a motorban, amely meghibásodás esetén súlyos károkat okozhat. Teljes szíj- és feszítőgörgő-cserét végzünk a gyári előírások szerint.', 45000.00),
(9, 'Kipufogórendszer ellenőrzés', 'Vizsgáljuk a kipufogórendszer szivárgásait, a katalizátor állapotát és a hangtompító épségét. Segítünk a környezetbarát és hatékony működés fenntartásában.', 13000.00),
(10, 'Teljes autó átvizsgálás', 'Részletes állapotfelmérés minden főbb járműkomponensre, beleértve a motor, fékek, futómű, akkumulátor és elektromos rendszerek ellenőrzését. Ajánlott hosszabb utak vagy használt autó vásárlás előtt.', 30000.00);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tipus`
--

CREATE TABLE `tipus` (
  `tipus_id` int(11) NOT NULL,
  `tipus` varchar(50) DEFAULT NULL,
  `marka_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `tipus`
--

INSERT INTO `tipus` (`tipus_id`, `tipus`, `marka_id`) VALUES
(1, 'Corolla', 1),
(2, 'Camry', 1),
(3, 'RAV4', 1),
(4, '320i', 2),
(5, 'X5', 2),
(6, 'M4', 2),
(7, 'Focus', 3),
(8, 'Mustang', 3),
(9, 'Explorer', 3);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfelek`
--

CREATE TABLE `ugyfelek` (
  `ugyfel_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `telefonszam` varchar(150) DEFAULT NULL,
  `cim` varchar(200) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `ugyfelek`
--

INSERT INTO `ugyfelek` (`ugyfel_id`, `nev`, `telefonszam`, `cim`, `email`) VALUES
(1, 'Chrén András Ferenc', '', NULL, 'andrewchren1@gmail.com');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfel_jarmuvek`
--

CREATE TABLE `ugyfel_jarmuvek` (
  `ugyfel_id` int(11) NOT NULL,
  `jarmu_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `ugyfel_jarmuvek`
--

INSERT INTO `ugyfel_jarmuvek` (`ugyfel_id`, `jarmu_id`) VALUES
(1, 1);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `alkatreszek`
--
ALTER TABLE `alkatreszek`
  ADD PRIMARY KEY (`alkatresz_id`);

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
  ADD KEY `felhasznalok_ugyfelek_ibfk_2` (`ugyfel_id`);

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
  ADD KEY `idopontfoglalasok_ibfk_1` (`jarmu_id`),
  ADD KEY `idopontfoglalasok_ibfk_2` (`csomag_id`);

--
-- A tábla indexei `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD PRIMARY KEY (`jarmu_id`),
  ADD UNIQUE KEY `rendszam` (`rendszam`),
  ADD KEY `jarmuvek_ibfk_1` (`tipus_id`),
  ADD KEY `jarmuvek_ibfk_2` (`kod_id`),
  ADD KEY `jarmuvek_ibfk_3` (`sablon_id`);

--
-- A tábla indexei `marka`
--
ALTER TABLE `marka`
  ADD PRIMARY KEY (`marka_id`);

--
-- A tábla indexei `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  ADD PRIMARY KEY (`sablon_id`);

--
-- A tábla indexei `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD PRIMARY KEY (`rendeles_id`),
  ADD KEY `rendelesek_ibfk_1` (`felhasznalo_id`),
  ADD KEY `rendelesek_ibfk_2` (`alkatresz_id`);

--
-- A tábla indexei `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD PRIMARY KEY (`utmutato_id`),
  ADD KEY `szerelesi_utmutatok_ibfk_1` (`jarmu_tipus`);

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
  ADD KEY `tipus_ibfk_1` (`marka_id`);

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
  ADD KEY `ugyfel_jarmuvek_ibfk_2` (`jarmu_id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `alkatreszek`
--
ALTER TABLE `alkatreszek`
  MODIFY `alkatresz_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `hibakodok`
--
ALTER TABLE `hibakodok`
  MODIFY `kod_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `idopont_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `jarmuvek`
--
ALTER TABLE `jarmuvek`
  MODIFY `jarmu_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `marka`
--
ALTER TABLE `marka`
  MODIFY `marka_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  MODIFY `sablon_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `rendelesek`
--
ALTER TABLE `rendelesek`
  MODIFY `rendeles_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  MODIFY `utmutato_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  MODIFY `csomag_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `tipus`
--
ALTER TABLE `tipus`
  MODIFY `tipus_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `ugyfel_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Megkötések a kiírt táblákhoz
--

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
-- Megkötések a táblához `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD CONSTRAINT `rendelesek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `rendelesek_ibfk_2` FOREIGN KEY (`alkatresz_id`) REFERENCES `alkatreszek` (`alkatresz_id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD CONSTRAINT `szerelesi_utmutatok_ibfk_1` FOREIGN KEY (`jarmu_tipus`) REFERENCES `tipus` (`tipus_id`);

--
-- Megkötések a táblához `tipus`
--
ALTER TABLE `tipus`
  ADD CONSTRAINT `tipus_ibfk_1` FOREIGN KEY (`marka_id`) REFERENCES `marka` (`marka_id`);

--
-- Megkötések a táblához `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_1` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_2` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`) ON DELETE CASCADE;
COMMIT;
