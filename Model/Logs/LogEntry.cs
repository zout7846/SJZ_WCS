namespace Model.Logs
{
    /// 一条日志的内存模型（写入用 DTO，不直接作为数据库实体）。
    /// 调用方通过  创建，再逐个属性补充上下文（一行一个赋值，便于阅读）；
    /// 落库由 Service 层的日志服务批量转换到 SysLog 实体完成，调用线程不会因为写数据库而阻塞。
    public sealed class LogEntry
    {
        // 日志分类：系统 / WMS / RGV / PLC / 用户操作
        public LogCategory Category { get; set; } = LogCategory.System;

        // 风险等级：低 / 中 / 高
        public LogRiskLevel RiskLevel { get; set; } = LogRiskLevel.Low;

        // 功能模块，如 "API"、"PLC/PLC1"、"RGV/RGV01"、"任务调度"
        public string Module { get; set; } = "";

        // 动作名称，如 "Connect"、"ReadDb"、"POST /api/plc/PLC1/db/500"
        public string Action { get; set; } = "";

        // 日志摘要（一句话说明发生了什么）
        public string Content { get; set; } = "";

        // 详细信息（报文、异常堆栈、请求参数等，可为空）
        public string Detail { get; set; } = "";

        // 操作对象（设备名、任务号、文件路径等，可为空）
        public string Target { get; set; } = "";

        // 用户编号（用户操作日志才有值）
        public string UserId { get; set; } = "";

        // 用户名（用户操作日志才有值；当前未接登录，写 "anonymous"）
        public string UserName { get; set; } = "";

        // 客户端 IP
        public string IpAddress { get; set; } = "";

        // 是否成功（true=成功，false=失败）
        public bool Success { get; set; } = true;

        // 耗时（毫秒），没有耗时的日志留 0
        public long ElapsedMs { get; set; }

        // 创建时间（写入方不填，落库时由日志写入服务补当前时间）
        public DateTime? CreatedTime { get; set; }

        /// 创建一条日志（静态工厂，统一默认值，避免调用方忘字段）。
        /// 日志分类
        /// 风险等级
        /// 功能模块
        /// 动作名称
        /// 日志摘要
        public static LogEntry Create(LogCategory category, LogRiskLevel riskLevel,
            string module, string action, string content)
        {
            LogEntry entry = new LogEntry();
            entry.Category = category;
            entry.RiskLevel = riskLevel;
            entry.Module = module;
            entry.Action = action;
            entry.Content = content;
            return entry;
        }
    }
}
