# ��������

#### 1��[EF Code First��֧����SQL Server�еı�/�ֶε�˵��](https://www.cnblogs.com/tanglang/p/4798211.html)
> ��������������ͼ��ʾ��
![](./docs/1319833657.png)

#### 2��[ʹ��T4ģ�屨�������ڱ���ת������ǰ�������в���������Host��](https://www.cnblogs.com/xiaoxiangfeizi/p/3572295.html)
![](./docs/272022528694589.png)
> �����������ͼ�е�False�ĳ�True�ͺ��ˣ�
![](./docs/272022531258132.png)

#### 3��[MySQL :: Download Connector/NET ](https://dev.mysql.com/downloads/connector/net/)
> ��װMySQL For Visual Studio��Connector/NET��
![](./docs/20190619211838.png)

#### 4��[ No MigrationSqlGenerator found for provider 'MySql.Data.MySqlClient'](https://www.cnblogs.com/likeli/p/5775719.html)
> ���������
��Ҫ��Contextָ��Mysql�������ļ���
[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

#### 5��[ʹ��WEBAPI���ӵ�MYSQL��������](https://blog.csdn.net/csdn102347501/article/details/79398863)

#### 6���ٷ� [EF6 Code First Migrations for Multiple Models](https://msdn.microsoft.com/en-us/magazine/dn948104.aspx)

# Code First �������Ĳ���
> Ĭ������£�
* `Enable-Migrations`
* `Update-Database`
> SqlServer �� MySql �������������ã�
* `Enable-Migrations -ContextTypeName DBDescriptionUpdater.Models.DataContext -MigrationsDirectory SqlServerMigrations`
* `Enable-Migrations -ContextTypeName DBDescriptionUpdater.Models.MySqlDataContext -MigrationsDirectory MySqlMigrations`
* Ϊ�˱����������������ĵ����ã��ɽ�����`Configuration`�����Ʒֱ��Ϊ`SqlServerConfig`��`MySqlConfig`
* ��λ�Ҫע�� entityFramework �ڵ��� `provider` ������
* `Add-Migration Initial -ConfigurationTypeName SqlServerConfig`
* `Add-Migration Initial -ConfigurationTypeName MySqlConfig`
* `Update-Database -ConfigurationTypeName SqlServerConfig`
* `Update-Database -ConfigurationTypeName MySqlConfig` 
