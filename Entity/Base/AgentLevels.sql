/*
Navicat SQL Server Data Transfer

Source Server         : SQL Server
Source Server Version : 105000
Source Host           : .:1433
Source Database       : Creeper
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2017-12-10 21:46:17
*/


-- ----------------------------
-- Table structure for AgentLevels
-- ----------------------------
DROP TABLE [dbo].[AgentLevels]
GO
CREATE TABLE [dbo].[AgentLevels] (
[GameId] int NOT NULL ,
[AgentLevelId] int NOT NULL ,
[GameName] nvarchar(MAX) NULL ,
[IsValid] nvarchar(MAX) NULL ,
[AgentLevelName] nvarchar(MAX) NULL ,
[DProportion] decimal(18,2) NOT NULL ,
[IProportion] decimal(18,2) NOT NULL ,
[IProportion2] decimal(18,2) NOT NULL ,
[Id] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AgentLevels
-- ----------------------------
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'1', N'观三家重庆棋牌', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10006|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'2', N'观三家重庆棋牌', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10006|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'3', N'观三家重庆棋牌', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10006|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'4', N'观三家重庆棋牌', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10006|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'5', N'观三家重庆棋牌', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10006|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10006', N'6', N'观三家重庆棋牌', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10006|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'1', N'闲雅兰州麻将', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10013|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'2', N'闲雅兰州麻将', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10013|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'3', N'闲雅兰州麻将', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10013|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'4', N'闲雅兰州麻将', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10013|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'5', N'闲雅兰州麻将', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10013|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10013', N'6', N'闲雅兰州麻将', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10013|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'1', N'闲雅户县麻将', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10017|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'2', N'闲雅户县麻将', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10017|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'3', N'闲雅户县麻将', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10017|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'4', N'闲雅户县麻将', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10017|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'5', N'闲雅户县麻将', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10017|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10017', N'6', N'闲雅户县麻将', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10017|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'1', N'闲雅西游麻将', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10022|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'2', N'闲雅西游麻将', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10022|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'3', N'闲雅西游麻将', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10022|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'4', N'闲雅西游麻将', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10022|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'5', N'闲雅西游麻将', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10022|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10022', N'6', N'闲雅西游麻将', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10022|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'1', N'闲雅本溪棋牌', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10028|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'2', N'闲雅本溪棋牌', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10028|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'3', N'闲雅本溪棋牌', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10028|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'4', N'闲雅本溪棋牌', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10028|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'5', N'闲雅本溪棋牌', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10028|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10028', N'6', N'闲雅本溪棋牌', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10028|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'1', N'闲雅大长安麻将', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10032|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'2', N'闲雅大长安麻将', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10032|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'3', N'闲雅大长安麻将', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10032|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'4', N'闲雅大长安麻将', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10032|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'5', N'闲雅大长安麻将', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10032|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10032', N'6', N'闲雅大长安麻将', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10032|6')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'1', N'闲雅大丰麻将', N'1', N'一级代理', N'.60', N'.10', N'.10', N'10034|1')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'2', N'闲雅大丰麻将', N'1', N'二级代理', N'.50', N'.10', N'.00', N'10034|2')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'3', N'闲雅大丰麻将', N'1', N'三级代理', N'.40', N'.00', N'.00', N'10034|3')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'4', N'闲雅大丰麻将', N'1', N'一级推广员', N'.27', N'.00', N'.00', N'10034|4')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'5', N'闲雅大丰麻将', N'1', N'二级推广员', N'.09', N'.00', N'.00', N'10034|5')
GO
GO
INSERT INTO [dbo].[AgentLevels] ([GameId], [AgentLevelId], [GameName], [IsValid], [AgentLevelName], [DProportion], [IProportion], [IProportion2], [Id]) VALUES (N'10034', N'6', N'闲雅大丰麻将', N'1', N'三级推广员', N'.03', N'.00', N'.00', N'10034|6')
GO
GO

-- ----------------------------
-- Indexes structure for table AgentLevels
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AgentLevels
-- ----------------------------
ALTER TABLE [dbo].[AgentLevels] ADD PRIMARY KEY ([GameId], [AgentLevelId])
GO
