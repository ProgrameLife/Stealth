# Stealth
<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Stealth.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="200" height="200" />
Perform various tasks in the background！

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
1. EmailSetting UI
2. FTPSetting UI
3. SFTPSetting UI
4. FileSetting UI
5. StealthStatu UI
6. QuartzSetting UI

### appsetting.json

#### "Localization": "zh","Localization": "en"
中文UI
<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Images/SFTPSetting.zh.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="400"/>
英文UI
<img src="https://github.com/ProgrameLife/Stealth/blob/master/StealthSolution/Images/SFTPSetting.zh.png" alt="GitHub" title="Ocelot.JwtAuthorize" width="400"/>
