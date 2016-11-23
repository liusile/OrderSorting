/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:57:13
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for OrderInfo
-- ----------------------------
DROP TABLE IF EXISTS "main"."OrderInfo";
CREATE TABLE [OrderInfo] (
    [OrderId] text PRIMARY KEY NOT NULL,
    [TraceId] text NOT NULL DEFAULT "",
    [CountryId] text NOT NULL,
    [CountryName] text NOT NULL DEFAULT "",
    [PostId] text NOT NULL,
    [PostName] text NOT NULL DEFAULT "",
    [Zip] text NOT NULL DEFAULT "",
    [Weight] decimal NOT NULL,
    [CreateTime] datetime NOT NULL
);

-- ----------------------------
-- Indexes structure for table OrderInfo
-- ----------------------------
CREATE INDEX "main"."IX_CountryId"
ON "OrderInfo" ("CountryId" ASC);
CREATE INDEX "main"."IX_CreateTime"
ON "OrderInfo" ("CreateTime" DESC);
CREATE INDEX "main"."IX_PostId"
ON "OrderInfo" ("PostId" ASC);
PRAGMA foreign_keys = ON;
