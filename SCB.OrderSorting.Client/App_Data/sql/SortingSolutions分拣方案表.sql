/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:59:06
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for SortingSolutions
-- ----------------------------
DROP TABLE IF EXISTS "main"."SortingSolutions";
CREATE TABLE [SortingSolutions] (
    [Id] text PRIMARY KEY NOT NULL,
    [Name] text NOT NULL DEFAULT ""
);

-- ----------------------------
-- Records of SortingSolutions
-- ----------------------------
INSERT INTO "main"."SortingSolutions" VALUES ('677f3fca-07dc-46b6-a28c-73a49c9e8982', '默认');
INSERT INTO "main"."SortingSolutions" VALUES ('fff6a9da-af80-439a-8a28-0566530a9dc2', 'ftyityuoynhvn');
INSERT INTO "main"."SortingSolutions" VALUES ('d5aec967-8595-48ea-b74f-f5f40c1f70cf', '飞特测试');
INSERT INTO "main"."SortingSolutions" VALUES ('a527547f-65a0-4412-9236-d16e2d39238f', '三个架子');
PRAGMA foreign_keys = ON;
