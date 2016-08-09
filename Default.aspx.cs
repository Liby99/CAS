﻿using System;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
	public string[] OnPage = new string[3];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CAS.User.IsLogin)
            Response.Redirect("Login.aspx");
        var si = new CAS.SqlIntegrate(CAS.Utility.connStr);
        OnPage[0] = "<ul class=\"nav bs-sidenav\">";
        DataTable dt = si.Adapter("SELECT TOP 7 * FROM CAS_Notice ORDER BY [date] DESC");
        for (int i = 0; i < dt.Rows.Count; i++)
            OnPage[0] += "<li>" +
                            "<a href=\"Bulletin.aspx?ID=" + dt.Rows[i]["GUID"] + "\">" + 
                                dt.Rows[i]["title"] + 
                            "</a>" +
                         "</li>";
        OnPage[0] += "</ul>";
        OnPage[1] = si.Query("SELECT TOP 1 GUID FROM CAS_Photo ORDER BY ID DESC").ToString();
        dt = si.Adapter("SELECT * FROM CAS_User WHERE (DATEADD(year, DATEPART(year, GETDATE()) - DATEPART(year, birthday), birthday)) BETWEEN GETDATE() AND DATEADD(day, 30, GETDATE()) ORDER BY DATEPART(month, birthday) ASC");
        for (var i = 0; i < dt.Rows.Count; i++)
            OnPage[2] += "<tr>" +
                            "<th>" + dt.Rows[i]["name"] + "</th>" +
                            "<th>" + dt.Rows[i]["birthday"].ToString().Replace(" 0:00:00", "") + "</th>" +
                         "</tr>";
    }
}