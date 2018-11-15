using System;
using System.Collections;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class COptions
    {
        /// <summary>
        /// SaveOnExit: 退出时保存数据。
        /// </summary>
        public bool SaveOnExit = false;

        /// <summary>
        /// StartupPage: 程序启动时显示的页面。
        /// </summary>
        public int StartupPage = 0;

        /// <summary>
        /// ViewSetting: ListView 查看方式。
        /// </summary>
        public int ViewSetting = 0;

        /// <summary>
        /// ShowHidden: 是否显示隐藏文件。
        /// </summary>
        public bool ShowHidden = false;

        /// <summary>
        /// LastPosition: 保存程序退出时的最后位置。
        /// </summary>
        public ArrayList LastPosition = new ArrayList();
    }
}