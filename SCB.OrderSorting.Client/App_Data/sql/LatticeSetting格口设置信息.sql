/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:56:31
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for LatticeSetting
-- ----------------------------
DROP TABLE IF EXISTS "main"."LatticeSetting";
CREATE TABLE [LatticeSetting] (
    [ID] int PRIMARY KEY NOT NULL,
    [CabinetId] int NOT NULL,
    [LatticeId] text NOT NULL,
    [Status] int NOT NULL,
    [LEDIndex] int NOT NULL,
    [GratingIndex] int NOT NULL,
    [ButtonIndex] int NOT NULL,
	[IsEnable] text NOT NULL,
);

-- ----------------------------
-- Records of LatticeSetting
-- ----------------------------
INSERT INTO "main"."LatticeSetting" VALUES (1, 1, 100, 1, 0, 0, 0,'true');
INSERT INTO "main"."LatticeSetting" VALUES (2, 1, 103, 1, 3, 3, 3,'true');
INSERT INTO "main"."LatticeSetting" VALUES (3, 1, 106, 1, 6, 6, 6,'true');
INSERT INTO "main"."LatticeSetting" VALUES (4, 1, 109, 1, 9, 9, 9,'true');
INSERT INTO "main"."LatticeSetting" VALUES (5, 1, 101, 1, 1, 1, 1,'true');
INSERT INTO "main"."LatticeSetting" VALUES (6, 1, 104, 1, 4, 4, 4,'true');
INSERT INTO "main"."LatticeSetting" VALUES (7, 1, 107, 1, 7, 7, 7,'true');
INSERT INTO "main"."LatticeSetting" VALUES (8, 1, 110, 1, 10, 10, 10,'true');
INSERT INTO "main"."LatticeSetting" VALUES (9, 1, 102, 1, 2, 2, 2,'true');
INSERT INTO "main"."LatticeSetting" VALUES (10, 1, 105, 1, 5, 5, 5,'true');
INSERT INTO "main"."LatticeSetting" VALUES (11, 1, 108, 1, 8, 8, 8,'true');
INSERT INTO "main"."LatticeSetting" VALUES (12, 1, 111, 1, 11, 11, 11,'true');
INSERT INTO "main"."LatticeSetting" VALUES (13, 2, 200, 1, 0, 0, 0,'true');
INSERT INTO "main"."LatticeSetting" VALUES (14, 2, 203, 1, 3, 3, 3,'true');
INSERT INTO "main"."LatticeSetting" VALUES (15, 2, 206, 1, 6, 6, 6,'true');
INSERT INTO "main"."LatticeSetting" VALUES (16, 2, 209, 1, 9, 9, 9,'true');
INSERT INTO "main"."LatticeSetting" VALUES (17, 2, 201, 1, 1, 1, 1,'true');
INSERT INTO "main"."LatticeSetting" VALUES (18, 2, 204, 1, 4, 4, 4,'true');
INSERT INTO "main"."LatticeSetting" VALUES (19, 2, 207, 1, 7, 7, 7,'true');
INSERT INTO "main"."LatticeSetting" VALUES (20, 2, 210, 1, 10, 10, 10,'true');
INSERT INTO "main"."LatticeSetting" VALUES (21, 2, 202, 1, 2, 2, 2,'true');
INSERT INTO "main"."LatticeSetting" VALUES (22, 2, 205, 1, 5, 5, 5,'true');
INSERT INTO "main"."LatticeSetting" VALUES (23, 2, 208, 1, 8, 8, 8,'true');
INSERT INTO "main"."LatticeSetting" VALUES (24, 2, 211, 1, 11, 11, 11,'true');
INSERT INTO "main"."LatticeSetting" VALUES (25, 3, 300, 1, 0, 0, 0,'true');
INSERT INTO "main"."LatticeSetting" VALUES (26, 3, 303, 1, 3, 3, 3,'true');
INSERT INTO "main"."LatticeSetting" VALUES (27, 3, 306, 1, 6, 6, 6,'true');
INSERT INTO "main"."LatticeSetting" VALUES (28, 3, 309, 1, 9, 9, 9,'true');
INSERT INTO "main"."LatticeSetting" VALUES (29, 3, 301, 1, 1, 1, 1,'true');
INSERT INTO "main"."LatticeSetting" VALUES (30, 3, 304, 1, 4, 4, 4,'true');
INSERT INTO "main"."LatticeSetting" VALUES (31, 3, 307, 1, 7, 7, 7,'true');
INSERT INTO "main"."LatticeSetting" VALUES (32, 3, 310, 1, 10, 10, 10,'true');
INSERT INTO "main"."LatticeSetting" VALUES (33, 3, 302, 1, 2, 2, 2,'true');
INSERT INTO "main"."LatticeSetting" VALUES (34, 3, 305, 1, 5, 5, 5,'true');
INSERT INTO "main"."LatticeSetting" VALUES (35, 3, 308, 1, 8, 8, 8,'true');
INSERT INTO "main"."LatticeSetting" VALUES (36, 3, 311, 1, 11, 11, 11,'true');
INSERT INTO "main"."LatticeSetting" VALUES (37, 4, 400, 1, 0, 0, 0,'true');
INSERT INTO "main"."LatticeSetting" VALUES (38, 4, 403, 1, 3, 3, 3,'true');
INSERT INTO "main"."LatticeSetting" VALUES (39, 4, 406, 1, 6, 6, 6,'true');
INSERT INTO "main"."LatticeSetting" VALUES (40, 4, 409, 1, 9, 9, 9,'true');
INSERT INTO "main"."LatticeSetting" VALUES (41, 4, 401, 1, 1, 1, 1,'true');
INSERT INTO "main"."LatticeSetting" VALUES (42, 4, 404, 1, 4, 4, 4,'true');
INSERT INTO "main"."LatticeSetting" VALUES (43, 4, 407, 1, 7, 7, 7,'true');
INSERT INTO "main"."LatticeSetting" VALUES (44, 4, 410, 1, 10, 10, 10,'true');
INSERT INTO "main"."LatticeSetting" VALUES (45, 4, 402, 1, 2, 2, 2,'true');
INSERT INTO "main"."LatticeSetting" VALUES (46, 4, 405, 1, 5, 5, 5,'true');
INSERT INTO "main"."LatticeSetting" VALUES (47, 4, 408, 1, 8, 8, 8,'true');
INSERT INTO "main"."LatticeSetting" VALUES (48, 4, 411, 1, 11, 11, 11,'true');

-- ----------------------------
-- Indexes structure for table LatticeSetting
-- ----------------------------
CREATE INDEX "main"."IX_CabinetId"
ON "LatticeSetting" ("CabinetId" ASC);
PRAGMA foreign_keys = ON;
