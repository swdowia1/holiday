﻿IF object_id(N'[dbo].[FK_dbo.UserRole_dbo.Role_RoleId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
IF object_id(N'[dbo].[FK_dbo.UserRole_dbo.User_UserId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_RoleId' AND object_id = object_id(N'[dbo].[UserRole]', N'U'))
    DROP INDEX [IX_RoleId] ON [dbo].[UserRole]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_UserId' AND object_id = object_id(N'[dbo].[UserRole]', N'U'))
    DROP INDEX [IX_UserId] ON [dbo].[UserRole]
ALTER TABLE [dbo].[User] ADD [RoleApp] [int] NOT NULL DEFAULT 0
DROP TABLE [dbo].[UserRole]
DROP TABLE [dbo].[Role]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201802141206371_roleapp', N'taskApi.Migrations.Configuration',  0x1F8B0800000000000400ED59DB6EE336107D2FD07F10F4D40259CBF6BEB481BC0BD7495AA3711244C9BED3D2D82696A454924AEDFDB53EF493FA0B1DEA7EF1458ED745502C0218D168E6CC8543CE70F4CF5F7FBB1FD79C592F20150DC5C81EF4FAB605C20F032A96233BD68B773FD91F3F7CFF9D7B1DF0B5F529E77B6FF85052A891BDD23ABA741CE5AF8013D5E3D497A10A17BAE787DC2141E80CFBFD9F9DC1C00184B011CBB2DCC75868CA2179C0C749287C88744CD82C0C80A98C8E6FBC04D5BA231C54447C18D99AA8CFE388F6AEC6B7B6356694A0091EB0856D1121424D341A78F9ACC0D332144B2F4202614F9B08906F419882CCF0CB92BDAB0FFDA1F1C1290573283F563AE447020EDE6741719AE2AF0AAD5D040DC3768DE1D51BE37512BA913D495480B4ADA6B2CB099386B116D95ECE7F6165D48B62F13147CCDF853589998E258C04C45A1276613DC47346FDDF61F3147E063112316355ABD02E7C572320E941861148BD79844566EB34B02DA72EE734050BB18A4CEAC554E8F743DBBA43E564CEA058F48AC79E0E25FC0A0224D1103C10AD410A830149D85ADA1BBACC6FAE0DB30C778A6DCDC8FA16C452AF4636FE6B5B37740D414EC92C781614379609B48CE1909271104850EAEC7A1E56A138BF37534E96E7D77285CB399180BFB92A4379A266B95AF97018EA390A8E87729D72EFEDDD91784475DE8D86F7DB4E6CEBBAE684B2F36F11A2D49FA10C7E236A7576651E61FAEC4A1E4306E3282AF460F9EC15B457EC94B7BBE9625ED972858B5375C3C8B26C080EEF4283A4F2189DBC1571B502906C83AB5BDD2AF5E0CC80CF4116E58053EC0B3E1116E353BF15C93A73AC57A1A45F92E4C94406FB45D2D328E31DB6A39BC6B17DA061E7A6091505CE531A184386B5DE12595494055765B95AB72A85F540371A17AC84E51A67295B3635CE7E10E3DD3680D4EB6D9954F855B6A24EDA8BE63DABB3A3697567248A30F2952636A3585EDAC14EDE79C777783CC5707CB5A5D12BAC2D34E1B98A15B7F11655A3A537542A8D1B8BCC89C9FD49C05B6C8D55DC11DC5C5973A19A75A50C7B2E61FE4FA5B6F59C4D80328237E814C7D290F807852195F6B6259ADC210823724B999A842CE66257A9DB279DB68055F994D21DA1E8EFAA2005B13B4ED6BF55513252778CAC3BAB85232575C7A896812A50957E1C5A5E099A6839BD8DE63A8D3C6966A2D34AC54673D3CCED4E999F9E2EAFCDFAE40C3A3EE3B78B9D27DBB336AB0A90918EC8D35A13554BD7DA9BEE8869A754454A29DD118A7EA00A5210BFE5FEEEAAD86429B417D5B15105DDAC221D9EEFB44A54CA625B18A6171A98F2E46D9406DE330C3DEF0F366114FD2D196644D005289D365CF6B03F183626456F676AE32815B08EA39BFFFC02474D540F5ED18E6CF2ABD313F142A4BF22F2074ED63F56915E31213909AB36053909A936E93809A97DB1327B5F7F9D8B5547A8E3A619FF8F04AD0D154ECBAA2D83839300ABC38193801A0380248E27DFFBDF5A7A36EFA6EDCB4FB7ABE7DE9B675A9DD0E27988C6A7961E752FDD792DDD867CD285B55D825DA7FA21C6BD0245972584F92C23C037B5AD04CD79A66211E6CB880E552DCA591AAB3C034D705DC9586ABA20BEC6D73E9EDFD521C5359F433015F7B18E623D560AF89CD58680AEB35F7F722BAFDBECDE47E6497D0D17D04C6A52F35EFC1253161476DF6C49CD1D102609B3C30AADF2B439B4969B02E9AE7575DB059485EF0A2210E6A87B021E310453F7C2232FF01ADB30BB6E6149FC4DDE49ED0639BC10F5B0BB57942C25E12AC328E5CDC745C77C5DFCF02F0B88BAB78F1C0000 , N'6.2.0-61023')
