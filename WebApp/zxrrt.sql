--/第1步**********删除所有表的外键约束*************************/  
  
DECLARE c1 cursor for  
select 'alter table ['+ object_name(parent_obj) + '] drop constraint ['+name+']; '  
from sysobjects  
where xtype = 'F'  
open c1  
declare @c1 varchar(8000)  
fetch next from c1 into @c1  
while(@@fetch_status=0)  
begin  
exec(@c1)  
fetch next from c1 into @c1  
end  
close c1  
deallocate c1  
  
--/第2步**********删除所有表*************************/  
  
   
GO  
declare @sql varchar(8000)  
while (select count(*) from sysobjects where type='U')>0  
begin  
SELECT @sql='drop table ' + name  
FROM sysobjects  
WHERE (type = 'U')  
ORDER BY 'drop table ' + name  
exec(@sql)   
end  

GO

CREATE TABLE [dbo].[Sys_WebMsg](
	[msgId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[msgKey] [nvarchar](255) NOT NULL,
	[msgValue] [ntext] NOT NULL
);

/********/
CREATE TABLE [dbo].[Sys_FileInfo](
	[fileId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[fileName] [nvarchar](255) NOT NULL,
	[extName] [nvarchar](255) NOT NULL,
	[fileType] [nvarchar](255) NOT NULL,
	[filePath] [nvarchar](255) NOT NULL,
	[uploadTime] [datetime] NOT NULL DEFAULT GETDATE()
);

/********/
CREATE TABLE [dbo].[Sys_Location](
	[locationId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cnName] [nvarchar](255) NOT NULL,
	[enName] [nvarchar](255) NOT NULL,
	[levelNo] [nvarchar](255) NOT NULL,
	[parentNo] [nvarchar](255) NOT NULL,
	[levelCnName] [nvarchar](255) NOT NULL,
	[levelEnName] [nvarchar](255) NOT NULL,
	[isLeaf] [bit] NOT NULL
);
/*
	levelEnName: Planet,Continent,Country,PoliticalArea,CustomArea,Province,City,Region
	levelCnName: 星球，洲，国，行政区域，自定义区域，省，市，区县
*/
CREATE TABLE [Sys_CustomArea](
	[areaId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cnName] [nvarchar](255) NOT NULL,
	[enName] [nvarchar](255) NOT NULL,
	[itemIndex] [int] NOT NULL
);

CREATE TABLE [Sys_AreaRelation](
	[arId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[areaId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_CustomArea]([areaId]),
	[locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[itemIndex] [int] NOT NULL
);

CREATE TABLE [Sys_Function](
	[funcId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[funcName] [nvarchar](255) NOT NULL,
	[funcNo] [nvarchar](255) NOT NULL,
	[parentNo] [nvarchar](255) NOT NULL,
	[funcUrl] [nvarchar](255) NOT NULL,
	[isLeaf] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL
);

CREATE TABLE [Sys_Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[roleName] [nvarchar](255) NOT NULL,
	[itemIndex] [int] NOT NULL
);

create table [Sys_User](
    [userId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userName] [nvarchar](255) NOT NULL,
    [userPwd] [nchar](128) NOT NULL,
    [md5Pwd] [nchar](32) NOT NULL,
    [userType] [nchar](1) NOT NULL,
    [lastLogin] [datetime] NOT NULL DEFAULT GETDATE(),
    [locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
    [isDeleted] [bit] NOT NULL,
    [isLocked] [bit] NOT NULL,
    [insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Sys_RoleFunc](
	[rfId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[roleId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Role]([roleId]),
	[funcId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Function]([funcId])
);

create table [Sys_UserRole](
	[urId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
	[roleId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Role]([roleId])
);

create table [Sys_UserFunc](
	[ufId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
	[funcId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Function]([funcId])
);

create table [Sys_Admin](
	[adminId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
    [locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
    [fullName] [nvarchar](255) NOT NULL,
    [phone] [nvarchar](255) NOT NULL,
    [email] [nvarchar](255) NOT NULL,
    [qq] [nvarchar](255) NOT NULL,
    [insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [User_Member](
	[memberId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
    [locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[fullName] [nvarchar](255) NOT NULL,
	[shortName] [nvarchar](255) NOT NULL,
	[address] [nvarchar](255) NOT NULL,
	[tel] [nvarchar](255) NOT NULL,
	[cellphone] [nvarchar](255) NOT NULL,
	[fax] [nvarchar](255) NOT NULL,
	[qq] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[transit] [nvarchar](1000),
	[logoUrl] [nvarchar](255) NOT NULL,
	[memo] [text],
	[isDeleted] [bit] NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [User_Designer](
	[designerId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
    [locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[fullName] [nvarchar](255) NOT NULL,
	[sex] [nchar](1) NOT NULL,
	[memberId] [bigint] NOT NULL FOREIGN KEY REFERENCES [User_Member]([memberId]),
	[job] [nvarchar](255) NOT NULL,
	[tel] [nvarchar](255) NOT NULL,
	[cellphone] [nvarchar](255) NOT NULL,
	[qq] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[photoUrl] [nvarchar](255) NOT NULL,
	[memo] [text],
	[isDeleted] [bit] NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [User_Client](
	[clientId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_User]([userId]),
    [locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[fullName] [nvarchar](255) NOT NULL,
	[sex] [nchar](1) NOT NULL,
	[address] [nvarchar](255) NOT NULL,
	[phone] [nvarchar](255) NOT NULL,
	[qq] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

/****装修参数****/
create table [Renovation_Parameter](
	[paramId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[paramName] [nvarchar](255) NOT NULL,
	[paramKey] [nvarchar](255) NOT NULL,
	[paramValue] [nvarchar](255) NOT NULL,
	[itemIndex] [int] NOT NULL	
);
/*
	paramKey: PriceLevel,RoomType,HouseType,Space,Style,Type,TimeTable,ServiceItem
	paramName: 预算，套型，户型，空间，装修风格，装修方式，开工时间，服务项目
*/

CREATE TABLE [Renovation_Building](
  	[buildingId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
  	[buildingsName] [nvarchar](255) NOT NULL,
	[cityId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
  	[regionId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
 	[itemIndex] [int] NOT NULL
);

create table [Renovation_Process](
	[processId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[processName] [nvarchar](255) NOT NULL,
	[processNo] [nvarchar](255) NOT NULL,
	[parentNo] [nvarchar](255) NOT NULL,
	[isLeaf] [bit] NOT NULL
);

create table [Renovation_Project](
	[projectId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[memberId] [bigint] not null FOREIGN KEY REFERENCES [User_Member]([memberId]),
	[designerId] [bigint] not null FOREIGN KEY REFERENCES [User_Designer]([designerId]),
	[cityId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[regionId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[buildingId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Building]([buildingId]),
	[pName] [nvarchar](255) NOT NULL,
	[clientId] [bigint] NOT NULL FOREIGN KEY REFERENCES [User_Client]([clientId]),
	[isClosed] [bit] not null,
	[startTime] [datetime],
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Renovation_ProjectParam](
	[pptId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[projectId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Project]([projectId]),
	[paramId] [int] NOT NULL FOREIGN KEY REFERENCES [Renovation_Parameter]([paramId])
);

create table [Renovation_ProjectPic](
	[ppcId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[projectId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Project]([projectId]),
	[fileId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Sys_FileInfo]([fileId]),
	[memo] [text],
	[itemIndex] [int] NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Renovation_Diary](
	[diaryId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[locationId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[clientId] [bigint] NOT NULL FOREIGN KEY REFERENCES [User_Client]([clientId]),
	[memberId] [bigint] NOT NULL FOREIGN KEY REFERENCES [User_Member]([memberId]),
	[processId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Process]([processId]),
	[projectId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Project]([projectId]),
	[longTitle] [nvarchar](255) NOT NULL,
	[titleColor] [nchar](7) NOT NULL,
	[shortTitle] [nvarchar](255) NOT NULL,
	[content] [text],
	[keywords] [nvarchar](255) NOT NULL,
	[picUrl] [nvarchar](255) NOT NULL,
	[readCount] [bigint] NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Renovation_Article](
	[raId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[processId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Renovation_Process]([processId]),
	[longTitle] [nvarchar](255) NOT NULL,
	[titleColor] [nchar](7) NOT NULL,
	[shortTitle] [nvarchar](255) NOT NULL,
	[content] [text],
	[keywords] [nvarchar](255) NOT NULL,
	[picUrl] [nvarchar](255) NOT NULL,
	[readCount] [bigint] NOT NULL,
	[fileIds] [nvarchar](500),
	[isTop] [bit] NOT NULL,
	[topTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[itemIndex] [int] NOT NULL,
	[outLink] [nvarchar](500) NOT NULL,
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Info_Activity](
	[actId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cityId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[actName] [nvarchar](255) NOT NULL,
	[titleColor] [nchar](7) NOT NULL,
	[startTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[endTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[publicAdpic] [nvarchar](255) NOT NULL,
	[content] [text],
	[address] [nvarchar](255) NOT NULL,
	[phone] [nvarchar](255) NOT NULL,
	[qq] [nvarchar](20) NOT NULL,
	[keywords] [nvarchar](255) NOT NULL,
	[readCount] [int] NOT NULL,
	[isClosed] [bit] NOT null,
	[itemIndex] [int] NOT NULL
);

create table [Info_Category](
	[cateId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cityId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[cateName] [nvarchar](255) NOT NULL,
	[cateNo] [nvarchar](255) NOT NULL,
	[parentNo] [nvarchar](255) NOT NULL,
	[isLeaf] [bit] NOT NULL
);

create table [Info_News](
	[newsId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cityId] [int] NOT NULL FOREIGN KEY REFERENCES [Sys_Location]([locationId]),
	[longTitle] [nvarchar](255) NOT NULL,
	[titleColor] [nchar](7) NOT NULL,
	[shortTitle] [nvarchar](255) NOT NULL,
	[content] [text],
	[fileIds] [nvarchar](500),
	[keywords] [nvarchar](255) NOT NULL,
	[picUrl] [nvarchar](255) NOT NULL,
	[readCount] [bigint] NOT NULL,
	[itemIndex] [int] NOT NULL,
	[outLink] [nvarchar](500) NOT NULL,
	[isTop] [bit] NOT NULL,
	[topTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[insertTime] [datetime] NOT NULL DEFAULT GETDATE(),
	[updateTime] [datetime] NOT NULL DEFAULT GETDATE()
);

create table [Info_Relationship](
	[irId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cateId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Info_Category]([cateId]),
	[newsId] [bigint] NOT NULL FOREIGN KEY REFERENCES [Info_News]([newsId])
);

GO

INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('memo', '一、网站简介 装修人人通是装修装饰行业垂直性服务平台，是实用性最强的装修服务网站。汇集海量装修信息，提供各大品牌装修公司、独立设计师、优秀施工队三种选择方式，同时提供建材企业信息等。拥有明显的区域优势，能更全面更深入更有效地为业主提供针对性强的理想信息及服务。装修人人通真正做到以业主为服务中心，为业主提供免费装修装饰招标信息发布，百分百人工审核，支持线上沟通。       二、网站特色       装修人人通致力于为装修业主提供真实可靠的高口碑装修公司，功能性强，全心全意为广大业主提供装修一条龙服务。网站具有以下特色：特色一：以业主为服务中心，竭诚提供装修一条龙服务        免费发布装修装饰招标信息，提供验房、量房、设计、施工、验收、购买建材等装修相关的一条龙服务。 特色二：为装修及建材公司等提供更低成本更高效果的宣传服务 以更低的营销成本，更佳的形象展示，将优秀的装修装饰公司、优质的建材产品推荐给广大业主。        特色三：严格把关装修公司或队伍，建材企业的网站入驻加盟 坚决杜绝差口碑，乱收费，差做工，假宣传的公司或个人，从源头上解决服务差的团队，为广大业主把好装修的第一关。        特色四：提供让人放心的第三方保障服务 专业行业资讯，准确对接供应商和第三方保障服务，让业主能更轻松更方便地知道装修，找到适合的装饰公司和建材商品，更重要的是业主能额外享受我们第三方免费提供的保障服务。        三、网站宗旨       装修人人通主要提供装修相关的行业资讯，装修招标，装修知识，装修图片，装修公司或团队，设计团队，建材团购，免费验房，装修监理等服务。网站以一心一意为广大业主提供一条龙服务，让菜鸟业主轻松应对潜规则遍布、价格紊乱、工期不明朗的装修问题为网站服务立足宗旨，让装修人人通成为装饰家居中最大的垂直商业领域的佼佼者。');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('fullName', '装修人人通');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('shortName', '装修人人通');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('keywords', '宁波装修，宁波办公室装修，宁波厂房装修，宁波装饰，宁波装修公司，宁波装修网，宁波装修公司排名，宁波哪家装修公司好，宁波家庭装修，宁波二手房装修，宁波精装修楼盘，宁波店面装修……');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('webUrl', 'www.zxrrt.com');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('phone', '');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('webNo', '浙ICP备14024496号');
INSERT INTO [Sys_WebMsg]([msgKey], [msgValue]) VALUES ('address', '浙江省宁波市海曙区中山西路11号海曙大厦10楼');

/*
	levelEnName: Continent,Country,PoliticalArea,CustomArea,Province,City,Region
	levelCnName: 洲，国，行政区域，自定义区域，省，市，区县
*/
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('地球', 'Earth', '001', '0', '星球', 'Planet', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('亚洲', 'Asia', '001001', '001', '洲', 'Continent', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('中国', 'China', '001001001', '001001', '国家或地区', 'Country', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('华东', 'EastChina', '001001001001', '001001001', '行政区域', 'PoliticalArea', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('浙江', 'Zhejiang', '001001001001001', '001001001001', '行政省', 'Province', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('宁波', 'Ningbo', '001001001001001001', '001001001001001', '行政市', 'City', 0);

INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('海曙', 'Haishu', '001001001001001001001', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('江东', 'Jiangdong', '001001001001001001002', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('江北', 'Jiangbei', '001001001001001001003', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('鄞州', 'Yinzhou', '001001001001001001004', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('北仑', 'Beilun', '001001001001001001005', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('镇海', 'Zhenhai', '001001001001001001006', '001001001001001001', '行政区县', 'Region', 1);
INSERT INTO [Sys_Location] ([cnName], [enName], [levelNo], [parentNo], [levelCnName], [levelEnName], [isLeaf]) VALUES ('高新区', 'Gaoxinqu', '001001001001001001007', '001001001001001001', '行政区县', 'Region', 1);

GO

INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('根管理员', 1);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('系统管理员', 2);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('地区管理员', 3);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('市级管理员', 4);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('市级编辑', 5);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('装修公司', 6);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('设计师', 7);
INSERT INTO [Sys_Role] ([roleName], [itemIndex]) VALUES ('装修业主', 8);
GO

INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('root', '7b6d6976e6cabfad25457b25b3b02d8146ff6db198852f7100490243d821bed442b3376ceb8e2990500eb3d5eb53d877156e28bf4f1fad152110b0ae5598ec70', '63a9f0ea7bb98050796b649e85481845', 'A', 1, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('admin', '3add0020a01435d46885dd21be5ec712b92745e6bc342339fea6ae9dba23b6da317cd78015e10b7751ac0ca24ac7ea6f87666230d81c735590b9d6333c08ee22', '21232f297a57a5a743894a0e4a801fc3','A', 3, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('nb@zxrrt.com', '8002b7685dbaef4ea39e490b393c75b75e494e22e6752ed408c61d51e1ba09c689016fdac118d4d7a1a623055096f57789fa02b90ab4c339288c4ef014b6c00a','e10adc3949ba59abbe56e057f20f883e', 'A', 6, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('editor001@nb.zxrrt.com', '8002b7685dbaef4ea39e490b393c75b75e494e22e6752ed408c61d51e1ba09c689016fdac118d4d7a1a623055096f57789fa02b90ab4c339288c4ef014b6c00a', 'e10adc3949ba59abbe56e057f20f883e','A', 6, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('member', '8002b7685dbaef4ea39e490b393c75b75e494e22e6752ed408c61d51e1ba09c689016fdac118d4d7a1a623055096f57789fa02b90ab4c339288c4ef014b6c00a', 'e10adc3949ba59abbe56e057f20f883e','M', 6, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('designer', '8002b7685dbaef4ea39e490b393c75b75e494e22e6752ed408c61d51e1ba09c689016fdac118d4d7a1a623055096f57789fa02b90ab4c339288c4ef014b6c00a','e10adc3949ba59abbe56e057f20f883e', 'D', 6, 0, 0);
INSERT INTO [Sys_User] ([userName], [userPwd], [md5Pwd], [userType] ,[locationId], [isDeleted], [isLocked]) VALUES ('client', '8002b7685dbaef4ea39e490b393c75b75e494e22e6752ed408c61d51e1ba09c689016fdac118d4d7a1a623055096f57789fa02b90ab4c339288c4ef014b6c00a','e10adc3949ba59abbe56e057f20f883e', 'C', 6, 0, 0);
GO

INSERT INTO [Sys_UserRole]([userId], [roleId])VALUES (1, 1);
INSERT INTO [Sys_UserRole]([userId], [roleId])VALUES (2, 2);
INSERT INTO [Sys_UserRole]([userId], [roleId])VALUES (3, 3);
INSERT INTO [Sys_UserRole]([userId], [roleId])VALUES (4, 4);
GO

INSERT INTO [Sys_Admin] ([userId], [locationId], [fullName], [phone], [email], [qq])VALUES (1, 1, '根管理员', '13857861942', '23586037@qq.com', '23586037');
INSERT INTO [Sys_Admin] ([userId], [locationId], [fullName], [phone], [email], [qq])VALUES (2, 3, '系统管理员', '13857861942', '23586037@qq.com', '23586037');
INSERT INTO [Sys_Admin] ([userId], [locationId], [fullName], [phone], [email], [qq])VALUES (3, 5, '区域管理员', '13857861942', '23586037@qq.com', '23586037');
INSERT INTO [Sys_Admin] ([userId], [locationId], [fullName], [phone], [email], [qq])VALUES (4, 5, '网站编辑', '13857861942', '23586037@qq.com', '23586037');
GO

INSERT INTO [User_Member] ([userId], [locationId], [fullName], [shortName], [address], [tel], [cellphone], [fax], [qq], [email], [logoUrl], [memo], [isDeleted]) VALUES (5, 6, '装修公司', '装修公司', '地址', '电话', '手机', '传真', '35246464', 'email@123.com', 'logoUrl', 'memo,memo', 0);
INSERT INTO [User_Designer] ([userId], [locationId], [fullName], [sex], [memberId], [job], [tel], [cellphone], [qq], [email], [photoUrl], [memo], [isDeleted]) VALUES (6, 6, '设计师', '女', 1, '高级设计师', '28829089', '13453536565', '35246464', '123@123.cn', 'photoUrl', 'memo,memo', 0);
INSERT INTO [User_Client] ([userId], [locationId], [fullName], [sex], [address], [phone], [qq], [email], [isDeleted]) VALUES (7, 6, '装修业主', '女', '地址', '电话', '352464640', '123@123.cn', 0);
GO

INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('资讯活动', '001', '0', '', 0, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('信息管理', '002', '0', '', 0, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('账户项目', '003', '0', '', 0, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('系统设置', '004', '0', '', 0, 0);

INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('活动管理', '001001', '001', 'info/activity/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('资讯管理', '001002', '001', 'info/news/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('资讯分类', '001003', '001', 'info/category/View.aspx', 1, 0);

INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('装修流程', '002001', '002', 'renovation/process/View.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('装修参数', '002002', '002', 'renovation/parameter/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('装修知识', '002003', '002', 'renovation/article/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('装修日志', '002004', '002', 'renovation/diary/List.aspx', 1, 0);

INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('公司管理', '003001', '003', 'user/member/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('设计师管理', '003002', '003', 'user/designer/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('业主管理', '003003', '003', 'user/client/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('项目管理', '003004', '003', 'renovation/project/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('楼盘管理', '003005', '003', 'renovation/building/List.aspx', 1, 0);

INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('用户列表', '004001', '004', 'sys/admin/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('角色管理', '004002', '004', 'sys/role/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('功能列表', '004003', '004', 'sys/function/View.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('用户角色分配', '004004', '004', 'sys/userrole/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('角色功能分配', '004005', '004', 'sys/rolefunc/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('用户功能分配', '004006', '004', 'sys/userfunc/List.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('地域管理', '004007', '004', 'sys/location/View.aspx', 1, 0);
INSERT INTO [Sys_Function] ([funcName], [funcNo], [parentNo], [funcUrl], [isLeaf], [isDeleted]) VALUES ('基本信息', '004008', '004', 'sys/webmsg/View.aspx', 1, 0);
GO

INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 1);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 2);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 3);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 4);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 5);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 6);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 7);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 8);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 9);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 10);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 11);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 12);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 13);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 14);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 15);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 16);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 17);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 18);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 19);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 20);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 21);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 22);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 23);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (1, 24);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 1);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 2);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 3);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 4);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 5);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 6);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 7);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 8);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 9);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 10);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 11);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 12);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 13);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 14);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 15);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 16);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 17);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 18);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 19);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 20);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 21);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 22);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 23);
INSERT INTO [Sys_RoleFunc]([roleId], [funcId]) VALUES (2, 24);
GO

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('全部流程', '001', '0', 0);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('收房准备中', '001001', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('装修准备中', '001002', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('拆改/隐蔽工程', '001003', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('泥瓦工程', '001004', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('木工工程', '001005', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('油漆工程', '001006', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('安装/收尾工程', '001007', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('软装进行中', '001008', '001', 0);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('即将入住', '001009', '001', 0);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('收房小常识', '001001001', '001001', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('交房流程', '001001002', '001001', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('相关法规文件', '001001003', '001001', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('装修小常识', '001002001', '001002', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('定设计/装修方案', '001002002', '001002', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('配置资金预算', '001002003', '001002', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('选择装修公司', '001002004', '001002', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('签订装修合同', '001002005', '001002', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('相关法规文件', '001002006', '001002', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('结构拆改', '001003001', '001003', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('水管', '001003002', '001003', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('强弱电/开关插座', '001003003', '001003', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('拆改施工验收', '001003004', '001003', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('瓷砖', '001004001', '001004', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('石材', '001004002', '001004', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('泥瓦施工验收', '001004003', '001004', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('板材', '001005001', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('龙骨', '001005002', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('顶角/踢脚线', '001005003', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('石膏制品', '001005004', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('胶黏剂/胶水', '001005005', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('铝合金/不锈钢', '001005006', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('玻璃', '001005007', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('铁艺制品', '001005008', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('扣板', '001005009', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('门窗', '001005010', '001005', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('木工施工验收', '001005011', '001005', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('壁纸/壁布', '001006001', '001006', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('涂料/油漆', '001006002', '001006', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('油漆施工验收', '001006003', '001006', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('木地板', '001007001', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('地毯', '001007002', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('灯具', '001007003', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('洁具', '001007004', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('龙头五金配件', '001007005', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('家电', '001007006', '001007', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('安装施工验收', '001007007', '001007', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('家具', '001008001', '001008', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('布艺', '001008002', '001008', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('壁饰/工艺品', '001008003', '001008', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('花卉', '001008004', '001008', 1);

INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('环保检测', '001009001', '001009', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('搬家搬场', '001009001', '001009', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('保洁', '001009001', '001009', 1);
INSERT INTO [Renovation_Process] ([processName], [processNo], [parentNo], [isLeaf]) VALUES ('装修风水', '001009001', '001009', 1);
GO
/*
	paramKey: PriceLevel,RoomType,HouseType,Space,Style,Type,TimeTable,ServiceItem,PartItem
	paramName: 预算，套型，户型，空间，装修风格，装修方式，开工时间，服务项目，局部位置
*/

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '2万以下', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '2-3万', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '3-4万', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '4-6万', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '6-8万', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '8-10万', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '10-12万', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '12-15万', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '15-20万', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '20-30万', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '30-50万', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '50-100万', 120);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '预算', 'PriceLevel', '100万以上', 130);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '3室2厅2卫', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '2室2厅1卫', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '3室2厅1卫', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '4室2厅2卫', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '2室1厅1卫', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '3室1厅1卫', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '3室1厅2卫', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '1室1厅1卫', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '2室2厅2卫', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '4室2厅3卫', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '5室3厅3卫', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '套型', 'RoomType', '其他', 120);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '普通住宅', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '复式住宅', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '跃层住宅', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '错层住宅', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '公寓式住宅', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '独栋别墅', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '联排别墅', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '工厂厂房', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '酒店宾馆', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '商业店铺', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '娱乐场所', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '楼梯', 120);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '户型', 'HouseType', '其他', 130);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '客厅', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '卧室', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '厨房', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '阳台', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '玄关', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '餐厅', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '书房', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '吧台', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '花园', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '儿童房', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '卫生间', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '空间', 'Space', '其他', 120);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '现代', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '现代前卫', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '雅致主义', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '中式', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '新古典', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '欧式', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '简欧', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '美式', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '地中海', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '田园', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '混搭', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '北欧', 120);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '日式', 130);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '东南亚', 140);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '法式', 150);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '简单', 160);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '古典', 170);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '经典', 180);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '简约', 190);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '新中式', 200);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '后现代', 210);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '美式乡村', 220);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '柔和', 230);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修风格', 'Style', '其他', 240);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修方式', 'Type', '全包', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '装修方式', 'Type', '半包', 20);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '本月内开工', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '一个月前开工', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '二个月前开工', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '三个月前开工', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '四个月前开工', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '开工时间', 'TimeTable', '更早', 60);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '家装', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '公装', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '店面装修', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '品牌展示', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '楼梯外观', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '服务项目', 'ServiceItem', '店面外观', 60);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '隐形门', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '推拉门', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '榻榻米', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '门厅', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '窗台', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '博古架', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '背景墙', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '吊顶', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '窗帘', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '楼梯', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '走廊', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '隔断', 120);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '衣帽间', 130);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '阁楼', 140);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '飘窗', 150);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '局部位置', 'PartItem', '照片墙', 160);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '办公空间', 'Office', '会议室', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '办公空间', 'Office', '写字楼', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '办公空间', 'Office', '办公室', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '办公空间', 'Office', '公司', 40);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '娱乐场所', 'Entertainment', '网吧', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '娱乐场所', 'Entertainment', '酒吧', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '娱乐场所', 'Entertainment', 'KTV', 30);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '饭店', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '快餐店', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '餐馆', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '烧烤店', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '食品店', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '火锅店', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '茶楼', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '咖啡馆', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '餐饮空间', 'Catering', '饮品店', 90);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '酒店', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '宾馆', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '旅馆', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '超市', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '商场', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '学校', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '美容', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '足浴', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '医院', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '商业空间', 'Commercial', '幼儿园', 100);

INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '珠宝', 10);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '花店', 20);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '药店', 30);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '化妆品', 40);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '数码店', 50);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '饰品', 60);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '汽车', 70);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '建材店', 80);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '家居', 90);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '便利', 100);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '服装', 110);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '鞋店', 120);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '母婴', 130);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '美发', 140);
INSERT INTO [Renovation_Parameter] ([locationId], [paramName], [paramKey], [paramValue], [itemIndex]) VALUES (5, '店面装修', 'Storefront', '美甲', 150);
GO

INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '全部类目', '001', '0', 0);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '软装家饰', '001001', '001', 1);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '家居风水', '001002', '001', 1);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '装修技巧', '001003', '001', 1);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '行业动态', '001004', '001', 1);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '样板美图', '001005', '001', 1);
INSERT INTO [Info_Category] ([cityId], [cateName], [cateNo], [parentNo], [isLeaf]) VALUES (5, '人才招聘', '001006', '001', 1);
go