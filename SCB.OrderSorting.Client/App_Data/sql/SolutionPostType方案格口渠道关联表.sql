/*
Navicat SQLite Data Transfer

Source Server         : mai
Source Server Version : 31202
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 31202
File Encoding         : 65001

Date: 2016-07-12 11:58:57
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for SolutionPostType
-- ----------------------------
DROP TABLE IF EXISTS "main"."SolutionPostType";
CREATE TABLE [SolutionPostType] (
    [Id] text PRIMARY KEY NOT NULL,
    [SortingSolutionId] text NOT NULL,
    [CabinetId] int NOT NULL,
    [LatticeSettingId] int NOT NULL,
    [PostTypeId] text NOT NULL,
    [PostTypeName] text NOT NULL DEFAULT ""
);

-- ----------------------------
-- Records of SolutionPostType
-- ----------------------------
INSERT INTO "main"."SolutionPostType" VALUES ('b3690211-ed80-47cc-b06c-0cae71fdcd29', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('0a3b53cf-4871-47c7-a203-f01571516237', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('0b030a49-c6b9-4dae-9506-524d7c9e5ee9', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('bdf4f081-55ad-4290-b8fe-beecce5e73ef', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('82f83d9e-ae82-4391-a6c3-a68d7e926df4', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('91db4a1f-7f44-48b4-817a-d3d73d7c7fe5', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('006fd5c7-5779-475c-b142-3cbb16de9bd1', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('7c357e21-6376-4de6-b3fc-333cc8069579', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('eb9d81e1-5e04-419f-a28c-54fd0fd5c1a6', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('59eefd10-806d-4314-a93e-6b5ea942ff5d', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('0b3f9a8f-812f-4e69-af13-5d24b0b483cc', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('68c13021-f85c-4efd-bbe0-46094d9449ba', '677f3fca-07dc-46b6-a28c-73a49c9e8982', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('1a86583a-3ff2-471c-93e9-d00e7bf9a710', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('1f3d5f45-6f1e-4107-a5cd-b3976102a6d5', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('25fd06e6-a750-4036-b6d7-e72b62c81f00', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('2a44f843-f1ca-47d6-bc82-4b4696ca0b61', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('533b12ad-0942-40f3-980f-42baac5c9df8', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('6770d397-f965-48f8-8361-35dcfa73ef4a', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('6c661e45-5bb1-45e6-8ba5-3fea6efe0c8d', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('6de20899-b661-4eb3-a5bf-361ea39cb9b6', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('b04a5a30-40a5-46b7-9a82-3674290e2ce2', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('d5f69fdb-6772-4048-bec8-b3ac527d7e5c', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('ddeba270-5257-4f59-8ef1-8edca277b73e', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('ec3523a8-b8df-4a54-8d1c-638d5a28609d', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('53ec910e-9f08-484e-87e8-10783cc82306', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 243, 'B2C英国挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('d95237d6-23eb-4a48-ac25-9e020ffe0a1a', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 282, '2号B2C Europe 平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('dcde0386-d102-47de-937b-7cace8f77853', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 13, 283, '2号B2C英国挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('0024c5e6-e3e1-4b3f-b38a-1e1f3d300506', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('11261705-f30c-40a1-ae3b-a85c4969d4c6', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 282, '2号B2C Europe 平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('14468988-0255-4b16-89c3-edc706458134', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('214bddfb-e39b-4a53-8ee8-91c8d81fcde7', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('3942569d-ab3d-495e-97fd-ea6e9d44f204', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('3e140564-7d38-4fa3-b2ce-8d45529c2e3d', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('3fe0fc28-9c44-4c48-8f2b-b5afb46ae22d', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('42984579-77e8-4a55-8c0d-f18816a4c711', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 283, '2号B2C英国挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('48446038-84f9-4434-813b-d017d4adb7fd', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('6348d004-fe83-46d0-b070-5150eeae47af', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('9aa561dd-ba4e-49dc-b988-2a01483c6c58', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('bf6c281f-68eb-4f04-a5b7-066ac4786d9a', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('c10fa1e6-3959-43b0-be9f-acd81c8518c3', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 2, 13, 243, 'B2C英国挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('c893beac-a062-496a-a5ed-f329ecf4b5a3', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('f4179fe2-2318-4c13-b8cf-47a83e73f1b6', 'd00f8880-4db0-4c80-bac0-f553e19cab1c', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('800ef055-89fb-47e4-b519-a30855c0e4c9', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 17, 242, 'B2C EUROP 平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('eb9bf6f6-c737-4ba9-a2c9-61d4d60abe2a', '2e167429-7b2c-4c1d-b8c8-5d5f99019b12', 2, 17, 243, 'B2C英国挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('0a3585b2-504b-44d7-9fb8-1c55729e5fff', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('1564a9fa-1f7b-4bca-a2e7-bed52bbfc92c', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('1d498dae-0181-4bb8-9bf7-8b5c08fdfa5b', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('25166422-0f17-4805-b53d-cf7c18e0022a', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('4b2fb6e3-4682-4a5b-a58e-6698a5628a70', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('57ad7046-5f66-443b-93bb-1c817a6e0f0e', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('58fadf5c-2ab2-4eeb-bce2-761186a4d062', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('67e94604-ae60-4cc0-9090-245e5ac7f551', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('6aa52d23-812b-4cd8-a2e9-b3ff26db6007', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('6d8f8999-6786-407b-b491-42cdcb38f6a9', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('8df56241-556e-45b8-beb9-8191837d9cb6', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('faffa269-7e45-4a4a-97ea-c022feace593', 'c1031dbb-2a1c-4932-96c5-83c5b5ef3102', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('15a70b70-5509-4a9c-a97b-4bd7720b514d', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('22191637-be5b-48e9-9161-f0cec1ffb26c', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('2e1ee171-7cea-4c77-989c-f968b5393c3a', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('43346852-4b40-4929-8409-75242a3e4089', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('4886652f-7611-4393-82cc-83cc71c7adc1', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('4d2e1f93-eda2-4df2-9a18-2cee1c41f77d', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('51c3120c-8799-41cf-8f22-9cc352c85ee0', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('59f0aa09-7a4d-44c9-bfb0-60ce1f9227f0', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('7f6f713e-e3ce-4620-baec-e00e0fff8b8b', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('92ee5c81-d897-4bbb-bc68-801610f890e1', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('c5fcce8d-3dc3-4e27-bb41-075f716fa4be', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('f8e6d9a9-79a3-46e2-8ecb-c93bd558398e', 'fff6a9da-af80-439a-8a28-0566530a9dc2', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('0177a168-878c-416e-a756-521dc50dbca0', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 5, 18, '中国DHL');
INSERT INTO "main"."SolutionPostType" VALUES ('0e922f99-eea8-44e8-95d8-55dbec492f57', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('11f7d9d1-7c45-4a5c-99a8-1b74cf0f2351', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('24a6ba89-89fc-4c1f-80b5-5b44b60cb6e9', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('30f23747-0c0e-4d03-8a78-fc5867c03d31', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('38ffe8a5-a419-4b88-a6ce-579d407c2027', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('6c9debbe-fdc6-443b-87d5-e391047656e9', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('9f2455c3-dc14-499f-a7b3-adf791dc662a', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('b4c7d262-e6e0-4213-809b-30a6f0afe936', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('bccc9af9-3939-451d-9ffb-9ffc046d005b', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('de5a1622-a941-495a-9dd5-1fe4f76be1ae', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('f8327ab2-1ed2-4bd5-9552-28fe4c9204d5', 'd5aec967-8595-48ea-b74f-f5f40c1f70cf', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('06365477-bc05-4f06-89d0-cf9eaa659f61', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 10, 211, '欧邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('469b0a40-4cf9-4817-b270-a64ba01f2609', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 6, 207, '佛山小包挂号');
INSERT INTO "main"."SolutionPostType" VALUES ('570902d3-d384-4a11-94c3-d826b55539e8', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 2, 149, '新加坡邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('5ecf9046-d6e4-4a5b-988f-d58f409c0c4b', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 9, 163, '俄罗斯专线小包');
INSERT INTO "main"."SolutionPostType" VALUES ('685d0a00-ef45-4856-8282-8adc47c7d6f9', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 3, 39, '飞特英国专线二类平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('6bc7795d-b6de-4b13-87e5-dd37e39c4823', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 1, 116, 'DHL小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('8c6a69f0-0a63-439d-bf63-eee9255d069e', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 7, 41, '美国E邮宝');
INSERT INTO "main"."SolutionPostType" VALUES ('8ffa33b9-87f5-44ab-a455-d7f5cc1d6db2', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 12, 159, '马来西亚邮政平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('920cf930-e01f-4973-96f4-218098edcbf6', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 4, 14, '中国UPS');
INSERT INTO "main"."SolutionPostType" VALUES ('bb5ac238-42f8-4d78-9bb4-f201570b4103', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 11, 258, '欧洲小包平邮');
INSERT INTO "main"."SolutionPostType" VALUES ('ca404ed7-b921-4fb5-9868-cbbbc1d5904f', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 8, 62, '中国FEDEX');
INSERT INTO "main"."SolutionPostType" VALUES ('e335121d-470e-47ce-ae31-02d2f3b3932c', 'a527547f-65a0-4412-9236-d16e2d39238f', 1, 5, 18, '中国DHL');

-- ----------------------------
-- Indexes structure for table SolutionPostType
-- ----------------------------
CREATE INDEX "main"."IX_SortingSolutionId"
ON "SolutionPostType" ("SortingSolutionId" ASC, "CabinetId" ASC);
PRAGMA foreign_keys = ON;
