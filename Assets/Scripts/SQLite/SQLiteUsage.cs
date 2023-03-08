using UnityEngine;
using Mono.Data.Sqlite; // 1、引用命名空间

/*
 * 1、新建一张表格
 * create table if not exists 表名(键 类型 修饰, 键 类型 修饰, 键 类型 修饰, ...)
 * 
 * 2、新增一条数据
 * insert into 表名 values (值1, 值2, 值3, ...)
 * insert into 表名(键1, 键2, 键3, ...) values (值1, 值2, 值3, ...)
 * 
 * 3、修改指定数据
 * update 表名 set 键1 = 值1, 键2 = 值2, 键3 = 值3 ... where 条件
 * 
 * 4、删除指定数据
 * delete from 表名 where 条件
 * 
 * 5、查询数据
 * select * from 表名 where 条件
 * select 键, 键, 键, ... from 表名 where 条件
 * 
 * 注: 条件
 * 可以使用 = > <  >=  <= 进行判断
 * 可以使用 + - * / % 进行简单的运算
 * 
 */


/*
 * ExecuteNonQuery: 
 * 1、指定非查询操作, 这个操作一般用于对数据库中的内容进行修改
 * 2、方法的返回值: int, 表示这个操作对几行数据产生了影响
 * 3、如果SQL语句有错误, 这个方法会产生异常
 * 
 * ExecuteScalar:
 * 1、查询满足条件的第一条数据的第一个字段值
 * 
 * ExecuteReader:
 * 1、查询满足条件的所有的数据
 * 
 */

/*
 * 注册、登录:
 * 描述:
 * 1、两个按钮, 登录, 注册
 * 2、注册场景: 用户名, 密码, 确认密码, 邮箱
 * 3、登录: 用户名, 密码
 * 
 * 功能: 注册的用户要保存起来, 下次启动程序, 还可以使用这些用户登录
 * 功能: 如果一个用户登录成功, 将这个用户的信息打印出来
 * 1、用xml打印用户信息
 * 2、用json打印用户信息
 */

public class SQLiteUsage : MonoBehaviour {

    /// <summary>
    /// 建立与数据库的连接, 功能: 连接数据库
    /// </summary>
    private SqliteConnection connection;

    /// <summary>
    /// 用指令来操作数据库, 功能: 执行SQL语句
    /// </summary>
    private SqliteCommand command;

    /// <summary>
    /// 用来读取数据库中的内容, 查询会用
    /// </summary>
    private SqliteDataReader reader;


	void Start () {
        // StreamingAssets:
        string sqlitePath = "Data Source = " + Application.streamingAssetsPath + "/HeroList222.sqlite";

        // 1、连接数据库, 建立一个与数据库的连接
        connection = new SqliteConnection(sqlitePath);

        // 2、打开数据库, 如果连接的数据库不存在, 这里会自动的创建一个数据库
        connection.Open();
        Debug.Log("数据库打开成功!");

        // 3、创建SqliteCommand实例
        command = connection.CreateCommand();

        // 4、建表
        CreateOperation();

        // 5、增数据
        // InsertOperation();

        // 6、改数据
        // UpdateOperation();

        // 7、删数据
        // DeleteOperation();

        // 8、查询数据
        SelectOPeration();

        // END: 关闭数据库！
        connection.Close();
	}

    // 建表操作
    private void CreateOperation() {
        // 设置command操作指令
        command.CommandText = "create table if not exists HeroList(HeroID integer primary key, HeroName text, HeroGender text, HeroAge integer, HeroSchool text)";
        // 执行操作
        try {
            command.ExecuteNonQuery();
        } catch (SqliteException e) {
            Debug.Log("建表出错了! 错误信息: " + e.Message);
        }
    }

    // 增
    private void InsertOperation() {
        // 设置操作指令
        // command.CommandText = "insert into HeroList values (1, '张小凡', '男', 10, '青云门')";

        command.CommandText = "insert into HeroList(HeroName, HeroGender, HeroAge) values('小白', '女', 1000)";
        
        // 执行操作
        try {
            command.ExecuteNonQuery();
        } catch (SqliteException e) {
            Debug.Log("插入数据失败! 错误代码: " + e.Message);
        }
    }
    
    // 删
    private void DeleteOperation() {
        // 设置指令
        command.CommandText = "delete from HeroList where HeroName = '小白'";
        // command.CommandText = "delete from HeroList";
        // 执行指令
        try {
            command.ExecuteNonQuery();
        } catch (SqliteException e) {
            Debug.Log("删除出错！ 错误代码: " + e.Message);
        }
    }
    
    // 改
    private void UpdateOperation() {
        // 设置指令
        command.CommandText = "update HeroList set HeroSchool = '狐族' where HeroName = '小白'";
        // 执行指令
        try {
            command.ExecuteNonQuery();
        } catch(SqliteException e) {
            Debug.Log("更新信息出现问题! 错误代码: " + e.Message);
        }

    }
    
    // 查
    private void SelectOPeration() {

        // 设置查询语句
        command.CommandText = "select * from HeroList";
        // command.CommandText = "select HeroName, HeroGender from HeroList";

        // 执行查询操作
        try {
            // 从数据库中查询满足条件的第一条数据的第一个字段值
            // object result = command.ExecuteScalar();
            // Debug.Log(result);

            // 从数据库中批量查询
            reader = command.ExecuteReader();

            // 拼接查询的结果
            string result = "";

            // 读取一行数据, 返回值是bool类型,
            // 如果是true, 说明后面还有数据
            // 如果是false, 说明后面没有数据了
            // 循环读取
            while (reader.Read()) {
                // 循环遍历每一个字段 FieldCount: 这条数据的字段的个数
                for (int i = 0; i < reader.FieldCount; i++) {
                    // 获取字段的名字
                    string key = reader.GetName(i);
                    // 获取字段的值
                    object value = reader.GetValue(i);

                    result += key + ": " + value + "| ";
                }
                result += "\n";
            }

            Debug.Log(result);

            // 查询结束后, reader需要关闭
            // 如果不关闭, 将无法再重新设置command指令
            reader.Close();
            
        } catch (SqliteException e) {
            Debug.Log("查询出错! 错误代码: " + e.Message);
        }
    }
}
