/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:55:45
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for ConfigParamers
-- ----------------------------
DROP TABLE IF EXISTS "main"."ConfigParamers";
CREATE TABLE [ConfigParamers] (
    [Key] text PRIMARY KEY NOT NULL,
    [Value] ntext NOT NULL
);

-- ----------------------------
-- Records of ConfigParamers
-- ----------------------------
INSERT INTO "main"."ConfigParamers" VALUES ('LoginData', 'W3siRW1haWwiOiJtYWl5b25nbGluQHNlbGxlcmN1YmUuY24ifV0=');
INSERT INTO "main"."ConfigParamers" VALUES ('SystemSetting', 'eyJNb2RidXNTZXR0aW5nIjp7IlBvcnROYW1lIjoiQ09NMyIsIkJhdWRSYXRlIjo5NjAwLCJQYXJpdHkiOjAsIkRhdGFCaXRzIjo4LCJTdG9wQml0cyI6MSwiTEVEU3RhcnRBZGRyZXNzIjo0MDA5OSwiR3JhdGluZ1N0YXJ0QWRkcmVzcyI6MzAwOTksIkJ1dHRvblN0YXJ0QWRkcmVzcyI6MzAxMTEsIlJlc2V0R3JhdGluZ1N0YXJ0QWRkcmVzcyI6NDAwMDAsIldhcm5pbmdSZWRMaWdodFN0YXJ0QWRkcmVzcyI6NDAxMTEsIldhcm5pbmdHcmVlbkxpZ2h0U3RhcnRBZGRyZXNzIjo0MDExMiwiV2FybmluZ1llbGxvd0xpZ2h0U3RhcnRBZGRyZXNzIjo0MDExMywiV2FybmluZ0J1enplclN0YXJ0QWRkcmVzcyI6NDAxMTQsIk51bWJlck9mUG9pbnRzIjoxMn0sIkNhYmluZXROdW1iZXIiOjQsIlNsYXZlQ29uZmlnIjpbeyJDYWJpbmV0SWQiOjEsIlNsYXZlQWRkcmVzcyI6MTV9LHsiQ2FiaW5ldElkIjoyLCJTbGF2ZUFkZHJlc3MiOjE0fSx7IkNhYmluZXRJZCI6MywiU2xhdmVBZGRyZXNzIjoxMn0seyJDYWJpbmV0SWQiOjQsIlNsYXZlQWRkcmVzcyI6MH1dLCJTb3J0aW5nUGF0dGVuIjoyLCJMb2dTdG9yYWdlRGF5cyI6NiwiSXNGbHl0Ijp0cnVlLCJTb3J0aW5nU29sdXRpb24iOiJhNTI3NTQ3Zi02NWEwLTQ0MTItOTIzNi1kMTZlMmQzOTIzOGYifQ==');
PRAGMA foreign_keys = ON;
