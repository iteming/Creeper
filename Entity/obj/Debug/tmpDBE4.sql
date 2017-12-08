CREATE TABLE [dbo].[Charges] (
    [Id] [int] NOT NULL IDENTITY,
    [GameId] [int] NOT NULL,
    [GameName] [nvarchar](max),
    [SourceOrderId] [nvarchar](max),
    [OrderId] [nvarchar](max),
    [PaymentId] [nvarchar](max),
    [OrderState] [int] NOT NULL,
    [IsReceive] [int] NOT NULL,
    [ChargeUserId] [int] NOT NULL,
    [NickName] [nvarchar](max),
    [UserId] [int] NOT NULL,
    [RealName] [nvarchar](max),
    [RebateAmount] [decimal](18, 2) NOT NULL,
    [ChargeAmount] [decimal](18, 2) NOT NULL,
    [DProportion] [decimal](18, 2) NOT NULL,
    [Writedate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Charges] PRIMARY KEY ([Id])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201712060628516_2017120614', N'Entity.DbHelper',  0x1F8B0800000000000400ED5CCD6EE33610BE17E83B083AB5C06E143B9776E16C91389BD6687E8C78777BA625C61156A25C890AE267EBA18FD457E850BF244545D48F656FBB2850C424E79B2135331C7238FBCF5F7FCF7E79F13DE31987911B90737372726A1A98D881E392CDB919D3C7B73F99BFBCFFFEBBD907C77F313EE7E3CED838A024D1B9F944E9F69D6545F613F65174E2BB761844C1233DB103DF424E604D4F4F7FB626130B0384095886317B8809757D9CFC809FF380D8784B63E4DD060EF6A2AC1D7A5609AA71877C1C6D918DCFCD0F404977A671E1B908B8AFB0F7681A889080220AB2BDFB14E1150D03B2596DA101791F775B0CE31E9117E14CE677E5705DF14FA74C7CAB24CCA1EC38A281DF12707296AD872593775A55B3582F58B1747DD8AC93553B372F1CDF055C99D3BBB917B251F98A9E5CA2089F2483DF1869D39BE283835EB0FFDE18F3D8A37188CF098E6988BC37C6325E7BAEFD3BDE7D0CBE60724E62CFE3C50181A04F6880A665186C7148770FF83117D2B603D08985631A96486EC9F4057595349DCF82D0B3A969DC812868EDE1E2DB73735FD120C4BF62824344B1B34494E2103EDD5D40704500891DE857C8FECAB981B281AD98C62D7AB9C164439FCE4DF8D334AEDD17ECE42D99049F880BA60544348C1B193D041E1E8D51F3EABD8EB18896A1EBA37097C35C06808A4803D0CC2AD5F57525DE6042B595980D3E8012FF0AFFAF6AB07228D3A22ECA9EB3184BD3C7E2C5E6358EB263E48DC2E8CEB5BF8CC268F904EB7B17EC9DCFED2EB1AB1BFC8C3D49295E272CC9C6590F14023FA5F2EACA39E948376D4F378779ED7D49124E2BB0D238DA3BAF7988C1FA3FBAE597BE2A7E7777FD99D6B5F0FF09C5716F02A59CC7BE158892FEA7368445F419796ECFF067641777C57E07617A60C88C0CDB107F81892C43F82B3BCDFD641A2B1BB1597488E8C6E5311D8889B65B993FA17083755D4A3AFA00EEA48B6718C246170ECE0EDACD76DAF7F0309AADAF8238B4F17DE8F0B1C1DEB88DC56789763EE60FC0FB9D118B2370DFD3E203B6B1FBDC1326354ABD63CA9104EB43083BDA11E601AFE1535FF8EC7A656F7B40FA11F7CC648C0DF38FD00507CAD94619F70EB363013F27B6B56F41B2E14713021F5744FB75C5970FD847E197FDEF8F387CC6E1C5D6FD147AFB0F622FFF4064B11C810FAC798808A6A33083063A301B6D0FC176175DF7C0C61E8D6FF876477A4C2EEBDBD525FF79D9F01142AD8D1B812A34DF9B55696F50446F828D4BBA10F7BB2D1D2D14BD74094B497799E175E862E22CAE5ACD2E251ACD10AE7BCCEF20D7D609DDD92B74AFEF59F38050E48213CCB7CDF56FD8DB2A372F50CD6CFF8AB2B514A54B0157988AE9EE927BFA46E0246BB71AA8D33C63953A6DD7A1CEBC8C1A22EB6CC0C9EFA62A187947037D7152A800143D0D08692451214F9B55DFB9F8A2E5DB0D2B7DBC913FF2B06A5E79CC6ED1760BBACFBDFAC85A8C55FAE463FE76D5FE5D849F625876A4781E51485B70826D196DB0D40BAC41D26B378C2858235A2366D873C7AF0C2BF4B766557336BC8ACA0150B9D8F968F6774A91056E4AC272BDAE610AEC2628990D2E98D7304C28D9691785F52F2AE68117FBA4E18DC66B58E573091EAA6CD5472ADF43F048656B3B247972799B3E0AF7E48107E29AAB58334BFA54B21258152D90825F59A5F4142EF15E5D144E45A8A3706ABABAA5CC23767E1DEB0E0AF5287910232B5A3B9432CA96A569AD66457024285AD1AA8F54C6213C52D9AA8F5444DC3C50D1A88F2346D43C98D8A38F28A7D004CF23F5B598AF10DE0A93167ABAC839A91372D20D6F5A875709C91AF1D2570515B4B4B92556FE6EA0829677E8E3F16F037838BEFDB89C66AAC65D3DA7925AD77DD610EFD7878A897EB53A1ECA9F1637A9E2769B351EDAD30829061E52E86831DB3ABC456FBC692DA0D2D31CC8FEB2B34E07DB53536AD85D1D61ED9ACACAD8DA32FA5BEC70F62565C77938A94B1F5389D60187CB718B9B78D1DC52A62C8B5D112B6B6FE3958A5CB6E8978AE616FBA390D0167648A1E71091E330D1F57031B1989C16D1F89EB6ABAF42147B0EB7277029661E8D6B3E1AEF9D5F347570DF35A41AFEBB96F26B71C143853879BE58348BB4ADC58620A48385FD40E869611079CE573086BCB10D0E9FD315C1F89E368869E256C44ADB8EC6A892EBD70E16A5A2D3302735D9FFEB12E7FF70F5526639E5554F5BDBF81D3E8D297A1FBE471F514A6EF29052D7C12F85D4B18DD4A58F29A4047944A1431FAFCC83F260656B5B24B565C87D2D506B277CDD71C65FC5555D9E4955E39D8DBEFB54127AF290827B91D8931278B32C99D65CCB5DC9AEA543D89BCCE0D97558666DB503AFE19FB00127AB3FBDB9E72689D87CC02D22EE238E68FA28C99C9E4EA65269F8F194695B51E4783AB5DAE2C71ABD7EDA650BDCF281A25C154D9E5168C3A9E9071FBDFCC8A375A97CEE0DD66F6E95DAE6B5DB04D2B2AEB9C3071FFD1D5D57ADE88720BF79EBA70AD26BA55E60F2DBA05E60D243B85E58AAC76ED2D26BD78C564ADAFACD52F1D6AC9764934150A67D51F81ADE5E0BA4A8D3ED8557ADC565B74454AA49E8F1E0B82E5175D4A5B25DFC90AA00F6E0FE4C2A2DE837AFC1A452D4DA382314A7EE93C77420266D8B533BD854BF7AD144851A2B423BE8FC11598DB208B417E290589562CEFE9209059BDD0260A95CB30B88AA58B30BCEA071577F71068D2955059783FB3555C1E5E04CC6D8042A0597AAE0668882CBC1429B312295638B30C4D2C57E9E5B519ED82F54114B107B6255CB0C7B022EB952C21650EDCA05BFDD3D8C6249FFF5EB02B952AEE73E58AD86D33BB956B194D5715DC186BCC118347250D48F759DA35C2FD7617EEAEAB95E135455C8759DE1D15D2089D5748D28ED6AEBAA254CA24072E95743015D9AA281C55F33FF938A9874555E82B72DAE5322B32E4D649DC2BB5A1E497F33A3C6CA3C1583B4AF197CD95CB6A782CF3A9BF15F2DEA5321B39E48AD7B1AF57ED534E0CCE2FFE1E7D9158EDC4D09C1FE1968826D76662841F3310BF218E4A60333E225CA87C81B1BA6083C04BA8053C823B22974DB388A92025E886A6318F2C15F636741EE63BA8DE94514617FED09755B33EB75FE4951A328F3EC7ECB7E45434C2109BEC1C9DD93CBD8F59C42EE6BC5A1A60682597F76970352AD28BBD3D9EC0AA4EABF3B5007942DDF15DE826B0783F988FDAD0760D13D59217635D05E3650AF1BBC41F62ECFE6D683347F0871D967572EDA84C88F328C921E7E820E3BFECBFB7F015CB21731FF5C0000 , N'6.2.0-61023')
