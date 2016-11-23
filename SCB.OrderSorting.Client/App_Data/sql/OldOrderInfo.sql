/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:56:44
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for OldOrderInfo
-- ----------------------------
DROP TABLE IF EXISTS "main"."OldOrderInfo";
CREATE TABLE [OldOrderInfo] (
    [OrderId] text PRIMARY KEY NOT NULL,
    [TraceId] text NOT NULL,
    [CountryId] text NOT NULL,
    [CountryName] text NOT NULL DEFAULT "",
    [PostId] text NOT NULL,
    [PostName] text NOT NULL DEFAULT "",
    [Zip] text NOT NULL,
    [Weight] decimal NOT NULL,
    [CreateTime] datetime NOT NULL
);
PRAGMA foreign_keys = ON;
