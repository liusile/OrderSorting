/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:57:25
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for OrderSortingLog
-- ----------------------------
DROP TABLE IF EXISTS "main"."OrderSortingLog";
CREATE TABLE [OrderSortingLog] (
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

-- ----------------------------
-- Indexes structure for table OrderSortingLog
-- ----------------------------
CREATE INDEX "main"."IX_OperationTime"
ON "OrderSortingLog" ("OperationTime" ASC);
CREATE INDEX "main"."IX_OperationType"
ON "OrderSortingLog" ("OperationType" ASC);
CREATE INDEX "main"."IX_OrderId"
ON "OrderSortingLog" ("OrderId" ASC);
CREATE INDEX "main"."IX_Status"
ON "OrderSortingLog" ("Status" ASC);
CREATE INDEX "main"."IX_Status_OperationType"
ON "OrderSortingLog" ("Status" ASC, "OperationType" ASC);
PRAGMA foreign_keys = ON;
