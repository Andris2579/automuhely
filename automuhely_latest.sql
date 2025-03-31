-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 07, 2025 at 09:02 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `automuhely`
--

-- --------------------------------------------------------

--
-- Table structure for table `alkatreszek`
--

CREATE TABLE `alkatreszek` (
  `alkatresz_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `keszlet_mennyiseg` int(11) DEFAULT NULL,
  `utanrendelesi_szint` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `alkatreszek`
--

INSERT INTO `alkatreszek` (`alkatresz_id`, `nev`, `leiras`, `keszlet_mennyiseg`, `utanrendelesi_szint`) VALUES
(1, 'Rozsdás rossz', 'nagyon jó állapotú', 2, 0),
(2, 'jó2', 'rossz', 1, 0),
(3, 'nagyon jó alkatrész', 'jó cucc', 0, 0),
(4, 'teszt', 'jó', 0, 0),
(5, 'jó', 'hello', 100, 0),
(6, 'ok', 'jó500', 100, 4),
(7, 'ok', 'jó500', 100, 4),
(8, 'jó', 'nagyon jó', 100, 0),
(9, 'kütyü', 'rossz', 9, 2),
(10, 'Benyó', 'nagyon szép', 100, 0),
(12, 'íxxa', '123', 10, 0);

-- --------------------------------------------------------

--
-- Table structure for table `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `felhasznalo_id` int(11) NOT NULL,
  `felhasznalonev` varchar(50) DEFAULT NULL,
  `jelszo_hash` varchar(255) DEFAULT NULL,
  `szerep` enum('Adminisztrátor','Szerelő','Ügyfél') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `felhasznalok`
--

INSERT INTO `felhasznalok` (`felhasznalo_id`, `felhasznalonev`, `jelszo_hash`, `szerep`) VALUES
(1, 'admin', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 'Adminisztrátor'),
(22, 'janicsak@veca.com', 'dd8a3af07bf0ed457e80ebfa07a8d2a7d834bb30aaee2cbf97d3b6120e6238b8', 'Szerelő');

-- --------------------------------------------------------

--
-- Table structure for table `felhasznalok_ugyfelek`
--

CREATE TABLE `felhasznalok_ugyfelek` (
  `felhasznalo_id` int(11) NOT NULL,
  `ugyfel_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `hibakodok`
--

CREATE TABLE `hibakodok` (
  `kod_id` int(11) NOT NULL,
  `kod` varchar(10) DEFAULT NULL,
  `leiras` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `hibakodok`
--

INSERT INTO `hibakodok` (`kod_id`, `kod`, `leiras`) VALUES
(1, 'benyó', 'jlkjlkjlk'),
(2, 'benyó', 'jlkjlkjlk'),
(3, 'benyó', 'jlkjlkjlk'),
(4, 'benyó', 'jlkjlkjlk'),
(5, 'benyó', 'jlkjlkjlk'),
(6, 'benyó', 'jlkjlkjlk'),
(7, 'benyó', 'jlkjlkjlk'),
(8, 'benyó', 'jlkjlkjlk'),
(9, 'benyó', 'jlkjlkjlk'),
(10, 'benyó', 'jlkjlkjlk'),
(11, 'benyó', 'jlkjlkjlk'),
(12, 'ja', 'nem'),
(13, 'talán', 'igen'),
(14, 'A0021', 'Ez akkor van ha valami nem jó'),
(15, '', '');

-- --------------------------------------------------------

--
-- Table structure for table `idopontfoglalasok`
--

CREATE TABLE `idopontfoglalasok` (
  `idopont_id` int(11) NOT NULL,
  `jarmu_id` int(11) DEFAULT NULL,
  `csomag_id` int(11) DEFAULT NULL,
  `idopont` datetime DEFAULT NULL,
  `allapot` enum('Foglalt','Folyamatban','Befejezett','Lemondva') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `idopontfoglalasok`
--

INSERT INTO `idopontfoglalasok` (`idopont_id`, `jarmu_id`, `csomag_id`, `idopont`, `allapot`) VALUES
(9, 10, 4, '2029-12-31 10:20:11', 'Folyamatban'),
(10, 41, 4, '2025-02-28 12:30:00', 'Befejezett'),
(11, 38, 4, '2025-02-22 12:30:00', 'Folyamatban'),
(12, 37, 3, '2025-02-28 16:00:00', 'Befejezett'),
(13, 34, 4, '0000-00-00 00:00:00', 'Foglalt'),
(14, 39, 4, '0000-00-00 00:00:00', 'Foglalt'),
(15, 40, 4, '2025-06-04 19:00:00', 'Folyamatban'),
(17, 43, 1, '2025-03-02 15:11:00', 'Folyamatban'),
(18, 42, 1, '2025-02-21 14:00:00', 'Befejezett'),
(19, 47, 5, '2025-03-15 13:00:00', 'Folyamatban');

-- --------------------------------------------------------

--
-- Table structure for table `jarmuvek`
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
-- Dumping data for table `jarmuvek`
--

INSERT INTO `jarmuvek` (`jarmu_id`, `rendszam`, `tipus_id`, `kod_id`, `sablon_id`, `gyartas_eve`, `motor_adatok`, `alvaz_adatok`, `elozo_javitasok`) VALUES
(10, 'abc-123', 2, 14, 1, '1990', 'asdfaewfdag', 'wvaav', 'Teljes szervíz'),
(34, 'abc-124', 23, NULL, NULL, NULL, NULL, NULL, 'sokat'),
(35, 'abc-125', 25, NULL, NULL, NULL, NULL, NULL, 'nincs'),
(37, 'abc-126', 25, NULL, NULL, NULL, NULL, NULL, 'nincs; \nKlíma tisztítás'),
(38, 'abc-127', 25, NULL, NULL, NULL, NULL, NULL, 'nincs'),
(39, '128', 6, 14, 1, '1999', 'majd lesz', 'nemtom', 'nincs'),
(40, '129', 21, NULL, NULL, NULL, NULL, NULL, 'nincs'),
(41, '130', 21, NULL, NULL, NULL, NULL, NULL, 'nincs\nTeljes szerviz'),
(42, 'inl-066', 21, NULL, NULL, NULL, NULL, NULL, 'sosemvolt; \nOlajcsere'),
(43, 'INL-067', 21, NULL, NULL, NULL, NULL, NULL, 'sosemvolt'),
(44, 'sadsad', 6, NULL, NULL, NULL, NULL, NULL, 'asdasfasf'),
(45, 'afjsafjasf', 21, NULL, NULL, NULL, NULL, NULL, 'afdjfsdkfh'),
(46, 'nfdsnfsd', 6, NULL, NULL, NULL, NULL, NULL, 'fkdslnflksd'),
(47, 'abc-1250', 8, NULL, NULL, NULL, NULL, NULL, 'adasdasd'),
(48, 'abd-1250', 11, NULL, NULL, NULL, NULL, NULL, 'adasdasd s'),
(49, 'ajsfsjfsak', 8, NULL, NULL, NULL, NULL, NULL, 'asjkdsj jasdasjkd.'),
(50, 'éaádésdásd', 9, NULL, NULL, NULL, NULL, NULL, 'asdlsékd ésdskaldas. lsadkasdkaséd.'),
(51, 'ABC123', 24, 14, 1, '2014', 'most nincs', 'azsincs', 'még nem volt'),
(52, 'ABC222', 9, 1, 1, '1917', 'asawrerdg', 'lolololololololololol', '');

-- --------------------------------------------------------

--
-- Table structure for table `marka`
--

CREATE TABLE `marka` (
  `marka_id` int(11) NOT NULL,
  `marka_neve` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `marka`
--

INSERT INTO `marka` (`marka_id`, `marka_neve`) VALUES
(1, 'Nissan'),
(2, 'Toyota'),
(3, 'BMW'),
(4, 'Audi'),
(5, 'Tesla');

-- --------------------------------------------------------

--
-- Table structure for table `munkafolyamat_sablonok`
--

CREATE TABLE `munkafolyamat_sablonok` (
  `sablon_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `lepesek` text DEFAULT NULL,
  `becsult_ido` varchar(7) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `munkafolyamat_sablonok`
--

INSERT INTO `munkafolyamat_sablonok` (`sablon_id`, `nev`, `lepesek`, `becsult_ido`) VALUES
(1, 'Így neveld a sárkányodat', 'Adj neki kaját\r\nne égessen el', '31 nap');

-- --------------------------------------------------------

--
-- Table structure for table `rendelesek`
--

CREATE TABLE `rendelesek` (
  `rendeles_id` int(11) NOT NULL,
  `felhasznalo_id` int(11) NOT NULL,
  `alkatresz_id` int(11) NOT NULL,
  `mennyiseg` int(11) NOT NULL,
  `statusz` enum('Leadva','Kérvényezve','Elutasítva') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rendelesek`
--

INSERT INTO `rendelesek` (`rendeles_id`, `felhasznalo_id`, `alkatresz_id`, `mennyiseg`, `statusz`) VALUES
(1, 1, 8, 6, 'Kérvényezve'),
(2, 1, 1, 11, 'Kérvényezve'),
(3, 22, 1, 9, 'Kérvényezve');

-- --------------------------------------------------------

--
-- Table structure for table `szerelesi_utmutatok`
--

CREATE TABLE `szerelesi_utmutatok` (
  `utmutato_id` int(11) NOT NULL,
  `cim` varchar(100) DEFAULT NULL,
  `tartalom` text DEFAULT NULL,
  `jarmu_tipus` int(11) DEFAULT NULL,
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `szerelesi_utmutatok`
--

INSERT INTO `szerelesi_utmutatok` (`utmutato_id`, `cim`, `tartalom`, `jarmu_tipus`) VALUES
(4, 'Így ne oldd meg a Teslád', 'Sehogy nem lehet gatekeeping faszok', 16),
(5, 'valami', 'ez nemtom mi', 24);

-- --------------------------------------------------------

--
-- Table structure for table `szervizcsomagok`
--

CREATE TABLE `szervizcsomagok` (
  `csomag_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `leiras` text DEFAULT NULL,
  `ar` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `szervizcsomagok`
--

INSERT INTO `szervizcsomagok` (`csomag_id`, `nev`, `leiras`, `ar`) VALUES
(1, 'Olajcsere', 'Motorolaj cseréje szűrővel, ingyenes ellenőrzéssel.', 15003.00),
(2, 'Fékellenőrzés', 'Teljes fékrendszer ellenőrzése, fékbetétek és tárcsák felmérése.', 8000.00),
(3, 'Klíma tisztítás', 'Autó klímarendszerének tisztítása és fertőtlenítése.', 12000.00),
(4, 'Teljes szerviz', 'Átfogó szerviz, beleértve az olajcserét, fékellenőrzést és diagnosztikát.', 50000.00),
(5, 'Kerékcsere', 'Nyári vagy téli gumiabroncsok cseréje, centrírozással.', 10000.00),
(6, 'Klíma csere', 'Kicserélik az autóban a klímát', 90000.00),
(7, 'Hengerfej tisztítás', 'Megtisztítják a hengerfejet az autóban.', 11100.00);

-- --------------------------------------------------------

--
-- Table structure for table `tipus`
--

CREATE TABLE `tipus` (
  `tipus_id` int(11) NOT NULL,
  `tipus` varchar(50) DEFAULT NULL,
  `marka_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `tipus`
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
(25, 'Ariya', 1);

-- --------------------------------------------------------

--
-- Table structure for table `ugyfelek`
--

CREATE TABLE `ugyfelek` (
  `ugyfel_id` int(11) NOT NULL,
  `nev` varchar(100) DEFAULT NULL,
  `telefonszam` varchar(150) DEFAULT NULL,
  `cim` varchar(200) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `ugyfelek`
--

INSERT INTO `ugyfelek` (`ugyfel_id`, `nev`, `telefonszam`, `cim`, `email`) VALUES
(1, 'Chrén András', '+36123123123', '4011, Valahol, Liliom utca 4', 'andrasvalami@gmail.com'),
(3, 'b b', 'b', NULL, 'b@b.com'),
(4, 'c c', 'c', NULL, 'c@c.com'),
(5, 'd d', 'd', NULL, 'd@d.com'),
(6, 'Ernő Majom', '06702886912', '2600, Vác, Márc 15 tér 0', 'e@e.com'),
(7, 'f f', 'f', NULL, 'f@f.com'),
(8, 'g g', 'g', NULL, 'g@g.com'),
(9, 'h h', 'h', NULL, 'h@h.com'),
(10, 'i i', 'i', NULL, 'i@i.com'),
(11, 'j j', 'j', NULL, 'j@j.com'),
(12, 'k k', 'k', NULL, 'k@k.com'),
(13, 'l l', 'l', NULL, 'l@l.com'),
(14, 'ly ly', 'ly', NULL, 'ly@ly.com'),
(15, 'm m', 'm', NULL, 'm'),
(16, '', '', NULL, ''),
(17, 'n n', 'n', NULL, 'n@n.com'),
(18, 'Chrén András Ferenc', '+36204772772', '2624,Szokolya,Hunyadi utca,19', 'andrewchre1@gmail.com'),
(19, 'Albert Benyó Mária', '123456789', NULL, 'albertbenyo@gmail.com'),
(20, 'Fenyvesi Ákos', '06702962920', '2600, Vác, Szegfű utca 34', 'donfenyvesi@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `ugyfel_jarmuvek`
--

CREATE TABLE `ugyfel_jarmuvek` (
  `ugyfel_id` int(11) NOT NULL,
  `jarmu_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Dumping data for table `ugyfel_jarmuvek`
--

INSERT INTO `ugyfel_jarmuvek` (`ugyfel_id`, `jarmu_id`) VALUES
(18, 10),
(18, 34),
(18, 37),
(18, 38),
(18, 39),
(18, 40),
(18, 41),
(18, 42),
(18, 43),
(18, 44),
(18, 45),
(18, 46),
(18, 47),
(18, 48),
(18, 49),
(18, 50);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `alkatreszek`
--
ALTER TABLE `alkatreszek`
  ADD PRIMARY KEY (`alkatresz_id`);

--
-- Indexes for table `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`felhasznalo_id`),
  ADD UNIQUE KEY `felhasznalonev` (`felhasznalonev`);

--
-- Indexes for table `felhasznalok_ugyfelek`
--
ALTER TABLE `felhasznalok_ugyfelek`
  ADD PRIMARY KEY (`felhasznalo_id`,`ugyfel_id`),
  ADD KEY `ugyfel_id` (`ugyfel_id`);

--
-- Indexes for table `hibakodok`
--
ALTER TABLE `hibakodok`
  ADD PRIMARY KEY (`kod_id`);

--
-- Indexes for table `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD PRIMARY KEY (`idopont_id`),
  ADD KEY `jarmu_id` (`jarmu_id`),
  ADD KEY `csomag_id` (`csomag_id`);

--
-- Indexes for table `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD PRIMARY KEY (`jarmu_id`),
  ADD UNIQUE KEY `rendszam` (`rendszam`),
  ADD KEY `tipus_id` (`tipus_id`),
  ADD KEY `kod_id` (`kod_id`),
  ADD KEY `sablon_id` (`sablon_id`);

--
-- Indexes for table `marka`
--
ALTER TABLE `marka`
  ADD PRIMARY KEY (`marka_id`);

--
-- Indexes for table `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  ADD PRIMARY KEY (`sablon_id`);

--
-- Indexes for table `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD PRIMARY KEY (`rendeles_id`),
  ADD KEY `felhasznalo_id` (`felhasznalo_id`),
  ADD KEY `alkatresz_id` (`alkatresz_id`);

--
-- Indexes for table `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD PRIMARY KEY (`utmutato_id`),
  ADD KEY `fk_jarmu_tipus` (`jarmu_tipus`);

--
-- Indexes for table `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  ADD PRIMARY KEY (`csomag_id`);

--
-- Indexes for table `tipus`
--
ALTER TABLE `tipus`
  ADD PRIMARY KEY (`tipus_id`),
  ADD KEY `fk_marka_id` (`marka_id`);

--
-- Indexes for table `ugyfelek`
--
ALTER TABLE `ugyfelek`
  ADD PRIMARY KEY (`ugyfel_id`);

--
-- Indexes for table `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD PRIMARY KEY (`ugyfel_id`,`jarmu_id`),
  ADD KEY `jarmu_id` (`jarmu_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `alkatreszek`
--
ALTER TABLE `alkatreszek`
  MODIFY `alkatresz_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT for table `hibakodok`
--
ALTER TABLE `hibakodok`
  MODIFY `kod_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `idopont_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `jarmuvek`
--
ALTER TABLE `jarmuvek`
  MODIFY `jarmu_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT for table `marka`
--
ALTER TABLE `marka`
  MODIFY `marka_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `munkafolyamat_sablonok`
--
ALTER TABLE `munkafolyamat_sablonok`
  MODIFY `sablon_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `rendelesek`
--
ALTER TABLE `rendelesek`
  MODIFY `rendeles_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  MODIFY `utmutato_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `szervizcsomagok`
--
ALTER TABLE `szervizcsomagok`
  MODIFY `csomag_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `tipus`
--
ALTER TABLE `tipus`
  MODIFY `tipus_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `ugyfel_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `felhasznalok_ugyfelek`
--
ALTER TABLE `felhasznalok_ugyfelek`
  ADD CONSTRAINT `felhasznalok_ugyfelek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `felhasznalok_ugyfelek_ibfk_2` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE;

--
-- Constraints for table `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD CONSTRAINT `idopontfoglalasok_ibfk_1` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`),
  ADD CONSTRAINT `idopontfoglalasok_ibfk_2` FOREIGN KEY (`csomag_id`) REFERENCES `szervizcsomagok` (`csomag_id`);

--
-- Constraints for table `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD CONSTRAINT `jarmuvek_ibfk_1` FOREIGN KEY (`tipus_id`) REFERENCES `tipus` (`tipus_id`),
  ADD CONSTRAINT `jarmuvek_ibfk_2` FOREIGN KEY (`kod_id`) REFERENCES `hibakodok` (`kod_id`),
  ADD CONSTRAINT `jarmuvek_ibfk_3` FOREIGN KEY (`sablon_id`) REFERENCES `munkafolyamat_sablonok` (`sablon_id`);

--
-- Constraints for table `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD CONSTRAINT `rendelesek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `rendelesek_ibfk_2` FOREIGN KEY (`alkatresz_id`) REFERENCES `alkatreszek` (`alkatresz_id`) ON DELETE CASCADE;

--
-- Constraints for table `szerelesi_utmutatok`
--
ALTER TABLE `szerelesi_utmutatok`
  ADD CONSTRAINT `fk_jarmu_tipus` FOREIGN KEY (`jarmu_tipus`) REFERENCES `tipus` (`tipus_id`);

--
-- Constraints for table `tipus`
--
ALTER TABLE `tipus`
  ADD CONSTRAINT `fk_marka_id` FOREIGN KEY (`marka_id`) REFERENCES `marka` (`marka_id`);

--
-- Constraints for table `ugyfel_jarmuvek`
--
ALTER TABLE `ugyfel_jarmuvek`
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_1` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `ugyfel_jarmuvek_ibfk_2` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
