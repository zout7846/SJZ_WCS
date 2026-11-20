namespace Model.Logs
{
    /// 日志分类枚举：一条日志只属于一个分类，分类决定日志的归属模块和查询入口。
    /// 扩展方式：新增业务域时在枚举末尾追加值（不要修改已有值，数据库里已按整数存储），
    /// 并在 ILogService 里补一个便捷写入方法即可，不需要改表结构。
    public enum LogCategory
    {
        /// 系统日志：程序启动/停止、后台服务、数据库初始化、与前端交互的接口请求（ApiLogMiddleware）等通用日志。
        System = 1,

        /// WMS 交互日志：与上游 WMS 系统的报文交互。
        /// 当前 WMS 接口协议尚未确定，这里先占位：等接口确定后由 WMS 适配层调用 ILogService 写入，分类值保持不变。
        Wms = 2,

        /// RGV 设备交互日志：与 RGV 小车 PLC（7 台，DeviceType=RGV）的 S7 通讯交互，如连接、读 DB、写 DB。
        Rgv = 3,

        /// PLC 设备交互日志：与输送线/拆垛段 PLC（4 台拆垛 PLC，DeviceType=Destacker）的 S7 通讯交互。
        Plc = 4,

        /// 用户操作日志：用户在界面上执行的新增/修改/删除/下发任务等写操作（由 ApiLogMiddleware 对写请求自动记录
        UserOperation = 5
    }
}
