CREATE TABLE [dbo].[Agents] (
    [Id] [int] NOT NULL IDENTITY,
    [GameId] [int] NOT NULL,
    [GameName] [nvarchar](max),
    [UserId] [int] NOT NULL,
    [RealName] [nvarchar](max),
    [NickName] [nvarchar](max),
    [PhoneNo] [nvarchar](max),
    [ParentUserId] [int],
    [MyAgentLevel] [int],
    [AgentLevelName] [nvarchar](max),
    [AgentLevel1] [int],
    [AgentLevel2] [int],
    [AgentLevel3] [int],
    [DProportion] [decimal](18, 2),
    [IProportion] [decimal](18, 2),
    [IProportion2] [decimal](18, 2),
    [AgentCode] [nvarchar](max),
    [AgentStatus] [nvarchar](max),
    [CreateTime] [datetime],
    CONSTRAINT [PK_dbo.Agents] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Levels] (
    [AgentLevelId] [int] NOT NULL,
    [AgentLevelName] [nvarchar](max),
    [DProportion] [decimal](18, 2) NOT NULL,
    [IProportion] [decimal](18, 2) NOT NULL,
    [IProportion2] [decimal](18, 2) NOT NULL,
    [GameId] [int],
    [GameName] [nvarchar](max),
    [IsValid] [int],
    [Discriminator] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.Levels] PRIMARY KEY ([AgentLevelId])
)
CREATE TABLE [dbo].[Products] (
    [GameId] [int] NOT NULL,
    [GameName] [nvarchar](max),
    [IsValid] [int] NOT NULL,
    [Remark] [nvarchar](max),
    [ServerApiUrl] [nvarchar](max),
    [DBWanIP] [nvarchar](max),
    [DBIntranetIP] [nvarchar](max),
    [DBProt] [nvarchar](max),
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY ([GameId])
)
CREATE TABLE [dbo].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [GameId] [int] NOT NULL,
    [GameName] [nvarchar](max),
    [UserId] [int] NOT NULL,
    [AgentRealName] [nvarchar](max),
    [NickName] [nvarchar](max),
    [PhoneNo] [nvarchar](max),
    [ParentUserId] [int],
    [MyAgentLevel] [int],
    [AgentLevel1] [int],
    [AgentLevel2] [int],
    [AgentLevel3] [int],
    [BindingTime] [datetime],
    [RegisterTime] [datetime],
    [LastLoginTime] [datetime],
    [Diamond] [decimal](18, 2),
    [Coin] [decimal](18, 2),
    [TotalCharge] [decimal](18, 2),
    [TodayCharge] [decimal](18, 2),
    [TotalRound] [decimal](18, 2),
    [TodayRound] [decimal](18, 2),
    [TotalWinRound] [decimal](18, 2),
    [TodayWinRound] [decimal](18, 2),
    [FriendID] [int],
    [FriendNickName] [nvarchar](max),
    [FBindingTime] [datetime],
    [GameVersion] [nvarchar](max),
    [UserType] [nvarchar](max),
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Id])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201712060136176_20171206', N'Entity.DbHelper',  0x1F8B0800000000000400ED5C5F6FDB36107F1FB0EF20E86903DA28765EBAC2599138CD662C7F8C38ED9E198976844A94275141FCD9F6B08FB4AFB0A3FE9214658992ACA65D50A0B08EBCDF9DC8E3F1A8E3E5DFBFFF997D78F63DE30987911B90537372746C1A98D881E392CDA919D3F5DB77E6875F7FFC61F6D1F19F8DCF79BF13D60F3849746A3E52BA7D6F5991FD887D141DF9AE1D0651B0A64776E05BC809ACE9F1F12FD6646261803001CB30667731A1AE8F9307789C07C4C65B1A23EF3A70B0176574685925A8C60DF271B445363E353F0227DD99C699E72290BEC2DEDA341021014514747BFF29C22B1A0664B3DA020179F7BB2D867E6BE44538D3F97DD9BDADFAC753A6BE5532E650761CD1C0D7049C9C64E361C9EC9D46D52CC60B462C1D1FF6D6C9A89D9A678EEF02AE2CE9FDDC0B59AF7C448FCE51848F92CE6F8C94F4A69870B00BF6EF8D318F3D1A87F894E09886C87B632CE307CFB5FFC0BBFBE00B26A724F63C5E1D5008DA0402909661B0C521DDDDE175AEA46D0760130BC7342C91DD92F90BEE2A6BFA3E0B424FA6A67103AAA0070F1773CFBDFB8A0621FE0D131C228A9D25A2148730753701C115052471605F21FB954B036383B5621AD7E8F90A930D7D3C35E1A7695CBACFD8C92999069F880B4B0B986818370ABA0B3C3C9AA0E6D1DB8FB18896A1EBA37097C39C07808A4803D0CC2ACD75BF116F30A1AD8D9875FE0A46DCC57A8730DB858333A7B87F927E83FFFB4E34C318C52AD93AEBABEC1D46DE28CADEB8F69751042D1FC145DD0487978342B029E51CEC67BCDE25ABEF0A3F614F8BB1641B65204B71938E6A4E3BF29D68F15DB0E7204C638394EF02DBE06A61749721FCCA02B777A6B1B2115B198D908B83424E87C24C466C0E133A8E2DACC0C7C6D1C165CD430CBEFBDE2D4DFCA278EEBE3366CB4D637B4C38FAEE9106C3CB060CE2F0A3548D01F6A06142B5D1F6AA45F41979AED666D57A7EB5A6769059ED12BE1726D5298217B8C7B08C91379B4E4E5C37021F5786869B1F661D806827B65B9F01B2EE5F612DE41E4E7715BC7AC63A903B0C27CB2F07D77585C3271C9E6DDD4FA177789770FE27228BE5087260CC4344301D451810E8C0625A7B08765A69EB1E58DFD72F04AF5F089240E0F533C1B7F899E0659FDBCF5DC27228CD073DD576B7712358B75D78AF5044AF828D4BBA305FB8C80F88D33EB26B38EA06EE601F18EEC1C179F347146EF070900EDA0D0D095ADE05F170639828393022E8F8A74B86577368D0CBD0C5C4595C68ADBB946934477BD9639DB38D328B3C46D92B99885163B3794028722116C9FDCBC3EFD8DB2A8334D02F8BD3A24C8EF83A29E00A5331A9594ACFBE406574AB813BCD2655B9537A0377B66955B8337A0377718EADF0172D0D08699C5B614FC9AAD929E6A1CCAB5B69623D4FC05B3519F8D935DA6EC150B88C7C463156693A7EFE76A59FB3F6530CCB8E14A9EB42DB4212C4B46883A556B6BF38F8D20D230A0B0E3D2066AA73C7AF742BACAE66547331BC61C9917C39D8796FF63BE5C88E154AC672BC2EE1157CB0AEE46D7021BC4660C2C9FC240AEBB3DDF3C08B7DD2903FDF8755A6B279A892DA1EA9CC55F34825550F497EB99CD61E854B47F3401CB98A35B3A4A9928DC0AA58817492934DAA9DC1253EA78BC1A918DB189C9AAF7628A5C9D09B88FC40C923A83F9035A154CDABA4EA99BCAC4F4ED330D2E2C0289869416D8F54062A3C52496D8F541CF978A082A881231CE90430A1A53DA278D6E311C596F688F2E77BC1134A6D5D50277590936E78D33ABC4A38DB0EEFA40EEF44074FC84AF078428386D3ADC35BF4C69BD602EA8F609A52AE8C5F4AD6C4CA93C615B4BCA13D1E9F18E6E178FA8BD9B414895E8D4D4BC9DD62E7AAE16B5E2F9568694FBA721FE24BDBCD8AEC8A18E46444250E04C68ECB168E01BD622CDE1F109C801BD9102CB904C12C1CC2F41A0C6611B1DFB7EB9F32CB4954FCF95BB49AC3EC56DFAFF73EB061F1F6F4C2CDA9B258BFC63AEDB845E45F333A4C4D0D6B8BC9A9E5FC8E3D7ACD09254D998BE79394D61E45CC88F358628B86D7CAD3DE8209E7441D1C3EAD2D82F12D3A8869EE5AC44A692F665125DFF83AAC28155F8BE5A4667BFD52A0B9F7AB3F17484DAFDF0CC6FA66F0FF38DD0B89291E4F68D0D951F8ACB4B8AFF02DED11A55C350F29356978F13C852D86481951E3349EE4AE857378504D2CED431032D63C90D0A083C7A5AB453CAE4153BF2C5F5B512FA36B6AA7442BE99ABA95E9E48A7A6593A686759842537BCC3243CDC395545D24B56797DB34506B9DC065472F20E4ACE52DB668D0DB65D3BCB4BCCFA6D49123AF4AC654EE52482F32A752867496652B9B0B992BE9CBB40BBB2E113CB90E4B5DAE76E057FD23D6E168F59737F7DC243F9D77B846C45DE388A67712CDE9F1642AD545BF9C1A652B8A1CAF4DA1B23859A3170FBB6C80352F11CA25C1E409853638E49F7CF4FC338FD6A5ECB73758BF77AB14F63EB84D209A45BD1D26BCDF35DA64181A2FCA76B8F5DA6FA4E57BB3BD265EBC92D9451DF9526C2F75E4FB58BDC0A4CBADFDB0141758A5D16A03A3BACEDA01465D96D4EB0515F7657B29361D04E5A4078AA298CAD1A8416A55B532AA88E9A164542A68FB5B925825DB0BAF5A09EBC0334D9EF72169564E76892706AF66ECE2800FE00C7A2E9DAE75888794D169F1F4DFD1DB8CF6A01BBA5448D7C56F8A89225127E126F064FA6EE8AACD0EAB70983ACAAF1E89ED9FB80EF58FBDB451D538F67329621D634FAC6AAD624FC025578FA801A55773F87A8279912718656DDFEB3166CC63CCF772E65054F9B40B5655CEBC5ADDD7154B59EDD7154CAAFE1BFA30C257030E8DADA80E1C5E44A55AF0206F2154D01DE4250E2C41515D7890D738B410B9FAB083D750D722F672F4AA7AC3AE0B5E517FD87BC7E66B0C078BB7E43AC26AE197A85791E011120BB5C58269DE05C6F1816DC6A98A4953E5FEB66E21A11299353523371419AA9093A666E4657301A20A3D6B6CC6DF5B9EA842662D91DA1E5A542E56F36D338BFFF3C2B30B1CB99B12825DAA24D866DF0E4AD0BCCF82AC83DCAAE18D788DF22E72C88429820588CE42EAAE914DA1D9C6519414D76697363FFA0FD85990DB986E637A1645D87FF0840AB499B55F7E529E29EA3CBBDDB2A768885748CEA7E0436EC979EC7A4EA1F7A5E2DC5F03C15664761401AD56941D4936BB02A9FA2789EA80B2E1BBC05BF09CB050EEB1BFF5002CBA252BF484BBE806E6758537C8DEE569D37A90E68910877D0631D326447E946194FCF00836ECF8CFBFFE0771FF0B9B655B0000 , N'6.2.0-61023')
