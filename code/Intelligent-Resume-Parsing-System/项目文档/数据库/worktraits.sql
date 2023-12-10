/*
 Navicat Premium Data Transfer

 Source Server         : test
 Source Server Type    : MySQL
 Source Server Version : 80032 (8.0.32)
 Source Host           : localhost:3306
 Source Schema         : resumesystem11

 Target Server Type    : MySQL
 Target Server Version : 80032 (8.0.32)
 File Encoding         : 65001

 Date: 07/11/2023 23:21:37
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for worktraits
-- ----------------------------
DROP TABLE IF EXISTS `worktraits`;
CREATE TABLE `worktraits`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantProfileID` int NOT NULL,
  `Trait` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_WorkTraits_ApplicantProfileID`(`ApplicantProfileID` ASC) USING BTREE,
  CONSTRAINT `FK_WorkTraits_ApplicantProfiles_ApplicantProfileID` FOREIGN KEY (`ApplicantProfileID`) REFERENCES `applicantprofiles` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 597 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for workexperiences
-- ----------------------------
DROP TABLE IF EXISTS `workexperiences`;
CREATE TABLE `workexperiences`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `CompanyName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Time` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Task` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_WorkExperiences_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  CONSTRAINT `FK_WorkExperiences_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 219 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Account` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Role` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyID` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_Users_Account`(`Account` ASC) USING BTREE,
  INDEX `IX_Users_CompanyID`(`CompanyID` ASC) USING BTREE,
  CONSTRAINT `FK_Users_Companies_CompanyID` FOREIGN KEY (`CompanyID`) REFERENCES `companies` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for skillsandexperiences
-- ----------------------------
DROP TABLE IF EXISTS `skillsandexperiences`;
CREATE TABLE `skillsandexperiences`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantProfileID` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_SkillsAndExperiences_ApplicantProfileID`(`ApplicantProfileID` ASC) USING BTREE,
  CONSTRAINT `FK_SkillsAndExperiences_ApplicantProfiles_ApplicantProfileID` FOREIGN KEY (`ApplicantProfileID`) REFERENCES `applicantprofiles` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 89 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for skillcertificates
-- ----------------------------
DROP TABLE IF EXISTS `skillcertificates`;
CREATE TABLE `skillcertificates`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `SkillName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_SkillCertificates_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  CONSTRAINT `FK_SkillCertificates_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 239 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for resumes
-- ----------------------------
DROP TABLE IF EXISTS `resumes`;
CREATE TABLE `resumes`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `JobPositionID` int NOT NULL,
  `CompanyID` int NOT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `FilePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedDate` datetime(6) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_Resumes_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  INDEX `IX_Resumes_CompanyID`(`CompanyID` ASC) USING BTREE,
  INDEX `IX_Resumes_JobPositionID`(`JobPositionID` ASC) USING BTREE,
  CONSTRAINT `FK_Resumes_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Resumes_Companies_CompanyID` FOREIGN KEY (`CompanyID`) REFERENCES `companies` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Resumes_JobPositions_JobPositionID` FOREIGN KEY (`JobPositionID`) REFERENCES `jobpositions` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for personalcharacteristics
-- ----------------------------
DROP TABLE IF EXISTS `personalcharacteristics`;
CREATE TABLE `personalcharacteristics`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantProfileID` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_PersonalCharacteristics_ApplicantProfileID`(`ApplicantProfileID` ASC) USING BTREE,
  CONSTRAINT `FK_PersonalCharacteristics_ApplicantProfiles_ApplicantProfileID` FOREIGN KEY (`ApplicantProfileID`) REFERENCES `applicantprofiles` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 87 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for jobpositions
-- ----------------------------
DROP TABLE IF EXISTS `jobpositions`;
CREATE TABLE `jobpositions`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CompanyID` int NOT NULL,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `MinimumWorkYears` int NOT NULL,
  `MinimumEducationLevel` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_JobPositions_CompanyID`(`CompanyID` ASC) USING BTREE,
  CONSTRAINT `FK_JobPositions_Companies_CompanyID` FOREIGN KEY (`CompanyID`) REFERENCES `companies` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for jobmatches
-- ----------------------------
DROP TABLE IF EXISTS `jobmatches`;
CREATE TABLE `jobmatches`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `JobTitle` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Score` int NOT NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ApplicantProfileID` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_JobMatches_ApplicantProfileID`(`ApplicantProfileID` ASC) USING BTREE,
  CONSTRAINT `FK_JobMatches_ApplicantProfiles_ApplicantProfileID` FOREIGN KEY (`ApplicantProfileID`) REFERENCES `applicantprofiles` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 895 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for jobkeywords
-- ----------------------------
DROP TABLE IF EXISTS `jobkeywords`;
CREATE TABLE `jobkeywords`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `JobPositionID` int NOT NULL,
  `Keyword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_JobKeywords_JobPositionID`(`JobPositionID` ASC) USING BTREE,
  CONSTRAINT `FK_JobKeywords_JobPositions_JobPositionID` FOREIGN KEY (`JobPositionID`) REFERENCES `jobpositions` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 31 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for educationbackgrounds
-- ----------------------------
DROP TABLE IF EXISTS `educationbackgrounds`;
CREATE TABLE `educationbackgrounds`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `Time` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `School` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Major` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_EducationBackgrounds_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  CONSTRAINT `FK_EducationBackgrounds_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 119 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for companies
-- ----------------------------
DROP TABLE IF EXISTS `companies`;
CREATE TABLE `companies`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for characteristics
-- ----------------------------
DROP TABLE IF EXISTS `characteristics`;
CREATE TABLE `characteristics`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Score` int NOT NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `AchievementsAndHighlightsID` int NULL DEFAULT NULL,
  `PersonalCharacteristicsID` int NULL DEFAULT NULL,
  `SkillsAndExperiencesID` int NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_Characteristics_AchievementsAndHighlightsID`(`AchievementsAndHighlightsID` ASC) USING BTREE,
  INDEX `IX_Characteristics_PersonalCharacteristicsID`(`PersonalCharacteristicsID` ASC) USING BTREE,
  INDEX `IX_Characteristics_SkillsAndExperiencesID`(`SkillsAndExperiencesID` ASC) USING BTREE,
  CONSTRAINT `FK_Characteristics_AchievementsAndHighlights_AchievementsAndHig~` FOREIGN KEY (`AchievementsAndHighlightsID`) REFERENCES `achievementsandhighlights` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Characteristics_PersonalCharacteristics_PersonalCharacterist~` FOREIGN KEY (`PersonalCharacteristicsID`) REFERENCES `personalcharacteristics` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Characteristics_SkillsAndExperiences_SkillsAndExperiencesID` FOREIGN KEY (`SkillsAndExperiencesID`) REFERENCES `skillsandexperiences` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 787 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for awards
-- ----------------------------
DROP TABLE IF EXISTS `awards`;
CREATE TABLE `awards`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `AwardName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `IX_Awards_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  CONSTRAINT `FK_Awards_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 161 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for applicants
-- ----------------------------
DROP TABLE IF EXISTS `applicants`;
CREATE TABLE `applicants`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Age` int NOT NULL,
  `Gender` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `JobIntention` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `HighestEducation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Major` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `GraduatedFrom` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `GraduatedFromLevel` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SelfEvaluation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `TotalWorkYears` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for applicantprofiles
-- ----------------------------
DROP TABLE IF EXISTS `applicantprofiles`;
CREATE TABLE `applicantprofiles`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantID` int NOT NULL,
  `MatchingReason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `MatchingScore` int NOT NULL,
  `WorkStability` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `StabilityReason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_ApplicantProfiles_ApplicantID`(`ApplicantID` ASC) USING BTREE,
  CONSTRAINT `FK_ApplicantProfiles_Applicants_ApplicantID` FOREIGN KEY (`ApplicantID`) REFERENCES `applicants` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for achievementsandhighlights
-- ----------------------------
DROP TABLE IF EXISTS `achievementsandhighlights`;
CREATE TABLE `achievementsandhighlights`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ApplicantProfileID` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_AchievementsAndHighlights_ApplicantProfileID`(`ApplicantProfileID` ASC) USING BTREE,
  CONSTRAINT `FK_AchievementsAndHighlights_ApplicantProfiles_ApplicantProfile~` FOREIGN KEY (`ApplicantProfileID`) REFERENCES `applicantprofiles` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 89 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
