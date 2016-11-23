/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:57:45
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for PackingLog
-- ----------------------------
DROP TABLE IF EXISTS "main"."PackingLog";
CREATE TABLE [PackingLog] (
    [ID] text PRIMARY KEY NOT NULL,
    [PackNumber] text NOT NULL,
    [CabinetId] text NOT NULL,
    [LatticeId] text NOT NULL,
    [OrderIds] text NOT NULL,
    [PostTypeIds] text NOT NULL,
    [PostTypeNames] text NOT NULL,
    [CountryIds] text NOT NULL,
    [CountryNames] text NOT NULL,
    [OrderQty] int NOT NULL,
    [OperationType] int NOT NULL,
    [OperationTime] datetime NOT NULL,
    [Weight] decimal NOT NULL,
    [UserName] text NOT NULL,
    [UserId] int NOT NULL
);

-- ----------------------------
-- Indexes structure for table PackingLog
-- ----------------------------
CREATE INDEX "main"."IX_PackNumber"
ON "PackingLog" ("PackNumber" ASC);
CREATE INDEX "main"."IX_PkgTime"
ON "PackingLog" ("OperationTime" ASC);
PRAGMA foreign_keys = ON;
