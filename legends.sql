/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50719
Source Host           : 127.0.0.1:3306
Source Database       : legends

Target Server Type    : MYSQL
Target Server Version : 50719
File Encoding         : 65001

Date: 2018-05-30 17:12:15
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for champions
-- ----------------------------
DROP TABLE IF EXISTS `champions`;
CREATE TABLE `champions` (
  `Id` int(40) NOT NULL AUTO_INCREMENT,
  `Name` mediumtext,
  `BaseHp` mediumtext,
  `BaseMp` mediumtext,
  `BaseMovementSpeed` mediumtext,
  `AbilityPowerIncPerLevel` mediumtext,
  `ArmorPerLevel` mediumtext,
  `AttackRange` mediumtext,
  `AttackSpeedPerLevel` mediumtext,
  `BaseAbilityPower` mediumtext,
  `BaseCritChance` mediumtext,
  `BaseDamage` mediumtext,
  `CritPerLevel` mediumtext,
  `DamagePerLevel` mediumtext,
  `HpPerLevel` mediumtext,
  `HpRegenPerLevel` mediumtext,
  `IsMelee` mediumtext,
  `MpPerLevel` mediumtext,
  `MPRegenPerLevel` mediumtext,
  `BaseArmor` mediumtext,
  `BaseMagicResist` mediumtext,
  `BaseHpRegen` mediumtext,
  `BaseMpRegen` mediumtext,
  `MagicResistPerLevel` mediumtext,
  `AttackDelayOffsetPercent` mediumtext,
  `BaseDodge` mediumtext,
  `BaseAttackSpeed` mediumtext,
  `SelectionHeight` mediumtext,
  `SelectionRadius` mediumtext,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=430 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of champions
-- ----------------------------
INSERT INTO `champions` VALUES ('1', 'Annie', '384', '250', '335', '0', '4', '625', '1,36', '0', '0', '48', '0', '2,625', '76', '0,11', 'False', '50', '0,12', '12,5', '30', '0,9', '1,38', '0', '0,08', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('2', 'Olaf', '441', '190', '350', '0', '3', '125', '2,7', '0', '0', '54,1', '0', '3,5', '93', '0,18', 'True', '45', '0,115', '21', '30', '1,4', '1,3', '1,25', '-0,1', '0', '0', '191,6667', '111,1111');
INSERT INTO `champions` VALUES ('3', 'Galio', '435', '235', '335', '0', '3,5', '125', '1,2', '0', '0', '56,3', '0', '3,375', '85', '0,15', 'True', '50', '0,14', '21', '30', '1,49', '1,4', '0', '-0,02', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('4', 'TwistedFate', '384', '202', '330', '0', '3,15', '525', '3,22', '0', '0', '46,41', '0', '3,3', '82', '0,12', 'False', '38', '0,1', '15,25', '30', '0,9', '1,3', '0', '-0,04', '0', '0', '140', '110');
INSERT INTO `champions` VALUES ('5', 'XinZhao', '445', '215', '345', '0', '3,5', '175', '2,6', '0', '0', '52', '0', '3,3', '87', '0,14', 'True', '35', '0,09', '20', '30', '1,4', '1,3', '1,25', '-0,07', '0', '0', '138,8889', '108,3333');
INSERT INTO `champions` VALUES ('6', 'Urgot', '437', '220', '335', '0', '3,3', '425', '2,9', '0', '0', '48', '0', '3,6', '89', '0,12', 'False', '55', '0,13', '19', '30', '1,1', '1,5', '0', '-0,03', '0', '0', '155,5556', '136,1111');
INSERT INTO `champions` VALUES ('7', 'Leblanc', '390', '250', '335', '0', '3,5', '525', '1,4', '0', '0', '51', '0', '3,5', '75', '0,11', 'False', '50', '0,12', '16', '30', '1,3', '1,38', '0', '0', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('8', 'Vladimir', '400', '0', '335', '0', '3,5', '450', '2', '0', '0', '45', '0', '3', '85', '0,12', 'False', '0', '0', '16', '30', '1,2', '0', '0', '-0,05', '0', '0', '183,3333', '120');
INSERT INTO `champions` VALUES ('9', 'FiddleSticks', '390', '251', '335', '0', '3,5', '480', '2,11', '0', '0', '45,95', '0', '2,625', '80', '0,12', 'False', '59', '0,13', '15', '30', '0,92', '1,38', '0', '0', '0', '0', '170', '135');
INSERT INTO `champions` VALUES ('10', 'Kayle', '418', '255', '335', '0', '3,5', '125', '2,5', '0', '0', '53,3', '0', '2,8', '93', '0,15', 'True', '40', '0,105', '21', '30', '1,4', '1,38', '0', '-0,02', '0', '0', '240', '140');
INSERT INTO `champions` VALUES ('11', 'MasterYi', '444', '180', '355', '0', '3', '125', '2', '0', '0', '55', '0', '3', '92', '0,13', 'True', '42', '0,09', '19', '30', '1,3', '1,3', '1,25', '-0,08', '0', '0', '155', '125');
INSERT INTO `champions` VALUES ('12', 'Alistar', '442', '215', '330', '0', '3,5', '125', '2,125', '0', '0', '55,03', '0', '3,62', '102', '0,17', 'True', '38', '0,09', '18,5', '30', '1,45', '1,29', '1,25', '0', '0', '0', '170', '145');
INSERT INTO `champions` VALUES ('13', 'Ryze', '414', '250', '340', '0', '3,9', '550', '2,112', '0', '0', '52', '0', '3', '86', '0,11', 'False', '55', '0,12', '15', '30', '0,87', '1,4', '0', '0', '0', '0', '150', '120');
INSERT INTO `champions` VALUES ('14', 'Sion', '403', '200', '345', '0', '3,25', '125', '1,63', '0', '0', '55,52', '0', '3,1875', '104', '0,19', 'True', '40', '0,08', '21,75', '30', '1,58', '1,26', '1,25', '0', '0', '0', '160', '125');
INSERT INTO `champions` VALUES ('15', 'Sivir', '378', '203', '335', '0', '3,25', '500', '1,6', '0', '0', '49', '0', '3', '82', '0,11', 'False', '43', '0,1', '16,75', '30', '0,85', '1,3', '0', '-0,05', '0', '0', '160', '115');
INSERT INTO `champions` VALUES ('16', 'Soraka', '405', '240', '340', '0', '3,8', '550', '2,14', '0', '0', '48,8', '0', '3', '76', '0,11', 'False', '60', '0,13', '17', '30', '0,9', '1,36', '0', '0', '0', '0', '145', '110');
INSERT INTO `champions` VALUES ('17', 'Teemo', '378', '200', '330', '0', '3,75', '500', '3,38', '0', '0', '44,5', '0', '3', '82', '0,13', 'False', '40', '0,09', '18', '30', '0,93', '1,29', '0', '-0,0947', '0', '0', '100', '100');
INSERT INTO `champions` VALUES ('18', 'Tristana', '415', '193', '325', '0', '3', '550', '4', '0', '0', '46,5', '0', '3', '82', '0,13', 'False', '32', '0,09', '19', '30', '1,02', '1,29', '0', '-0,04734', '0', '0', '110', '100');
INSERT INTO `champions` VALUES ('19', 'Warwick', '428', '190', '345', '0', '3,5', '125', '2,88', '0', '0', '56,76', '0', '3,375', '98', '0,16', 'True', '30', '0,12', '20', '30', '1,41', '1,42', '1,25', '-0,08', '0', '0', '175', '140');
INSERT INTO `champions` VALUES ('20', 'Nunu', '437', '213', '350', '0', '3,5', '125', '2,25', '0', '0', '51,06', '0', '3,45', '96', '0,16', 'True', '42', '0,1', '20,5', '30', '1,41', '1,32', '1,25', '0', '0', '0', '170', '140');
INSERT INTO `champions` VALUES ('21', 'MissFortune', '435', '212', '325', '0', '3', '550', '3,01', '0', '0', '46,5', '0', '3', '85', '0,13', 'False', '38', '0,13', '19', '30', '1,02', '1,39', '0', '-0,04734', '0', '0', '122,2222', '100');
INSERT INTO `champions` VALUES ('22', 'Ashe', '527,72', '231,8', '325', '0', '3,4', '600', '4', '0', '0', '51,088', '0', '2,85', '79', '0,11', 'False', '35', '0,08', '21,212', '30', '1,0848', '1,3944', '0', '-0,05', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('23', 'Tryndamere', '461', '100', '345', '0', '3,1', '125', '2,9', '0', '0', '56', '0', '3,2', '98', '0,18', 'True', '0', '0', '18,9', '30', '1,4', '0', '1,25', '-0,0672', '0', '0', '180', '135');
INSERT INTO `champions` VALUES ('24', 'Jax', '463', '230', '350', '0', '3,5', '125', '3,4', '0', '0', '56,3', '0', '3,375', '98', '0,11', 'True', '35', '0,14', '22', '30', '1,49', '1,28', '1,25', '-0,02', '0', '0', '130', '120');
INSERT INTO `champions` VALUES ('25', 'Morgana', '403', '240', '335', '0', '3,8', '450', '1,53', '0', '0', '51,58', '0', '3,5', '86', '0,12', 'False', '60', '0,13', '19', '30', '0,94', '1,36', '0', '0', '0', '0', '145', '120');
INSERT INTO `champions` VALUES ('26', 'Zilean', '380', '260', '335', '0', '3,8', '600', '2,13', '0', '0', '48,6', '0', '3', '71', '0,1', 'False', '60', '0,13', '10,75', '30', '0,92', '1,39', '0', '0', '0', '0', '185', '135');
INSERT INTO `champions` VALUES ('27', 'Singed', '542,76', '290,6', '345', '0', '3,5', '125', '1,81', '0', '0', '62,32', '0', '3,375', '82', '0,11', 'True', '45', '0,11', '27,88', '30', '1,6048', '1,5048', '0', '0,02', '0', '0', '175', '135');
INSERT INTO `champions` VALUES ('28', 'Evelynn', '531,2', '265,6', '340', '0', '3,8', '125', '3,6', '0', '0', '53,88', '0', '3,5', '90', '0,11', 'True', '45', '0,12', '26,5', '32,1', '1,9648', '1,6216', '1,25', '0', '0', '0', '110', '120');
INSERT INTO `champions` VALUES ('29', 'Twitch', '389', '220', '330', '0', '3', '550', '3,38', '0', '0', '49', '0', '3', '81', '0,12', 'False', '40', '0,09', '18', '30', '1', '1,3', '0', '-0,08', '0', '0', '120', '135');
INSERT INTO `champions` VALUES ('30', 'Karthus', '390', '270', '335', '0', '3,5', '450', '2,11', '0', '0', '42,2', '0', '3,25', '75', '0,11', 'False', '61', '0,12', '15', '30', '1,1', '1,3', '0', '0', '0', '0', '150', '100');
INSERT INTO `champions` VALUES ('31', 'Chogath', '440', '205', '345', '0', '3,5', '125', '1,44', '0', '0', '54,1', '0', '4,2', '80', '0,17', 'True', '40', '0,09', '23', '30', '1,5', '1,29', '1,25', '0', '0', '0', '150', '130');
INSERT INTO `champions` VALUES ('32', 'Amumu', '472', '220', '335', '0', '3,3', '125', '2,18', '0', '0', '47', '0', '3,8', '84', '0,17', 'True', '40', '0,105', '22', '30', '1,49', '1,3', '1,25', '-0,02', '0', '0', '130', '100');
INSERT INTO `champions` VALUES ('33', 'Rammus', '420', '255', '335', '0', '3,8', '125', '2,215', '0', '0', '50', '0', '3,5', '86', '0,11', 'True', '33', '0,1', '25', '30', '1,4', '1,4', '1,25', '0', '0', '0', '130', '120');
INSERT INTO `champions` VALUES ('34', 'Anivia', '350', '257', '325', '0', '4', '600', '1,68', '0', '0', '48', '0', '3,2', '70', '0,11', 'False', '53', '0,12', '14,5', '30', '0,93', '1,4', '0', '0', '0', '0', '140', '140');
INSERT INTO `champions` VALUES ('35', 'Shaco', '441', '230', '350', '0', '3,5', '125', '3', '0', '0', '51,7', '0', '3,5', '84', '0,11', 'True', '40', '0,09', '19', '30', '1,49', '1,28', '1,25', '-0,1', '0', '0', '180', '135');
INSERT INTO `champions` VALUES ('36', 'DrMundo', '433', '0', '345', '0', '3,5', '125', '2,8', '0', '0', '56,23', '0', '3', '89', '0,15', 'True', '0', '0', '21', '30', '1,3', '0', '1,25', '0', '0', '0', '175', '135');
INSERT INTO `champions` VALUES ('37', 'Sona', '380', '265', '330', '0', '3,3', '550', '2,3', '0', '0', '47', '0', '3', '70', '0,11', 'False', '45', '0,13', '12', '30', '0,9', '1,4', '0', '-0,03', '0', '0', '145', '110');
INSERT INTO `champions` VALUES ('38', 'Kassadin', '433', '230', '350', '0', '3,2', '150', '3,7', '0', '0', '52,3', '0', '3,9', '78', '0,1', 'True', '70', '0,12', '18', '30', '1,39', '1,38', '0', '-0,023', '0', '0', '165', '120');
INSERT INTO `champions` VALUES ('39', 'Irelia', '607,2', '288,8', '345', '0', '3,75', '125', '3,2', '0', '0', '61,544', '0', '3,3', '90', '0,13', 'True', '35', '0,13', '25,3', '32,1', '1,7184', '1,6184', '1,25', '-0,06', '0', '0', '120', '100');
INSERT INTO `champions` VALUES ('40', 'Janna', '487,04', '409,52', '335', '0', '3,8', '475', '2,61', '0', '0', '51,956', '0', '2,95', '78', '0,11', 'False', '64', '0,08', '19,384', '30', '1,0848', '1,8', '0', '0', '0', '10', '200', '125');
INSERT INTO `champions` VALUES ('41', 'Gangplank', '495', '215', '345', '0', '3,3', '125', '2,75', '0', '0', '54', '0', '3', '81', '0,15', 'True', '40', '0,14', '20,5', '30', '0,85', '1,3', '1,25', '-0,04', '0', '0', '145', '85');
INSERT INTO `champions` VALUES ('42', 'Corki', '375', '243', '325', '0', '3,5', '550', '2,3', '0', '0', '48,2', '0', '3', '82', '0,11', 'False', '37', '0,11', '17,5', '30', '0,9', '1,3', '0', '0', '0', '0', '170', '135');
INSERT INTO `champions` VALUES ('43', 'Karma', '383', '290', '335', '0', '3,8', '525', '2,3', '0', '0', '50', '0', '3,3', '83', '0,11', 'False', '50', '0,13', '14', '30', '0,94', '1,36', '0', '0', '0', '0', '145', '120');
INSERT INTO `champions` VALUES ('44', 'Taric', '619,2', '349,08', '340', '0', '3,2', '125', '2,02', '0', '0', '57,88', '0', '3,5', '90', '0,1', 'True', '56', '0,16', '25,876', '32,1', '1,588', '1,2', '1,25', '0', '0', '0', '165', '135');
INSERT INTO `champions` VALUES ('45', 'Veigar', '355', '250', '340', '0', '3,75', '525', '2,24', '0', '0', '48,3', '0', '2,625', '82', '0,11', 'False', '55', '0,12', '16,25', '30', '0,9', '1,38', '0', '0', '0', '0', '140', '93');
INSERT INTO `champions` VALUES ('48', 'Trundle', '455', '206', '350', '0', '2,7', '125', '2,9', '0', '0', '55', '0', '3', '96', '0,17', 'True', '45', '0,12', '23', '30', '1,6', '1,3', '1,25', '-0,0672', '0', '0', '130', '135');
INSERT INTO `champions` VALUES ('50', 'Swain', '516,04', '324', '335', '0', '4', '500', '2,11', '0', '0', '52,04', '0', '3', '78', '0,13', 'False', '50', '0,16', '22,72', '30', '1,5684', '1,2', '0', '0', '0', '0', '148,8889', '100');
INSERT INTO `champions` VALUES ('51', 'Caitlyn', '390', '255', '325', '0', '3,5', '650', '4', '0', '0', '47', '0', '3', '80', '0,11', 'False', '35', '0,11', '17', '30', '0,95', '1,3', '0', '0', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('53', 'Blitzcrank', '423', '200', '325', '0', '3,5', '125', '1,13', '0', '0', '55,66', '0', '3,5', '95', '0,15', 'True', '40', '0,1', '18,5', '30', '1,45', '1,32', '1,25', '0', '0', '0', '170', '165');
INSERT INTO `champions` VALUES ('54', 'Malphite', '423', '215', '335', '0', '3,75', '125', '3,4', '0', '0', '56,3', '0', '3,375', '90', '0,11', 'True', '40', '0,11', '22', '30', '1,49', '1,28', '1,25', '-0,02', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('55', 'Katarina', '559,4', '0', '345', '0', '3,5', '125', '2,74', '0', '0', '58,376', '0', '3,2', '80', '0,11', 'True', '0', '0', '26,88', '32,1', '1,5748', '0', '1,25', '-0,05', '0', '0', '140', '120');
INSERT INTO `champions` VALUES ('56', 'Nocturne', '440', '215', '345', '0', '3,5', '125', '2,7', '0', '0', '54', '0', '3,1', '85', '0,15', 'True', '35', '0,09', '21', '30', '1,4', '1,2', '1,25', '-0,065', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('57', 'Maokai', '421', '250', '335', '0', '4', '125', '2,125', '0', '0', '58', '0', '3,3', '90', '0,17', 'True', '46', '0,09', '22', '30', '1,45', '1,29', '0', '-0,1', '0', '0', '190', '125');
INSERT INTO `champions` VALUES ('58', 'Renekton', '426', '100', '345', '0', '3,8', '125', '2,65', '0', '0', '53,12', '0', '3,1', '87', '0,15', 'True', '0', '0', '19,2', '30', '1,34', '0', '1,25', '-0,06', '0', '0', '155', '125');
INSERT INTO `champions` VALUES ('59', 'JarvanIV', '420', '235', '340', '0', '3,6', '175', '2,5', '0', '0', '50', '0', '3,4', '90', '0,14', 'True', '40', '0,09', '18', '30', '1,4', '1,2', '1,25', '-0,05', '0', '0', '100', '125');
INSERT INTO `champions` VALUES ('60', 'Elise', '529,4', '324', '335', '0', '3,35', '550', '1,75', '0', '0', '50,54', '0', '3', '80', '0,12', 'False', '50', '0,16', '22,128', '30', '1,1416', '1,2', '0', '0', '0', '0', '145', '120');
INSERT INTO `champions` VALUES ('61', 'Orianna', '517,72', '334', '325', '0', '3', '525', '3,5', '0', '0', '40,368', '0', '2,6', '79', '0,11', 'False', '50', '0,16', '17,04', '30', '1,3748', '1,2', '0', '-0,05', '0', '0', '140', '120');
INSERT INTO `champions` VALUES ('62', 'MonkeyKing', '435', '202', '345', '0', '3,5', '175', '3', '0', '0', '54,5', '0', '3,2', '85', '0,13', 'True', '38', '0,13', '19', '30', '1,02', '1,39', '1,25', '-0,05', '0', '0', '122,2222', '100');
INSERT INTO `champions` VALUES ('63', 'Brand', '507,68', '325,6', '340', '0', '3,5', '550', '1,36', '0', '0', '57,04', '0', '3', '76', '0,11', 'False', '45', '0,12', '21,88', '30', '1,0848', '1,6016', '0', '0', '0', '0', '302,7778', '88,8889');
INSERT INTO `champions` VALUES ('64', 'LeeSin', '428', '200', '350', '0', '3,7', '125', '3', '0', '0', '55,8', '0', '3,2', '85', '0,14', 'True', '0', '0', '20', '30', '1,25', '10', '1,25', '-0,04', '0', '0', '225', '102,7778');
INSERT INTO `champions` VALUES ('67', 'Vayne', '498,44', '231,8', '330', '0', '3,4', '550', '4', '0', '0', '53,46', '0', '3,25', '83', '0,11', 'False', '35', '0,08', '19,012', '30', '1,0848', '1,3944', '0', '-0,05', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('68', 'Rumble', '450', '100', '345', '0', '3,5', '125', '1,85', '0', '0', '55,66', '0', '3,2', '80', '0,12', 'True', '0', '0', '20', '30', '1,4', '0', '1,25', '-0,03', '0', '0', '170', '165');
INSERT INTO `champions` VALUES ('69', 'Cassiopeia', '380', '250', '335', '0', '4', '550', '1,68', '0', '0', '47', '0', '3,2', '75', '0,1', 'False', '50', '0,15', '15,5', '30', '0,97', '1,42', '0', '-0,034', '0', '0', '120', '120');
INSERT INTO `champions` VALUES ('72', 'Skarner', '440', '205', '345', '0', '3,8', '125', '2,1', '0', '0', '54,1', '0', '4,2', '96', '0,17', 'True', '40', '0,09', '23', '30', '1,5', '1,29', '1,25', '0', '0', '0', '150', '130');
INSERT INTO `champions` VALUES ('74', 'Heimerdinger', '350', '240', '340', '0', '3', '550', '1,36', '0', '0', '53', '0', '2,7', '75', '0,12', 'False', '40', '0,12', '14', '30', '0', '1,2', '0', '0', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('75', 'Nasus', '561,2', '275,6', '350', '0', '3,5', '125', '3,48', '0', '0', '59,18', '0', '3,5', '90', '0,18', 'True', '45', '0,1', '24,88', '32,1', '1,8024', '1,488', '1,25', '-0,02', '0', '0', '170', '135');
INSERT INTO `champions` VALUES ('76', 'Nidalee', '370', '220', '335', '0', '3,5', '525', '3,22', '0', '0', '49', '0', '3,5', '90', '0,12', 'False', '45', '0,1', '15', '30', '1', '1,4', '0', '-0,0672', '0', '0', '155', '110');
INSERT INTO `champions` VALUES ('77', 'Udyr', '593,32', '270,4', '345', '0', '4', '125', '2,67', '0', '0', '58,286', '0', '3,2', '99', '0,15', 'True', '30', '0,09', '25,47', '32,1', '1,742', '1,5012', '1,25', '-0,05', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('78', 'Poppy', '423', '185', '345', '0', '4', '125', '3,35', '0', '0', '56,3', '0', '3,375', '81', '0,11', 'True', '30', '0,09', '22', '30', '1,49', '1,28', '0', '-0,02', '0', '0', '115', '95');
INSERT INTO `champions` VALUES ('79', 'Gragas', '583,52', '299,96', '330', '0', '3,6', '125', '2,05', '0', '0', '61,38', '0', '3,5', '89', '0,17', 'True', '47', '0,16', '26,048', '32,1', '1,7356', '1,2', '1,25', '-0,04', '0', '0', '185', '155');
INSERT INTO `champions` VALUES ('80', 'Pantheon', '433', '210', '355', '0', '3,9', '150', '2,95', '0', '0', '50,7', '0', '2,9', '87', '0,13', 'True', '34', '0,09', '21,1', '30', '1,35', '1,32', '1,25', '-0,08', '0', '0', '175', '125');
INSERT INTO `champions` VALUES ('81', 'Ezreal', '350', '235', '325', '0', '3,5', '550', '2,8', '0', '0', '47,2', '0', '3', '80', '0,11', 'False', '45', '0,13', '16', '30', '1,1', '1,4', '0', '0', '0', '0', '170', '115');
INSERT INTO `champions` VALUES ('82', 'Mordekaiser', '421', '120', '340', '0', '3,5', '125', '3', '0', '0', '51,7', '0', '3,5', '80', '0,11', 'True', '0', '0', '19', '30', '1,49', '0', '1,25', '-0,1', '0', '0', '190', '125');
INSERT INTO `champions` VALUES ('83', 'Yorick', '421', '235', '345', '0', '3,6', '125', '3', '0', '0', '51,7', '0', '3,5', '85', '0,14', 'True', '35', '0,09', '19', '30', '1,4', '1,2', '1,25', '0', '0', '0', '190', '125');
INSERT INTO `champions` VALUES ('84', 'Akali', '587,8', '200', '350', '0', '3,5', '125', '3,1', '0', '0', '58,376', '0', '3,2', '85', '0,13', 'True', '0', '0', '26,38', '32,1', '1,6684', '10', '1,25', '-0,1', '0', '0', '138,8889', '100');
INSERT INTO `champions` VALUES ('85', 'Kennen', '403', '200', '335', '0', '3,75', '550', '3,4', '0', '0', '47', '0', '3,3', '79', '0,13', 'False', '0', '0', '18', '30', '0,9', '10', '0', '-0,0947', '0', '0', '177,7778', '100');
INSERT INTO `champions` VALUES ('86', 'Garen', '455', '0', '345', '0', '2,7', '125', '2,9', '0', '0', '52', '0', '3,5', '96', '0,1', 'True', '0', '0', '23', '30', '1,4', '0', '1,25', '0', '0', '0', '188,8889', '75');
INSERT INTO `champions` VALUES ('89', 'Leona', '430', '235', '335', '0', '3,1', '125', '2,9', '0', '0', '55', '0', '3', '87', '0,17', 'True', '40', '0,14', '22', '30', '1,4', '1,4', '1,25', '0', '0', '0', '188,8889', '75');
INSERT INTO `champions` VALUES ('90', 'Malzahar', '380', '250', '340', '0', '3,5', '550', '1,36', '0', '0', '51,66', '0', '3', '80', '0,11', 'False', '45', '0,12', '16', '30', '0,9', '1,4', '0', '0', '0', '0', '302,7778', '88,8889');
INSERT INTO `champions` VALUES ('91', 'Talon', '440', '260', '350', '0', '3,5', '125', '2,7', '0', '0', '50', '0', '3,1', '85', '0,15', 'True', '40', '0,1', '21', '30', '1,45', '1,35', '1,25', '-0,065', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('92', 'Riven', '414', '0', '345', '0', '3,2', '125', '3,5', '0', '0', '51', '0', '3', '86', '0,1', 'True', '0', '0', '19', '30', '0,5', '0', '1,25', '0', '0', '0', '150', '130');
INSERT INTO `champions` VALUES ('96', 'KogMaw', '440', '255', '330', '0', '3,5', '500', '2,65', '0', '0', '46', '0', '3', '84', '0,11', 'False', '40', '0,14', '14', '30', '1', '1,5', '0', '-0,06', '0', '0', '150', '130');
INSERT INTO `champions` VALUES ('98', 'Shen', '428', '200', '335', '0', '4', '125', '3,4', '0', '0', '54,5', '0', '3,375', '85', '0,11', 'True', '0', '0', '19', '30', '1,49', '10', '0', '-0,04', '0', '0', '225', '102,7778');
INSERT INTO `champions` VALUES ('99', 'Lux', '345', '250', '330', '0', '4', '550', '1,36', '0', '0', '50', '0', '3,3', '79', '0,11', 'False', '50', '0,12', '12', '30', '0,9', '1,2', '0', '0', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('101', 'Xerath', '514,4', '316,96', '340', '0', '3,5', '525', '1,36', '0', '0', '54,7', '0', '3', '80', '0,11', 'False', '47', '0,16', '21,88', '30', '1,0848', '1,2', '0', '0', '0', '0', '302,7778', '88,8889');
INSERT INTO `champions` VALUES ('102', 'Shyvana', '435', '100', '350', '0', '3,35', '125', '2,5', '0', '0', '55', '0', '3,4', '95', '0,16', 'True', '0', '0', '22', '30', '1,45', '0', '1,25', '-0,05', '0', '0', '100', '100');
INSERT INTO `champions` VALUES ('103', 'Ahri', '514,4', '334', '330', '0', '3,5', '550', '2', '0', '0', '53,04', '0', '3', '80', '0,12', 'False', '50', '0,16', '20,88', '30', '1,3016', '1,2', '0', '-0,065', '0', '0', '135', '100');
INSERT INTO `champions` VALUES ('104', 'Graves', '551,12', '322,2', '330', '0', '3,2', '525', '2,9', '0', '0', '54,208', '0', '3,1', '84', '0,14', 'False', '40', '0,14', '24,376', '30', '1,3352', '1,5852', '0', '0', '0', '0', '140', '110');
INSERT INTO `champions` VALUES ('105', 'Fizz', '414', '200', '335', '0', '3,4', '175', '3,1', '0', '0', '53', '0', '3', '86', '0,14', 'True', '40', '0,09', '16,7', '30', '1,4', '1,22', '1,25', '-0,05', '0', '0', '138,8889', '108,3333');
INSERT INTO `champions` VALUES ('106', 'Volibear', '584,48', '270,4', '345', '0', '3,5', '125', '2,67', '0', '0', '59,544', '0', '3,3', '86', '0,13', 'True', '30', '0,13', '26,38', '32,1', '1,6184', '1,6184', '1,25', '-0,05', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('107', 'Rengar', '586,2', '5', '345', '0', '3,5', '125', '2,85', '0', '0', '60,04', '0', '3', '90', '0,08', 'True', '0', '0', '25,88', '32,1', '0,8544', '0', '1,25', '-0,08', '0', '0', '225', '70');
INSERT INTO `champions` VALUES ('110', 'Varus', '537,76', '310,48', '330', '0', '3,4', '575', '3', '0', '0', '49,04', '0', '3', '82', '0,11', 'False', '36', '0,1', '23,212', '30', '1,0848', '1,468', '0', '-0,05', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('111', 'Nautilus', '432', '200', '325', '0', '3,25', '175', '1', '0', '0', '52', '0', '3,3', '86', '0,11', 'True', '50', '0,14', '21', '30', '1,49', '1,49', '1,25', '0,02', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('112', 'Viktor', '516,04', '324', '335', '0', '4', '525', '2,11', '0', '0', '52,04', '0', '3', '78', '0,13', 'False', '50', '0,16', '22,72', '30', '1,5684', '1,2', '0', '-0,05', '0', '0', '140', '160');
INSERT INTO `champions` VALUES ('113', 'Sejuani', '440', '220', '340', '0', '3', '125', '1,44', '0', '0', '52', '0', '3,3', '95', '0,17', 'True', '40', '0,09', '24,5', '30', '1,45', '1,29', '1,25', '-0,0672', '0', '0', '140', '140');
INSERT INTO `champions` VALUES ('114', 'Fiora', '592,8', '287,2', '350', '0', '3,5', '125', '3', '0', '0', '59,876', '0', '3,2', '85', '0,16', 'True', '40', '0,1', '24,88', '32,1', '1,3688', '1,518', '1,25', '-0,07', '0', '0', '138,8889', '100');
INSERT INTO `champions` VALUES ('115', 'Ziggs', '390', '250', '330', '0', '3,3', '550', '2', '0', '0', '51', '0', '3,1', '80', '0,12', 'False', '50', '0,12', '16', '30', '1,05', '1,35', '0', '-0,04734', '0', '0', '110', '100');
INSERT INTO `champions` VALUES ('117', 'Lulu', '415', '200', '325', '0', '3,7', '550', '2,25', '0', '0', '44', '0', '2,6', '82', '0,12', 'False', '55', '0,1', '13', '30', '1', '1', '0', '0', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('119', 'Draven', '420', '240', '330', '0', '3,3', '550', '2,7', '0', '0', '46,5', '0', '3,5', '82', '0,14', 'False', '42', '0,13', '20', '30', '1', '1,39', '0', '-0,08', '0', '0', '191,6667', '111,1111');
INSERT INTO `champions` VALUES ('120', 'Hecarim', '440', '210', '345', '0', '4', '175', '2,5', '0', '0', '56', '0', '3,2', '95', '0,15', 'True', '40', '0,12', '20', '30', '1,6', '1,3', '1,25', '-0,0672', '0', '0', '140', '140');
INSERT INTO `champions` VALUES ('121', 'Khazix', '430', '260', '350', '0', '3', '125', '2,7', '0', '0', '50', '0', '3,1', '85', '0,15', 'True', '40', '0,1', '19', '30', '1,25', '1,35', '1,25', '-0,065', '0', '0', '150', '130');
INSERT INTO `champions` VALUES ('122', 'Darius', '426', '200', '340', '0', '3,5', '125', '2,65', '0', '0', '50', '0', '3,5', '93', '0,19', 'True', '37,5', '0,07', '24', '30', '1,65', '1,2', '1,25', '-0,08', '0', '0', '155', '125');
INSERT INTO `champions` VALUES ('126', 'Jayce', '420', '240', '335', '0', '3,5', '125', '3', '0', '0', '46,5', '0', '3,5', '90', '0,16', 'True', '40', '0,14', '16,5', '30', '1,2', '1,4', '0', '-0,05', '0', '0', '188,8889', '75');
INSERT INTO `champions` VALUES ('127', 'Lissandra', '365', '220', '325', '0', '3,7', '550', '1,36', '0', '0', '48', '0', '2,7', '84', '0,11', 'False', '50', '0,08', '14', '30', '1,2', '1', '0', '0', '0', '0', '250', '88,8889');
INSERT INTO `champions` VALUES ('131', 'Diana', '438', '230', '345', '0', '3,6', '150', '2,25', '0', '0', '48', '0', '3', '90', '0,17', 'True', '40', '0,12', '20', '30', '1,2', '1,4', '1,25', '0', '0', '0', '188,8889', '75');
INSERT INTO `champions` VALUES ('133', 'Quinn', '390', '210', '335', '0', '3,5', '525', '3,1', '0', '0', '48', '0', '3', '85', '0,11', 'False', '35', '0,08', '17,5', '30', '0,9', '1,26', '0', '-0,065', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('134', 'Syndra', '511,04', '334', '330', '0', '3,4', '550', '2', '0', '0', '53,872', '0', '2,9', '78', '0,12', 'False', '50', '0,16', '24,712', '30', '1,3016', '1,2', '0', '0', '0', '10', '200', '125');
INSERT INTO `champions` VALUES ('143', 'Zyra', '355', '250', '325', '0', '3', '575', '2,11', '0', '0', '50', '0', '3,2', '74', '0,1', 'False', '50', '0,15', '15', '30', '0,97', '1,42', '0', '0', '0', '0', '120', '120');
INSERT INTO `champions` VALUES ('150', 'Gnar', '540', '100', '325', '0', '2,5', '150', '6', '0', '0', '48', '0', '3', '65', '0,1', 'False', '0', '0', '23', '30', '0,5', '0', '0', '0', '0', '0', '85', '100');
INSERT INTO `champions` VALUES ('154', 'Zac', '455', '0', '335', '0', '3,5', '125', '1,6', '0', '0', '54', '0', '3,375', '95', '0,11', 'True', '0', '0', '18', '30', '1,4', '0', '1,25', '-0,02', '0', '0', '100', '90');
INSERT INTO `champions` VALUES ('157', 'Yasuo', '430', '60', '350', '0', '3,4', '175', '3,2', '0', '0', '50', '0', '3,2', '82', '0,18', 'True', '0', '0', '19', '30', '1', '0', '0', '-0,05', '0', '0', '180', '75');
INSERT INTO `champions` VALUES ('161', 'Velkoz', '507,68', '325,6', '340', '0', '3,5', '525', '1,36', '0', '0', '54,937875658', '0', '3,14159265359', '76', '0,11', 'False', '45', '0,16', '21,88', '30', '1,0848', '1,2', '0', '0', '0', '0', '302,7778', '88,8889');
INSERT INTO `champions` VALUES ('201', 'Braum', '430', '235', '335', '0', '4', '125', '3,5', '0', '0', '53', '0', '3,2', '87', '0,2', 'True', '45', '0,16', '20', '30', '1,3', '1,1', '1,25', '-0,03', '0', '0', '122', '100');
INSERT INTO `champions` VALUES ('222', 'Jinx', '380', '170', '325', '0', '3,5', '525', '1', '0', '0', '50', '0', '3', '82', '0,1', 'False', '45', '0,2', '17', '30', '1', '1', '0', '0', '0', '0', '155', '120');
INSERT INTO `champions` VALUES ('236', 'Lucian', '390', '230', '330', '0', '3', '550', '3,3', '0', '0', '46', '0', '3', '80', '0,13', 'False', '41', '0,14', '19', '30', '1,02', '1,4', '0', '-0,02', '0', '0', '122,2222', '100');
INSERT INTO `champions` VALUES ('238', 'Zed', '579,4', '200', '345', '0', '3,5', '125', '3,1', '0', '0', '54,712', '0', '3,4', '80', '0,13', 'True', '0', '0', '26,88', '32,1', '1,4184', '10', '1,25', '-0,05', '0', '0', '138,8889', '100');
INSERT INTO `champions` VALUES ('254', 'Vi', '440', '220', '350', '0', '3,5', '125', '2,5', '0', '0', '50', '0', '3,5', '85', '0,18', 'True', '45', '0,13', '20', '30', '1,5', '1,4', '1,25', '-0,03', '0', '0', '100', '100');
INSERT INTO `champions` VALUES ('266', 'Aatrox', '395', '30', '345', '0', '3,8', '150', '3', '0', '0', '55', '0', '3,2', '85', '0,1', 'True', '45', '0', '18', '30', '1,15', '0', '1,25', '-0,04', '0', '0', '180', '135');
INSERT INTO `champions` VALUES ('267', 'Nami', '365', '305', '340', '0', '4', '550', '2,61', '0', '0', '48', '0', '3,1', '74', '0,11', 'False', '43', '0,12', '13', '30', '0,9', '1,38', '0', '-0,03', '0', '0', '180', '125');
INSERT INTO `champions` VALUES ('268', 'Azir', '524,4', '350,56', '335', '0', '3', '525', '1,5', '0', '0', '49,704', '0', '2,8', '80', '0,11', 'False', '42', '0,16', '19,04', '30', '1,3848', '1,2', '0', '0,2', '0', '0', '302,7778', '88,8889');
INSERT INTO `champions` VALUES ('412', 'Thresh', '411', '200', '335', '0', '0', '450', '3,5', '0', '0', '46', '0', '2,2', '89', '0,11', 'False', '44', '0,14', '16', '30', '1,2', '1', '0', '0', '0', '0', '150', '125');
INSERT INTO `champions` VALUES ('429', 'Kalista', '517,76', '231,8', '325', '0', '3,5', '550', '3,3', '0', '0', '53,46', '0', '3,25', '83', '0,11', 'False', '35', '0,08', '19,012', '30', '1,2', '1,26', '0', '0', '0', '0', '135', '100');

-- ----------------------------
-- Table structure for experiences
-- ----------------------------
DROP TABLE IF EXISTS `experiences`;
CREATE TABLE `experiences` (
  `Level` int(11) NOT NULL,
  `CumulativeExp` int(11) DEFAULT NULL,
  PRIMARY KEY (`Level`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of experiences
-- ----------------------------
INSERT INTO `experiences` VALUES ('1', '0');
INSERT INTO `experiences` VALUES ('2', '280');
INSERT INTO `experiences` VALUES ('3', '660');
INSERT INTO `experiences` VALUES ('4', '1140');
INSERT INTO `experiences` VALUES ('5', '1720');
INSERT INTO `experiences` VALUES ('6', '2400');
INSERT INTO `experiences` VALUES ('7', '3180');
INSERT INTO `experiences` VALUES ('8', '4060');
INSERT INTO `experiences` VALUES ('9', '5040');
INSERT INTO `experiences` VALUES ('10', '6120');
INSERT INTO `experiences` VALUES ('11', '7300');
INSERT INTO `experiences` VALUES ('12', '8580');
INSERT INTO `experiences` VALUES ('13', '9960');
INSERT INTO `experiences` VALUES ('14', '11440');
INSERT INTO `experiences` VALUES ('15', '13020');
INSERT INTO `experiences` VALUES ('16', '14700');
INSERT INTO `experiences` VALUES ('17', '16480');
INSERT INTO `experiences` VALUES ('18', '18360');

-- ----------------------------
-- Table structure for maps
-- ----------------------------
DROP TABLE IF EXISTS `maps`;
CREATE TABLE `maps` (
  `Id` int(40) NOT NULL AUTO_INCREMENT,
  `Name` mediumtext,
  `MiddleOfMap` mediumtext,
  `Width` mediumtext,
  `Height` mediumtext,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of maps
-- ----------------------------
INSERT INTO `maps` VALUES ('10', 'Map10', '(7683.748 7186.561)', '15367,5', '14373,12');
INSERT INTO `maps` VALUES ('12', 'Map12', '(6535.046 6283.502)', '13070,09', '12567');
INSERT INTO `maps` VALUES ('1', 'Map1', '(6967.012 7198.586)', '13934,02', '14397,17');
INSERT INTO `maps` VALUES ('8', 'Map8', '(6922.592 6582.814)', '13845,18', '13165,63');
INSERT INTO `maps` VALUES ('11', 'map11', '(7333.701 7387.497)', '14667,4', '14774,99');

-- ----------------------------
-- Table structure for skins
-- ----------------------------
DROP TABLE IF EXISTS `skins`;
CREATE TABLE `skins` (
  `ChampionSkinId` mediumtext,
  `Name` mediumtext,
  `Scale` mediumtext
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of skins
-- ----------------------------
INSERT INTO `skins` VALUES ('1000', 'Annie', '1');
INSERT INTO `skins` VALUES ('1006', 'AnnieReverse', '1');
INSERT INTO `skins` VALUES ('1008', 'AnniePanda', '1');
INSERT INTO `skins` VALUES ('1002', 'AnnieRedRiding', '1');
INSERT INTO `skins` VALUES ('1005', 'AnnieFrostFire', '1');
INSERT INTO `skins` VALUES ('1004', 'AnnieProm', '1');
INSERT INTO `skins` VALUES ('1001', 'AnnieGoth', '1');
INSERT INTO `skins` VALUES ('1003', 'AnnieWonderland', '1');
INSERT INTO `skins` VALUES ('1007', 'AnnieFrankenstein', '1');
INSERT INTO `skins` VALUES ('2004', 'PentakillOlaf', '1,1');
INSERT INTO `skins` VALUES ('2003', 'BroOlaf', '1,1');
INSERT INTO `skins` VALUES ('2002', 'NiflheimOlaf', '1,1');
INSERT INTO `skins` VALUES ('2001', 'ValhallanOlaf', '1,1');
INSERT INTO `skins` VALUES ('2000', 'BaseOlaf', '1,1');
INSERT INTO `skins` VALUES ('6002', 'ButcherUrgot', '1,6');
INSERT INTO `skins` VALUES ('6003', 'BattlecastUrgot', '1,4');
INSERT INTO `skins` VALUES ('6001', 'CrabUrgot', '1,6');
INSERT INTO `skins` VALUES ('6000', 'BaseUrgot', '1,6');
INSERT INTO `skins` VALUES ('7000', 'BaseLeblanc', '0,95');
INSERT INTO `skins` VALUES ('7002', 'MagicianLeblanc', '0,95');
INSERT INTO `skins` VALUES ('7001', 'WhiteHairLeblanc', '1');
INSERT INTO `skins` VALUES ('7003', 'WinterbellLeblanc', '0,95');
INSERT INTO `skins` VALUES ('7004', 'LeblancSkin04', '0,95');
INSERT INTO `skins` VALUES ('11000', 'BaseMasterYi', '1,55');
INSERT INTO `skins` VALUES ('11003', 'IonianMasterYi', '1,55');
INSERT INTO `skins` VALUES ('11001', 'AssassinMasterYi', '1,55');
INSERT INTO `skins` VALUES ('11002', 'CouncilMasterYi', '1,55');
INSERT INTO `skins` VALUES ('11004', 'SamuraiMasterYi', '1,55');
INSERT INTO `skins` VALUES ('11005', 'TribalMasterYi', '1,55');
INSERT INTO `skins` VALUES ('12000', 'Alistar', '1,4');
INSERT INTO `skins` VALUES ('12005', 'BlueAlistar', '1,4');
INSERT INTO `skins` VALUES ('12004', 'CowboyMinotaur', '1,4');
INSERT INTO `skins` VALUES ('12002', 'GoldenMinotaur', '1,4');
INSERT INTO `skins` VALUES ('12003', 'MatadorMinotaur', '1,4');
INSERT INTO `skins` VALUES ('12001', 'BlackMinotaur', '1,4');
INSERT INTO `skins` VALUES ('12007', 'Alistar_Skin07', '1,4');
INSERT INTO `skins` VALUES ('12006', 'ArmoredAlistar', '1,4');
INSERT INTO `skins` VALUES ('13004', 'WinnerRyze', '1,67');
INSERT INTO `skins` VALUES ('13003', 'UncleRyze', '1,67');
INSERT INTO `skins` VALUES ('13002', 'TribalRyze', '1,67');
INSERT INTO `skins` VALUES ('13001', 'HumanRyze', '1,67');
INSERT INTO `skins` VALUES ('13008', 'pirateRyze', '1,67');
INSERT INTO `skins` VALUES ('13007', 'VoidCrystalRyze', '1,67');
INSERT INTO `skins` VALUES ('13006', 'ZombieRyze', '1,67');
INSERT INTO `skins` VALUES ('13005', 'ScholarRyze', '1,67');
INSERT INTO `skins` VALUES ('13000', 'RyzeBase', '1,67');
INSERT INTO `skins` VALUES ('14004', 'SionSkin04', '1,08');
INSERT INTO `skins` VALUES ('14003', 'SionSkin03', '1,08');
INSERT INTO `skins` VALUES ('14002', 'SionSkin02', '1,08');
INSERT INTO `skins` VALUES ('14001', 'SionSkin01', '1,08');
INSERT INTO `skins` VALUES ('14000', 'SionBase', '1,05');
INSERT INTO `skins` VALUES ('15000', 'BaseSivir', '0,9');
INSERT INTO `skins` VALUES ('15006', 'SivirSkin06', '1');
INSERT INTO `skins` VALUES ('15005', 'PaxSivir', '0,9');
INSERT INTO `skins` VALUES ('15002', 'RedSivir', '0,9');
INSERT INTO `skins` VALUES ('15003', 'AboriginalSivir', '0,9');
INSERT INTO `skins` VALUES ('15001', 'WarriorSivir', '0,9');
INSERT INTO `skins` VALUES ('15004', 'FreljordSivir', '0,9');
INSERT INTO `skins` VALUES ('16001', 'DryadSoraka', '1,27');
INSERT INTO `skins` VALUES ('16000', 'BaseSoraka', '1,27');
INSERT INTO `skins` VALUES ('16002', 'HumanSoraka_REWORK', '1,27');
INSERT INTO `skins` VALUES ('16003', 'ClericSoraka', '1,27');
INSERT INTO `skins` VALUES ('16004', 'SorakaSkin04', '1,27');
INSERT INTO `skins` VALUES ('18000', 'TristanaBase', '1,57');
INSERT INTO `skins` VALUES ('18003', 'FirefighterTristana', '1,57');
INSERT INTO `skins` VALUES ('18002', 'EarnestElfTristana', '1,57');
INSERT INTO `skins` VALUES ('18006', 'RocketTristana', '1,57');
INSERT INTO `skins` VALUES ('18005', 'ScurvyDogTristana', '1,57');
INSERT INTO `skins` VALUES ('18004', 'GuerillaTristana', '1,57');
INSERT INTO `skins` VALUES ('18001', 'PinkTristana', '1,57');
INSERT INTO `skins` VALUES ('21000', 'BaseMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21006', 'MafiaMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21004', 'CandyCaneMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21001', 'CowgirlMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21005', 'WastelandMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21002', 'WaterlooMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21003', 'SecretAgentMissFortune', '1,2');
INSERT INTO `skins` VALUES ('21007', 'MissFortuneSkin07', '1,2');
INSERT INTO `skins` VALUES ('22000', 'Ashe', '1,1');
INSERT INTO `skins` VALUES ('22001', 'FreljordBowmaster', '1,1');
INSERT INTO `skins` VALUES ('22005', 'CrystalBowmaster', '1,1');
INSERT INTO `skins` VALUES ('22002', 'SherwoodBowmaster', '1,1');
INSERT INTO `skins` VALUES ('22003', 'WoadBowmaster', '1,1');
INSERT INTO `skins` VALUES ('22006', 'AsheSkin06', '1,1');
INSERT INTO `skins` VALUES ('22004', 'QueenBowmaster', '1,1');
INSERT INTO `skins` VALUES ('23000', 'TryndamereBase', '2');
INSERT INTO `skins` VALUES ('23003', 'VikingTryndamere', '2');
INSERT INTO `skins` VALUES ('23001', 'HighlandTryndamere', '2');
INSERT INTO `skins` VALUES ('23004', 'TryndamereDemonsword', '2');
INSERT INTO `skins` VALUES ('23006', 'TryndamereSkin06', '2');
INSERT INTO `skins` VALUES ('23002', 'KingTryndamere', '2');
INSERT INTO `skins` VALUES ('23005', 'TryndamereSultan', '2');
INSERT INTO `skins` VALUES ('29000', 'TwitchBase', '1');
INSERT INTO `skins` VALUES ('29001', 'KingpinTwitch', '1');
INSERT INTO `skins` VALUES ('29002', 'KaiserTwitch', '1');
INSERT INTO `skins` VALUES ('29003', 'IronMaskTwitch', '1');
INSERT INTO `skins` VALUES ('29004', 'GangsterTwitch', '1');
INSERT INTO `skins` VALUES ('29005', 'PunkTwitch', '1');
INSERT INTO `skins` VALUES ('29006', 'TwitchSkin06', '1');
INSERT INTO `skins` VALUES ('30000', 'BaseLich', '1,35');
INSERT INTO `skins` VALUES ('30002', 'StatueLich', '1,35');
INSERT INTO `skins` VALUES ('30003', 'GrimLich', '1,35');
INSERT INTO `skins` VALUES ('30001', 'PhantomLich', '1,35');
INSERT INTO `skins` VALUES ('30004', 'pentakillLich', '1,35');
INSERT INTO `skins` VALUES ('30000', 'Karthus', '1,05');
INSERT INTO `skins` VALUES ('30003', 'KarthusSkin03', '1,05');
INSERT INTO `skins` VALUES ('30001', 'KarthusSkin01', '1,05');
INSERT INTO `skins` VALUES ('30004', 'KarthusSkin04', '1,07');
INSERT INTO `skins` VALUES ('30002', 'KarthusSkin02', '1,05');
INSERT INTO `skins` VALUES ('30005', 'KarthusSkin05', '1');
INSERT INTO `skins` VALUES ('35006', 'KoreanShaco', '1,85');
INSERT INTO `skins` VALUES ('35001', 'HatterJester', '1,85');
INSERT INTO `skins` VALUES ('35002', 'YellowJester', '1,85');
INSERT INTO `skins` VALUES ('35005', 'AsylumJester', '1,85');
INSERT INTO `skins` VALUES ('35004', 'ClockworkJester', '1,85');
INSERT INTO `skins` VALUES ('35000', 'BaseJester', '1,85');
INSERT INTO `skins` VALUES ('35003', 'NutcrackerJester', '1,85');
INSERT INTO `skins` VALUES ('37002', 'MetalSona', '1,1');
INSERT INTO `skins` VALUES ('37005', 'PaxSona', '1,3');
INSERT INTO `skins` VALUES ('37000', 'BaseSona', '1,3');
INSERT INTO `skins` VALUES ('37004', 'GuqinSona', '1,3');
INSERT INTO `skins` VALUES ('37003', 'SilentNightSona', '1,3');
INSERT INTO `skins` VALUES ('37001', 'MuseSona', '1,3');
INSERT INTO `skins` VALUES ('37002', 'MetalSona', '1,3');
INSERT INTO `skins` VALUES ('40000', 'BaseJanna', '1,31');
INSERT INTO `skins` VALUES ('40003', 'FrostqueenJanna', '1,31');
INSERT INTO `skins` VALUES ('40004', 'Warhero', '1,31');
INSERT INTO `skins` VALUES ('40001', 'LightningJanna', '1,31');
INSERT INTO `skins` VALUES ('40002', 'HextechJanna', '1,31');
INSERT INTO `skins` VALUES ('40006', 'JannaSkin06', '1,31');
INSERT INTO `skins` VALUES ('40005', 'JannaForecast', '1,2');
INSERT INTO `skins` VALUES ('41000', 'Gangplank', '1,4');
INSERT INTO `skins` VALUES ('41003', 'SailorGangplank', '1,4');
INSERT INTO `skins` VALUES ('41005', 'SpecialForcesGangplank', '1,4');
INSERT INTO `skins` VALUES ('41004', 'ToySoldierGangplank', '1,4');
INSERT INTO `skins` VALUES ('41001', 'GhostGangplank', '1,4');
INSERT INTO `skins` VALUES ('41002', 'MinutemanPirate', '1,4');
INSERT INTO `skins` VALUES ('41006', 'GangplankSkin06', '1,4');
INSERT INTO `skins` VALUES ('43003', 'KarmaClassic', '1,2');
INSERT INTO `skins` VALUES ('43001', 'SungoddessKarma', '1,2');
INSERT INTO `skins` VALUES ('43002', 'GeishaKarma', '1,2');
INSERT INTO `skins` VALUES ('43000', 'Karma', '1,2');
INSERT INTO `skins` VALUES ('43004', 'KarmaSkin04', '1,2');
INSERT INTO `skins` VALUES ('45000', 'Veigar', '1,8');
INSERT INTO `skins` VALUES ('45007', 'BadSantaVeigar', '1,8');
INSERT INTO `skins` VALUES ('45002', 'CurlingVeigar', '1,8');
INSERT INTO `skins` VALUES ('45006', 'DapperVeigar', '1,8');
INSERT INTO `skins` VALUES ('45001', 'RedVeigar', '1,8');
INSERT INTO `skins` VALUES ('45003', 'GreyVeigar', '1,8');
INSERT INTO `skins` VALUES ('45004', 'LeprechaunVeigar', '1,8');
INSERT INTO `skins` VALUES ('45005', 'BaronVonVeigar', '1,8');
INSERT INTO `skins` VALUES ('45008', 'VeigarSkin08', '1,8');
INSERT INTO `skins` VALUES ('48001', 'BaseballTrundle', '1');
INSERT INTO `skins` VALUES ('48003', 'ClassicTrundle', '1');
INSERT INTO `skins` VALUES ('48002', 'JunkyardTrundle', '1');
INSERT INTO `skins` VALUES ('48000', 'BaseTrundle', '1');
INSERT INTO `skins` VALUES ('48004', 'Trundle_Skin04', '1');
INSERT INTO `skins` VALUES ('51002', 'SheriffCaitlyn', '1,6');
INSERT INTO `skins` VALUES ('51001', 'MilitaryCaitlyn', '1,6');
INSERT INTO `skins` VALUES ('51004', 'NorthernCaitlyn', '1,6');
INSERT INTO `skins` VALUES ('51003', 'SafariCaitlyn', '1,6');
INSERT INTO `skins` VALUES ('51005', 'CopCaitlyn', '1,6');
INSERT INTO `skins` VALUES ('51000', 'CaitlynBase', '1,6');
INSERT INTO `skins` VALUES ('51006', 'CaitlynSkin06', '1,6');
INSERT INTO `skins` VALUES ('57004', 'GraveDiggerMaokai', '1');
INSERT INTO `skins` VALUES ('57000', 'Maokai', '1,05');
INSERT INTO `skins` VALUES ('57003', 'MaokaiSkin03', '1');
INSERT INTO `skins` VALUES ('57002', 'TotemMaokai', '1');
INSERT INTO `skins` VALUES ('57005', 'skin05', '1,05');
INSERT INTO `skins` VALUES ('57001', 'BurntMaokai', '1');
INSERT INTO `skins` VALUES ('58000', 'Renekton_Base', '1,56');
INSERT INTO `skins` VALUES ('58001', 'Renekton_galactic', '1,56');
INSERT INTO `skins` VALUES ('58004', 'Renekton_RuneWars', '1,48');
INSERT INTO `skins` VALUES ('58003', 'Renekton_brutal', '1,56');
INSERT INTO `skins` VALUES ('58002', 'Renekton_outback', '1,56');
INSERT INTO `skins` VALUES ('58005', 'Renekton_VolcanoGod', '1,56');
INSERT INTO `skins` VALUES ('58006', 'RenektonSkin06', '1,56');
INSERT INTO `skins` VALUES ('60000', 'Elise', '1,25');
INSERT INTO `skins` VALUES ('60001', 'EliseLotusFlower', '1,25');
INSERT INTO `skins` VALUES ('60002', 'EliseSkin02', '1,25');
INSERT INTO `skins` VALUES ('64002', 'TempleBlindMonk', '1,82');
INSERT INTO `skins` VALUES ('64003', 'DragonBlindMonk', '1,82');
INSERT INTO `skins` VALUES ('64001', 'ClassicBlindMonk', '1,82');
INSERT INTO `skins` VALUES ('64004', 'MuayThaiBlindMonk', '1,82');
INSERT INTO `skins` VALUES ('64000', 'BaseBlindMonk', '1,82');
INSERT INTO `skins` VALUES ('64005', 'LeeSinSkin05', '1,82');
INSERT INTO `skins` VALUES ('64006', 'leesinSkin06', '1,82');
INSERT INTO `skins` VALUES ('68002', 'bilgeratRumble', '1,9');
INSERT INTO `skins` VALUES ('68001', 'JungleRumble', '1,9');
INSERT INTO `skins` VALUES ('68000', 'rumble', '1,9');
INSERT INTO `skins` VALUES ('68003', 'RumbleSkin03', '1,7');
INSERT INTO `skins` VALUES ('69000', 'Cassiopeia', '1,7');
INSERT INTO `skins` VALUES ('69004', 'LunarRevelCassiopeia', '1,7');
INSERT INTO `skins` VALUES ('69003', 'GreekCassiopeia', '1,7');
INSERT INTO `skins` VALUES ('69001', 'RattlesnakeCassiopeia', '1,7');
INSERT INTO `skins` VALUES ('69002', 'SeasnakeCassiopeia', '1,7');
INSERT INTO `skins` VALUES ('72001', 'SkarnerScorpion', '0,9');
INSERT INTO `skins` VALUES ('72002', 'SkarnerRunes', '0,9');
INSERT INTO `skins` VALUES ('72000', 'SkarnerBase', '0,9');
INSERT INTO `skins` VALUES ('72003', 'SkarnerSkin03', '0,93');
INSERT INTO `skins` VALUES ('74001', 'HeimerdingerSkin01', '1');
INSERT INTO `skins` VALUES ('74003', 'HeimerdingerSkin03', '1');
INSERT INTO `skins` VALUES ('74004', 'HeimerdingerSkin04', '1');
INSERT INTO `skins` VALUES ('74002', 'HeimerdingerSkin02', '1');
INSERT INTO `skins` VALUES ('74000', 'HeimerdingerBase', '1');
INSERT INTO `skins` VALUES ('74001', 'HeimerdingerSkin01', '1');
INSERT INTO `skins` VALUES ('74003', 'HeimerdingerSkin03', '1');
INSERT INTO `skins` VALUES ('74004', 'HeimerdingerSkin04', '1');
INSERT INTO `skins` VALUES ('74002', 'HeimerdingerSkin02', '1');
INSERT INTO `skins` VALUES ('74005', 'HeimerdingerSkin05', '1');
INSERT INTO `skins` VALUES ('75003', 'DreadKnightNasus', '1,18');
INSERT INTO `skins` VALUES ('75004', 'RiotNasus', '1,18');
INSERT INTO `skins` VALUES ('75001', 'SilverNasus', '1,18');
INSERT INTO `skins` VALUES ('75002', 'PharaohNasus', '1,18');
INSERT INTO `skins` VALUES ('75000', 'BaseNasus', '1,23');
INSERT INTO `skins` VALUES ('75005', 'NasusSkin05', '1,22');
INSERT INTO `skins` VALUES ('76002', 'LeopardNidalee', '1,35');
INSERT INTO `skins` VALUES ('76004', 'CleopatraNidalee', '1,35');
INSERT INTO `skins` VALUES ('76003', 'FrenchMaidNidalee', '1,35');
INSERT INTO `skins` VALUES ('76001', 'SnowBunnyNidalee', '1,35');
INSERT INTO `skins` VALUES ('76005', 'WitchNidalee', '1,35');
INSERT INTO `skins` VALUES ('76000', 'Nidalee', '1,35');
INSERT INTO `skins` VALUES ('76006', 'HeadhunterNidalee', '1,35');
INSERT INTO `skins` VALUES ('76006', 'HeadhunterNidalee', '1,35');
INSERT INTO `skins` VALUES ('77003', 'SpiritUdyr', '1,1');
INSERT INTO `skins` VALUES ('77002', 'PrimalUdyr', '1,1');
INSERT INTO `skins` VALUES ('77001', 'BlackBeltUdyr', '1,1');
INSERT INTO `skins` VALUES ('77000', 'BaseUdyr', '1,1');
INSERT INTO `skins` VALUES ('77001', 'BlackBeltUdyr', '1,1');
INSERT INTO `skins` VALUES ('78000', 'BasePoppy', '1');
INSERT INTO `skins` VALUES ('78006', 'redknightPoppy', '1');
INSERT INTO `skins` VALUES ('78002', 'LolliPoppy', '1');
INSERT INTO `skins` VALUES ('78005', 'RegalknightPoppy', '1');
INSERT INTO `skins` VALUES ('78003', 'BlacksmithPoppy', '1');
INSERT INTO `skins` VALUES ('78001', 'Noxus_Poppy', '1');
INSERT INTO `skins` VALUES ('78004', 'RaggedyPoppy', '1');
INSERT INTO `skins` VALUES ('79006', 'OktoberGragas', '1,2');
INSERT INTO `skins` VALUES ('79004', 'ClassyGragas', '1,2');
INSERT INTO `skins` VALUES ('79001', 'ScubaGragas', '1,2');
INSERT INTO `skins` VALUES ('79005', 'VandalsGragas', '1,2');
INSERT INTO `skins` VALUES ('79003', 'SantaGragas', '1,2');
INSERT INTO `skins` VALUES ('79002', 'HillBillyGragas', '1,2');
INSERT INTO `skins` VALUES ('79007', 'GragasSkin07', '1,2');
INSERT INTO `skins` VALUES ('79000', 'Gragas', '1,2');
INSERT INTO `skins` VALUES ('79008', 'GragasSkin08', '1,2');
INSERT INTO `skins` VALUES ('86006', 'SteellegionGaren', '1,09');
INSERT INTO `skins` VALUES ('86002', 'DesertGaren', '1');
INSERT INTO `skins` VALUES ('86005', 'RangerGaren', '1');
INSERT INTO `skins` VALUES ('86004', 'DreadknightGaren', '1');
INSERT INTO `skins` VALUES ('86001', 'SanguineGaren', '1');
INSERT INTO `skins` VALUES ('86003', 'CommandoGaren', '1');
INSERT INTO `skins` VALUES ('86000', 'Garen', '1');
INSERT INTO `skins` VALUES ('92000', 'BaseRiven', '1,5');
INSERT INTO `skins` VALUES ('92004', 'Season2Riven', '1,5');
INSERT INTO `skins` VALUES ('92005', 'RivenSkin05', '1,5');
INSERT INTO `skins` VALUES ('92002', 'CrimsonRiven', '1,5');
INSERT INTO `skins` VALUES ('92001', 'RedeemedRiven', '1,5');
INSERT INTO `skins` VALUES ('92003', 'BattlebunnyRiven', '1,5');
INSERT INTO `skins` VALUES ('96001', 'CaterpillarKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96002', 'PoisonFrogKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96000', 'BaseKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96003', 'ButterflyKogMaw', '0,65');
INSERT INTO `skins` VALUES ('96004', 'ReindeerKogMaw', '0,65');
INSERT INTO `skins` VALUES ('96005', 'NewYearDragonKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96006', 'DeepseaKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96007', 'FossilKogMaw', '0,7');
INSERT INTO `skins` VALUES ('96008', 'KogMawSkin08', '0,7');
INSERT INTO `skins` VALUES ('101000', 'BaseXerath', '1,7');
INSERT INTO `skins` VALUES ('101001', 'RuneXerath', '1,7');
INSERT INTO `skins` VALUES ('101002', 'IronXerath', '1,7');
INSERT INTO `skins` VALUES ('101003', 'IronForgeXerath', '1,7');
INSERT INTO `skins` VALUES ('103001', 'AhriHanbok', '1,26');
INSERT INTO `skins` VALUES ('103002', 'AhriShadowfox', '1,26');
INSERT INTO `skins` VALUES ('103003', 'AhriFireFox', '1,26');
INSERT INTO `skins` VALUES ('103000', 'Ahri', '1,26');
INSERT INTO `skins` VALUES ('103004', 'AhriSkin04', '1,26');
INSERT INTO `skins` VALUES ('107001', 'HunterRengar', '0,95');
INSERT INTO `skins` VALUES ('107000', 'Rengar', '0,95');
INSERT INTO `skins` VALUES ('107002', 'rengarSkin02', '1');
INSERT INTO `skins` VALUES ('107000', 'Rengar', '0,95');
INSERT INTO `skins` VALUES ('112000', 'Viktor_base', '1,05');
INSERT INTO `skins` VALUES ('112003', 'MakerViktor', '1,05');
INSERT INTO `skins` VALUES ('112002', 'Viktor_prototype', '1,05');
INSERT INTO `skins` VALUES ('112001', 'Viktor_fullmachine', '1,05');
INSERT INTO `skins` VALUES ('113004', 'SejuaniSkin04', '1');
INSERT INTO `skins` VALUES ('113003', 'SejuaniClassic', '1');
INSERT INTO `skins` VALUES ('113000', 'SejuaniBase', '1');
INSERT INTO `skins` VALUES ('113001', 'SejuaniBeastLord', '1');
INSERT INTO `skins` VALUES ('113002', 'SejuaniDeathKnight', '1');
INSERT INTO `skins` VALUES ('119000', 'Draven_Base', '1,1');
INSERT INTO `skins` VALUES ('119001', 'Draven_SoulReaper', '1,1');
INSERT INTO `skins` VALUES ('119003', 'DravenSkin03', '1,1');
INSERT INTO `skins` VALUES ('119002', 'Draven_Gladiator', '1,1');
INSERT INTO `skins` VALUES ('121001', 'KhazixDroid', '1');
INSERT INTO `skins` VALUES ('121000', 'Khazix', '1');
INSERT INTO `skins` VALUES ('121002', 'KhazixSkin02', '1');
INSERT INTO `skins` VALUES ('122000', 'Darius_Base', '1');
INSERT INTO `skins` VALUES ('122003', 'Darius_NorseKing', '1');
INSERT INTO `skins` VALUES ('122002', 'Darius_ZaunKnight', '1');
INSERT INTO `skins` VALUES ('122001', 'Darius_Emperor', '1');
INSERT INTO `skins` VALUES ('122004', 'DariusSkin04', '1');
INSERT INTO `skins` VALUES ('126000', 'Jayce_Base', '1');
INSERT INTO `skins` VALUES ('126001', 'Jayce_Sentinel', '1');
INSERT INTO `skins` VALUES ('126002', 'Jayce_Gentleman', '1');
INSERT INTO `skins` VALUES ('127001', 'Lissandra_skin01', '1,12');
INSERT INTO `skins` VALUES ('127002', 'lissandraSkin02', '1,12');
INSERT INTO `skins` VALUES ('127002', 'lissandraSkin02', '1,12');
INSERT INTO `skins` VALUES ('127000', 'BaseLiss', '1,12');
INSERT INTO `skins` VALUES ('127001', 'Lissandra_skin01', '1,12');
INSERT INTO `skins` VALUES ('131000', 'Diana', '1,2');
INSERT INTO `skins` VALUES ('131001', 'DianaDarkValkyrie', '1,2');
INSERT INTO `skins` VALUES ('131002', 'DianaSkin02', '1,2');
INSERT INTO `skins` VALUES ('133000', 'BaseQuinn', '1,3');
INSERT INTO `skins` VALUES ('133001', 'PhoenixQuinn', '1,3');
INSERT INTO `skins` VALUES ('133002', 'QuinnSkin02', '1,3');
INSERT INTO `skins` VALUES ('150001', 'GnarSkin01', '1');
INSERT INTO `skins` VALUES ('150000', 'Gnar', '0,9');
INSERT INTO `skins` VALUES ('154001', 'ZacGrumpy', '0,95');
INSERT INTO `skins` VALUES ('154000', 'Zac', '0,95');
INSERT INTO `skins` VALUES ('157001', 'YasuoSkin01', '0,93');
INSERT INTO `skins` VALUES ('157000', 'Yasuo', '0,93');
INSERT INTO `skins` VALUES ('157002', 'YasuoSkin02', '0,93');
INSERT INTO `skins` VALUES ('161001', 'VelkozSkin01', '0,75');
INSERT INTO `skins` VALUES ('161000', 'Velkoz_Base', '0,75');
INSERT INTO `skins` VALUES ('161001', 'VelkozSkin01', '0,75');
INSERT INTO `skins` VALUES ('201001', 'BraumSkin01', '1,15');
INSERT INTO `skins` VALUES ('201000', 'Braum', '1,15');
INSERT INTO `skins` VALUES ('222001', 'JinxSkin01', '1,03');
INSERT INTO `skins` VALUES ('222000', 'Jinx', '1,03');
INSERT INTO `skins` VALUES ('236000', 'Lucian', '1,15');
INSERT INTO `skins` VALUES ('236001', 'LucianSkin01', '1,2');
INSERT INTO `skins` VALUES ('236002', 'LucianSkin02', '1,2');
INSERT INTO `skins` VALUES ('238001', 'ZedStormNinja', '1,17');
INSERT INTO `skins` VALUES ('238000', 'Zed', '1,17');
INSERT INTO `skins` VALUES ('238002', 'zedSkin02', '1,17');
INSERT INTO `skins` VALUES ('254000', 'Vi', '1,07');
INSERT INTO `skins` VALUES ('254002', 'ViSkin02', '1,07');
INSERT INTO `skins` VALUES ('254003', 'Skin03', '1,07');
INSERT INTO `skins` VALUES ('254001', 'ViRacer', '1,07');
INSERT INTO `skins` VALUES ('266000', 'Aatrox', '1,09');
INSERT INTO `skins` VALUES ('266001', 'AatroxSkin01', '1,09');
INSERT INTO `skins` VALUES ('266002', 'AatroxSkin02', '1,09');
INSERT INTO `skins` VALUES ('267000', 'Nami', '1,05');
INSERT INTO `skins` VALUES ('268001', 'AzirSkin01', '0,85');
INSERT INTO `skins` VALUES ('268000', 'Azir', '0,85');
INSERT INTO `skins` VALUES ('412000', 'Thresh', '0,95');
INSERT INTO `skins` VALUES ('412002', 'ThreshSkin02', '0,95');
INSERT INTO `skins` VALUES ('412001', 'ThreshUndersea', '0,95');
INSERT INTO `skins` VALUES ('429000', 'Kalista', '0,82');
INSERT INTO `skins` VALUES ('429001', 'KalistaSkin01', '0,82');
