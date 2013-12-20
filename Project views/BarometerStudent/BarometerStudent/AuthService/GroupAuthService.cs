using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroupAuthService : ICrudAuthService, IGroupAuthService
{
	public virtual bool CanCreate()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanRead()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanUpdate()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanDelete()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanAddStudent()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanRemoveStudent()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanChangeTutor()
	{
		throw new System.NotImplementedException();
	}

}

