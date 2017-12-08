ALTER TABLE [dbo].[Admins] DROP CONSTRAINT [PK_dbo.Admins]
ALTER TABLE [dbo].[Admins] ADD CONSTRAINT [PK_dbo.Admins] PRIMARY KEY ([AccountId])
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Admins')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Admins] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Admins] DROP COLUMN [Id]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201712051021406_Admin', N'Entity.DbHelper',  0x1F8B0800000000000400CD57DB6EDB46107D2FD07F20F6A9011CAD2F2FAD4125B0A5B8151AD986E9E47D448EE445F6C2EE2E0DE9DBF2D04FEA2F74285E454A96650749A0176A3873E6CCEC5C96FF7DFD377CBF54327844EB84D1437632386601EAD824422F862CF3F3B7BFB3F7EF7EFD25FC90A865F0B9D23BCBF5C852BB217BF03E3DE7DCC50FA8C00D9488AD7166EE07B1511C12C34F8F8FFFE027271C0982115610847799F642E1FA0FFD1D191D63EA33905393A074A59CDE446BD4E01A14BA14621CB20F64E9572CB89002C87B8472CE02D0DA78F0C4EDFC93C3C85BA317514A0290F7AB14496F0ED261C9F9BC517F2EFDE3D39C3E6F0C2BA83873DEA803014FCECA7CF0AEF98BB2CAEA7C51C68AFCE451AFB3366417891284DBF5743E9236D7AA323AB8048783B5F25150888EEA03A7BAC87F47C128933EB338D498790BF228B8CD6652C47FE3EADE7C413DD499946D3A4488DE6D0848746B4D8AD6AFEE705E918C634335314958C037CD79D7BEB6EE9B16F14CB43F3B65C135518199C4FAEC5BB147DE58FC13355AF098DC82F768E9E8AE8DC61E818E3BAA2F9B3F55DEA8D8A857583085E547D40BFF3064F4C8822BB1C4A492940C3E6941AD4546DE667B1DDD1989DFCDD1FEEC3D8D3171B75628B0AB0AE6D2102AE83D40216FCAB55FC434163C083AA4D2C978F6174AF2BAA598E954CA7A7665D09B740BC008FD665334DE8B493228E5DB38D66C9AE9C48BF1548D31BE638E855348533ABBD65C2B2541540CB5D1DBE8F0CE5705068FDD960150B3AD3D51C9C3023B6FC93531BD12D6F9317898417E42A344F5D4EADCEFC86BE5A69DDE6EDB37D9AEB4F3E7C2A21C425B0D9B7C5D51080AB55F4783B5F31D0ED796510C12ECEE99313232537ACF147A0AAB19086DA846FA7CA4A6E3DB488DF430A46E7095ECF928ADA66E03B5C47DAC90778EAA5B04BC57059D79DF2DA9A75AB1AB527BAF5BB2D37A61D906FBEF19BDBE28545840497A1449DE13D1CA7954835C6110FD23475250BC8DC214B498A3F3C56664B4F64F3BD7969FE70AC19D4BE473EE113F78B78B3CC107EEA6EEC6D68F60E307B0BF2958BE69A3BD642BBF1AEC75B1F5F6EE4CEC03396CE7F6D7C3AB166BD1444396CC0C312E38AE5FB9172FDD7E4787BCFD7D118ED1894503917F6D688CF35669402B9D899E9B2AC514549B51A5D23981297A48283717D68B39C49E5EC7E8DCFAAAF6196496EF3635C364A26F329F66FEC2395433B9313C43FEB4FFF5CD6293737893E6FFDCB70881680A0A016FF465266452F3BEDA523C3B20F23229EFD3C48AAEAA04B758D548FD8BF52EA0327D634C5127344EEF51A592C0DC8D8EE0115FC28DFAFF232E205E55837937C8FE83D84C7B3816B0B0A05C89D1D8E7DFCC3CFF687EF73FE1CFD157660F0000 , N'6.2.0-61023')
