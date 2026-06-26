/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 100428 (10.4.28-MariaDB)
 Source Host           : localhost:3306
 Source Schema         : giny_auth

 Target Server Type    : MySQL
 Target Server Version : 100428 (10.4.28-MariaDB)
 File Encoding         : 65001

 Date: 10/12/2023 15:46:33
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `LastSelectedServerId` int NULL DEFAULT 0,
  `IPs` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `CharactersSlots` int NULL DEFAULT 5,
  `Banned` int NULL DEFAULT 0,
  `Role` int NULL DEFAULT 1,
  `Nickname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Ogrines` int NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 195 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES (1, 'admin', 'test', 50, '127.0.0.1', 15, 0, 5, 'Street', 600);
INSERT INTO `accounts` VALUES (2, 'admin2', 'test', 50, '127.0.0.1', 5, 0, 5, 'Skinz', 0);

-- ----------------------------
-- Table structure for world_characters
-- ----------------------------
DROP TABLE IF EXISTS `world_characters`;
CREATE TABLE `world_characters`  (
  `Id` int NOT NULL,
  `CharacterId` bigint NULL DEFAULT NULL,
  `AccountId` int NULL DEFAULT NULL,
  `ServerId` smallint NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = FIXED;

-- ----------------------------
-- Records of world_characters
-- ----------------------------

-- ----------------------------
-- Table structure for world_servers
-- ----------------------------
DROP TABLE IF EXISTS `world_servers`;
CREATE TABLE `world_servers`  (
  `Id` smallint NOT NULL,
  `Name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Type` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `MonoAccount` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Host` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Port` int NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of world_servers
-- ----------------------------
INSERT INTO `world_servers` VALUES (291, 'Imagiro', 'SERVER_TYPE_CLASSICAL', '0', 'localhost', 5555);
INSERT INTO `world_servers` VALUES (50, 'Ombre', 'SERVER_TYPE_HARDCORE', '0', 'localhost', 5555);

SET FOREIGN_KEY_CHECKS = 1;
