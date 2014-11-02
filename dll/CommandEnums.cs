using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seika.CooException;

namespace Seika.Transform.Command.Enum
{
    public enum CMD_IDS
    {
        /// <summary>
        /// 无
        /// </summary>
        CMD_NONE                    = 0x00,

        /// <summary>
        /// 登录
        /// </summary>
        CMD_LOGIN                   = 0x01,

        /// <summary>
        /// 退出
        /// </summary>
        CMD_LOGOUT                  = 0x02,

        /// <summary>
        /// 发送文件
        /// </summary>
        CMD_SEND_FILE               = 0x03,

        /// <summary>
        /// 追加风格
        /// </summary>
        CMD_ADD_STYLE               = 0x04,

        /// <summary>
        /// 发送共通资源风格
        /// </summary>
        CMD_SET_COMMON_FILES        = 0x05,

        /// <summary>
        /// 转换
        /// </summary>
        CMD_TRANSFORM_FS            = 0x06,

        /// <summary>
        /// 追加新用户站点
        /// </summary>
        CMD_ADD_USER_SITE           = 0x07,

        /// <summary>
        /// 追加用户语言
        /// </summary>
        CMD_ADD_LANGUAGE            = 0x08,

        /// <summary>
        /// 公开用户语言站点
        /// </summary>
        CMD_SITE_LANGUAGE_OPEN      = 0x09,

        /// <summary>
        /// 关闭用户语言站点
        /// </summary>
        CMD_SITE_LANGUAGE_CLOSE     = 0x0A,

        /// <summary>
        /// 设置用户站点语言
        /// </summary>
        CMD_SET_USER_STYLE          = 0x0B,

        /// <summary>
        /// 上传图片文件
        /// </summary>
        CMD_SEND_PICTURE_FILE       = 0x0C,

        /// <summary>
        /// 上传到自定义文件夹下
        /// </summary>
        CMD_SEND_MYSTYLE_FILE       = 0x0D,

        /// <summary>
        /// 删除用户语言
        /// </summary>
        CMD_DEL_USER_LANGUAGE       = 0x0E,

        /// <summary>
        /// 删除用户文件
        /// </summary>
        CMD_DEL_USER_FILES          = 0x0f,

        /// <summary>
        /// 取得用户网站文件列表
        /// </summary>
        CMD_GET_FILE_LIST           = 0x10,

        /// <summary>
        /// 删除用户网站
        /// </summary>
        CMD_DEL_USER_SITE           = 0x11,

        /// <summary>
        /// 取得文件流
        /// </summary>
        CMD_GET_FILE_STREAM         = 0x12,

        /// <summary>
        /// 目录存在验证
        /// </summary>
        CMD_EXIST_DIRECT            = 0x13,

        /// <summary>
        /// 用户网站备份
        /// </summary>
        CMD_BACKUP_USER_SITE            = 0x14,

        /// <summary>
        /// 用户网站还原
        /// </summary>
        CMD_RESUME_USER_SITE            = 0x15,

        /// <summary>
        /// 删除用户网站备份
        /// </summary>
        CMD_DEL_BACKUP_USER_SITE            = 0x16,

        /// <summary>
        /// 追加用户域名
        /// </summary>
        CMD_ADD_USER_DOMAIN            = 0x17,

         /// <summary>
        /// 删除用户域名
        /// </summary>
        CMD_DEL_USER_DOMAIN            = 0x18
    }

    public enum FILE_TYPE
    {
        HTML        = 0x01, 
        CSS         = 0x02,
        JS          = 0x03,
        PDF         = 0x04, 
        PHP         = 0x05, 
        OTHER       = 0x06
    }

    public enum SITE_TYPE
    {
        NONE        = 0x01, 
        PC          = 0x02, 
        WAP         = 0x03
    }

    public enum COMMAND_POSITION
    {
        USERSITE    = 0x01, 
        LOCAL       = 0x02, 
        MEMORY      = 0x03
    }

    public enum CMD_SD_DIV
    {
        RESET       = 0xFF, 
        START       = 0x01, 
        CMD         = 0x02, 
        PARAM       = 0x03, 
        DATA        = 0x04, 
        STREAM      = 0x05, 
        END         = 0xFE
    }

    public enum CMD_DIV
    {
        START           = 0x01,
        COMMAND_ID      = 0x02,
        PARAM           = 0x03,
        DATA            = 0x04,
        FILE_NAME       = 0x11,
        FILE_STREAM     = 0x12,
        RESULT          = 0x21,
        ERROR_CD        = 0x22,
        ERROR_MESSAGE   = 0x23,
        END             = 0xFE,
        DIV             = 0xFF
    }

    public enum CMD_RESULT
    {
        SUCCESS         = 0x01, 
        ERROR           = 0x02, 
        WARING          = 0x03
    }
}
