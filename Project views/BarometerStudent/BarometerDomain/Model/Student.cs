﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

public class Student : User
{

    public Student() : base()
    {
        Project = new List<Project>();
    }
    [Required]
	public int Number
	{
		get;
		set;
	}

    public ICollection<Project> Project
	{
		get;
		set;
	}

}

