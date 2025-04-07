-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 07. 13:51
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

--
-- A tábla adatainak kiíratása `alkatreszek`
--

INSERT INTO `alkatreszek` (`alkatresz_id`, `nev`, `leiras`, `keszlet_mennyiseg`, `utanrendelesi_szint`) VALUES
(1, 'Üzemanyagcső 4mm', '4mm belső átmérőjű üzemanyagcső/ méter', 8, 2),
(2, 'Kerékcsavar', 'M12x1.5 45mm', 4, 1),
(3, 'Olajszűrő', 'Toyota modellekhez kompatibilis', 15, 5),
(4, 'Fékbetét', 'BMW 3 Serieshez', 10, 3),
(5, 'Légszűrő', 'Audi A4-hez alkalmas', 12, 4),
(6, 'Gyújtógyertya', 'NGK, Honda Civic kompatibilis', 20, 5),
(7, 'Akkumulátor', '12V 60Ah', 5, 2),
(8, 'Féktárcsa', 'Mercedes E-Classhez', 8, 3),
(9, 'Vezérműszíj', 'Ford Focus kompatibilis', 6, 2),
(10, 'Üzemanyagszűrő', 'Dízel motorokhoz', 10, 4),
(11, 'Kuplung tárcsa', 'VW Golfhoz', 4, 1),
(12, 'Hűtőfolyadék', 'G12, 5 liter', 15, 5),
(13, 'Lengéscsillapító', 'Tesla Model 3-hoz', 6, 2),
(14, 'Kerékcsapágy', 'Subaru Outbackhez', 5, 2),
(15, 'Fékfolyadék', 'DOT 4, 1 liter', 20, 5),
(16, 'Kipufogódob', 'Mazda CX-5-höz', 3, 1),
(17, 'Olajszivattyú', 'Porsche Cayenne-hez', 2, 1),
(18, 'Generátor', 'Hyundai Tucson-hoz', 4, 2),
(19, 'Szélvédő', 'Toyota RAV4-hez', 3, 1),
(20, 'Fényszóró izzó', 'H7, univerzális', 25, 10),
(21, 'Ablaktörlő lapát', 'Bosch, 60cm', 15, 5),
(22, 'Kormánymű tömítés', 'BMW X5-höz', 5, 2),
(23, 'Klíma kompresszor', 'Volvo XC60-hoz', 3, 1),
(24, 'Üzemanyagpumpa', 'Chevrolet Camaro-hoz', 4, 2),
(25, 'Hűtőradiátor', 'Nissan Leafhez', 6, 2),
(26, 'Fojtószelep', 'Audi Q5-höz', 3, 1),
(27, 'EGR szelep', 'VW Tiguan-hoz', 5, 2),
(28, 'Oxigénérzékelő', 'Honda CR-V-hez', 8, 3),
(29, 'Motorolaj', '5W-30, 4 liter', 30, 10),
(30, 'Sebességváltó olaj', 'ATF, 5 liter', 12, 4);

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
(1, 'admin', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 'Adminisztrátor'),
(23, 'Ati123', 'f.jelszo_hash', 'Ügyfél'),
(24, 'Kis Sándor', '7f91e8a4b648b0125b15dc5a3b6466f9f4906d92c72bea9bd6be92c853bebda2', 'Szerelő'),
(25, 'Klari25', 'd09313aaedc4c134f8e329afe86cf57354e1a1ab65b7ba82de126b659aa8e543', 'Ügyfél');

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
(23, 21),
(25, 22);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `hibakodok`
--

CREATE TABLE `hibakodok` (
  `kod_id` int(11) NOT NULL,
  `kod` varchar(10) DEFAULT NULL,
  `leiras` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `hibakodok`
--

INSERT INTO `hibakodok` (`kod_id`, `kod`, `leiras`) VALUES
(1, 'P0300', 'Véletlenszerű hengerkihagyás észlelve'),
(2, 'P0420', 'Katalizátor hatékonysága az elvárt szint alatt'),
(3, 'P0171', 'Túl szegény üzemanyag-levegő keverék'),
(4, 'P0130', 'Oxigénérzékelő meghibásodása'),
(5, 'P0500', 'Járműsebesség-érzékelő hiba'),
(6, 'P0700', 'Sebességváltó vezérlőegység hiba'),
(7, 'P0301', '1. henger kihagyás észlelve'),
(8, 'P0302', '2. henger kihagyás észlelve'),
(9, 'P0440', 'Üzemanyag-tartály szellőztető rendszer hiba'),
(10, 'P0455', ' Nagy szivárgás az üzemanyag-gőz rendszerben'),
(11, 'P0100', 'Tömegáram-érzékelő hiba'),
(12, 'P0110', 'Szívó levegő hőmérséklet-érzékelő hiba'),
(13, 'P0120', 'Fojtószelep helyzetérzékelő hiba'),
(14, 'P0200', 'Befecskendező szelep áramkör hiba'),
(15, 'P0325', 'Kopogásérzékelő hiba'),
(16, 'P0335', 'Főtengely helyzetérzékelő hiba'),
(17, 'P0340', 'Vezérműtengely helyzetérzékelő hiba'),
(18, 'P0400', 'EGR rendszer áramlási hiba'),
(19, 'P0430', 'Katalizátor hatékonyság alacsony, 2. bank'),
(20, 'P0505', 'Üresjárati szabályzó hiba'),
(21, 'P0600', 'Kommunikációs hiba a vezérlőegységgel'),
(22, 'P0705', 'Sebességváltó helyzetérzékelő hiba'),
(23, 'P1120', 'Fojtószelep motor hiba'),
(24, 'P1130', 'Oxigénérzékelő áramkör hiba'),
(25, 'P1340', 'Gyújtáskimaradás több hengeren'),
(26, 'P1400', 'DPFE érzékelő hiba (EGR rendszer)'),
(27, 'P1500', 'Generátor szabályzó hiba'),
(28, 'P1600', 'ECU belső hiba'),
(29, 'P1700', 'Sebességváltó vezérlés hiba'),
(30, 'P2000', 'NOx csapda hatékonyság alacsony');

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
(20, 53, 1, '2025-04-17 12:25:00', 'Folyamatban'),
(21, 53, 11, NULL, 'Foglalt'),
(22, 54, 1, NULL, 'Foglalt'),
(23, 54, 2, '2025-04-24 08:30:00', 'Befejezett'),
(24, 53, 1, NULL, 'Lemondva'),
(25, 53, 1, NULL, 'Lemondva');

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
(53, 'ABC123', 18, NULL, NULL, NULL, NULL, NULL, ''),
(54, 'DEF587', 45, NULL, NULL, NULL, NULL, NULL, '; \nFékellenőrzés');

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
(1, 'Nissan'),
(2, 'Toyota'),
(3, 'BMW'),
(4, 'Audi'),
(5, 'Tesla'),
(6, 'Mercedes-Benz'),
(7, 'Volkswagen'),
(8, 'Ford'),
(9, 'Honda'),
(10, 'Hyundai'),
(11, 'Porsche'),
(12, 'Chevrolet'),
(13, 'Subaru'),
(14, 'Mazda'),
(15, 'Volvo');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `munkafolyamat_sablonok`
--

CREATE TABLE `munkafolyamat_sablonok` (
  `sablon_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `lepesek` text DEFAULT NULL,
  `becsult_ido` varchar(7) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `munkafolyamat_sablonok`
--

INSERT INTO `munkafolyamat_sablonok` (`sablon_id`, `nev`, `lepesek`, `becsult_ido`) VALUES
(1, 'Olajcsere', 'Emelés, leeresztés, szűrőcsere, feltöltés', '45min'),
(2, 'Fék', 'Kerék levétele, Féknyereg levétele, Féktárcsa levétele, Takarítás, Összeszerelés', '90min'),
(3, 'Kerékcsere', 'Emelés, csavarok lazítása, kerék levétele, új kerék felszerelése', '30min'),
(4, 'Diagnosztika', 'OBD csatlakoztatása, hibakód kiolvasása, elemzés', '20min'),
(5, 'Futóműbeállítás', 'Emelés, mérőeszköz felhelyezése, beállítás, ellenőrzés', '60min'),
(6, 'Akkumulátor csere', 'Kapcsoló kikapcsolása, kábelek levétele, új akku beszerelése', '30min'),
(7, 'Kipufogó javítás', 'Emelés, csövek ellenőrzése, sérült rész cseréje', '60min'),
(8, 'Légszűrő csere', 'Motorháztető felnyitása, szűrő eltávolítása, új szűrő behelyezése', '15min'),
(9, 'Gyújtógyertya csere', 'Kábelek levétele, régi gyertyák ki, újak be', '45min'),
(10, 'Hűtőfolyadék csere', 'Rendszer leeresztése, átmosás, új folyadékkal töltés', '40min'),
(11, 'Szélvédő csere', 'Régi üveg eltávolítása, új üveg ragasztása, száradás', '120min'),
(12, 'Generátor felújítás', 'Levétel, szétszerelés, alkatrész csere, összeszerelés', '90min'),
(13, 'Fényszóró beállítás', 'Fényszórók ellenőrzése, csavarokkal igazítás', '20min'),
(14, 'Kuplung csere', 'Váltó levétele, kuplung csere, visszahelyezés', '180min'),
(15, 'Vezérműszíj csere', 'Fedél levétele, régi szíj ki, új szíj be, feszítés', '120min'),
(16, 'Üzemanyagszűrő csere', 'Szűrő helyének megtalálása, csere, ellenőrzés', '30min'),
(17, 'Kerék centrírozás', 'Kerék levétele, centrírozó gépen beállítás', '25min'),
(18, 'Fűtés javítás', 'Fűtőradiátor ellenőrzése, csere vagy tisztítás', '90min'),
(19, 'Ablaktörlő csere', 'Régi lapátok levétele, újak felszerelése', '10min'),
(20, 'Hűtőrendszer javítás', 'Szivárgás keresése, tömítés vagy cső csere', '60min'),
(21, 'Kormánymű felújítás', 'Kormánymű levétele, javítás, visszahelyezés', '120min'),
(22, 'Lengéscsillapító csere', 'Kerék levétele, régi csillapító ki, új be', '60min'),
(23, 'Kerékcsapágy csere', 'Kerék levétele, csapágy kiütése, új behelyezése', '90min'),
(24, 'Klíma feltöltés', 'Rendszer ellenőrzése, hűtőközeg feltöltése', '45min'),
(25, 'Motor tisztítás', 'Motorblokk leszedése, tisztítás, visszahelyezés', '60min'),
(26, 'Sebességváltó olajcsere', 'Leeresztés, új olaj betöltése, ellenőrzés', '60min'),
(27, 'Fékfolyadék csere', 'Rendszer leeresztése, új folyadékkal töltés', '45min'),
(28, 'Üzemanyagpumpa csere', 'Tank elérése, pumpa csere, összeszerelés', '90min'),
(29, 'Olajszivattyú csere', 'Motor megbontása, szivattyú csere, összeszerelés', '150min'),
(30, 'Kipufogódob csere', 'Régi dob levétele, új felszerelése, rögzítés', '60min');

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
  `jarmu_tipus` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `szerelesi_utmutatok`
--

INSERT INTO `szerelesi_utmutatok` (`utmutato_id`, `cim`, `tartalom`, `jarmu_tipus`) VALUES
(1, 'Olajcsere Toyota Corolla', '1. Emelje fel az autót. 2. Csavarja le az olajleeresztő csavart. 3. Cserélje ki a szűrőt. 4. Töltse fel új olajjal.', 1),
(2, 'Fékbetét csere Toyota Camry', '1. Kerék levétele. 2. Féknyereg csavarok ki. 3. Betétek cseréje. 4. Összeszerelés.', 2),
(3, 'Klíma tisztítás Toyota RAV4', '1. Szűrő elérése. 2. Tisztítás vagy csere. 3. Rendszer ellenőrzése.', 3),
(4, 'Akkumulátor csere Toyota Prius', '1. Kábelek levétele. 2. Régi akku ki. 3. Új be.', 4),
(5, 'Légszűrő csere Toyota Yaris', '1. Szűrőház nyitása. 2. Régi szűrő ki. 3. Új be.', 5),
(6, 'Féktárcsa csere BMW 3 Series', '1. Kerék levétele. 2. Féknyereg ki. 3. Tárcsa csere.', 6),
(7, 'Olajcsere BMW 5 Series', '1. Emelés. 2. Leeresztés. 3. Szűrő csere. 4. Feltöltés.', 7),
(8, 'Kipufogó javítás BMW X5', '1. Emelés. 2. Csövek ellenőrzése. 3. Csere.', 8),
(9, 'Diagnosztika BMW i3', '1. OBD csatlakoztatása. 2. Kódok kiolvasása.', 9),
(10, 'Gyújtógyertya csere BMW M4', '1. Kábelek levétele. 2. Gyertyák cseréje.', 10),
(11, 'Kerékcsere Audi A3', '1. Emelés. 2. Csavarok ki. 3. Új kerék fel.', 11),
(12, 'Klíma feltöltés Audi A4', '1. Rendszer ellenőrzése. 2. Hűtőközeg betöltése.', 12),
(13, 'Fényszóró beállítás Audi Q5', '1. Fényszórók ellenőrzése. 2. Igazítás.', 13),
(14, 'Akkumulátor csere Audi e-tron', '1. Kábelek levétele. 2. Csere.', 14),
(15, 'Hűtőfolyadék csere Audi TT', '1. Rendszer leeresztése. 2. Új folyadék.', 15),
(16, 'Olajcsere Tesla Model S', '1. Emelés. 2. Leeresztés. 3. Feltöltés.', 16),
(17, 'Diagnosztika Tesla Model 3', '1. OBD csatlakozás. 2. Kódok elemzése.', 17),
(18, 'Lengéscsillapító csere Tesla Model X', '1. Kerék levétele. 2. Csillapító csere.', 18),
(19, 'Kerékcsapágy csere Tesla Model Y', '1. Kerék levétele. 2. Csapágy csere.', 19),
(20, 'Fékbetét csere Tesla Roadster', '1. Féknyereg ki. 2. Betétek csere.', 20),
(21, 'Olajcsere Nissan Leaf', '1. Emelés. 2. Leeresztés. 3. Szűrő csere.', 21),
(22, 'Klíma tisztítás Nissan Qashqai', '1. Szűrő elérése. 2. Tisztítás.', 22),
(23, 'Akkumulátor csere Nissan e-NV200', '1. Kábelek levétele. 2. Csere.', 23),
(24, 'Fényszóró izzó csere Nissan IMx', '1. Izzó elérése. 2. Csere.', 24),
(25, 'Gyújtógyertya csere Nissan Ariya', '1. Kábelek ki. 2. Gyertyák csere.', 25),
(26, 'Fék csere Mercedes C-Class', '1. Kerék levétele. 2. Féknyereg ki. 3. Csere.', 26),
(27, 'Olajcsere Mercedes E-Class', '1. Emelés. 2. Leeresztés. 3. Feltöltés.', 27),
(28, 'Kipufogó javítás Mercedes S-Class', '1. Emelés. 2. Csövek cseréje.', 28),
(29, 'Futóműbeállítás Mercedes GLC', '1. Mérőeszköz fel. 2. Beállítás.', 29),
(30, 'Olajcsere VW Golf', '1. Emelés. 2. Leeresztés. 3. Szűrő csere.', 30),
(31, 'Klíma feltöltés VW Passat', '1. Rendszer ellenőrzése. 2. Feltöltés.', 31),
(32, 'Fékbetét csere VW Tiguan', '1. Kerék levétele. 2. Betétek csere.', 32),
(33, 'Akkumulátor csere VW Polo', '1. Kábelek ki. 2. Csere.', 33),
(34, 'Vezérműszíj csere Ford F-150', '1. Fedél levétele. 2. Szíj csere.', 34),
(35, 'Gyújtógyertya csere Ford Mustang', '1. Kábelek ki. 2. Gyertyák csere.', 35),
(36, 'Olajcsere Ford Focus', '1. Emelés. 2. Leeresztés. 3. Feltöltés.', 36),
(37, 'Fék csere Honda Civic', '1. Kerék levétele. 2. Féknyereg ki. 3. Csere.', 37),
(38, 'Klíma tisztítás Honda Accord', '1. Szűrő elérése. 2. Tisztítás.', 38),
(39, 'Légszűrő csere Honda CR-V', '1. Szűrőház nyitása. 2. Csere.', 39),
(40, 'Olajcsere Hyundai Tucson', '1. Emelés. 2. Leeresztés. 3. Szűrő csere.', 40),
(41, 'Fékbetét csere Hyundai Elantra', '1. Kerék levétele. 2. Betétek csere.', 41),
(42, 'Kipufogó javítás Hyundai Santa Fe', '1. Emelés. 2. Csövek cseréje.', 42),
(43, 'Olajcsere Porsche 911', '1. Emelés. 2. Leeresztés. 3. Feltöltés.', 43),
(44, 'Fék csere Porsche Cayenne', '1. Kerék levétele. 2. Féknyereg ki. 3. Csere.', 44),
(45, 'Gyújtógyertya csere Chevrolet Camaro', '1. Kábelek ki. 2. Gyertyák csere.', 45),
(46, 'Kerékcsere Chevrolet Silverado', '1. Emelés. 2. Csavarok ki. 3. Új kerék fel.', 46),
(47, 'Olajcsere Subaru Outback', '1. Emelés. 2. Leeresztés. 3. Szűrő csere.', 47),
(48, 'Fékbetét csere Subaru Impreza', '1. Kerék levétele. 2. Betétek csere.', 48),
(49, 'Klíma tisztítás Mazda MX-5', '1. Szűrő elérése. 2. Tisztítás.', 49),
(50, 'Légszűrő csere Mazda CX-5', '1. Szűrőház nyitása. 2. Csere.', 50),
(51, 'Olajcsere Volvo XC60', '1. Emelés. 2. Leeresztés. 3. Feltöltés.', 51),
(52, 'Fék csere Volvo S90', '1. Kerék levétele. 2. Féknyereg ki. 3. Csere.', 52),
(53, 'Diagnosztika Toyota Corolla', '1. OBD csatlakozás. 2. Kódok kiolvasása.', 1),
(54, 'Vezérműszíj csere VW Golf', '1. Fedél levétele. 2. Szíj csere.', 30),
(55, 'Fékfolyadék csere BMW 3 Series', '1. Rendszer leeresztése. 2. Új folyadék.', 6),
(56, 'Üzemanyagpumpa csere Audi A4', '1. Tank elérése. 2. Pumpa csere.', 12),
(57, 'Kuplung csere Ford Focus', '1. Váltó levétele. 2. Kuplung csere.', 36),
(58, 'Hűtőrendszer javítás Honda Civic', '1. Szivárgás keresése. 2. Csere.', 37),
(59, 'Kerék centrírozás Tesla Model 3', '1. Kerék levétele. 2. Centrírozás.', 17),
(60, 'Fűtés javítás Mercedes E-Class', '1. Radiátor ellenőrzése. 2. Javítás.', 27),
(61, 'Ablaktörlő csere Nissan Leaf', '1. Régi lapátok ki. 2. Újak fel.', 21),
(62, 'Olajszivattyú csere Porsche Cayenne', '1. Motor megbontása. 2. Szivattyú csere.', 44),
(63, 'Kipufogódob csere Subaru Outback', '1. Régi dob ki. 2. Új fel.', 47),
(64, 'Fényszóró izzó csere Toyota RAV4', '1. Izzó elérése. 2. Csere.', 3),
(65, 'Lengéscsillapító csere VW Tiguan', '1. Kerék levétele. 2. Csillapító csere.', 32),
(66, 'Klíma feltöltés BMW 5 Series', '1. Rendszer ellenőrzése. 2. Feltöltés.', 7),
(67, 'Üzemanyagszűrő csere Hyundai Tucson', '1. Szűrő elérése. 2. Csere.', 40),
(68, 'Motor tisztítás Tesla Model S', '1. Motorblokk leszedése. 2. Tisztítás.', 16),
(69, 'Sebességváltó olajcsere Audi Q5', '1. Leeresztés. 2. Új olaj betöltése.', 13),
(70, 'Kormánymű felújítás Mercedes GLC', '1. Kormánymű levétele. 2. Javítás.', 29);

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
(1, 'Olajcsere', 'Motorolaj cseréje szűrővel, ingyenes ellenőrzéssel.', 15000.00),
(2, 'Fékellenőrzés', 'Teljes fékrendszer ellenőrzése, fékbetétek és tárcsák felmérése.', 8000.00),
(3, 'Klíma tisztítás', 'Autó klímarendszerének tisztítása és fertőtlenítése.', 12000.00),
(4, 'Teljes szerviz', 'Átfogó szerviz, beleértve az olajcserét, fékellenőrzést és diagnosztikát.', 50000.00),
(5, 'Kerékcsere', 'Nyári vagy téli gumiabroncsok cseréje, centrírozással.', 10000.00),
(6, 'Fék', 'Fékbetét, Féktárcsa cseréje 1 tengelyen', 15000.00),
(7, 'Futóműbeállítás', 'Futómű pontos beállítása korszerű eszközzel.', 22000.00),
(8, 'Akkumulátor csere', 'Régi akkumulátor eltávolítása és új beszerelése.', 20000.00),
(9, 'Diagnosztika', 'Teljes körű hibakód olvasás és elemzés.', 10000.00),
(10, 'Kipufogó javítás', 'Kipufogórendszer ellenőrzése és javítása.', 18000.00),
(11, 'Légszűrő csere', 'Motor légszűrőjének cseréje.', 5000.00),
(12, 'Gyújtógyertya csere', 'Gyújtógyertyák cseréje, 4 hengeres motorhoz.', 12000.00),
(13, 'Hűtőfolyadék csere', 'Hűtőrendszer átmosása és új folyadékkal töltése.', 8000.00),
(14, 'Szélvédő csere', 'Sérült szélvédő cseréje, ragasztással.', 35000.00),
(15, 'Generátor felújítás', 'Generátor ellenőrzése és javítása.', 25000.00),
(16, 'Olajszűrő csere', 'Olajszűrő cseréje olajcsere nélkül.', 3000.00),
(17, 'Fényszóró beállítás', 'Fényszórók pontos beállítása.', 6000.00),
(18, 'Kuplung csere', 'Kuplungtárcsa és szerkezet cseréje.', 60000.00),
(19, 'Vezérműszíj csere', 'Vezérműszíj és feszítő cseréje.', 45000.00),
(20, 'Üzemanyagszűrő csere', 'Üzemanyagszűrő cseréje dízel vagy benzines motorhoz.', 7000.00),
(21, 'Kerék centrírozás', 'Négy kerék centrírozása.', 8000.00),
(22, 'Fűtés javítás', 'Fűtőrendszer ellenőrzése és javítása.', 15000.00),
(23, 'Ablaktörlő csere', 'Első ablaktörlő lapátok cseréje.', 4000.00),
(24, 'Hűtőrendszer javítás', 'Hűtő ellenőrzése és szivárgás javítása.', 20000.00),
(25, 'Kormánymű felújítás', 'Kormánymű ellenőrzése és javítása.', 30000.00),
(26, 'Lengéscsillapító csere', 'Első tengely lengéscsillapítóinak cseréje.', 25000.00),
(27, 'Kerékcsapágy csere', 'Egy kerék csapágyának cseréje.', 18000.00),
(28, 'Klíma feltöltés', 'Klímarendszer hűtőközeggel való feltöltése.', 15000.00),
(29, 'Motor tisztítás', 'Motorblokk külső tisztítása.', 10000.00),
(30, 'Sebességváltó olajcsere', 'Manuális vagy automata váltó olajcseréje.', 20000.00);

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
(1, 'Corolla', 2),
(2, 'Camry', 2),
(3, 'RAV4', 2),
(4, 'Prius', 2),
(5, 'Yaris', 2),
(6, '3 Series', 3),
(7, '5 Series', 3),
(8, 'X5', 3),
(9, 'i3', 3),
(10, 'M4', 3),
(11, 'A3', 4),
(12, 'A4', 4),
(13, 'Q5', 4),
(14, 'e-tron', 4),
(15, 'TT', 4),
(16, 'Model S', 5),
(17, 'Model 3', 5),
(18, 'Model X', 5),
(19, 'Model Y', 5),
(20, 'Roadster', 5),
(21, 'Leaf', 1),
(22, 'Qashqai', 1),
(23, 'e-NV200', 1),
(24, 'IMx', 1),
(25, 'Ariya', 1),
(26, 'C-Class', 6),
(27, 'E-Class', 6),
(28, 'S-Class', 6),
(29, 'GLC', 6),
(30, 'Golf', 7),
(31, 'Passat', 7),
(32, 'Tiguan', 7),
(33, 'Polo', 7),
(34, 'F-150', 8),
(35, 'Mustang', 8),
(36, 'Focus', 8),
(37, 'Civic', 9),
(38, 'Accord', 9),
(39, 'CR-V', 9),
(40, 'Tucson', 10),
(41, 'Elantra', 10),
(42, 'Santa Fe', 10),
(43, '911', 11),
(44, 'Cayenne', 11),
(45, 'Camaro', 12),
(46, 'Silverado', 12),
(47, 'Outback', 13),
(48, 'Impreza', 13),
(49, 'MX-5', 14),
(50, 'CX-5', 14),
(51, 'XC60', 15),
(52, 'S90', 15);

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
(21, 'Nagy Attila', '06201234567', '0001,Messzifölde,Első utca,52', 'ati123@gmail.com'),
(22, 'Kovács Klára', '', NULL, 'klari25@gmail.com');

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
(21, 53),
(22, 54);

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
  ADD KEY `felhasznalo_id` (`felhasznalo_id`),
  ADD KEY `alkatresz_id` (`alkatresz_id`);

--
-- A tábla indexei `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD PRIMARY KEY (`utmutato_id`),
  ADD KEY `fk_jarmu_tipus` (`jarmu_tipus`);

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
  ADD KEY `fk_marka_id` (`marka_id`);

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
  MODIFY `alkatresz_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT a táblához `hibakodok`
--
ALTER TABLE `hibakodok`
  MODIFY `kod_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `idopont_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT a táblához `jarmuvek`
--
ALTER TABLE `jarmuvek`
  MODIFY `jarmu_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=55;

--
-- AUTO_INCREMENT a táblához `marka`
--
ALTER TABLE `marka`
  MODIFY `marka_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  MODIFY `sablon_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `rendelesek`
--
ALTER TABLE `rendelesek`
  MODIFY `rendeles_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  MODIFY `utmutato_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=71;

--
-- AUTO_INCREMENT a táblához `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  MODIFY `csomag_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `tipus`
--
ALTER TABLE `tipus`
  MODIFY `tipus_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `ugyfel_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

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
  ADD CONSTRAINT `fk_jarmu_tipus` FOREIGN KEY (`jarmu_tipus`) REFERENCES `tipus` (`tipus_id`);

--
-- Megkötések a táblához `tipus`
--
ALTER TABLE `tipus`
  ADD CONSTRAINT `fk_marka_id` FOREIGN KEY (`marka_id`) REFERENCES `marka` (`marka_id`);

--
-- Megkötések a táblához `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_1` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_2` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
