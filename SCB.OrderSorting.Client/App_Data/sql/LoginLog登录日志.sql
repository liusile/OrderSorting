/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-08-12 11:56:07
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for LoginLog
-- ----------------------------
DROP TABLE IF EXISTS "main"."LoginLog";
CREATE TABLE [LoginLog] (
    [Id] text PRIMARY KEY NOT NULL,
    [UserName] text NOT NULL DEFAULT '',
    [LoginTime] datetime NOT NULL
);