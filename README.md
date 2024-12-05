# LLOneBot.Net 链接框架
## 接口协议标准（基于onebot-11):
https://github.com/botuniverse/onebot-11/blob/master/api/public.md

# 调用教程
## 第一步:使用nuget管理器安装LLOneBot.Net
对应的nuget地址为:https://www.nuget.org/packages/LLOneBot.Net

## 第二步:需要添加的引用
```
using LLOneBot.Net.Data;
using LLOneBot.Net.Data.MessageDataType;
using LLOneBot.Net.Receivers.Message.Group;
using LLOneBot.Net.Receivers.Message.Private;
using LLOneBot.Net.Receivers.Notice;
using LLOneBot.Net.Receivers.Request;
using LLOneBot.Net.Sessions;
```
## 第三部:获取message的代码示例