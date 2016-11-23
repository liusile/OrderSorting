/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:58:49
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for SolutionCountry
-- ----------------------------
DROP TABLE IF EXISTS "main"."SolutionCountry";
CREATE TABLE [SolutionCountry] (
    [Id] text PRIMARY KEY NOT NULL,
    [SortingSolutionId] text NOT NULL,
    [CabinetId] int NOT NULL,
    [LatticeSettingId] int NOT NULL,
    [CountryId] text NOT NULL,
    [CountryName] text NOT NULL
);

-- ----------------------------
-- Records of SolutionCountry
-- ----------------------------
INSERT INTO "main"."SolutionCountry" VALUES ('04e78206-40e1-46d3-8c79-88578d4307f2', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('61c7b58b-a4e1-4625-9bd8-8b7f00d709b4', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('09b5da63-c7d5-42ee-8160-b2f967ff93ef', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 4, 216, '瑞典');
INSERT INTO "main"."SolutionCountry" VALUES ('3212ff5c-ad7e-4f06-bdb6-c2e3141b77ba', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('14391703-b5d1-4d1b-9baf-11931c0647e5', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('0b71d39f-03c0-40a4-95ac-678bbb661108', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 6, 219, '台湾');
INSERT INTO "main"."SolutionCountry" VALUES ('a48c8263-fd1f-45ae-9781-3dc4ed71c875', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('49fb4b9f-e8d3-4621-bedb-e8448c4950c3', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 8, 108, '印度尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('f70baba6-c864-4638-9788-982cbffa06d6', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('01ebe620-67b8-4062-aafa-8e3e239c3f9c', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('6554e383-0147-470b-8789-f7758bd495f9', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('913164fc-8b75-4e2b-940e-e065385d8c34', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 12, 138, '马来西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('10bc1d65-fc14-4a9f-89f3-281affae3aee', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 6, 219, '台湾');
INSERT INTO "main"."SolutionCountry" VALUES ('4455960d-0ce1-4ce0-88fe-161818176475', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('45c35660-e54a-44a9-9d95-36fece6d75ec', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 8, 108, '印度尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('45e660f9-d60f-47a3-8c79-a6366ca8a8bf', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('84fa8ce8-5487-43c8-85ab-03bf2af13c01', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('9c206d88-1983-42a8-8d0c-1a50409e8012', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('a4207fe5-0b51-4120-a296-be61c8631eaf', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('a4671c0e-be85-4c76-9316-91036e894e17', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('abae47a9-9c79-4a67-b57a-23503c176857', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 12, 138, '马来西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('cbea802a-004b-4164-bc5a-8f312114b4c7', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('e128f264-b954-4266-8b19-4304df1c09b3', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('fec5e0ef-7087-430a-a7f5-62ac912a4134', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 4, 216, '瑞典');
INSERT INTO "main"."SolutionCountry" VALUES ('180a2b10-d2ac-48f9-b5f5-9a2583261208', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 55, '刚果');
INSERT INTO "main"."SolutionCountry" VALUES ('51d9ae52-e5e1-4f43-b74b-68dea3ad7e64', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 217, '瑞士');
INSERT INTO "main"."SolutionCountry" VALUES ('b305652e-fe7f-48f4-83bc-794334ba99d3', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 48, '智利');
INSERT INTO "main"."SolutionCountry" VALUES ('03425557-5d59-4c25-8761-7b40bc0d2c50', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('062ca441-445a-423c-83cc-e6b327fab0c0', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 217, '瑞士');
INSERT INTO "main"."SolutionCountry" VALUES ('1951f8b7-5b99-448b-bb6c-14b392798ef2', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('2ada07a2-28b6-49bd-8e6b-e254a7de20f7', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 55, '刚果');
INSERT INTO "main"."SolutionCountry" VALUES ('528133b9-ef49-4c5b-be93-7a06ee3ba124', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('8d15fc97-32be-4090-930d-8649851e448b', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 4, 216, '瑞典');
INSERT INTO "main"."SolutionCountry" VALUES ('9b9b6988-bccb-45a5-a309-6f1654dd941c', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('9d5ec69d-1191-4158-829c-b38bdce69d25', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('ca1a751e-85e1-4a23-a3ff-c72fff9088ed', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 48, '智利');
INSERT INTO "main"."SolutionCountry" VALUES ('d2223454-4511-4c94-be7e-9b04728c851f', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 12, 138, '马来西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('dde68ff1-5497-4efe-a666-ac95fefb56b2', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 6, 219, '台湾');
INSERT INTO "main"."SolutionCountry" VALUES ('e0291ee9-beee-410e-a7e5-bd8e4b13b3ae', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('f157379e-b370-4e1a-bbf1-de3e22bad94e', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('f5857fb1-fda3-4280-bda8-6dd0ae8b4c93', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 8, 108, '印度尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('fcdb4f69-db5b-44ec-9a4d-db4672ed0d2c', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('a53a7f57-73bc-4ffd-8d14-8a6b7e86c768', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 17, 10, '亚美尼亚');
INSERT INTO "main"."SolutionCountry" VALUES ('e2963d68-a482-4248-b3cb-204cfecd4c51', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 17, 2, '阿尔巴尼亚');
INSERT INTO "main"."SolutionCountry" VALUES ('3e4dc18b-ef78-4bb3-9920-7c94b8700f78', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 17, 7, '南极洲');
INSERT INTO "main"."SolutionCountry" VALUES ('627ccf2c-6558-4fad-a3e1-80293b0b0f34', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 17, 9, '阿根廷');
INSERT INTO "main"."SolutionCountry" VALUES ('7fb9c0ac-103d-4b82-842a-88c0119572c6', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 17, 5, '安哥拉');
INSERT INTO "main"."SolutionCountry" VALUES ('ce25fdb5-f7b0-4b3b-92f3-aba09249cbe2', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 21, 1, '阿富汗');
INSERT INTO "main"."SolutionCountry" VALUES ('f8dfaf09-a4d1-416e-803a-8e86ab8daacc', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 21, 4, '安道尔');
INSERT INTO "main"."SolutionCountry" VALUES ('04571860-1caf-4975-8407-efeee889ad5b', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 8, 108, '印度尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('18907509-9d9d-4cd4-af37-0dbc98d2329e', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 4, 216, '瑞典');
INSERT INTO "main"."SolutionCountry" VALUES ('2487568b-71fc-45cc-b381-29366de58775', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('34fdc85a-8aa1-428d-a431-b39521070aaf', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 12, 138, '马来西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('50b271d0-685e-433f-ba97-d3929d2a1033', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('5b15cf2c-e57c-4bc1-b213-4fabe9659810', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('7bd959f7-73e1-4397-93c2-d4d6b02b436a', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('90d9ecaa-e067-47bf-ba91-92cfd02ebcdc', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('aa6249b4-f455-4547-8e0f-b18888892870', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('bb9fcf21-346c-4344-839f-d9cccadadcbb', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 6, 219, '台湾');
INSERT INTO "main"."SolutionCountry" VALUES ('d18bb148-a4f4-46f3-9f9b-ed6952f5d1c2', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('eceac543-6f40-4b70-a0ae-b487ae27edb8', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('0207e44e-0430-4829-be46-92c65765e04d', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('15370222-4a6a-4751-b9e7-a71c083e779c', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 8, 108, '印度尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('3b6863a7-218f-4808-bd6b-03e38c722f9a', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 4, 216, '瑞典');
INSERT INTO "main"."SolutionCountry" VALUES ('5471e4ba-73fb-4b96-9e30-0e50001da4f0', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 6, 219, '台湾');
INSERT INTO "main"."SolutionCountry" VALUES ('59c05195-6cff-488c-a8a1-989c8980e53f', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('5a0b1bda-8dbb-4249-83ac-60ac2365b15b', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('99b6197a-3a62-4689-a8d6-4834134732e7', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('9e290537-75b3-4d31-b0f8-d6d64b7c7dfb', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('ad4b3336-bbc6-4599-867b-1f918a46339d', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('c0e680be-af9c-48bc-ba9e-d0cec48f682f', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('e2d4dac0-6515-47b0-84a9-57d8ebed6922', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('f1dc60bd-cadb-497c-a598-52d006441735', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 12, 138, '马来西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('1548e22c-185d-4d1c-918d-3c751192c6a2', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('3544f637-0691-4574-a87a-bafdfdc52caf', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('408861c9-36c9-493b-9b58-26923c0d79e2', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('593721ae-ebc4-4b83-9244-d8f8f2b8b556', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('a7ff00a3-6eb5-459f-b1a9-93841b2c3ad6', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('b41e9c99-7418-4b46-83e2-853fe8879e21', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('e68419d0-7777-45e8-b2d9-78ae337854b9', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('e78d196e-6b41-4334-b525-fc25e4f6a019', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('5a18b8e4-427c-4c30-bc8b-cb2f37e1bfd5', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 6, 13, '澳大利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('48fe1800-17bc-4f76-86a1-500f21af73cc', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 4, 40, '加拿大');
INSERT INTO "main"."SolutionCountry" VALUES ('b86a7c2c-236b-4f5c-ab53-63004bef5ccf', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 8, 180, '波兰');
INSERT INTO "main"."SolutionCountry" VALUES ('c55bca86-5b0f-4dad-b344-af2aac8a4e69', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 12, 111, '爱尔兰');
INSERT INTO "main"."SolutionCountry" VALUES ('59ea27a2-5fd6-4fbd-9fb9-c9f816e2ed92', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 13, 105, '匈牙利');
INSERT INTO "main"."SolutionCountry" VALUES ('d57ed988-960c-475f-b851-ddd02821643f', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 17, 92, '希腊');
INSERT INTO "main"."SolutionCountry" VALUES ('ce3fa77a-1833-4f14-bf01-5a90f496a645', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 21, 161, '荷兰');
INSERT INTO "main"."SolutionCountry" VALUES ('7bba465e-0d6f-4ef5-916e-df0730bfa935', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 14, 112, '以色列');
INSERT INTO "main"."SolutionCountry" VALUES ('eae6e415-943f-42d5-84e7-bb35ec60974d', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 18, 164, '新西兰');
INSERT INTO "main"."SolutionCountry" VALUES ('3f7a66f4-8ac7-4a66-92e2-bc8f3c1da310', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 22, 202, '新加坡');
INSERT INTO "main"."SolutionCountry" VALUES ('14544dca-7447-4f76-be14-aee4b2cc7600', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 15, 88, '格鲁吉亚');
INSERT INTO "main"."SolutionCountry" VALUES ('ae757cde-9630-4f90-8cbb-656f85b1516a', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 19, 148, '墨西哥');
INSERT INTO "main"."SolutionCountry" VALUES ('1816cc68-6b03-4adc-a3dd-e7f998abcd42', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 23, 171, '挪威');
INSERT INTO "main"."SolutionCountry" VALUES ('32cc5831-cf9b-41f6-b7b4-23284e259ba2', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 16, 83, '法属波利尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('db202968-5ff6-4a69-aea4-aa54225e65f0', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 20, 246, '美属处女群岛');
INSERT INTO "main"."SolutionCountry" VALUES ('b79e06b8-394c-4728-b8bf-347405a15150', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 2, 24, 35, '保加利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('7b26dabe-e176-47c8-8e93-e63f4df969d0', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 25, 63, '捷克');
INSERT INTO "main"."SolutionCountry" VALUES ('a48b2cee-b4f3-47b5-a95a-a839a57963aa', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 26, 207, '南非');
INSERT INTO "main"."SolutionCountry" VALUES ('a55fc441-0f75-4e61-8130-d3508f21b017', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 27, 230, '土耳其');
INSERT INTO "main"."SolutionCountry" VALUES ('d543d368-8cba-410a-8641-b65362f5233b', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 28, 125, '拉脱维亚');
INSERT INTO "main"."SolutionCountry" VALUES ('93ec6229-88c2-44e3-9a13-309770fb8b4f', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 29, 107, '印度');
INSERT INTO "main"."SolutionCountry" VALUES ('daf5fa3f-e5f5-4b8c-af3b-ce65da3af2dd', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 30, 3, '阿尔及利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('a8f58b00-a995-4542-9198-17821b49b05b', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 31, 9, '阿根廷');
INSERT INTO "main"."SolutionCountry" VALUES ('dd5082f2-87d2-49a0-9ecd-51e3731ab5b0', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 32, 4, '安道尔');
INSERT INTO "main"."SolutionCountry" VALUES ('d661baf9-9699-4de7-b551-7263b6d9a083', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 33, 10, '亚美尼亚');
INSERT INTO "main"."SolutionCountry" VALUES ('cf71d100-ac12-4cda-8264-a5f8eaf3af7b', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 34, 14, '奥地利');
INSERT INTO "main"."SolutionCountry" VALUES ('c9bfcc29-7d85-4e1e-b5d9-0732a56764fa', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 35, 15, '阿塞拜疆');
INSERT INTO "main"."SolutionCountry" VALUES ('059369a6-5e59-4375-8158-2e2f5059d3b8', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 3, 36, 22, '白俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('0694a1bf-c961-442e-aa10-19bd8398d21a', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 17, 92, '希腊');
INSERT INTO "main"."SolutionCountry" VALUES ('1408351b-3ef8-43c3-b681-9220124806e2', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 19, 148, '墨西哥');
INSERT INTO "main"."SolutionCountry" VALUES ('1421ecf1-54e6-4136-b29c-8ad3a1a0cc08', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 14, 112, '以色列');
INSERT INTO "main"."SolutionCountry" VALUES ('1a4b9d82-439c-462b-875f-f3ea808f40e0', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 34, 14, '奥地利');
INSERT INTO "main"."SolutionCountry" VALUES ('21bb0386-8be3-4c1b-b50f-484e64a4eb15', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 6, 13, '澳大利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('32d0c3e2-23e4-443c-803b-e32a988e786c', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 23, 171, '挪威');
INSERT INTO "main"."SolutionCountry" VALUES ('35235197-4e53-4ccb-b1ab-7b1e1d467957', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 11, 209, '西班牙');
INSERT INTO "main"."SolutionCountry" VALUES ('35a80887-416b-48ec-93ad-dec191853549', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 4, 40, '加拿大');
INSERT INTO "main"."SolutionCountry" VALUES ('3aa07b2b-d866-4889-91c4-ea4d7d602677', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 12, 111, '爱尔兰');
INSERT INTO "main"."SolutionCountry" VALUES ('3b172b3a-3c6c-4044-85a3-d2773589620c', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 30, 3, '阿尔及利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('40b90c7f-4664-4645-9408-06f8e8aa70f2', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 25, 63, '捷克');
INSERT INTO "main"."SolutionCountry" VALUES ('4701eb7d-765e-494f-bafe-8d3e16418353', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 3, 237, '英国');
INSERT INTO "main"."SolutionCountry" VALUES ('47bc6489-7ed0-4906-81dd-cbbc8ba7c019', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 20, 246, '美属处女群岛');
INSERT INTO "main"."SolutionCountry" VALUES ('5035aa6c-3db9-49b7-8081-097636362bfc', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 26, 207, '南非');
INSERT INTO "main"."SolutionCountry" VALUES ('5634c837-7768-489e-a737-b2deeeca63d9', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 15, 88, '格鲁吉亚');
INSERT INTO "main"."SolutionCountry" VALUES ('6a30b30a-f5a2-4f9f-a4c1-f051f4ce5753', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 33, 10, '亚美尼亚');
INSERT INTO "main"."SolutionCountry" VALUES ('6f9d5e8c-69ab-472b-978f-d4b964292c6f', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 24, 35, '保加利亚');
INSERT INTO "main"."SolutionCountry" VALUES ('7f1f0f5f-a7f1-42cc-b98b-16c7483c45bc', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 2, 89, '德国');
INSERT INTO "main"."SolutionCountry" VALUES ('7fd4217b-77f8-43a5-8972-eb7ac4db03db', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 29, 107, '印度');
INSERT INTO "main"."SolutionCountry" VALUES ('93fde975-299b-4425-973f-4b2ad03e17d8', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 10, 113, '意大利');
INSERT INTO "main"."SolutionCountry" VALUES ('964bf9c9-e2be-4dc1-8430-85648a6521d9', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 7, 238, '美国');
INSERT INTO "main"."SolutionCountry" VALUES ('987b15e6-e384-4db4-a7f5-89a2da7bc191', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 21, 161, '荷兰');
INSERT INTO "main"."SolutionCountry" VALUES ('9ac5c0e2-394d-436a-afb8-ded3f1a40b57', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 28, 125, '拉脱维亚');
INSERT INTO "main"."SolutionCountry" VALUES ('b373e557-060b-4be7-9d1b-5a19b632670d', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 32, 4, '安道尔');
INSERT INTO "main"."SolutionCountry" VALUES ('b55d2162-2b9b-4fdc-b4cb-ffb18096d3e2', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 18, 164, '新西兰');
INSERT INTO "main"."SolutionCountry" VALUES ('cab032fe-9bec-43a9-b867-5d672feb4dbd', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 8, 180, '波兰');
INSERT INTO "main"."SolutionCountry" VALUES ('d0a48b0d-2db9-4aa4-ad4f-1cf71a4e3f67', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 31, 9, '阿根廷');
INSERT INTO "main"."SolutionCountry" VALUES ('d5c0bcfe-4f59-4e79-a486-227f998c18d7', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 13, 105, '匈牙利');
INSERT INTO "main"."SolutionCountry" VALUES ('d91c7d70-65ed-49d9-a913-2c9cf93e3fd8', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 35, 15, '阿塞拜疆');
INSERT INTO "main"."SolutionCountry" VALUES ('dfbe1bdf-fe69-477c-b7fe-a08bdc3810a2', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 22, 202, '新加坡');
INSERT INTO "main"."SolutionCountry" VALUES ('eeb141ab-a1d5-473a-b373-bb0bf9d07dd1', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 1, 80, '法国');
INSERT INTO "main"."SolutionCountry" VALUES ('f0af5931-e697-425b-b627-8a76d348a156', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 36, 22, '白俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('f102d2b9-1285-4c1e-ae0b-47014e1497c2', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 5, 115, '日本');
INSERT INTO "main"."SolutionCountry" VALUES ('f87de0f1-94b9-4976-9b87-06d3e5413863', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 9, 186, '俄罗斯');
INSERT INTO "main"."SolutionCountry" VALUES ('f91a3260-42c7-4946-b167-5395066ea4a0', 'a527547f-65a0-4412-9236-d16e2d39238f', 2, 16, 83, '法属波利尼西亚');
INSERT INTO "main"."SolutionCountry" VALUES ('fa800a2b-ec93-41aa-9db4-71fa6f414f5b', 'a527547f-65a0-4412-9236-d16e2d39238f', 3, 27, 230, '土耳其');

-- ----------------------------
-- Indexes structure for table SolutionCountry
-- ----------------------------
CREATE INDEX "main"."IX_SortingSolutionId_CabinetId"
ON "SolutionCountry" ("SortingSolutionId" ASC, "CabinetId" ASC);
PRAGMA foreign_keys = ON;
