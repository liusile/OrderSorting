/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:56:07
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for LatticeOrdersCache
-- ----------------------------
DROP TABLE IF EXISTS "main"."LatticeOrdersCache";
CREATE TABLE [LatticeOrdersCache] (
    [OrderId] text PRIMARY KEY NOT NULL,
    [TraceId] text NOT NULL DEFAULT "",
    [LatticesettingId] int NOT NULL,
    [Weight] decimal NOT NULL
);

-- ----------------------------
-- Indexes structure for table LatticeOrdersCache
-- ----------------------------
CREATE INDEX "main"."IX_LatticesettingId"
ON "LatticeOrdersCache" ("LatticesettingId" ASC);
PRAGMA foreign_keys = ON;
