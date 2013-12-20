using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ProjectAuthService : ICrudAuthService, IProjectAuthService
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

	public virtual bool CanAddGroup()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool CanDeleteGroup()
	{
		throw new System.NotImplementedException();
	}

}

