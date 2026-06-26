-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : mar. 02 déc. 2025 à 08:41
-- Version du serveur : 9.1.0
-- Version de PHP : 8.3.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `giny_auth`
--

-- --------------------------------------------------------

--
-- Structure de la table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `LastSelectedServerId` int DEFAULT '0',
  `IPs` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci,
  `CharactersSlots` int DEFAULT '5',
  `Banned` int DEFAULT '0',
  `Role` int DEFAULT '1',
  `Nickname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `Ogrines` int DEFAULT '0',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=315 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Déchargement des données de la table `accounts`
--

INSERT INTO `accounts` (`Id`, `Username`, `Password`, `LastSelectedServerId`, `IPs`, `CharactersSlots`, `Banned`, `Role`, `Nickname`, `Ogrines`) VALUES
(1, 'admin', 'test', 291, '127.0.0.1', 15, 0, 5, 'Street', 600),
(2, 'admin2', 'test', 291, '127.0.0.1', 5, 0, 5, 'Skinz', 0);

-- --------------------------------------------------------

--
-- Structure de la table `world_characters`
--

DROP TABLE IF EXISTS `world_characters`;
CREATE TABLE IF NOT EXISTS `world_characters` (
  `Id` int NOT NULL,
  `CharacterId` bigint DEFAULT NULL,
  `AccountId` int DEFAULT NULL,
  `ServerId` smallint DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=FIXED;

--
-- Déchargement des données de la table `world_characters`
--

-- --------------------------------------------------------

--
-- Structure de la table `world_servers`
--

DROP TABLE IF EXISTS `world_servers`;
CREATE TABLE IF NOT EXISTS `world_servers` (
  `Id` smallint NOT NULL,
  `Name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `Type` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci,
  `MonoAccount` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci,
  `Host` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `Port` int DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

--
-- Déchargement des données de la table `world_servers`
--

INSERT INTO `world_servers` (`Id`, `Name`, `Type`, `MonoAccount`, `Host`, `Port`) VALUES
(291, 'Imagiro', 'SERVER_TYPE_CLASSICAL', '0', '127.0.0.1', 5555),
(50, 'Ombre', 'SERVER_TYPE_HARDCORE', '0', 'localhost', 5555);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
