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

public class User : IEntety
{
    public int Id { get; set; }
    [Required]
	public string Login
	{
		get;
		set;
	}

    [Required]
	public string Name
	{
		get;
		set;
	}

    [Required]
	public IEnumerable<RoleType> RoleType
	{
		get;
		set;
	}

	public Student Student
	{
		get;
		set;
	}

}

