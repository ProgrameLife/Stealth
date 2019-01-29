# Stealth
<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Images/Stealth.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="200" height="200" />
Stealth是一个使用Quartz.net作为后台任务框架的后来任务项目，系统开发了自动发EMail,FTP,SFTP,生成本地文件几个功能，用户也可以自己来继承IBackHandle来处理其他后台任务，IProvider是负责把各种配置文件存放PostgreSql或Sql Server，或自己实现IProvider的数据库中；IDataBuilder是用来提供EMail或FTP或SFTP的内容的接口，所有这些都在StealthGirder的Startup中注入。

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ProgrameLife/Stealth/blob/master/License)

## Function of Complete
##### BackHandle
BackHandle是处理后台操作的具体操作，如果是Email就负责发送邮件，FTP,SFTP负责上传文件，File负责生成本地文件；关于这些BackHandle的配置文件，是Provider提供。
1. EmailBackHandle
2. FTPBackHandle
3. SFTPBackHandle
4. FileBackHandle

##### PostgreProvider
PostgreProvider是负责各BackHandle和UI操作PostgreSql中配置数据的类库。
1. EmailProvider
2. FTPProvider
3. SFTPProvider
4. FileProvider
5. StatuProvider

##### SqlServerProvider
SqlServerProvider是负责各BackHandle和UI操作SqlServer中配置数据的类库。
1. EmailProvider
2. FTPProvider
3. SFTPProvider
4. FileProvider
5. StatuProvider

##### UI
UI完成配置文件的添加，修改，删除，包括Quartz的配置
运行StealthGirder项目
1. EmailSetting UI  http://localhost:5000/emailsettings
2. FTPSetting UI http://localhost:5000/sftpsettings
3. SFTPSetting UI http://localhost:5000/sftpsettings
4. FileSetting UI http://localhost:5000/filesettings
5. StealthStatu UI http://localhost:5000/stealthstatu
6. QuartzSetting UI http://localhost:5000/quartzsettings

##### 通过实现StealthBuildData的IBuildData接口来获取后台任务执行的数据

### appsetting.json

#### 通过配置文件切换UI本地化
##### 中文UI  "Localization": "zh",
<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Images/SFTPSetting.zh.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="600"/>



##### 英文UI "Localization": "en"

<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Images/SFTPSetting.en.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="600"/>
