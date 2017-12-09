//在程序包管理器控制台中 切换至携带上下文的默认程序集 ex:Entity
1. //启用功能
	Enable-Migrations

2. //添加区别
	Add-Migration XXXXXX
3. //生成更新Script
	update-database -script
4. //运行Script执行数据库迁移
	update-database

Resharper License Server
http://xidea.online