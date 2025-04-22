using SqlSugar;

namespace AspNetBlog.Common.Db;

public class BaseDbConfig
    {
        /// <summary>
        /// 所有库配置
        /// </summary>
        public static readonly List<ConnectionConfig> AllConfigs = new();

        /// <summary>
        /// 有效的库连接（除去 Log 库）
        /// </summary>
        public static List<ConnectionConfig> ValidConfig = new();

        public static ConnectionConfig MainConfig;
        public static ConnectionConfig LogConfig; //日志库

        public static bool IsMulti => ValidConfig.Count > 1;

        //目前是多库操作，默认加载的是 appsettings.json 设置为 true 的第一个 db 连接。
        // 优化配置连接，直接在单个配置中可以配置从库，新增故障转移方案
        // 增加主库备用连接，配置方式为 ConfigId 为主库的 ConfigId+随便数字 只要不重复就好
        // 主库在无法连接后会自动切换到备用链接
        public static (List<MutiDbOperate> allDbs, List<MutiDbOperate> slaveDbs) MutiConnectionString => MutiInitConn();

        public static (List<MutiDbOperate>, List<MutiDbOperate>) MutiInitConn()
        {
            List<MutiDbOperate> listDatabase = AppSettings.App<MutiDbOperate>("DBS")
                .Where(i => i.Enabled).ToList();
            var mainDbId = AppSettings.App(new string[] { "MainDB" }).ToString();
            var mainDbModel = listDatabase.Single(d => d.ConnId == mainDbId);
            listDatabase.Remove(mainDbModel);
            listDatabase.Insert(0, mainDbModel);

            foreach (var i in listDatabase) SpecialDbString(i);
            return (listDatabase, mainDbModel.Slaves);
        }

        private static string DifDbConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception)
                {
                }
            }

            return conn[conn.Length - 1];
        }

        /// <summary>
        /// 定制 Db 字符串
        /// 目的是保证安全：优先从本地 txt文件获取，若没有文件则从 appsettings.json 中获取
        /// </summary>
        /// <param name="mutiDbOperate"></param>
        /// <returns></returns>
        private static MutiDbOperate SpecialDbString(MutiDbOperate mutiDbOperate)
        {
            if (mutiDbOperate.DbType == DataBaseType.Sqlite)
            {
                mutiDbOperate.Connection =
                    $"DataSource=" + Path.Combine(Environment.CurrentDirectory, mutiDbOperate.Connection);
            }
            else if (mutiDbOperate.DbType == DataBaseType.SqlServer)
            {
                // 这个文件有没有不重要，没有文件则从 appsettings.json 中获取
                mutiDbOperate.Connection = DifDbConnOfSecurity(@"D:\Code\CSharp\DbTestCache\dbCountPsw1_SqlserverConn.txt",
                    mutiDbOperate.Connection);
            }
            else if (mutiDbOperate.DbType == DataBaseType.MySql)
            {
                // 这个文件有没有不重要，没有文件则从 appsettings.json 中获取
                mutiDbOperate.Connection =
                    DifDbConnOfSecurity(@"D:\Code\CSharp\DbTestCache\dbCountPsw1_MySqlConn.txt", mutiDbOperate.Connection);
            }
            else if (mutiDbOperate.DbType == DataBaseType.Oracle)
            {
                // 这个文件有没有不重要，没有文件则从 appsettings.json 中获取
                mutiDbOperate.Connection =
                    DifDbConnOfSecurity(@"D:\Code\CSharp\DbTestCache\dbCountPsw1_OracleConn.txt", mutiDbOperate.Connection);
            }

            return mutiDbOperate;
        }
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSql = 4,
        Dm = 5,
        Kdbndp = 6,
    }

    public class MutiDbOperate
    {
        /// <summary>
        /// 连接启用开关
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 连接 ID
        /// </summary>
        public string ConnId { get; set; }

        /// <summary>
        /// 从库执行级别，越大越先执行
        /// </summary>
        public int HitRate { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DbType { get; set; }

        /// <summary>
        /// 从库
        /// </summary>
        public List<MutiDbOperate> Slaves { get; set; }
    }