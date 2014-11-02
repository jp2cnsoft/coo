using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seika.CooException
{
    /// <summary>
    /// 用户自定义异常
    /// </summary>
    public class AppException : System.Exception
    {
        String _mess = String.Empty;
        Control _ctrl = null;

        public AppException(){}

        public AppException(String mess) 
        {
            this._mess = mess;
        }

        public AppException(String mess, Control ctrl)
        {
            this._mess = mess;
            this._ctrl = ctrl;
        }

        public AppException(String mess, String[] str) 
        {
            this._mess = String.Format(mess, str);
        }

        public String AppMessage
        {
            set { this._mess = value; }
            get { return _mess; }
        }

        public Control AppCtrl
        {
            set { this._ctrl = value; }
            get { return _ctrl; }
        }

    }
    /// <summary>
    /// 系统异常
    /// </summary>
    public class SysException : System.Exception
    {
        public SysException(){}

        public SysException(String mess) : base(mess)
        {
        }

        public SysException(String msg, System.Exception e) : base (msg, e)
        {
        }

        public SysException(String id, String mess)
            : base(mess)
        {   
            this.MessageId = id;
        }
     
        public String MessageId { get; set; }

    }
    /// <summary>
    /// 用户自定义异常信息集
    /// </summary>
    public class ExceptionData 
    {
        List<AppException> _app = new List<AppException>(1000);

        public ExceptionData() { }

        public ExceptionData(AppException app)
        {
            _app.Add(app);
        }

        public void Add(AppException app)
        {
            _app.Add(app);
        }

        public int Count
        {
            get { return _app.Count; }
        }

        public List<AppException> GetAppException
        {
            get { return _app; }
        }
    }
    /// <summary>
    /// 异常信息集合
    /// </summary>
    public class ExceptionMess : System.Exception
    {
        List<ExceptionData> _list = new List<ExceptionData>(1000);
        List<Control> _col = new List<Control>(1000);
        List<ExceptionControls> _cols = new List<ExceptionControls>(1000);
        ExceptionData _ed;

        public ExceptionMess() { }

        public ExceptionMess(ExceptionData ed)
        {
            _ed = ed;
        }

        public ExceptionMess(List<ExceptionData> edlist)
        {
            _list = edlist;
        }

        public ExceptionMess(ExceptionData edlist, Control col)
        {
            _list.Add(edlist);
            _col.Add(col);
        }

        public ExceptionMess(ExceptionData edlist, ExceptionControls cols, Control col)
        {
            _list.Add(edlist);
            _cols.Add(cols);
            _col.Add(col);
        }

        public void Add(ExceptionData ed)
        {
            _list.Add(ed);
        }

        public void Add(ExceptionData ed, Control col)
        {
            _list.Add(ed);
            _col.Add(col);
        }

        public void Add(ExceptionData ed, ExceptionControls cols, Control col)
        {
            _list.Add(ed);
            _cols.Add(cols);
            _col.Add(col);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public List<ExceptionData> GetData
        {
            get { return _list; }
        }

        public List<ExceptionControls> GetControls
        {
            get { return _cols; }
        }

        public List<Control> GetControl
        {
            get { return _col; }
        }
    }

    /// <summary>
    /// 异常控件集合
    /// </summary>
    public class ExceptionControls 
    {
        List<Control> _col = new List<Control>(1000);

        public ExceptionControls()
        {
        }

        public int Count
        {
            get { return _col.Count; }
        }

        public void Add(Control col)
        {
            _col.Add(col);
        }

        public List<Control> Get
        {
            get { return _col; }
        }
    }

}
