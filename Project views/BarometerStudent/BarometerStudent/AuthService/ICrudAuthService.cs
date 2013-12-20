using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

public interface ICrudAuthService 
{
	MembershipUser CurrentUser { get;set; }

	bool CanCreate();

	bool CanRead();

	bool CanUpdate();

	bool CanDelete();

}

