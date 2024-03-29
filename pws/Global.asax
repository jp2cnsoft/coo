﻿<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        if (Context == null || Context.Session == null) return;
        Seika.COO.Web.PG.SessionManager session = new Seika.COO.Web.PG.SessionManager(Context.Session);
        //错误信息
        session.SystemExceptionId = Context.Error.InnerException;
        //跳转到错误页面
        Response.Redirect(@"~/Pages/P3000/P3000P1010.aspx");
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        //HttpContext.Current.Session.Abandon();
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
