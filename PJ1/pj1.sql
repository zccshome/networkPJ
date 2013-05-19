/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 60004
Source Host           : localhost:3306
Source Database       : pj1

Target Server Type    : MYSQL
Target Server Version : 60004
File Encoding         : 65001

Date: 2012-11-27 22:57:30
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accountmanagement`
-- ----------------------------
DROP TABLE IF EXISTS `accountmanagement`;
CREATE TABLE `accountmanagement` (
  `account` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `groupID` int(11) NOT NULL,
  `priority` int(11) NOT NULL,
  PRIMARY KEY (`account`),
  KEY `groupID` (`groupID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of accountmanagement
-- ----------------------------
INSERT INTO `accountmanagement` VALUES ('VM1', 'vm1', '1', '1');

-- ----------------------------
-- Table structure for `group`
-- ----------------------------
DROP TABLE IF EXISTS `group`;
CREATE TABLE `group` (
  `groupName` varchar(10) DEFAULT NULL,
  `groupID` int(11) NOT NULL AUTO_INCREMENT,
  `groupAddress` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`groupID`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of group
-- ----------------------------
INSERT INTO `group` VALUES ('Name1', '1', 'Address1');

-- ----------------------------
-- Table structure for `ownermanagement`
-- ----------------------------
DROP TABLE IF EXISTS `ownermanagement`;
CREATE TABLE `ownermanagement` (
  `ID` char(18) NOT NULL,
  `name` varchar(10) NOT NULL,
  `gender` char(2) NOT NULL,
  `age` int(11) NOT NULL,
  `drivingTime` datetime DEFAULT NULL,
  `contract` char(20) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ownermanagement
-- ----------------------------

-- ----------------------------
-- Table structure for `recordmanagement`
-- ----------------------------
DROP TABLE IF EXISTS `recordmanagement`;
CREATE TABLE `recordmanagement` (
  `recordID` int(10) NOT NULL AUTO_INCREMENT,
  `carID` char(10) NOT NULL,
  `engineID` char(10) NOT NULL,
  `fineNeed` float NOT NULL,
  `finePaid` float DEFAULT NULL,
  `payDue` datetime NOT NULL,
  `recordTime` datetime NOT NULL,
  `type` char(15) DEFAULT NULL,
  `description` char(100) DEFAULT NULL,
  PRIMARY KEY (`recordID`,`carID`),
  KEY `vehicleID` (`engineID`,`carID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of recordmanagement
-- ----------------------------

-- ----------------------------
-- Table structure for `vehiclemanagement`
-- ----------------------------
DROP TABLE IF EXISTS `vehiclemanagement`;
CREATE TABLE `vehiclemanagement` (
  `type` char(15) NOT NULL,
  `carID` char(10) NOT NULL,
  `engineID` char(10) NOT NULL,
  `ID` char(20) NOT NULL,
  `groupID` int(11) NOT NULL,
  `color` char(10) NOT NULL,
  `place` char(15) NOT NULL,
  `time` datetime NOT NULL,
  `value` float NOT NULL,
  PRIMARY KEY (`carID`,`engineID`),
  KEY `ID` (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of vehiclemanagement
-- ----------------------------
INSERT INTO `vehiclemanagement` VALUES ('Type1', 'ID1', 'ID1', '10302010009', '1', 'Black', 'Shanghai', '2012-11-27 22:19:15', '998');
