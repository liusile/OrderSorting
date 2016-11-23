/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:56:50
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for OldOrderSortingLog
-- ----------------------------
DROP TABLE IF EXISTS "main"."OldOrderSortingLog";
CREATE TABLE [OldOrderSortingLog] (
    [ID] text PRIMARY KEY NOT NULL,
    [OrderId] text NOT NULL,
    [TargetCabinetId] text NOT NULL,
    [TargetLatticeId] text NOT NULL,
    [ResultCabinetId] text NOT NULL,
    [ResultLatticeId] text NOT NULL,
    [Weight] decimal NOT NULL,
    [UserId] int NOT NULL,
    [UserName] text NOT NULL,
    [Status] int NOT NULL,
    [OperationType] int NOT NULL,
    [OperationTime] datetime NOT NULL
);
PRAGMA foreign_keys = ON;
