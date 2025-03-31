CREATE TABLE `alkatreszek` ( 
  `alkatresz_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `nev` varchar(100) DEFAULT NULL, 
  `leiras` text DEFAULT NULL, 
  `keszlet_mennyiseg` int(11) DEFAULT NULL, 
  `utanrendelesi_szint` int(11) DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

CREATE TABLE `felhasznalok` ( 
  `felhasznalo_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `felhasznalonev` varchar(50) DEFAULT NULL UNIQUE KEY, 
  `jelszo_hash` varchar(255) DEFAULT NULL, 
  `szerep` enum('Adminisztrátor','Szerelő','Ügyfél') DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

CREATE TABLE `ugyfelek` (
  `ugyfel_id` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `nev` varchar(100) DEFAULT NULL,
  `telefonszam` varchar(150) DEFAULT NULL,
  `cim` varchar(200) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci; 

CREATE TABLE `felhasznalok_ugyfelek` ( 
  `felhasznalo_id` int(11) NOT NULL, 
  `ugyfel_id` int(11) NOT NULL,
  PRIMARY KEY (`felhasznalo_id`,`ugyfel_id`),
  CONSTRAINT `felhasznalok_ugyfelek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  CONSTRAINT `felhasznalok_ugyfelek_ibfk_2` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

CREATE TABLE `hibakodok` ( 
  `kod_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `kod` varchar(10) DEFAULT NULL, 
  `leiras` text DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

CREATE TABLE `szervizcsomagok` ( 
  `csomag_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `nev` varchar(100) DEFAULT NULL, 
  `leiras` text DEFAULT NULL, 
  `ar` decimal(10,2) DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

INSERT INTO szervizcsomagok (nev, leiras, ar) VALUES
('Olajcsere', 'A motorolaj időszakos cseréje elengedhetetlen a motor élettartamának növelése érdekében. Szolgáltatásunk során prémium minőségű olajjal és új olajszűrővel látjuk el járművét.', 20000),
('Fékrendszer ellenőrzés', 'A fékek megfelelő működése létfontosságú a biztonságos közlekedéshez. Ellenőrizzük a fékbetéteket, tárcsákat és a fékfolyadék szintjét, és szükség esetén cserét is végzünk.', 15000),
('Gumicsere', 'Nyári vagy téli gumik cseréje, centírozása és állapotfelmérése modern gépekkel. Biztosítjuk a megfelelő tapadást és futómű-geometriát a biztonságos vezetés érdekében.', 10000),
('Klímarendszer karbantartás', 'A klímaberendezés tisztítása és a hűtőközeg újratöltése biztosítja a megfelelő hűtési teljesítményt és a baktériummentes levegőt. Kellemetlen szagok és hatékonyságvesztés esetén ajánlott.', 25000),
('Futómű beállítás', 'A helytelen futómű-beállítás egyenetlen gumikopást és instabil vezetési élményt eredményezhet. Precíz mérőműszereinkkel pontos beállítást végzünk a hosszabb élettartam és jobb tapadás érdekében.', 18000),
('Motor diagnosztika', 'Modern számítógépes diagnosztikával kiolvassuk és elemezzük a motorvezérlő hibakódjait. Segítünk az ismeretlen motorhibák gyors feltárásában és a javítási lehetőségek meghatározásában.', 12000),
('Akkumulátor ellenőrzés', 'Akkumulátora állapotának felmérése kulcsfontosságú a megbízható indításhoz. Feszültség- és kapacitásméréssel meghatározzuk, hogy szükség van-e töltésre vagy cserére.', 8000),
('Vezérműszíj csere', 'A vezérműszíj az egyik legfontosabb alkatrész a motorban, amely meghibásodás esetén súlyos károkat okozhat. Teljes szíj- és feszítőgörgő-cserét végzünk a gyári előírások szerint.', 45000),
('Kipufogórendszer ellenőrzés', 'Vizsgáljuk a kipufogórendszer szivárgásait, a katalizátor állapotát és a hangtompító épségét. Segítünk a környezetbarát és hatékony működés fenntartásában.', 13000),
('Teljes autó átvizsgálás', 'Részletes állapotfelmérés minden főbb járműkomponensre, beleértve a motor, fékek, futómű, akkumulátor és elektromos rendszerek ellenőrzését. Ajánlott hosszabb utak vagy használt autó vásárlás előtt.', 30000);


CREATE TABLE `munkafolyamat_sablonok` ( 
  `sablon_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `nev` varchar(100) DEFAULT NULL, 
  `lepesek` text DEFAULT NULL, 
  `becsult_ido` time DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci; 

CREATE TABLE marka ( 
    marka_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
    marka_neve varchar(50) DEFAULT NULL 
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO marka (marka_neve) VALUES
('Toyota'),
('BMW'),
('Ford');

CREATE TABLE `tipus` (
  `tipus_id` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `tipus` varchar(50) DEFAULT NULL,
  `marka_id` int(11) DEFAULT NULL,
  CONSTRAINT `tipus_ibfk_1` FOREIGN KEY (`marka_id`) REFERENCES `marka` (`marka_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

INSERT INTO tipus (tipus, marka_id) VALUES
('Corolla', 1),
('Camry', 1),
('RAV4', 1),
('320i', 2),
('X5', 2),
('M4', 2),
('Focus', 3),
('Mustang', 3),
('Explorer', 3);

CREATE TABLE `szerelesi_utmutatok` ( 
  `utmutato_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `cim` varchar(100) DEFAULT NULL, 
  `tartalom` text DEFAULT NULL, 
  `jarmu_tipus` int(50) DEFAULT NULL,
  CONSTRAINT `szerelesi_utmutatok_ibfk_1` FOREIGN KEY (`jarmu_tipus`) REFERENCES `tipus` (`tipus_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci; 

CREATE TABLE `jarmuvek` ( 
  `jarmu_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `rendszam` varchar(10) DEFAULT NULL UNIQUE KEY, 
  `tipus_id` int(11) DEFAULT NULL, 
  `kod_id` int(11) DEFAULT NULL, 
  `sablon_id` int(11) DEFAULT NULL, 
  `gyartas_eve` year(4) DEFAULT NULL, 
  `motor_adatok` varchar(100) DEFAULT NULL, 
  `alvaz_adatok` varchar(100) DEFAULT NULL, 
  `elozo_javitasok` text DEFAULT NULL,
  CONSTRAINT `jarmuvek_ibfk_1` FOREIGN KEY (`tipus_id`) REFERENCES `tipus` (`tipus_id`), 
 CONSTRAINT `jarmuvek_ibfk_2` FOREIGN KEY (`kod_id`) REFERENCES `hibakodok` (`kod_id`), 
 CONSTRAINT `jarmuvek_ibfk_3` FOREIGN KEY (`sablon_id`) REFERENCES `munkafolyamat_sablonok` (`sablon_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

CREATE TABLE `idopontfoglalasok` ( 
  `idopont_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, 
  `jarmu_id` int(11) DEFAULT NULL, 
  `csomag_id` int(11) DEFAULT NULL, 
  `idopont` datetime DEFAULT NULL, 
  `allapot` enum('Foglalt','Folyamatban','Befejezett','Lemondva') DEFAULT NULL,
  CONSTRAINT `idopontfoglalasok_ibfk_1` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`),
  CONSTRAINT `idopontfoglalasok_ibfk_2` FOREIGN KEY (`csomag_id`) REFERENCES `szervizcsomagok` (`csomag_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci; 

CREATE TABLE `rendelesek` (
  `rendeles_id` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `felhasznalo_id` int(11) NOT NULL,
  `alkatresz_id` int(11) NOT NULL,
  `mennyiseg` int(11) NOT NULL,
  `statusz` enum('Leadva','Kérvényezve','Elutasítva') NOT NULL,
  CONSTRAINT `rendelesek_ibfk_1` FOREIGN KEY (`felhasznalo_id`) REFERENCES `felhasznalok` (`felhasznalo_id`) ON DELETE CASCADE,
  CONSTRAINT `rendelesek_ibfk_2` FOREIGN KEY (`alkatresz_id`) REFERENCES `alkatreszek` (`alkatresz_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `ugyfel_jarmuvek` ( 
  `ugyfel_id` int(11) NOT NULL, 
  `jarmu_id` int(11) NOT NULL,
  PRIMARY KEY (`ugyfel_id`,`jarmu_id`),
 CONSTRAINT `ugyfel_jarmuvek_ibfk_1` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`ugyfel_id`) ON DELETE CASCADE, 
 CONSTRAINT `ugyfel_jarmuvek_ibfk_2` FOREIGN KEY (`jarmu_id`) REFERENCES `jarmuvek` (`jarmu_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;