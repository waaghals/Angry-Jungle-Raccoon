﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Student : User
{
	public int Number
	{
		get;
		set;
	}

	public IEnumerable<Project> Project
	{
		get;
		set;
	}

}
