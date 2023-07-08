/*
 Navicat Premium Data Transfer

 Source Server         : local
 Source Server Type    : MySQL
 Source Server Version : 100424
 Source Host           : localhost:3306
 Source Schema         : giny_auth

 Target Server Type    : MySQL
 Target Server Version : 100424
 File Encoding         : 65001

 Date: 08/07/2023 02:39:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `LastSelectedServerId` int(11) NULL DEFAULT 0,
  `IPs` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `CharactersSlots` int(255) NULL DEFAULT 5,
  `Banned` int(255) NULL DEFAULT 0,
  `Role` int(255) NULL DEFAULT 1,
  `Nickname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Ogrines` int(11) NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 195 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES (1, 'admin', 'overflow35', 291, '127.0.0.1', 15, 0, 5, 'Street', 600);
INSERT INTO `accounts` VALUES (2, 'admin2', 'overflow35', 291, '127.0.0.1', 5, 0, 5, 'Skinz', 0);

-- ----------------------------
-- Table structure for webcomments
-- ----------------------------
DROP TABLE IF EXISTS `webcomments`;
CREATE TABLE `webcomments`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NewId` int(11) NULL DEFAULT NULL,
  `Author` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Content` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Date` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 28 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for webnews
-- ----------------------------
DROP TABLE IF EXISTS `webnews`;
CREATE TABLE `webnews`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Content` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Views` int(11) NULL DEFAULT NULL,
  `ImageLink` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `AuthorName` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Date` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 56 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of webnews
-- ----------------------------
INSERT INTO `webnews` VALUES (5, 'Mise a jour du 19 mai', '         <p>Bonjour à tous, <br> Nous espérons que vous amusez sur Raiders ! Voici un récapitulatif des derniers changements : \r\n\r\n<h2>Roleplay</h2>\r\n <ul>\r\n <li>Il est désormais possible de créer une <b>guilde</b> a pandala </li>\r\n  <li>La rate de drop d\'items (hors pokéfus) passe de <b>1</b> à <b>2.5</b></li>\r\n  <li>La rate des métiers passe de <b>1</b> à <b>3</b></li> \r\n  <li>Les <b>soleils</b> (changement de cartes) fonctionnent correctement</li> \r\n <li>Le <b>domaine ancestral</b> apporte désormais des récompenses de fin de donjon <img  src=\"https://i.imgur.com/8xtdH6R.png\"/> </li>\r\n</ul> \r\n\r\n<h2>Combats</h2>\r\n <ul>\r\n  <li>La couleur du Glyphe de Répulsion du féca a été corrigée</li>\r\n  <li>Les invocations de l\'Osamodas jouent correctement</li>\r\n  <li>Le message affiché lors du drop d\'un Dofus a été corrigé</li>\r\n  <li>Les monstres jouent correctement lorsqu\'un personnage est invisible</li>\r\n</ul> \r\n\r\n\r\nBon jeu sur Raiders !          ', 44, 'assets/images/news/11.jpg', 'Admin', '2021-04-07 18:47:29');
INSERT INTO `webnews` VALUES (2, 'Une bonne année 2021 !', ' hello ', 15, 'assets/images/news/12.jpg', 'Admin', '2021-04-16 00:05:18');
INSERT INTO `webnews` VALUES (3, 'Pokéfus, en avant !', ' <b> Pokéfus est de retour ! </b> ', 43, 'assets/images/news/9.jpg', 'Admin', '2021-05-01 00:05:37');
INSERT INTO `webnews` VALUES (7, 'Mise a jour du 21 mai', '                <p>Bonjour à tous, <br> Nous espérons que vous amusez sur Raiders ! Voici un récapitulatif des derniers changements : \r\n\r\n<h2>Roleplay</h2>\r\n <ul>\r\n <li>Fix d\'un bug guilde permettant d\'avoir deux meneurs</li>\r\n  <li>Le drop de ressouces unique a été ajusté en fonction de la tranche de niveau</li>\r\n<li>La dernière salle du donjon scarafeuille a été debug</li>\r\n<li>La dernière salle du Clos des blops a été debug</li>\r\n<li>L\'avant dernière salle du donjon ensablé a été debug</li>\r\n<li>ajout d\'une commande <b>.event</b></li>\r\n  <li>Ajout d\'items boutiques</li> <img  src=\"https://i.imgur.com/lsbStVe.png\" />\r\n  <li>Le maître des donjons a été mis a jour</li> <img  src=\"https://i.imgur.com/UBZApkf.png\" />\r\n <li>Ajout d\'un PNJ <b>Téléporteur donjons intermédiaires</b> a pandala.</li>  <img  src=\"https://i.imgur.com/TRqVqMW.png\"/> \r\n</ul> \r\n<li> Ajout d\'un PNJ permettant d\'obtenir des parchemins de caracteristiques </li> <img src=\"https://i.imgur.com/6Q0f4dL.png\"/>\r\n\r\n<h2>Combats</h2>\r\n <ul>\r\n  <li>Fix d\'un bug empéchant le sadida d\'invoquer correctement</li>\r\n  <li>Fix d\'une interaction pokéfus qui empéchait de controller son pokéfus après l\'invocation du double du sram</li>\r\n</ul> \r\n\r\n<h2>Divers</h2>\r\n <ul>\r\n  <li>Il est desormais possible de télécharger le client sur 1Fichier ou sur Mega</li>\r\n  <li>L\'Uplauncher a été corrigé</li>\r\n</ul> \r\n\r\n\r\n\r\nBon jeu sur Raiders !                 ', 86, 'assets/images/news/8.jpg', 'Admin', '2021-05-02 00:05:55');
INSERT INTO `webnews` VALUES (6, 'Mise a jour du 20 mai', '                <p>Bonjour à tous, <br> Nous espérons que vous amusez sur Raiders ! Voici un récapitulatif des derniers changements : \r\n\r\n<h2>Roleplay</h2>\r\n<ul>\r\n <li>Vous pouvez désormais loot <b>toute les ressources du jeu en théorie non droppable</b> sur les monstres. Ainsi, tout les items du jeu sont craftable</li>\r\n  <li>Fix d\'un bug qui empêchait d\'inviter un personnage dans un groupe.</li>\r\n  <li>Fix d\'un bug qui conduisait a des quantités d\'item négatif suite a un craft.</li> \r\n  <li>Plusieurs <b>Pokéfus</b> ont été nerf.</li> \r\n  <li>Ajout de la commande <b>.spawn</b> qui téléporte au point de sauvegarde.</li> \r\n <li>Un PNJ vendeur d\'équipement jusqu\'au niveau 60 a été ajouté  </li> <img  src=\"https://i.imgur.com/p1oS4zh.png\"/>\r\n <li><b>Epée de justice</b> a été ajouté a la boutique.  </li> <img  src=\"https://i.imgur.com/PErDLEg.png\"/>\r\n</ul>\r\n\r\n\r\n\r\n<h2>Combats</h2>\r\n\r\n<ul>\r\n  <li>Fix d\'un bug qui empechait a l\'énutrof d\'invoquer</li> \r\n  <li>Les <b>Pokéfus</b> ne comptent plus comme des invocations</li>\r\n</ul>\r\n\r\n<h2>Site</h2>\r\n\r\n<ul>\r\n<li>Le bug de votes non comptabilisé a été fixé</li>\r\n</ul>\r\n     ', 77, 'assets/images/news/13.jpg', 'Admin', '2021-04-16 00:06:20');

-- ----------------------------
-- Table structure for worldcharacters
-- ----------------------------
DROP TABLE IF EXISTS `worldcharacters`;
CREATE TABLE `worldcharacters`  (
  `Id` int(11) NOT NULL,
  `CharacterId` bigint(20) NULL DEFAULT NULL,
  `AccountId` int(11) NULL DEFAULT NULL,
  `ServerId` smallint(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of worldcharacters
-- ----------------------------
INSERT INTO `worldcharacters` VALUES (5, 5, 1, 291);
INSERT INTO `worldcharacters` VALUES (4, 4, 1, 291);
INSERT INTO `worldcharacters` VALUES (3, 3, 1, 291);
INSERT INTO `worldcharacters` VALUES (2, 2, 1, 291);
INSERT INTO `worldcharacters` VALUES (1, 1, 1, 291);
INSERT INTO `worldcharacters` VALUES (7, 7, 1, 291);
INSERT INTO `worldcharacters` VALUES (6, 6, 1, 291);

-- ----------------------------
-- Table structure for worldservers
-- ----------------------------
DROP TABLE IF EXISTS `worldservers`;
CREATE TABLE `worldservers`  (
  `Id` smallint(6) NOT NULL,
  `Name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Type` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `MonoAccount` mediumtext CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `Host` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `Port` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of worldservers
-- ----------------------------
INSERT INTO `worldservers` VALUES (291, 'Imagiro', 'SERVER_TYPE_CLASSICAL', '0', 'localhost', 5555);

SET FOREIGN_KEY_CHECKS = 1;
