using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Models
{
    public enum FocuseOnType { UNSET,URL,TEXT,DATA};
    public enum FocuseToType { UNSET,MAIN,INNER};
    public class Task
    {

        #region 内部使用变量不对用户公开

        /// <summary>
        /// 父任务序列，用于但页面多数据的顺序存储
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 数据保存的目录
        /// </summary>
        public string WorkDirectory { get; set; }
        /// <summary>
        /// 从规则文件读取到的原始解析字串
        /// </summary>
        public string CMD { get; set; }
        /// <summary>
        /// 可配置变量执行顺序
        /// </summary>
        public string ExcuteSequence { get; set; }
        #endregion
        #region 公开变量可被用户配置
        /// <summary>
        /// 任务唯一标记，用于重定向时进行查找
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 指示解析规则 同此字段指向的规则相同
        /// </summary>
        public string BaseOn { get; set; }

        /// <summary>
        /// 任务链接 用于打开指定目标及判断对应执行策略
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 关注对象的类型，决定了数据收集方式
        /// </summary>
        public FocuseOnType DataType { get; set; }
        /// <summary>
        /// 决定产生的子任务是分配到主任务列表还是内部任务列表
        /// </summary>
        public FocuseToType AssignTo { get; set; }
        /// <summary>
        /// 数据保存的名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 执行前为获取指定数据的方法 执行后为需要处理的数据对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 在同页面有多种解析规则时，此变量保存 是否满足解析条件的js代码，通过DoFunc执行 返回结果哦以bool变量存储
        /// </summary>
        public object MatchSymbol { get; set; }
        /// <summary>
        /// 指示满足该页面解析规则的网页如何滚动以完整载入数据
        /// </summary>
        public object ExDo { get; set; }
        /// <summary>
        /// 是否将Data字段的数据按逆序排列
        /// </summary>
        public bool ReverseData { get; set; }
        /// <summary>
        /// 自定义的Data存储的文件名
        /// </summary>
        public string AttachName { get; set; }
        #endregion
        /// <summary>
        /// 由解析对象向任务对象转换时提供数据副本，以保证解析对象不被更改；
        /// </summary>
        /// <returns></returns>
        public Task Clone()
        {
            var t = new Task() {
                Action = this.Action,
                AttachName = this.AttachName,
                DataType = this.DataType,
                AssignTo = this.AssignTo,
                Title = this.Title,
                WorkDirectory = this.WorkDirectory,
                Data = this.Data,
                Index = this.Index,
                ReverseData = this.ReverseData,
                ExDo = this.ExDo,
                MatchSymbol = this.MatchSymbol,
                ExcuteSequence = this.ExcuteSequence,
                BaseOn=this.BaseOn,
                CMD = this.CMD
            };
            return t;
        }
        public void BaseTo(Task target)
        {
            if (target.Action == null) target.Action = this.Action;
            if (string.IsNullOrWhiteSpace(target.AttachName)) target.AttachName = this.AttachName;
            if (target.Data == null) target.Data = this.Data;
            if (target.DataType == FocuseOnType.UNSET) target.DataType = this.DataType;
            if (target.AssignTo == FocuseToType.UNSET) target.AssignTo = this.AssignTo;
            if (target.MatchSymbol == null) target.MatchSymbol = this.MatchSymbol;
            if (target.ExDo == null) target.ExDo = this.ExDo;
            if (String.IsNullOrWhiteSpace(target.Title)) target.Title = this.Title;

            target.ExcuteSequence = this.ExcuteSequence;
        }

        public void ConvertToRunTask(Task InfoTask)
        {
            this.Action = InfoTask.Action;
            this.WorkDirectory = InfoTask.WorkDirectory;
            this.Index= InfoTask.Index ;
        }
    }
}
