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
-- Table structure for table `marka`
--

CREATE TABLE `marka` (
  `marka_id` int(11) NOT NULL,
  `marka_neve` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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


-- --------------------------------------------------------

--
-- Table structure for table `szerelesi_utmutatok`
--

CREATE TABLE `szerelesi_utmutatok` (
  `utmutato_id` int(11) NOT NULL,
  `cim` varchar(100) DEFAULT NULL,
  `tartalom` text DEFAULT NULL,
  `jarmu_tipus` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;


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
-- Table structure for table `tipus`
--

CREATE TABLE `tipus` (
  `tipus_id` int(11) NOT NULL,
  `tipus` varchar(50) DEFAULT NULL,
  `marka_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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

-- --------------------------------------------------------

--
-- Table structure for table `ugyfel_jarmuvek`
--

CREATE TABLE `ugyfel_jarmuvek` (
  `ugyfel_id` int(11) NOT NULL,
  `jarmu_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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
-- AUTO_INCREMENT for table `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
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